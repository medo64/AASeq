namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{MessageName,nq}: ←{SourceInstance.PluginName,nq}")]
public sealed class FlowMessageIn : IFlowAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="sourceInstance">Source endpoint instance.</param>
    /// <param name="data">Data nodes.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowMessageIn(string messageName, EndpointInstance sourceInstance, AASeqNodes data) {
        MessageName = messageName;
        SourceInstance = sourceInstance;
    }


    private string MessageName { get; }
    private EndpointInstance SourceInstance { get; }

    internal void Receive() {
        SourceInstance.Receive(Guid.Empty, out string messageName);
    }
}
