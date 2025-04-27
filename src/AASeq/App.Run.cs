namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Threading;

internal static partial class App {

    private static readonly ManualResetEvent DoneEvent = new(initialState: false);

    public static void Run(FileInfo file) {
        Run(file, isInteractive: false);
    }

    public static void Run(FileInfo file, bool isInteractive = false) {
        Console.CancelKeyPress += delegate {
            Output.WriteError("^C", prependEmptyLine: true);
            DoneEvent.Set();
            Environment.Exit(1);
        };

        try {
            var document = AASeqNodes.Load(file.FullName);

            try {
                var pluginManager = new PluginManager(Logger.Instance);
                using var engine = new Engine(Logger.Instance, pluginManager, document);

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
                    Output.WriteNote($"# Flow: {e.FlowIndex}", prependEmptyLine: true);
                };

                engine.ActionBegin += (sender, e) => {
                    Output.WriteNote($"## Action: {e.FlowIndex}:{e.ActionIndex}", prependEmptyLine: true);
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
                            Output.WriteNote($"# Paused", prependEmptyLine: true);
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

            } catch (InvalidOperationException ex) {
                Output.WriteError("Error creating the engine: " + ex.Message);
            }
        } catch (InvalidOperationException ex) {
            Output.WriteError("Error parsing the document: " + ex.Message);
        }
    }

}
