namespace AASeq;
using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

/// <summary>
/// Endpoint plugin.
/// </summary>
internal sealed class EndpointPlugin : PluginBase {

    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="configuration">Configuration nodes.</param>
    public EndpointInstance CreateInstance(ILogger logger, AASeqNodes configuration) {
        var instance = CreateInstanceMethodInfo.Invoke(null, [logger, configuration])!;
        return new EndpointInstance(instance, StartMethodInfo, SendMethodInfo, ReceiveMethodInfo);
    }


    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    /// <param name="createInstanceMethodInfo">Reflection data for CreateInstance method.</param>
    /// <param name="sendMethodInfo">Reflection data for Send method.</param>
    /// <param name="receiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    public EndpointPlugin(Type type, MethodInfo createInstanceMethodInfo, MethodInfo startMethodInfo, MethodInfo sendMethodInfo, MethodInfo receiveMethodInfo)
        : base(type) {
        CreateInstanceMethodInfo = createInstanceMethodInfo;
        StartMethodInfo = startMethodInfo;
        SendMethodInfo = sendMethodInfo;
        ReceiveMethodInfo = receiveMethodInfo;
    }


    private readonly MethodInfo CreateInstanceMethodInfo;

    private readonly MethodInfo StartMethodInfo;
    private readonly MethodInfo SendMethodInfo;
    private readonly MethodInfo ReceiveMethodInfo;

}
