namespace AASeq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Main execution engine.
/// </summary>
public sealed partial class Engine : IDisposable {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="document">Document.</param>
    public Engine(AASeqDocument document) {
        ArgumentNullException.ThrowIfNull(document);

        var endpoints = new SortedDictionary<string, EndpointInstance>(StringComparer.OrdinalIgnoreCase);
        foreach (var node in document.Nodes) {
            if (node.Name.StartsWith('@')) {
                var nodeName = node.Name[1..];
                var pluginName = node.AsString(nodeName);
                if (!NameRegex().IsMatch(nodeName)) { throw new InvalidOperationException($"Invalid endpoint name '{nodeName}'."); }
                if (!NameRegex().IsMatch(pluginName)) { throw new InvalidOperationException($"Invalid plugin name '{pluginName}'."); }

                var plugin = PluginManager.FindEndpointPlugin(pluginName) ?? throw new InvalidOperationException($"Cannot find plugin named '{pluginName}'.");
                endpoints.Add(nodeName, plugin.GetInstance(node.Nodes));
            }
        }
        if (!endpoints.ContainsKey("Me")) {
            //TODO endpoints.Add("Me", new EndpointInstance(new EndpointPlugins.Me()));
        }
        Endpoints = [.. endpoints.Values];

        var flows = new List<IFlowAction>();
        foreach (var node in document.Nodes) {
            if (!node.Name.StartsWith('@')) {
                var actionName = node.Name;
                if (!NameRegex().IsMatch(actionName)) { throw new InvalidOperationException($"Invalid message name '{actionName}'."); }

                var endpointDefinition = node.AsString(string.Empty);
                if (endpointDefinition.Contains('>', StringComparison.Ordinal)) {  // outgoing message

                    var endpointDefinitions = endpointDefinition.Split('>', StringSplitOptions.None);
                    if (endpointDefinitions.Length != 2) { throw new InvalidOperationException($"Cannot determine endpoints for '{endpointDefinition}'."); }
                    if (string.IsNullOrEmpty(endpointDefinitions[0])) { endpointDefinitions[0] = "Me"; }
                    if (string.IsNullOrEmpty(endpointDefinitions[1])) { endpointDefinitions[1] = "Me"; }
                    foreach (var endpointName in endpointDefinitions) {
                        if (!endpoints.ContainsKey(endpointName)) { throw new InvalidOperationException($"Cannot find endpoint '{endpointName}' in '{endpointDefinition}'."); }
                    }
                    // TODO: check if action exists for endpoint
                    var flow = new FlowMessage(actionName, endpoints[endpointDefinitions[0]], endpoints[endpointDefinitions[1]], node.Nodes);
                    flows.Add(flow);

                } else if (endpointDefinition.Contains('<', StringComparison.Ordinal)) {  // incoming message

                    var endpointDefinitions = endpointDefinition.Split('<', StringSplitOptions.None);
                    if (endpointDefinitions.Length != 2) { throw new InvalidOperationException($"Cannot determine endpoints for '{endpointDefinition}'."); }
                    if (string.IsNullOrEmpty(endpointDefinitions[0])) { endpointDefinitions[0] = "Me"; }
                    if (string.IsNullOrEmpty(endpointDefinitions[1])) { endpointDefinitions[1] = "Me"; }
                    foreach (var endpointName in endpointDefinitions) {
                        if (!endpoints.ContainsKey(endpointName)) { throw new InvalidOperationException($"Cannot find endpoint '{endpointName}' in '{endpointDefinition}'."); }
                    }
                    // TODO: check if action exists for endpoint
                    var flow = new FlowMessage(actionName, endpoints[endpointDefinitions[1]], endpoints[endpointDefinitions[0]], node.Nodes);
                    flows.Add(flow);

                } else {  // command

                    var plugin = PluginManager.FindCommandPlugin(actionName) ?? throw new InvalidOperationException($"Cannot find command plugin '{actionName}'.");
                    flows.Add(new FlowCommand(plugin.GetInstance(node.Nodes), node.Nodes));

                }
            }
        }

        Flows = flows.AsReadOnly();
    }


    /// <summary>
    /// Gets all endpoints.
    /// </summary>
    public IReadOnlyCollection<EndpointInstance> Endpoints { get; }

    /// <summary>
    /// Gets all flows.
    /// </summary>
    public IReadOnlyCollection<IFlowAction> Flows { get; }


    #region IDisposable

    /// <summary>
    /// Disposes the engine.
    /// </summary>
    public void Dispose() {
    }

    #endregion IDisposable


    #region Helpers

    [GeneratedRegex("^\\p{L}[\\p{L}\\p{Nd}_-]*$")]  // allow only letters, digits, underscores, and hyphens
    private static partial Regex NameRegex();

    #endregion Helpers

}
