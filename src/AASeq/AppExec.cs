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
        var doc = new AASeqDocument();
        doc.Save(file.FullName);
    }

    public static void Lint(FileInfo file) {
        try {
            var document = AASeqDocument.Load(file.FullName);
            document.Save(Console.Out, AASeqDocumentOutputOptions.Default with { HeaderExecutable = "aaseq", ExtraEmptyRootNodeLines = true });
        } catch (InvalidOperationException ex) {
            Output.ErrorLine("Error parsing the document: " + ex.Message);
        }
    }

    public static void Run(FileInfo file) {
        try {
            var document = AASeqDocument.Load(file.FullName);

            try {
                using var engine = new Engine(document);

                var newDocument = new AASeqDocument();
                foreach (var endpoint in engine.Endpoints) {
                    newDocument.Nodes.Add(endpoint.GetDefinitionNode());
                }
                foreach (var action in engine.FlowSequence) {
                    newDocument.Nodes.Add(action.GetDefinitionNode());
                }
                newDocument.Save(Console.Out, AASeqDocumentOutputOptions.Default with { ExtraEmptyRootNodeLines = true });
                Console.WriteLine();

                engine.Start();

                var prevStepCount = 0;
                while (true) {
                    var currStepCount = engine.StepCount;
                    if (currStepCount != prevStepCount) {
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                        Console.Write(currStepCount);
                        prevStepCount = currStepCount;
                    }

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
