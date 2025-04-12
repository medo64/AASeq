namespace AASeq;
using System;

public sealed partial class Engine {

    private void OnFlowBegin(int flowIndex) {
        FlowBegin?.Invoke(this, new FlowEventArgs(flowIndex));
    }

    private void OnFlowEnd(int flowIndex) {
        FlowEnd?.Invoke(this, new FlowEventArgs(flowIndex));
    }

    private void OnActionStart(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        ActionBegin?.Invoke(this, new ActionEventArgs(flowIndex, actionIndex, action, node));
    }

    private void OnActionEnd(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        ActionEnd?.Invoke(this, new ActionEventArgs(flowIndex, actionIndex, action, node));
    }


    /// <summary>
    /// Raised when flow starts.
    /// </summary>
    public event EventHandler<FlowEventArgs>? FlowBegin;

    /// <summary>
    /// Raised when flow is completed.
    /// </summary>
    public event EventHandler<FlowEventArgs>? FlowEnd;


    /// <summary>
    /// Raised when action starts.
    /// </summary>
    public event EventHandler<ActionEventArgs>? ActionBegin;

    /// <summary>
    /// Raised when action is completed.
    /// </summary>
    public event EventHandler<ActionEventArgs>? ActionEnd;

}
