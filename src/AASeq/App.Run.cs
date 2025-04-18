namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

internal static partial class App {

    public static void Run(FileInfo file) {
        Run(file, startPaused: false);
    }

    public static void Run(FileInfo file, bool startPaused = false) {
        try {
            var document = AASeqNodes.Load(file.FullName);
            var outputOptions = AASeqOutputOptions.Default with { ExtraEmptyRootNodeLines = true, NoTypeAnnotation = true };

            try {

                using var engine = new Engine(document);

                var newDocument = new AASeqNodes([engine.OwnDefinitionNode]);
                foreach (var endpoint in engine.Endpoints) {
                    newDocument.Add(endpoint.DefinitionNode);
                }
                foreach (var action in engine.FlowSequence) {
                    newDocument.Add(action.DefinitionNode);
                }
                newDocument.Save(Console.Out, outputOptions);

                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("┌────────────┬─────────────┬──────────────┐");
                Console.WriteLine("│ Enter: run │ Space: step │ Escape: quit │");
                Console.WriteLine("└────────────┴─────────────┴──────────────┘");
                Console.ResetColor();
                Console.WriteLine();

                if (!startPaused) {
                    engine.Start();
                }

                engine.FlowBegin += (sender, e) => {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"# Flow: {e.FlowIndex}");
                    Console.ResetColor();
                };

                engine.ActionBegin += (sender, e) => {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"## Action: {e.FlowIndex}:{e.ActionIndex}");
                    Console.ResetColor();
                };

                engine.ActionDone += (sender, e) => {
                    e.Node.Save(Console.Out, outputOptions);
                };

                engine.ActionError += (sender, e) => {
                    Console.ForegroundColor = ConsoleColor.Red;
                    e.Node.Save(Console.Out, outputOptions);
                    Console.ResetColor();
                };

                while (true) {
                    if (Console.KeyAvailable) {
                        var key = Console.ReadKey(intercept: true);
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
                    } else {
                        Thread.Sleep(10);
                    }
                }

            } catch (InvalidOperationException ex) {
                Output.ErrorLine("Error creating the engine: " + ex.Message);
            }
        } catch (InvalidOperationException ex) {
            Output.ErrorLine("Error parsing the document: " + ex.Message);
        }
    }

}
