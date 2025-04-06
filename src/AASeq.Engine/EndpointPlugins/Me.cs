namespace AASeq.EndpointPlugins;
using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Default local endpoint.
/// </summary>
internal sealed class Me {

    /// <summary>
    /// Creates a new instance. 
    /// </summary>
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Needed for interface")]
    public Me(AASeqNodes nodes) {
    }

}
