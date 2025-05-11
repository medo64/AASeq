namespace AASeqCli;
using AASeq;
using System;
using System.Collections.Generic;
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

            var newDocument = LintDocument(engine);
            newDocument.Save(Console.Out, AASeqOutputOptions.Default with { HeaderExecutable = "aaseq", ExtraEmptyRootNodeLines = true });

        } catch (InvalidOperationException ex) {
            Output.WriteError("Error parsing the document: " + ex.Message);
        }
    }


    private static AASeqNodes LintDocument(Engine engine) {
        var newDocument = new AASeqNodes();

        var variableNode = new AASeqNode("$");
        var varKeys = new List<string>(engine.Variables.Keys);
        varKeys.Sort((a, b) => string.Compare(a, b, StringComparison.OrdinalIgnoreCase));
        foreach (var key in varKeys) {
            variableNode.Nodes.Add(new AASeqNode(key, engine.Variables[key]));
        }
        newDocument.Add(variableNode);

        newDocument.Add(engine.OwnDefinitionNode);
        foreach (var endpoint in engine.Endpoints) {
            newDocument.Add(endpoint.DefinitionNode);
        }
        foreach (var action in engine.FlowSequence) {
            newDocument.Add(action.DefinitionNode);
        }

        return newDocument;
    }

}
