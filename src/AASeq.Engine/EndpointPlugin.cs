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
    /// <param name="getInstanceMethodInfo">Reflection data for GetInstance method.</param>
    /// <param name="getConfigurationMethodInfo">Reflection data for GetConfiguration method</param>
    /// <param name="trySendMethodInfo">Reflection data for Send method.</param>
    /// <param name="tryReceiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    public EndpointPlugin(Type type, MethodInfo getInstanceMethodInfo, MethodInfo getConfigurationMethodInfo, MethodInfo trySendMethodInfo, MethodInfo tryReceiveMethodInfo)
        : base(type) {
        GetInstanceMethodInfo = getInstanceMethodInfo;
        GetConfigurationMethodInfo = getConfigurationMethodInfo;
        TrySendMethodInfo = trySendMethodInfo;
        TryReceiveMethodInfo = tryReceiveMethodInfo;
    }


    private readonly MethodInfo GetInstanceMethodInfo;
    private readonly MethodInfo GetConfigurationMethodInfo;
    private readonly MethodInfo TrySendMethodInfo;
    private readonly MethodInfo TryReceiveMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="configuration">Configuration nodes.</param>
    public EndpointInstance GetInstance(AASeqNodes configuration) {
        var instance = GetInstanceMethodInfo.Invoke(null, [configuration])!;
        return new EndpointInstance(instance, GetConfigurationMethodInfo, TrySendMethodInfo, TryReceiveMethodInfo);
    }

}
