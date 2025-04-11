namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{MessageName,nq} →{DestinationName,nq} ({DestinationInstance.PluginName,nq})")]
public sealed class FlowMessageOut : IFlowAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="destinationName">Destination name.</param>
    /// <param name="destinationInstance">Destination endpoint instance.</param>
    /// <param name="templateData">Data nodes.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowMessageOut(string messageName, string destinationName, EndpointInstance destinationInstance, AASeqNodes templateData) {
        MessageName = messageName;
        DestinationName = destinationName;
        DestinationInstance = destinationInstance;
        TemplateData = templateData;
    }


    internal string MessageName { get; }
    internal string DestinationName { get; }
    internal EndpointInstance DestinationInstance { get; }
    internal AASeqNodes TemplateData { get; }

}
