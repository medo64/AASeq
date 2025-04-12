namespace AASeq;
using System;

/// <summary>
/// Action event arguments.
/// </summary>
public sealed class ActionEventArgs : EventArgs {

    internal ActionEventArgs(int flowIndex, int actionIndex, AASeqNode node) {
        FlowIndex = flowIndex;
        ActionIndex = actionIndex;
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
    /// Gets the node.
    /// </summary>
    public AASeqNode Node { get; }

}
