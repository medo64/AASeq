namespace AASeq;
using System;

/// <summary>
/// Endpoint plugin.
/// </summary>
internal sealed class EndpointPlugin : Plugin {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    public EndpointPlugin(Type type)
        : base(type) {
    }


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="nodes">Configuration nodes.</param>
    public EndpointInstance GetInstance(AASeqNodes nodes) {
        return new EndpointInstance(Activator.CreateInstance(Type, nodes)!);
    }

}
