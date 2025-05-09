namespace AASeq;
using System;
using System.Collections.Generic;
using System.Threading;

public sealed partial class Engine {

    /// <summary>
    /// Gets all endpoints.
    /// </summary>
    public IReadOnlyList<IEndpoint> Endpoints { get; }

    /// <summary>
    /// Gets all flows.
    /// </summary>
    public IReadOnlyList<IFlowAction> FlowSequence { get; }


    /// <summary>
    /// Gets timeout for command execution.
    /// </summary>
    public TimeSpan CommandTimeout { get; }

    /// <summary>
    /// Gets timeout for message receiving.
    /// </summary>
    public TimeSpan ReceiveTimeout { get; }

    /// <summary>
    /// Gets timeout for message sending.
    /// </summary>
    public TimeSpan SendTimeout { get; }

    /// <summary>
    /// Gets repeat count.
    /// </summary>
    public int RepeatCount { get; }


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


    private readonly Lock EndpointsStartedLock = new();
    private bool EndpointsStarted;
    private void StartEndpointsIfNeeded() {
        lock (EndpointsStartedLock) {
            if (!EndpointsStarted) {
                foreach (var endpoint in Endpoints) {
                    using var cts = (SendTimeout < TimeSpan.MaxValue)
                                  ? new CancellationTokenSource(SendTimeout)
                                  : new CancellationTokenSource();
                    ((EndpointStore)endpoint).Instance.Start(cts.Token);
                }
                EndpointsStarted = true;
            }
        }
    }

    /// <summary>
    /// Starts running the engine.
    /// </summary>
    public void Start() {
        StartEndpointsIfNeeded();
        if (FlowSequence.Count > 0) {
            StepEvent.Reset(0);
            CanStopEvent.WaitOne();
            StepEvent.Reset(int.MaxValue);
        }
    }

    /// <summary>
    /// Pauses the engine.
    /// </summary>
    public void Pause() {
        if (FlowSequence.Count > 0) {
            StepEvent.Reset(0);
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

    /// <summary>
    /// Performs a single step.
    /// </summary>
    public void Step() {
        StartEndpointsIfNeeded();
        if (FlowSequence.Count > 0) {
            StepEvent.Reset(1);
        }
    }


    /// <summary>
    /// Returns own (@Me) endpoint definition.
    /// </summary>
    public AASeqNode OwnDefinitionNode {
        get {
            var response = new AASeqNode("@Me", AASeqValue.Null);
            response.Nodes.Add(new AASeqNode("Repeat", RepeatCount));

            if (LogFile.Path is not null) {
                response.Nodes.Add(new AASeqNode("LogFile", LogFile.Path));
            }

            if ((CommandTimeout.Ticks == ReceiveTimeout.Ticks) && (ReceiveTimeout.Ticks == SendTimeout.Ticks)) {
                response.Nodes.Add(new AASeqNode("Timeout", CommandTimeout));
            } else {
                response.Nodes.Add(new AASeqNode("CommandTimeout", CommandTimeout == TimeSpan.MaxValue ? double.PositiveInfinity : CommandTimeout));
                response.Nodes.Add(new AASeqNode("ReceiveTimeut", ReceiveTimeout));
                response.Nodes.Add(new AASeqNode("SendTimeut", SendTimeout));
            }

            return response;
        }
    }

}
