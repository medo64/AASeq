namespace AASeq;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <summary>
/// Endpoint plugin instance.
/// </summary>
[DebuggerDisplay("{Instance.GetType().Name,nq}")]
internal sealed class EndpointInstance : IEndpointPluginInstance {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="trySendMethodInfo">Reflection data for Send method.</param>
    /// <param name="tryReceiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    internal EndpointInstance(Object instance, MethodInfo? trySendMethodInfo, MethodInfo? tryReceiveMethodInfo) {
        Instance = instance;
        TrySendMethodInfo = trySendMethodInfo;
        TryReceiveMethodInfo = tryReceiveMethodInfo;
    }


    private readonly Object Instance;
    private readonly MethodInfo? TrySendMethodInfo;
    private readonly MethodInfo? TryReceiveMethodInfo;

    [SuppressMessage("Style", "IDE0051:Remove unused private members", Justification = "Used for IFlowAction DebuggerDisplay")]
    private string PluginName => Instance.GetType().Name;


    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public bool TrySend(Guid id, string messageName, AASeqNodes data) {
        if (TrySendMethodInfo is null) { throw new NotSupportedException(); }
        var result = TrySendMethodInfo.Invoke(Instance, [id, messageName, data]);
        return (bool)result!;
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public bool TryReceive(Guid id, [MaybeNullWhen(false)] out string messageName, [MaybeNullWhen(false)] out AASeqNodes data) {
        if (TryReceiveMethodInfo is null) { throw new NotSupportedException(); }
        var parameters = new object?[] { id, null, null };
        var result = TryReceiveMethodInfo.Invoke(Instance, parameters)!;
        messageName = (string)parameters[1]!;
        data = (AASeqNodes)parameters[2]!;
        return (bool)result!;
    }

}
