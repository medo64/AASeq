namespace AASeq;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <summary>
/// Command plugin instance.
/// </summary>
[DebuggerDisplay("{Instance.GetType().Name,nq}")]
internal sealed class CommandInstance : ICommandPluginInstance {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="tryExecuteMethodInfo">Reflection data for TryExecute method.</param>
    internal CommandInstance(Object instance, MethodInfo tryExecuteMethodInfo) {
        Instance = instance;
        TryExecuteMethodInfo = tryExecuteMethodInfo;
    }


    private readonly Object Instance;
    private readonly MethodInfo TryExecuteMethodInfo;

    [SuppressMessage("Style", "IDE0051:Remove unused private members", Justification = "Used for IFlowAction DebuggerDisplay")]
    private string PluginName => Instance.GetType().Name;


    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="data">Data.</param>
    public bool TryExecute(AASeqNodes data) {
        var result = TryExecuteMethodInfo.Invoke(Instance, [data]);
        return (bool)result!;
    }

}
