namespace AASeq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
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
    public Engine(AASeqDocument document) {
        ArgumentNullException.ThrowIfNull(document);

        // setup endpoints
        var endpoints = new SortedDictionary<string, EndpointInstance>(StringComparer.OrdinalIgnoreCase);
        foreach (var node in document.Nodes) {
            if (node.Name.StartsWith('@')) {
                var nodeName = node.Name[1..];
                var pluginName = node.GetValue(nodeName);
                if (!NameRegex().IsMatch(nodeName)) { throw new InvalidOperationException($"Invalid endpoint name '{nodeName}'."); }
                if (!NameRegex().IsMatch(pluginName)) { throw new InvalidOperationException($"Invalid plugin name '{pluginName}'."); }

                if (pluginName.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                    // TODO: not a real plugin, but we can use it's data for settings
                } else {
                    var plugin = PluginManager.FindEndpointPlugin(pluginName) ?? throw new InvalidOperationException($"Cannot find plugin named '{pluginName}'.");
                    endpoints.Add(nodeName, plugin.GetInstance(node.Nodes));
                }
            }
        }
        Endpoints = [.. endpoints.Values];

        // setup flow sequence
        var flowSequence = new List<IFlowAction>();
        foreach (var node in document.Nodes) {
            if (!node.Name.StartsWith('@')) {
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
                        var flow = new FlowMessageOut(actionName, endpoints[right], node.Nodes);
                        flowSequence.Add(flow);
                    } else if (right.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                        var flow = new FlowMessageOut(actionName, endpoints[left], node.Nodes);
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
                        var flow = new FlowMessageIn(actionName, endpoints[right], node.Nodes);
                        flowSequence.Add(flow);
                    } else if (right.Equals("Me", StringComparison.OrdinalIgnoreCase)) {
                        var flow = new FlowMessageIn(actionName, endpoints[left], node.Nodes);
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
                    flowSequence.Add(new FlowCommand(plugin.GetInstance(), node.Nodes));

                }
            }
        }

        // Figure out who connects to whom

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
    internal IReadOnlyList<EndpointInstance> Endpoints { get; }

    /// <summary>
    /// Gets all flows.
    /// </summary>
    public IReadOnlyList<IFlowAction> FlowSequence { get; }


    #region Execution

    /// <summary>
    /// Gets number of executed steps so far.
    /// </summary>
    public Int32 StepCount { get { return Interlocked.CompareExchange(ref CurrentStepCount, 0, 0); } }

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
    private int CurrentStepCount;  // interlocked

    private void Run() {
        if (FlowSequence.Count == 0) { return; }  // cannot really do anything if nothing in flow

        try {
            var currIsRunning = false;
            var actionIndex = 0;

            while (!CancelEvent.WaitOne(0, false)) {
                var action = FlowSequence[actionIndex];
                var doStep = !StepEvent.IsSet;
                if (doStep) {
                    try {
                        CanStopEvent.Reset();  // prevent any stop action
                        if (!currIsRunning) {
                            currIsRunning = true;
                            Interlocked.Exchange(ref CurrentIsRunning, 1);
                        }

                        StepEvent.Signal();  // wait to be allowed to step

                        Interlocked.Increment(ref CurrentStepCount);
                        Debug.WriteLine(CurrentStepCount);

                        if (action is FlowCommand commandAction) {
                            commandAction.TryExecute();
                        } else if (action is FlowMessageOut messageOutAction) {
                            messageOutAction.TrySend();
                        } else if (action is FlowMessageIn messageInAction) {
                            messageInAction.TryReceive();
                        }

                        actionIndex++;
                        if (actionIndex == FlowSequence.Count) { actionIndex = 0; }
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
