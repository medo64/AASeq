namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Command plugin instance.
/// </summary>
[DebuggerDisplay("{Instance.GetType().Name,nq}")]
public sealed class CommandInstance {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    public CommandInstance(Object instance) {
        Instance = instance;
        PluginName = Instance.GetType().Name;
    }


    private readonly Object Instance;
    internal string PluginName { get; }

}
