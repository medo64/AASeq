namespace AASeq;
using System;
using static System.Collections.Specialized.BitVector32;

/// <summary>
/// Action exception event arguments.
/// </summary>
public sealed class ActionExceptionEventArgs : EventArgs {

    internal ActionExceptionEventArgs(int flowIndex, int actionIndex, IFlowAction action, Exception exception) {
        FlowIndex = flowIndex;
        ActionIndex = actionIndex;
        Action = action ?? throw new ArgumentNullException(nameof(action));
        Exception = exception ?? throw new ArgumentNullException(nameof(action));
    }


    /// <summary>
    /// Gets the flow index.
    /// </summary>
    public int FlowIndex { get; }

    /// <summary>
    /// Gets the action index.
    /// </summary>
    public int ActionIndex { get; }

    /// <summary>
    /// Gets the action.
    /// </summary>
    public IFlowAction Action { get; }

    /// <summary>
    /// Gets the exception.
    /// </summary>
    public Exception Exception { get; }

}
