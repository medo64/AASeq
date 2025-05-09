namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

internal static partial class App {

    public static void Lint(FileInfo file, bool debug, bool verbose) {
        var logger = Logger.GetInstance(debug, verbose);

        try {

            var document = AASeqNodes.Load(file.FullName);

            var pluginManager = new PluginManager(logger);
            using var engine = new Engine(logger, pluginManager, document);

            var newDocument = new AASeqNodes([engine.OwnDefinitionNode]);
            foreach (var endpoint in engine.Endpoints) {
                newDocument.Add(endpoint.DefinitionNode);
            }
            foreach (var action in engine.FlowSequence) {
                newDocument.Add(action.DefinitionNode);
            }
            newDocument.Save(Console.Out, AASeqOutputOptions.Default with { HeaderExecutable = "aaseq", ExtraEmptyRootNodeLines = true });

        } catch (InvalidOperationException ex) {
            Output.WriteError("Error parsing the document: " + ex.Message);
        }
    }

}
