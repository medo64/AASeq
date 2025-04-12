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

}
