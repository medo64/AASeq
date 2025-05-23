namespace AASeqCli;
using System;
using System.IO;
using AASeq;

internal static partial class App {

    public static void Mermaid(FileInfo file, bool debug, bool verbose) {
        var logger = Logger.GetInstance(debug, verbose);

        try {
            var document = AASeqNodes.Load(file.FullName);

            var pluginManager = new PluginManager(logger);
            using var engine = new Engine(logger, pluginManager, document);

            if (engine.FlowSequence.Count == 0) {
                Output.WriteError("No flows found in the document.");
                Environment.Exit(1);
            }

            Output.WriteLine("sequenceDiagram");
            foreach (var action in engine.FlowSequence) {
                if (action is IFlowCommandAction command) {
                    Output.WriteLine($"Note over Me: {command.CommandName.Replace('_', ' ')}");
                } else if (action is IFlowMessageOutAction messageOut) {
                    Output.WriteLine($"Me ->> {messageOut.DestinationName.Replace('_', ' ')}: {messageOut.MessageName}");
                } else if (action is IFlowMessageInAction messageIn) {
                    Output.WriteLine($"{messageIn.SourceName.Replace('_', ' ')} ->> Me: {messageIn.MessageName}");
                }
            }

        } catch (InvalidOperationException ex) {
            Output.WriteError("Error parsing the document: " + ex.Message);
        }
    }

}
