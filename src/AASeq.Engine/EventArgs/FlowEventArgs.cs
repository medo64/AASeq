namespace AASeq;
using System;

/// <summary>
/// Flow event arguments.
/// </summary>
public sealed class FlowEventArgs : EventArgs {

    internal FlowEventArgs(int flowIndex) {
        FlowIndex = flowIndex;
    }


    /// <summary>
    /// Gets the flow index.
    /// </summary>
    public int FlowIndex { get; }

}
