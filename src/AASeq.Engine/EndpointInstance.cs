namespace AASeq;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <summary>
/// Endpoint plugin instance.
/// </summary>
[DebuggerDisplay("{Instance.GetType().Name,nq}")]
internal sealed class EndpointInstance {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="sendMethodInfo">Reflection data for Send method.</param>
    /// <param name="receiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    internal EndpointInstance(Object instance, MethodInfo? sendMethodInfo, MethodInfo? receiveMethodInfo) {
        Instance = instance;
        SendMethodInfo = sendMethodInfo;
        ReceiveMethodInfo = receiveMethodInfo;
    }


    private readonly Object Instance;
    private readonly MethodInfo? SendMethodInfo;
    private readonly MethodInfo? ReceiveMethodInfo;

    [SuppressMessage("Style", "IDE0051:Remove unused private members", Justification = "Used for IFlowAction DebuggerDisplay")]
    private string PluginName => Instance.GetType().Name;


    /// <summary>
    /// Sends the message as returns ID for the answer.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public Guid Send(string messageName, AASeqNodes data) {
        if (SendMethodInfo is null) { throw new NotSupportedException(); }
        return (Guid)SendMethodInfo.Invoke(Instance, [messageName, data])!;
    }

    /// <summary>
    /// Returns the received message.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    public AASeqNodes Receive(Guid id, out string messageName) {
        if (ReceiveMethodInfo is null) { throw new NotSupportedException(); }
        var parameters = new object?[] { id, null };
        var result = (AASeqNodes)ReceiveMethodInfo.Invoke(Instance, parameters)!;
        messageName = (string)parameters[1]!;
        return result;
    }

}
