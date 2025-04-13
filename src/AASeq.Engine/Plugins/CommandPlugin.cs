namespace AASeq;
using System;
using System.Reflection;

/// <summary>
/// Command plugin.
/// </summary>
internal sealed class CommandPlugin : PluginBase {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    /// <param name="createInstanceMethodInfo">Reflection data for CreateInstance method.</param>
    /// <param name="tryExecuteMethodInfo">Reflection data for TryExecute method.</param>
    public CommandPlugin(Type type, MethodInfo createInstanceMethodInfo, MethodInfo tryExecuteMethodInfo)
        : base(type) {
        CreateInstanceMethodInfo = createInstanceMethodInfo;
        TryExecuteMethodInfo = tryExecuteMethodInfo;
    }


    private readonly MethodInfo CreateInstanceMethodInfo;
    private readonly MethodInfo TryExecuteMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    public CommandInstance CreateInstance() {
        var instance = CreateInstanceMethodInfo.Invoke(null, [])!;
        return new CommandInstance(instance, TryExecuteMethodInfo);
    }

}
