namespace AASeq;
using System;
using System.Reflection;

/// <summary>
/// Command plugin.
/// </summary>
internal sealed class CommandPlugin : Plugin {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    /// <param name="getInstanceMethodInfo">Reflection data for GetInstance method.</param>
    /// <param name="executeMethodInfo">Reflection data for Execute method.</param>
    public CommandPlugin(Type type, MethodInfo getInstanceMethodInfo, MethodInfo executeMethodInfo)
        : base(type) {
        GetInstanceMethodInfo = getInstanceMethodInfo;
        ExecuteMethodInfo = executeMethodInfo;
    }


    private readonly MethodInfo GetInstanceMethodInfo;
    private readonly MethodInfo ExecuteMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="configuration">Configuration nodes.</param>
    public CommandInstance GetInstance(AASeqNodes configuration) {
        var instance = GetInstanceMethodInfo.Invoke(null, [configuration])!;
        return new CommandInstance(instance, ExecuteMethodInfo);
    }

}
