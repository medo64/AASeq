namespace AASeq;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;

/// <summary>
/// Command plugin instance.
/// </summary>
internal sealed class CommandInstance : PluginInstanceBase, ICommandPluginInstance {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="tryExecuteMethodInfo">Reflection data for TryExecute method.</param>
    internal CommandInstance(Object instance, MethodInfo tryExecuteMethodInfo)
        : base(instance) {
        TryExecuteMethodInfo = tryExecuteMethodInfo;
    }


    private readonly MethodInfo TryExecuteMethodInfo;


    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public bool TryExecute(AASeqNodes data, CancellationToken cancellationToken) {
        var result = TryExecuteMethodInfo.Invoke(Instance, [data, cancellationToken]);
        return (bool)result!;
    }

}
