namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{MessageName,nq}: {SourceInstance.PluginName,nq}→{DestinationInstance.PluginName,nq}")]
public sealed class FlowMessage : IFlowAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="sourceInstance">Source endpoint instance.</param>
    /// <param name="destinationInstance">Destination endpoint instance.</param>
    /// <param name="data">Data nodes.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowMessage(string messageName, EndpointInstance sourceInstance, EndpointInstance destinationInstance, AASeqNodes data) {
        MessageName = messageName;
        SourceInstance = sourceInstance;
        DestinationInstance = destinationInstance;
    }


    private readonly string MessageName;
    private readonly EndpointInstance SourceInstance;
    private readonly EndpointInstance DestinationInstance;

}
