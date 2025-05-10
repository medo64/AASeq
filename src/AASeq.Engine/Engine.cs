namespace AASeq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Extensions.Logging;

/// <summary>
/// Main execution engine.
/// </summary>
public sealed partial class Engine : IDisposable {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="pluginManager">Plugin manager.</param>
    /// <param name="document">Document.</param>
    public Engine(ILogger logger, PluginManager pluginManager, AASeqNodes document) {
        ArgumentNullException.ThrowIfNull(document);

        LogFile = new LogToFile();
        Variables = new Variables(Environment.GetEnvironmentVariables());

        var repeatCount = 1;
        var commandTimeout = TimeSpan.MaxValue;
        var receiveTimeout = TimeSpan.FromSeconds(3);
        var sendTimeout = TimeSpan.FromSeconds(1);

        // setup endpoints
        var endpoints = new SortedDictionary<string, EndpointStore>(StringComparer.OrdinalIgnoreCase);  // instance per name storage
        foreach (var node in document) {
            if (!node.Name.StartsWith('@')) { continue; }  // messages and commands will be processed later
            var nodeName = node.Name[1..];
            var pluginName = node.GetValue(nodeName);
            if (!EndpointNameRegex().IsMatch(nodeName)) { throw new InvalidOperationException($"Invalid endpoint name '{nodeName}'."); }
            if (!EndpointNameRegex().IsMatch(pluginName)) { throw new InvalidOperationException($"Invalid plugin name '{pluginName}'."); }

            if (pluginName.Equals("Me", StringComparison.OrdinalIgnoreCase)) {

                if (node.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{node.Name}'."); }

                var configuration = Variables.GetExpanded(node.Nodes);

                if (configuration.TryConsumeNode("LogFile", out var logFileNode)) {
                    if (logFileNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{logFileNode.Name}'."); }
                    var logFileValue = logFileNode.Value.AsString("");
                    if (string.IsNullOrEmpty(logFileValue)) { throw new InvalidOperationException("Invalid file name"); }
                    LogFile.SetDestination(logFileValue);
                }

                if (configuration.TryConsumeNode("Repeat", out var repeatNode)) {
                    if (repeatNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{repeatNode.Name}'."); }
                    if (repeatNode.Value.IsPositiveInfinity) {
                        repeatCount = int.MaxValue;
                    } else {
                        var count = repeatNode.Value.AsInt32();
                        if ((count != null) && (count > 0)) {
                            repeatCount = count.Value;
                        } else {
                            logger.LogWarning($"Cannot convert 'Repeat' value.");
                        }
                    }
                }

                if (configuration.TryConsumeNode("Timeout", out var timeoutNode)) {
                    if (timeoutNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{timeoutNode.Name}'."); }
                    var timeout = timeoutNode.Value.AsTimeSpan();
                    if ((timeout != null) && (timeout.Value.TotalSeconds > 0)) {
                        commandTimeout = timeout.Value;
                        receiveTimeout = timeout.Value;
                        sendTimeout = timeout.Value;
                    } else {
                        logger.LogWarning($"Cannot convert 'Timeout' value.");
                    }
                }
                if (configuration.TryConsumeNode("CommandTimeout", out var commandTimeoutNode)) {
                    if (commandTimeoutNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{commandTimeoutNode.Name}'."); }
                    var timeout = commandTimeoutNode.Value.AsTimeSpan();
                    if ((timeout != null) && (timeout.Value.TotalSeconds > 0)) {
                        commandTimeout = timeout.Value;
                    } else {
                        logger.LogWarning($"Cannot convert 'CommandTimeout' value.");
                    }
                }
                if (configuration.TryConsumeNode("ReceiveTimeout", out var receiveTimeoutNode)) {
                    if (receiveTimeoutNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{receiveTimeoutNode.Name}'."); }
                    var timeout = receiveTimeoutNode.Value.AsTimeSpan();
                    if ((timeout != null) && (timeout.Value.TotalSeconds > 0)) {
                        receiveTimeout = timeout.Value;
                    } else {
                        logger.LogWarning($"Cannot convert 'ReceiveTimeout' value.");
                    }
                }
                if (configuration.TryConsumeNode("SendTimeout", out var sendTimeoutNode)) {
                    if (sendTimeoutNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{sendTimeoutNode.Name}'."); }
                    var timeout = sendTimeoutNode.Value.AsTimeSpan();
                    if ((timeout != null) && (timeout.Value.TotalSeconds > 0)) {
                        sendTimeout = timeout.Value;
                    } else {
                        logger.LogWarning($"Cannot convert 'SendTimeout' value.");
                    }
                }

                if (configuration.Count > 0) { logger.LogWarning($"Unrecognized node '{configuration[0].Name}' for own endpoint."); }

            } else {

                var plugin = pluginManager.FindEndpointPlugin(pluginName) ?? throw new InvalidOperationException($"Cannot find plugin named '{pluginName}'.");
                var configuration = node.Nodes;
                endpoints.Add(nodeName, new EndpointStore(
                    nodeName,
                    configuration.Clone(),
                    plugin,
                    plugin.CreateInstance(new LogToParent(logger, nodeName, LogFile), Variables.GetExpanded(configuration)))
                );
            }

        }

        RepeatCount = repeatCount;
        CommandTimeout = commandTimeout;
        ReceiveTimeout = receiveTimeout;
        SendTimeout = sendTimeout;
        Endpoints = [.. endpoints.Values];

        // setup flow sequence
        var flowSequence = new List<IFlowAction>();
        foreach (var node in document) {
            if (node.Name.StartsWith('@')) { continue; }  // we already created instances above
            var actionName = node.Name;
            if (!MessageNameRegex().IsMatch(actionName)) { throw new InvalidOperationException($"Invalid message name '{actionName}'."); }

            var endpointDefinition = node.GetValue(string.Empty).Trim();
            var parsingState = 'L';
            var sbParamLeft = new StringBuilder();
            var sbParamOperator = new StringBuilder();
            var sbParamRight = new StringBuilder();
            foreach (var ch in endpointDefinition) {
                if (parsingState is 'L') {
                    if (ch is '<' or '>') { parsingState = 'O'; } else { sbParamLeft.Append(ch); }
                }
                if (parsingState is 'O') {
                    if (ch is '<' or '>') { sbParamOperator.Append(ch); } else { parsingState = 'R'; }
                }
                if (parsingState is 'R') {
                    if (ch is '<' or '>') { parsingState = 'Z'; } else { sbParamRight.Append(ch); }
                }
            }
            if (parsingState == 'Z') { throw new InvalidOperationException($"Cannot parse endpoint definition '{endpointDefinition}'."); }

            var isCommand = false;
            var isMessageIn = false;
            var isMessageOut = false;
            var skipMatching = false;
            var endpointName = "";

            var left = sbParamLeft.ToString();
            var op = sbParamOperator.ToString();
            var right = sbParamRight.ToString();

            if (!string.IsNullOrEmpty(op)) {
                if (left.Equals("Me", StringComparison.OrdinalIgnoreCase)) { left = ""; }
                if (right.Equals("Me", StringComparison.OrdinalIgnoreCase)) { right = ""; }
                if (string.IsNullOrEmpty(left) && string.IsNullOrEmpty(right)) { throw new InvalidOperationException($"Cannot parse endpoint defitiniton '{endpointDefinition}' (both endpoints local)."); }
                if (!string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right)) { throw new InvalidOperationException($"Cannot parse endpoint defitiniton '{endpointDefinition}' (both endpoints remote)."); }

                switch (op) {
                    case ">":
                    case ">>":
                        if (string.IsNullOrEmpty(left)) {
                            isMessageOut = true;
                            endpointName = right;
                        } else {
                            isMessageIn = true;
                            endpointName = left;
                        }
                        if (op == ">>") { skipMatching = true; }
                        break;

                    case "<":
                    case "<<":
                        if (string.IsNullOrEmpty(left)) {
                            isMessageIn = true;
                            endpointName = right;
                        } else {
                            isMessageOut = true;
                            endpointName = left;
                        }
                        if (op == "<<") { skipMatching = true; }
                        break;

                    default: throw new InvalidOperationException($"Cannot parse endpoint defitiniton '{endpointDefinition}' (unknown operator {op}).");
                }
            } else {
                isCommand = true;
            }

            if (isCommand) {  // command

                var plugin = pluginManager.FindCommandPlugin(actionName) ?? throw new InvalidOperationException($"Cannot find command plugin '{actionName}'.");
                if (node.Value is not null) {
                    node.Nodes.Add(new AASeqNode("Value", node.Value));
                    node.Value = AASeqValue.Null;
                }
                var data = node.Nodes;
                flowSequence.Add(new FlowCommand(
                    new LogToParent(logger, plugin.Name, LogFile),
                    plugin,
                    data
                ));

            } else if (isMessageOut) {

                var data = node.Nodes;
                var flow = new FlowMessageOut(actionName, endpoints[endpointName].Name, endpoints[endpointName].Instance, data, node.GetPropertyValue("/match")) {
                    SkipMatching = skipMatching
                };
                flowSequence.Add(flow);

            } else if (isMessageIn) {

                var data = node.Nodes;
                var flow = new FlowMessageIn(actionName, endpoints[endpointName].Name, endpoints[endpointName].Instance, data, node.GetPropertyValue("/match")) {
                    SkipMatching = skipMatching
                };
                flowSequence.Add(flow);

            } else {
                throw new InvalidOperationException($"Cannot parse endpoint defitiniton '{endpointDefinition}'.");
            }
        }

        // Figure out who connects to whom
        for (var i = 0; i < flowSequence.Count; i++) {
            var action = flowSequence[i];
            if (action is FlowCommand commandAction) {
                // nothing to do for commands
            } else if (action is FlowMessageOut currOutAction) {
                if (currOutAction.SkipMatching) { continue; }  // no response expected
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
                if (currInAction.SkipMatching) { continue; }  // no response expected
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
                if (outAction.SkipMatching) { continue; }
                if ((outAction.RequestForActionIndex is null) && (outAction.ResponseToActionIndex is null)) {
                    throw new InvalidOperationException($"Cannot match request/response for flow {i + 1}:{outAction.MessageName}.");
                }
            } else if (action is FlowMessageIn inAction) {
                if (inAction.SkipMatching) { continue; }
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

    private readonly LogToFile LogFile;
    private readonly Variables Variables;

    [GeneratedRegex(@"^[\p{L}\p{Nd}][\p{L}\p{Nd}_-]*$")]  // allow only letters, digits, underscores, and hyphens; if two part, separate by colon
    private static partial Regex EndpointNameRegex();

    [GeneratedRegex(@"^[\p{L}\p{Nd}][\p{L}\p{Nd}_-]*(:[\p{L}\p{Nd}][\p{L}\p{Nd}_-]*)?$")]  // allow only letters, digits, underscores, and hyphens; if two part, separate by colon
    private static partial Regex MessageNameRegex();

    #endregion Helpers

}
