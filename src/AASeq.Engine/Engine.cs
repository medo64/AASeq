namespace AASeq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // setup endpoints
        var endpoints = new SortedDictionary<string, EndpointStore>(StringComparer.OrdinalIgnoreCase);  // instance per name storage
        foreach (var node in document) {
            if (node.Name.StartsWith('@')) {
                var nodeName = node.Name[1..];
                var pluginName = node.GetValue(nodeName);
                if (!NameRegex().IsMatch(nodeName)) { throw new InvalidOperationException($"Invalid endpoint name '{nodeName}'."); }
                if (!NameRegex().IsMatch(pluginName)) { throw new InvalidOperationException($"Invalid plugin name '{pluginName}'."); }

                if (pluginName.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                    // TODO: not a real plugin, but we can use it's data for settings
                } else {
                    var plugin = PluginManager.FindEndpointPlugin(pluginName) ?? throw new InvalidOperationException($"Cannot find plugin named '{pluginName}'.");
                    endpoints.Add(nodeName, new EndpointStore(nodeName, plugin.GetInstance(node.Nodes)));
                }
            }
        }
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


    /// <summary>
    /// Gets all endpoints.
    /// </summary>
    public IReadOnlyList<IEndpoint> Endpoints { get; }

    /// <summary>
    /// Gets all flows.
    /// </summary>
    public IReadOnlyList<IFlowAction> FlowSequence { get; }


    #region Execution

    /// <summary>
    /// Gets number of executing flow.
    /// Zero if execution hasn't started.
    /// </summary>
    public Int32 FlowIndex { get { return Interlocked.CompareExchange(ref CurrentFlowIndex, 0, 0); } }

    /// <summary>
    /// Gets number of execuring step.
    /// Zero if execution hasn't started.
    /// </summary>
    public Int32 StepIndex { get { return Interlocked.CompareExchange(ref CurrentStepIndex, 0, 0); } }

    /// <summary>
    /// Gets whether the engine is running.
    /// </summary>
    public bool IsRunning { get { return Interlocked.CompareExchange(ref CurrentIsRunning, 0, 0) == 1; } }

    /// <summary>
    /// Starts running the engine.
    /// </summary>
    public void Start() {
        if (FlowSequence.Count > 0) {
            StepEvent.Reset(0);
            CanStopEvent.WaitOne();
            StepEvent.Reset(int.MaxValue);
        }
    }

    /// <summary>
    /// Performs a single step.
    /// </summary>
    public void Step() {
        if (FlowSequence.Count > 0) {
            StepEvent.Reset(1);
        }
    }

    /// <summary>
    /// Stops the engine.
    /// </summary>
    public void Stop() {
        if (FlowSequence.Count > 0) {
            StepEvent.Reset(0);
            CanStopEvent.WaitOne();
        }
    }

    #endregion


    #region Thread

    private readonly Thread Thread;
    private readonly ManualResetEvent CancelEvent = new(initialState: false);
    private readonly CountdownEvent StepEvent = new(initialCount: 0);
    private readonly ManualResetEvent CanStopEvent = new(initialState: true);
    private int CurrentIsRunning;  // interlocked
    private int CurrentFlowIndex;  // interlocked
    private int CurrentStepIndex;  // interlocked

    private void Run() {
        if (FlowSequence.Count == 0) { return; }  // cannot really do anything if nothing in flow

        try {
            var currIsRunning = false;
            var executingFlows = new AASeqNode?[FlowSequence.Count];
            var executingGuids = new Guid?[FlowSequence.Count];

            while (!CancelEvent.WaitOne(0, false)) {
                var doStep = !StepEvent.IsSet;
                if (doStep) {
                    try {
                        CanStopEvent.Reset();  // prevent any stop action
                        if (!currIsRunning) {
                            currIsRunning = true;
                            Interlocked.Exchange(ref CurrentIsRunning, 1);
                        }

                        StepEvent.Signal();  // wait to be allowed to step

                        Interlocked.CompareExchange(ref CurrentStepIndex, 0, FlowSequence.Count);
                        var stepIndex = Interlocked.Increment(ref CurrentStepIndex);
                        if (StepIndex == 1) {
                            Interlocked.Increment(ref CurrentFlowIndex);
                            Array.Clear(executingFlows);
                            Array.Clear(executingGuids);
                        } else {
                            Interlocked.CompareExchange(ref CurrentFlowIndex, 0, 0);
                        }
                        var i = stepIndex - 1;

                        var action = FlowSequence[stepIndex - 1];
                        if (action is FlowCommand commandAction) {

                            Debug.WriteLine($"[AASeq.Engine] {stepIndex}: Executing {commandAction.CommandName}");

                            var actionNode = new AASeqNode(commandAction.CommandName, AASeqValue.Null, commandAction.TemplateData.Clone());
                            executingFlows[i] = actionNode;
                            commandAction.Instance.TryExecute(actionNode.Nodes);  // TODO: process data instead of clone

                        } else if (action is FlowMessageOut messageOutAction) {

                            Debug.WriteLine($"[AASeq.Engine] {stepIndex}: Sending {messageOutAction.MessageName}");

                            var actionNode = new AASeqNode(messageOutAction.MessageName, ">" + messageOutAction.DestinationName, messageOutAction.TemplateData.Clone());
                            var id = (messageOutAction.ResponseToActionIndex != null)
                                   ? executingGuids[messageOutAction.ResponseToActionIndex.Value]!.Value
                                   : Guid.NewGuid();
                            executingFlows[i] = actionNode;
                            executingGuids[i] = id;
                            messageOutAction.DestinationInstance.TrySend(id, messageOutAction.MessageName, actionNode.Nodes);  // TODO: process data instead of clone

                        } else if (action is FlowMessageIn messageInAction) {

                            Debug.WriteLine($"[AASeq.Engine] {stepIndex}: Receiving {messageInAction.MessageName}");

                            var actionNode = new AASeqNode(messageInAction.MessageName, "<" + messageInAction.SourceName, messageInAction.TemplateData.Clone());  // TODO: process data instead of clone
                            var id = (messageInAction.ResponseToActionIndex != null)
                                   ? executingGuids[messageInAction.ResponseToActionIndex.Value]!.Value
                                   : Guid.NewGuid();
                            executingFlows[i] = actionNode;
                            executingGuids[i] = id;
                            messageInAction.SourceInstance.TryReceive(Guid.NewGuid(), out var _, out var _);

                        } else {
                            throw new ArgumentException($"Unknown action type '{action.GetType().Name}'.");
                        }
                    } finally {
                        CanStopEvent.Set();  // signal it's a good place to stop
                    }
                } else if (currIsRunning) {  // stop running
                    currIsRunning = false;
                    Interlocked.Exchange(ref CurrentIsRunning, 0);
                } else {  // wait for the next step
                    Thread.Sleep(100);
                }
            }
        } catch (ThreadAbortException) { }
    }

    #endregion


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
