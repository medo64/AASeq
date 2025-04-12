namespace AASeq;
using System;
using static System.Collections.Specialized.BitVector32;

/// <summary>
/// Action event arguments.
/// </summary>
public sealed class ActionEventArgs : EventArgs {

    internal ActionEventArgs(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        FlowIndex = flowIndex;
        ActionIndex = actionIndex;
        Action = action ?? throw new ArgumentNullException(nameof(action));
        Node = node ?? throw new ArgumentNullException(nameof(node));
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
    /// Gets the node.
    /// </summary>
    public AASeqNode Node { get; }

}
