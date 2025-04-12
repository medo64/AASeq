namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;

/// <summary>
/// Endpoint plugin instance.
/// </summary>
internal sealed class EndpointInstance : PluginInstanceBase, IEndpointPluginInstance {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="getConfigurationMethodInfo">Reflection data for GetConfiguration method.</param>
    /// <param name="trySendMethodInfo">Reflection data for Send method.</param>
    /// <param name="tryReceiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    internal EndpointInstance(Object instance, MethodInfo getConfigurationMethodInfo, MethodInfo trySendMethodInfo, MethodInfo tryReceiveMethodInfo)
        : base(instance) {
        GetConfigurationMethodInfo = getConfigurationMethodInfo;
        TrySendMethodInfo = trySendMethodInfo;
        TryReceiveMethodInfo = tryReceiveMethodInfo;
    }


    private readonly MethodInfo GetConfigurationMethodInfo;
    private readonly MethodInfo TrySendMethodInfo;
    private readonly MethodInfo TryReceiveMethodInfo;


    /// <summary>
    /// Returns instance configuration.
    /// </summary>
    public AASeqNodes GetConfiguration() {
        if (GetConfigurationMethodInfo is null) { throw new NotSupportedException(); }
        var result = GetConfigurationMethodInfo.Invoke(Instance, []);
        return (AASeqNodes)result!;
    }


    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public bool TrySend(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        if (TrySendMethodInfo is null) { throw new NotSupportedException(); }
        var result = TrySendMethodInfo.Invoke(Instance, [id, messageName, data, cancellationToken]);
        return (bool)result!;
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public bool TryReceive(Guid id, [MaybeNullWhen(false)] out string messageName, [MaybeNullWhen(false)] out AASeqNodes data, CancellationToken cancellationToken) {
        if (TryReceiveMethodInfo is null) { throw new NotSupportedException(); }
        var parameters = new object?[] { id, null, null, cancellationToken };
        var result = TryReceiveMethodInfo.Invoke(Instance, parameters)!;
        messageName = (string)parameters[1]!;
        data = (AASeqNodes)parameters[2]!;
        return (bool)result!;
    }

}
