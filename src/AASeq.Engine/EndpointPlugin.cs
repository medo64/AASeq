namespace AASeq;
using System;
using System.Reflection;

/// <summary>
/// Endpoint plugin.
/// </summary>
internal sealed class EndpointPlugin : Plugin {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    /// <param name="getInstanceMethodInfo">Reflection data for GetInstance method.</param>
    /// <param name="sendMethodInfo">Reflection data for Send method.</param>
    /// <param name="receiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    public EndpointPlugin(Type type, MethodInfo getInstanceMethodInfo, MethodInfo sendMethodInfo, MethodInfo receiveMethodInfo)
        : base(type) {
        GetInstanceMethodInfo = getInstanceMethodInfo;
        SendMethodInfo = sendMethodInfo;
        ReceiveMethodInfo = receiveMethodInfo;
    }


    private readonly MethodInfo GetInstanceMethodInfo;
    private readonly MethodInfo SendMethodInfo;
    private readonly MethodInfo ReceiveMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="configuration">Configuration nodes.</param>
    public EndpointInstance GetInstance(AASeqNodes configuration) {
        var instance = GetInstanceMethodInfo.Invoke(null, [configuration])!;
        return new EndpointInstance(instance, SendMethodInfo, ReceiveMethodInfo);
    }

}
