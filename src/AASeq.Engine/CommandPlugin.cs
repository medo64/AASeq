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
    /// <param name="tryExecuteMethodInfo">Reflection data for TryExecute method.</param>
    public CommandPlugin(Type type, MethodInfo getInstanceMethodInfo, MethodInfo tryExecuteMethodInfo)
        : base(type) {
        GetInstanceMethodInfo = getInstanceMethodInfo;
        TryExecuteMethodInfo = tryExecuteMethodInfo;
    }


    private readonly MethodInfo GetInstanceMethodInfo;
    private readonly MethodInfo TryExecuteMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    public CommandInstance GetInstance() {
        var instance = GetInstanceMethodInfo.Invoke(null, [])!;
        return new CommandInstance(instance, TryExecuteMethodInfo);
    }

}
