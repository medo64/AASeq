namespace AASeq;
using System;
using System.Reflection;

/// <summary>
/// Endpoint plugin.
/// </summary>
internal sealed class EndpointPlugin : PluginBase {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    /// <param name="createInstanceMethodInfo">Reflection data for CreateInstance method.</param>
    /// <param name="getConfigurationMethodInfo">Reflection data for GetConfiguration method</param>
    /// <param name="trySendMethodInfo">Reflection data for Send method.</param>
    /// <param name="tryReceiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    public EndpointPlugin(Type type, MethodInfo createInstanceMethodInfo, MethodInfo getConfigurationMethodInfo, MethodInfo trySendMethodInfo, MethodInfo tryReceiveMethodInfo)
        : base(type) {
        CreateInstanceMethodInfo = createInstanceMethodInfo;
        GetConfigurationMethodInfo = getConfigurationMethodInfo;
        TrySendMethodInfo = trySendMethodInfo;
        TryReceiveMethodInfo = tryReceiveMethodInfo;
    }


    private readonly MethodInfo CreateInstanceMethodInfo;
    private readonly MethodInfo GetConfigurationMethodInfo;
    private readonly MethodInfo TrySendMethodInfo;
    private readonly MethodInfo TryReceiveMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="configuration">Configuration nodes.</param>
    public EndpointInstance CreateInstance(AASeqNodes configuration) {
        var instance = CreateInstanceMethodInfo.Invoke(null, [configuration])!;
        return new EndpointInstance(instance, GetConfigurationMethodInfo, TrySendMethodInfo, TryReceiveMethodInfo);
    }

}
