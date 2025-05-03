namespace AASeq;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

/// <summary>
/// Command plugin.
/// </summary>
internal sealed class CommandPlugin : PluginBase {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    /// <param name="createInstanceMethodInfo">Reflection data for CreateInstance method.</param>
    /// <param name="executeMethodInfo">Reflection data for Execute method.</param>
    public CommandPlugin(Type type, MethodInfo executeMethodInfo)
        : base(type) {
        ExecuteMethodInfo = executeMethodInfo;
    }


    private readonly MethodInfo ExecuteMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    public AASeqNodes Execute(ILogger logger, AASeqNodes parameters, CancellationToken cancellationToken) {
        var task = (Task<AASeqNodes>)ExecuteMethodInfo.Invoke(null, [logger, parameters, cancellationToken])!;
        return task.GetAwaiter().GetResult();
    }

}
