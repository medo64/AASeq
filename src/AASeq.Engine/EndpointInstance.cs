namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Endpoint plugin instance.
/// </summary>
[DebuggerDisplay("{Instance.GetType().Name,nq}")]
public sealed class EndpointInstance {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    public EndpointInstance(Object instance) {
        Instance = instance;
        PluginName = Instance.GetType().Name;
    }


    private readonly Object Instance;
    internal string PluginName { get; }

}
