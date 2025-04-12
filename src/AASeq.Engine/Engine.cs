namespace AASeq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

/// <summary>
/// Main execution engine.
/// </summary>
public sealed partial class Engine : IDisposable {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="document">Document.</param>
    public Engine(AASeqNodes document) {
        ArgumentNullException.ThrowIfNull(document);

        var commandTimeout = TimeSpan.FromSeconds(3);
        var receiveTimeout = TimeSpan.FromSeconds(3);
        var sendTimeout = TimeSpan.FromSeconds(1);

        // setup endpoints
        var endpoints = new SortedDictionary<string, EndpointStore>(StringComparer.OrdinalIgnoreCase);  // instance per name storage
        foreach (var node in document) {
            if (node.Name.StartsWith('@')) {
                var nodeName = node.Name[1..];
                var pluginName = node.GetValue(nodeName);
                if (!NameRegex().IsMatch(nodeName)) { throw new InvalidOperationException($"Invalid endpoint name '{nodeName}'."); }
                if (!NameRegex().IsMatch(pluginName)) { throw new InvalidOperationException($"Invalid plugin name '{pluginName}'."); }

                if (pluginName.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                    commandTimeout = node.Nodes.GetValue("CommandTimeout", node.Nodes.GetValue("Timeout", commandTimeout));
                    receiveTimeout = node.Nodes.GetValue("ReceiveTimeout", node.Nodes.GetValue("Timeout", receiveTimeout));
                    sendTimeout = node.Nodes.GetValue("SendTimeout", node.Nodes.GetValue("Timeout", sendTimeout));
                } else {
                    var plugin = PluginManager.FindEndpointPlugin(pluginName) ?? throw new InvalidOperationException($"Cannot find plugin named '{pluginName}'.");
                    endpoints.Add(nodeName, new EndpointStore(nodeName, plugin.GetInstance(node.Nodes)));
                }
            }
        }
        CommandTimeout = commandTimeout;
        ReceiveTimeout = receiveTimeout;
        SendTimeout = sendTimeout;
        Endpoints = [.. endpoints.Values];

        // setup flow sequence
        var flowSequence = new List<IFlowAction>();
        foreach (var node in document) {
            if (!node.Name.StartsWith('@')) {  // we already created instances above
                var actionName = node.Name;
                if (!NameRegex().IsMatch(actionName)) { throw new InvalidOperationException($"Invalid message name '{actionName}'."); }

                var endpointDefinition = node.GetValue(string.Empty);
                if (endpointDefinition.Contains('>', StringComparison.Ordinal)) {  // outgoing message

                    var endpointDefinitions = endpointDefinition.Split('>', StringSplitOptions.None);
                    if (endpointDefinitions.Length != 2) { throw new InvalidOperationException($"Cannot determine endpoints for '{endpointDefinition}'."); }
                    if (string.IsNullOrEmpty(endpointDefinitions[0])) { endpointDefinitions[0] = "Me"; }
                    if (string.IsNullOrEmpty(endpointDefinitions[1])) { endpointDefinitions[1] = "Me"; }
                    foreach (var endpointName in endpointDefinitions) {
                        if (endpointName.Contains("Me", StringComparison.OrdinalIgnoreCase)) { continue; }
                        if (!endpoints.ContainsKey(endpointName)) { throw new InvalidOperationException($"Cannot find endpoint '{endpointName}' in '{endpointDefinition}'."); }
                    }

                    var left = endpointDefinitions[0];
                    var right = endpointDefinitions[1];
                    if (left.Equals(right, StringComparison.OrdinalIgnoreCase)) {
                        throw new InvalidOperationException($"Cannot send message to self '{left}' in '{endpointDefinition}'.");
                    } else if (left.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                        var flow = new FlowMessageOut(actionName, endpoints[right].Name, endpoints[right].Instance, node.Nodes, node.GetPropertyValue("match"));
                        flowSequence.Add(flow);
                    } else if (right.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                        var flow = new FlowMessageOut(actionName, endpoints[left].Name, endpoints[left].Instance, node.Nodes, node.GetPropertyValue("match"));
                        flowSequence.Add(flow);
                    } else {
                        throw new InvalidOperationException($"Cannot send message to nobody.");
                    }

                } else if (endpointDefinition.Contains('<', StringComparison.Ordinal)) {  // incoming message

                    var endpointDefinitions = endpointDefinition.Split('<', StringSplitOptions.None);
                    if (endpointDefinitions.Length != 2) { throw new InvalidOperationException($"Cannot determine endpoints for '{endpointDefinition}'."); }
                    if (string.IsNullOrEmpty(endpointDefinitions[0])) { endpointDefinitions[0] = "Me"; }
                    if (string.IsNullOrEmpty(endpointDefinitions[1])) { endpointDefinitions[1] = "Me"; }
                    foreach (var endpointName in endpointDefinitions) {
                        if (endpointName.Contains("Me", StringComparison.OrdinalIgnoreCase)) { continue; }
                        if (!endpoints.ContainsKey(endpointName)) { throw new InvalidOperationException($"Cannot find endpoint '{endpointName}' in '{endpointDefinition}'."); }
                    }

                    var left = endpointDefinitions[0];
                    var right = endpointDefinitions[1];
                    if (left.Equals(right, StringComparison.OrdinalIgnoreCase)) {
                        throw new InvalidOperationException($"Cannot send message to self '{left}' in '{endpointDefinition}'.");
                    } else if (left.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                        var flow = new FlowMessageIn(actionName, endpoints[right].Name, endpoints[right].Instance, node.Nodes, node.GetPropertyValue("match"));
                        flowSequence.Add(flow);
                    } else if (right.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                        var flow = new FlowMessageIn(actionName, endpoints[left].Name, endpoints[left].Instance, node.Nodes, node.GetPropertyValue("match"));
                        flowSequence.Add(flow);
                    } else {
                        throw new InvalidOperationException($"Cannot receive message.");
                    }

                } else {  // command

                    var plugin = PluginManager.FindCommandPlugin(actionName) ?? throw new InvalidOperationException($"Cannot find command plugin '{actionName}'.");
                    if (node.Value is not null) {
                        node.Nodes.Add(new AASeqNode("Value", node.Value));
                        node.Value = AASeqValue.Null;
                    }
                    flowSequence.Add(new FlowCommand(plugin.Name, plugin.GetInstance(), node.Nodes));

                }
            }
        }

        // Figure out who connects to whom
        for (var i = 0; i < flowSequence.Count; i++) {
            var action = flowSequence[i];
            if (action is FlowCommand commandAction) {
                // nothing to do for commands
            } else if (action is FlowMessageOut currOutAction) {
                if (currOutAction.ResponseToActionIndex is null) {  // this is a first out of this kind
                    for (var j = i + 1; j < flowSequence.Count; j++) {
                        var nextAction = flowSequence[j];
                        if (nextAction is FlowMessageIn nextInAction) {
                            if (!nextInAction.SourceInstance.Equals(currOutAction.DestinationInstance)) { continue; }
                            if (nextInAction.ResponseToActionIndex is null) {
                                if (string.Equals(currOutAction.MatchId, nextInAction.MatchId, StringComparison.Ordinal)) {
                                    currOutAction.RequestForActionIndex = j;
                                    nextInAction.ResponseToActionIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            } else if (action is FlowMessageIn currInAction) {
                if (currInAction.ResponseToActionIndex is null) {  // this is a first out of this kind
                    for (var j = i + 1; j < flowSequence.Count; j++) {
                        var nextAction = flowSequence[j];
                        if (nextAction is FlowMessageOut nextOutAction) {
                            if (nextOutAction.ResponseToActionIndex == null) {
                                if (!nextOutAction.DestinationInstance.Equals(currInAction.SourceInstance)) { continue; }
                                if (string.Equals(currInAction.MatchId, nextOutAction.MatchId, StringComparison.Ordinal)) {
                                    currInAction.RequestForActionIndex = i;
                                    nextOutAction.ResponseToActionIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            } else {
                throw new InvalidOperationException($"Unknown action type '{action.GetType().Name}'.");
            }
        }

        // check if all is matched
        for (var i = 0; i < flowSequence.Count; i++) {
            var action = flowSequence[i];
            if (action is FlowMessageOut outAction) {
                if ((outAction.RequestForActionIndex is null) && (outAction.ResponseToActionIndex is null)) {
                    throw new InvalidOperationException($"Cannot match request/response for flow {i + 1}:{outAction.MessageName}.");
                }
            } else if (action is FlowMessageIn inAction) {
                if ((inAction.RequestForActionIndex is null) && (inAction.ResponseToActionIndex is null)) {
                    throw new InvalidOperationException($"Cannot match request/response for flow {i + 1}:{inAction.MessageName}.");
                }
            }
        }

        // Done
        FlowSequence = flowSequence.AsReadOnly();
        Thread = new Thread(Run) {
            Name = "Engine",
            IsBackground = true,
            CurrentCulture = CultureInfo.InvariantCulture
        };
        if (FlowSequence.Count > 0) {
            Thread.Start();
        }
    }


    #region IDisposable

    /// <summary>
    /// Disposes the engine.
    /// </summary>
    public void Dispose() {
        Stop();
        CancelEvent.Dispose();
        StepEvent.Dispose();
        CanStopEvent.Dispose();
    }

    #endregion IDisposable


    #region Helpers

    [GeneratedRegex("^\\p{L}[\\p{L}\\p{Nd}_-]*$")]  // allow only letters, digits, underscores, and hyphens
    private static partial Regex NameRegex();

    #endregion Helpers

}
