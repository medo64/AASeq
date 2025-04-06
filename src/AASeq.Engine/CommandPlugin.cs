namespace AASeq;
using System;

/// <summary>
/// Command plugin.
/// </summary>
internal sealed class CommandPlugin : Plugin {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    public CommandPlugin(Type type)
        : base(type) {
    }


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="nodes">Configuration nodes.</param>
    public CommandInstance GetInstance(AASeqNodes nodes) {
        return new CommandInstance(Activator.CreateInstance(Type, nodes)!);
    }

}
