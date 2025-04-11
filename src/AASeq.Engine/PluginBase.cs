namespace AASeq;
using System;
using System.Diagnostics;

[DebuggerDisplay("{Name,nq}")]
internal abstract class PluginBase {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    protected PluginBase(Type type) {
        Type = type;
    }


    /// <summary>
    /// Gets plugin name.
    /// </summary>
    public string Name => Type.Name;

    /// <summary>
    /// Gets type.
    /// </summary>
    public Type Type { get; }

}
