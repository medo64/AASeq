namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

internal static partial class App {

    public static void Lint(FileInfo file) {
        try {

            var document = AASeqNodes.Load(file.FullName);
            using var engine = new Engine(document);

            var newDocument = new AASeqNodes([engine.GetOwnDefinition()]);
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

}
