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
    internal EndpointInstance(Object instance, MethodInfo sendMethodInfo, MethodInfo receiveMethodInfo) {
        Instance = instance;
        SendMethodInfo = sendMethodInfo;
        ReceiveMethodInfo = receiveMethodInfo;
    }


    private readonly Object Instance;
    private readonly MethodInfo SendMethodInfo;
    private readonly MethodInfo ReceiveMethodInfo;

    [SuppressMessage("Style", "IDE0051:Remove unused private members", Justification = "Used for IFlowAction DebuggerDisplay")]
    private string PluginName => Instance.GetType().Name;


    /// <summary>
    /// Sends the message.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public void Send(Object instance, string messageName, AASeqNodes data) {
        SendMethodInfo.Invoke(instance, [messageName, data]);
    }

    /// <summary>
    /// Receives the message.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="expectedData">Expected data.</param>
    public AASeqNodes Receive(Object instance, string messageName, AASeqNodes expectedData) {
        return (AASeqNodes)ReceiveMethodInfo.Invoke(instance, [messageName, expectedData])!;
    }

}
