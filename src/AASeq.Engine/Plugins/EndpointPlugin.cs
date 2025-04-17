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
    /// <param name="validateConfigurationMethodInfo">Reflection data for ValidateConfiguration method</param>
    /// <param name="validateDataMethodInfo">Reflection data for ValidateData method</param>
    /// <param name="sendMethodInfo">Reflection data for Send method.</param>
    /// <param name="receiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    public EndpointPlugin(Type type, MethodInfo createInstanceMethodInfo, MethodInfo validateConfigurationMethodInfo, MethodInfo validateDataMethodInfo, MethodInfo sendMethodInfo, MethodInfo receiveMethodInfo)
        : base(type) {
        CreateInstanceMethodInfo = createInstanceMethodInfo;
        ValidateConfigurationMethodInfo = validateConfigurationMethodInfo;
        ValidateDataMethodInfo = validateDataMethodInfo;
        SendMethodInfo = sendMethodInfo;
        ReceiveMethodInfo = receiveMethodInfo;
    }


    private readonly MethodInfo CreateInstanceMethodInfo;
    private readonly MethodInfo ValidateConfigurationMethodInfo;
    private readonly MethodInfo ValidateDataMethodInfo;

    private readonly MethodInfo SendMethodInfo;
    private readonly MethodInfo ReceiveMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="configuration">Configuration nodes.</param>
    public EndpointInstance CreateInstance(AASeqNodes configuration) {
        var instance = CreateInstanceMethodInfo.Invoke(null, [configuration])!;
        return new EndpointInstance(instance, SendMethodInfo, ReceiveMethodInfo);
    }

    /// <summary>
    /// Returns validated configuration.
    /// </summary>
    public AASeqNodes ValidateConfiguration(AASeqNodes configuration) {
        if (ValidateConfigurationMethodInfo is null) { throw new NotSupportedException(); }
        var result = ValidateConfigurationMethodInfo.Invoke(null, [configuration]);
        return (AASeqNodes)result!;
    }

    /// <summary>
    /// Returns validated data.
    /// </summary>
    public AASeqNodes ValidateData(string message, AASeqNodes data) {
        if (ValidateDataMethodInfo is null) { throw new NotSupportedException(); }
        var result = ValidateDataMethodInfo.Invoke(null, [message, data]);
        return (AASeqNodes)result!;
    }

}
