namespace AASeq;
using System;
using System.Diagnostics;
using System.Threading;

[DebuggerDisplay("{PluginName,nq}/{PluginInstanceIndex,nq}")]
internal abstract class PluginInstanceBase {

    protected PluginInstanceBase(Object instance) {
        Instance = instance;
        PluginName = Instance.GetType().Name;
        PluginInstanceIndex = Interlocked.Increment(ref PluginInstanceIndexRoot);
    }

    protected readonly Object Instance;

    internal string PluginName { get; }

    private readonly int PluginInstanceIndex;
    private static int PluginInstanceIndexRoot;

}
