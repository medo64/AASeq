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
    /// <param name="trySendMethodInfo">Reflection data for Send method.</param>
    /// <param name="tryReceiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    public EndpointPlugin(Type type, MethodInfo createInstanceMethodInfo, MethodInfo validateConfigurationMethodInfo, MethodInfo trySendMethodInfo, MethodInfo tryReceiveMethodInfo)
        : base(type) {
        CreateInstanceMethodInfo = createInstanceMethodInfo;
        ValidateConfigurationMethodInfo = validateConfigurationMethodInfo;
        TrySendMethodInfo = trySendMethodInfo;
        TryReceiveMethodInfo = tryReceiveMethodInfo;
    }


    private readonly MethodInfo CreateInstanceMethodInfo;
    private readonly MethodInfo ValidateConfigurationMethodInfo;
    private readonly MethodInfo TrySendMethodInfo;
    private readonly MethodInfo TryReceiveMethodInfo;


    /// <summary>
    /// Returns a new instance of the plugin.
    /// </summary>
    /// <param name="configuration">Configuration nodes.</param>
    public EndpointInstance CreateInstance(AASeqNodes configuration) {
        var instance = CreateInstanceMethodInfo.Invoke(null, [configuration])!;
        return new EndpointInstance(instance, TrySendMethodInfo, TryReceiveMethodInfo);
    }

    /// <summary>
    /// Returns validated configuration.
    /// </summary>
    public AASeqNodes ValidateConfiguration(AASeqNodes configuration) {
        if (ValidateConfigurationMethodInfo is null) { throw new NotSupportedException(); }
        var result = ValidateConfigurationMethodInfo.Invoke(null, [configuration]);
        return (AASeqNodes)result!;
    }

}
