namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

internal static class AppExec {

    public static void Base(bool showVersion, bool useVerbose) {
        if (showVersion) {
            var assembly = Assembly.GetEntryAssembly();
            var name = assembly?.GetName().Name ?? string.Empty;
            var version = assembly?.GetName().Version ?? new Version();
            Console.WriteLine($"{name} {version.Major}.{version.Minor} {version.Revision}");
            if (useVerbose) {
            }
            return;
        }
    }


    public static void New(FileInfo file) {
        var doc = new AASeqNodes();
        doc.Save(file.FullName);
    }

    public static void Lint(FileInfo file) {
        try {

            var document = AASeqNodes.Load(file.FullName);
            using var engine = new Engine(document);

            var newDocument = new AASeqNodes();
            foreach (var endpoint in engine.Endpoints) {
                newDocument.Add(endpoint.GetDefinitionNode());
            }
            foreach (var action in engine.FlowSequence) {
                newDocument.Add(action.GetDefinitionNode());
            }
            newDocument.Save(Console.Out, AASeqOutputOptions.Default with { HeaderExecutable = "aaseq", ExtraEmptyRootNodeLines = true });

        } catch (InvalidOperationException ex) {
            Output.ErrorLine("Error parsing the document: " + ex.Message);
        }
    }

    public static void Run(FileInfo file) {
        try {
            var document = AASeqNodes.Load(file.FullName);
            var outputOptions = AASeqOutputOptions.Default with { ExtraEmptyRootNodeLines = true };

            try {

                using var engine = new Engine(document);

                var newDocument = new AASeqNodes([engine.GetOwnDefinition()]);
                foreach (var endpoint in engine.Endpoints) {
                    newDocument.Add(endpoint.GetDefinitionNode());
                }
                foreach (var action in engine.FlowSequence) {
                    newDocument.Add(action.GetDefinitionNode());
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

                engine.ActionException += (sender, e) => {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Exception);
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
