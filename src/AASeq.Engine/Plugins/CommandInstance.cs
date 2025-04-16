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
    /// <param name="executeMethodInfo">Reflection data for Execute method.</param>
    internal CommandInstance(Object instance, MethodInfo executeMethodInfo)
        : base(instance) {
        ExecuteMethodInfo = executeMethodInfo;
    }


    private readonly MethodInfo ExecuteMethodInfo;


    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public void Execute(AASeqNodes data, CancellationToken cancellationToken) {
        try {
            ExecuteMethodInfo.Invoke(Instance, [data, cancellationToken]);
        } catch (TargetInvocationException ex) {
            throw ex.InnerException is null ? ex : ex.InnerException;
        }
    }

}
