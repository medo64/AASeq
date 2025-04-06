namespace AASeq;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <summary>
/// Command plugin instance.
/// </summary>
[DebuggerDisplay("{Instance.GetType().Name,nq}")]
internal sealed class CommandInstance {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="executeMethodInfo">Reflection data for Execute method.</param>
    internal CommandInstance(Object instance, MethodInfo executeMethodInfo) {
        Instance = instance;
        ExecuteMethodInfo = executeMethodInfo;
    }


    private readonly Object Instance;
    private readonly MethodInfo ExecuteMethodInfo;

    [SuppressMessage("Style", "IDE0051:Remove unused private members", Justification = "Used for IFlowAction DebuggerDisplay")]
    private string PluginName => Instance.GetType().Name;


    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="data">Data.</param>
    public void Execute(AASeqNodes data) {
        ExecuteMethodInfo.Invoke(Instance, [data]);
    }

}
