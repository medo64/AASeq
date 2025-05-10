namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using System.Reflection;

internal static partial class App {

    private static readonly ManualResetEvent DoneEvent = new(initialState: false);

    public static void Run(FileInfo file, bool debug, bool verbose) {
        Run(file, debug, verbose, isInteractive: false);
    }

    public static void Run(FileInfo file, bool debug, bool verbose, bool isInteractive = false) {
        Console.CancelKeyPress += delegate {
            Output.WriteError("^C", prependEmptyLine: true);
            DoneEvent.Set();
            Environment.Exit(1);
        };

        var logger = Logger.GetInstance(debug, verbose);

        try {
            var document = AASeqNodes.Load(file.FullName);

            try {
                var pluginManager = new PluginManager(logger);
                using var engine = new Engine(logger, pluginManager, document);

                var newDocument = new AASeqNodes([engine.OwnDefinitionNode]);
                foreach (var endpoint in engine.Endpoints) {
                    newDocument.Add(endpoint.DefinitionNode);
                }
                foreach (var action in engine.FlowSequence) {
                    newDocument.Add(action.DefinitionNode);
                }
                Output.WriteDocument(newDocument);

                if (isInteractive) {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("┌────────────┬─────────────┬──────────────┐");
                    Console.WriteLine("│ Enter: run │ Space: step │ Escape: quit │");
                    Console.WriteLine("└────────────┴─────────────┴──────────────┘");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                engine.FlowBegin += (sender, e) => {
                    Output.WriteNote($"Flow: {e.FlowIndex}", level: 1, prependEmptyLine: true);
                };

                engine.ActionBegin += (sender, e) => {
                    Output.WriteNote($"Action: {e.FlowIndex}:{e.ActionIndex}", level: 2, prependEmptyLine: true);
                };

                engine.ActionDone += (sender, e) => {
                    Output.WriteActionOk(e.Action, e.Node);
                };

                engine.ActionError += (sender, e) => {
                    Output.WriteActionNok(e.Action, e.Node);
                };

                engine.FlowDone += (sender, e) => {
                    if (e.FlowIndex == engine.RepeatCount) {
                        engine.Pause();  // just pause
                        if (isInteractive) {
                            Output.WriteNote($"Paused", level: 1, prependEmptyLine: true);
                        } else {
                            DoneEvent.Set();
                        }
                    }
                };

                if (isInteractive) {
                    while (true) {
                        if (Console.KeyAvailable) {
                            var key = Console.ReadKey(intercept: true);
#pragma warning disable IDE0010
                            switch (key.Key) {
                                case ConsoleKey.Escape:  // done
                                    engine.Stop();
                                    return;

                                case ConsoleKey.Spacebar:
                                    engine.Step();
                                    break;

                                case ConsoleKey.Enter:
                                    if (engine.IsRunning) {
                                        engine.Step();
                                    } else {
                                        engine.Start();
                                    }
                                    break;

                                default: break;
                            }
#pragma warning restore IDE0010
                        } else {
                            Thread.Sleep(10);
                        }
                    }
                } else {
                    engine.Start();
                    DoneEvent.WaitOne();
                }
            } catch (Exception ex) {
                if ((ex is TargetInvocationException) && (ex.InnerException is not null)) {
                    Output.WriteErrorLine("Error creating the engine: " + ex.InnerException.Message);
                } else {
                    Output.WriteErrorLine("Error creating the engine: " + ex.Message);
                }
            }
        } catch (InvalidOperationException ex) {
            Output.WriteErrorLine("Error parsing the document: " + ex.Message);
        }
    }

}
