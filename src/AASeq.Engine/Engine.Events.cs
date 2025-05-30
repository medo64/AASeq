namespace AASeq;
using System;

public sealed partial class Engine {

    private void OnFlowBegin(int flowIndex) {
        FlowBegin?.Invoke(this, new FlowEventArgs(flowIndex));
        LogFile.WriteFlowBegin(flowIndex);
    }

    private void OnFlowDone(int flowIndex) {
        FlowDone?.Invoke(this, new FlowEventArgs(flowIndex));
        LogFile.WriteFlowDone(flowIndex);
    }

    private void OnActionStart(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        ActionBegin?.Invoke(this, new ActionEventArgs(flowIndex, actionIndex, action, node));
        LogFile.WriteActionStart(flowIndex, actionIndex, action, node);
    }

    private void OnActionDone(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        ActionDone?.Invoke(this, new ActionEventArgs(flowIndex, actionIndex, action, node));
        LogFile.WriteActionDone(flowIndex, actionIndex, action, node);
    }

    private void OnActionError(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        ActionError?.Invoke(this, new ActionEventArgs(flowIndex, actionIndex, action, node));
        LogFile.WriteActionError(flowIndex, actionIndex, action, node);
    }


    /// <summary>
    /// Raised when flow starts.
    /// </summary>
    public event EventHandler<FlowEventArgs>? FlowBegin;

    /// <summary>
    /// Raised when flow is completed.
    /// </summary>
    public event EventHandler<FlowEventArgs>? FlowDone;


    /// <summary>
    /// Raised when action starts.
    /// </summary>
    public event EventHandler<ActionEventArgs>? ActionBegin;

    /// <summary>
    /// Raised when action is completed.
    /// </summary>
    public event EventHandler<ActionEventArgs>? ActionDone;

    /// <summary>
    /// Raised when action caused an exception.
    /// </summary>
    public event EventHandler<ActionEventArgs>? ActionError;

}
