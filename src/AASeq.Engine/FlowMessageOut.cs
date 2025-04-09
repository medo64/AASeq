namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{MessageName,nq}: →{DestinationInstance.PluginName,nq}")]
public sealed class FlowMessageOut : IFlowAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="destinationInstance">Destination endpoint instance.</param>
    /// <param name="data">Data nodes.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowMessageOut(string messageName, EndpointInstance destinationInstance, AASeqNodes data) {
        MessageName = messageName;
        DestinationInstance = destinationInstance;
        Data = data;
    }


    private string MessageName { get; }
    private EndpointInstance DestinationInstance { get; }
    private AASeqNodes Data { get; }

    internal bool TrySend() {
        return DestinationInstance.TrySend(Guid.Empty, MessageName, Data);
    }

}
