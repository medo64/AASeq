namespace AASeq;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task ExecuteAsync(AASeqNodes parameters, CancellationToken cancellationToken) {
        if (ExecuteMethodInfo is null) { throw new NotSupportedException(); }
        var task = (Task)ExecuteMethodInfo.Invoke(Instance, [parameters, cancellationToken])!;
        await task.ConfigureAwait(false);
    }

}
