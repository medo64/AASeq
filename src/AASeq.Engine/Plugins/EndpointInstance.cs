namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Endpoint plugin instance.
/// </summary>
internal sealed class EndpointInstance : PluginInstanceBase {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="startMethodInfo">Reflection data for Start method.</param>
    /// <param name="sendMethodInfo">Reflection data for Send method.</param>
    /// <param name="receiveMethodInfo">Reflection data for Receive method.</param>
    internal EndpointInstance(Object instance, MethodInfo startMethodInfo, MethodInfo sendMethodInfo, MethodInfo receiveMethodInfo)
        : base(instance) {
        StartMethodInfo = startMethodInfo;
        SendMethodInfo = sendMethodInfo;
        ReceiveMethodInfo = receiveMethodInfo;
    }


    private readonly MethodInfo StartMethodInfo;
    private readonly MethodInfo SendMethodInfo;
    private readonly MethodInfo ReceiveMethodInfo;


    /// <summary>
    /// Starts the endpoint.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public void Start(CancellationToken cancellationToken) {
        if (StartMethodInfo is null) { throw new NotSupportedException(); }
        var task = (Task)StartMethodInfo.Invoke(Instance, [cancellationToken])!;
        task.GetAwaiter().GetResult();
    }

    /// <summary>
    /// Sends message to the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public AASeqNodes Send(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        if (SendMethodInfo is null) { throw new NotSupportedException(); }
        var task = (Task<AASeqNodes>)SendMethodInfo.Invoke(Instance, [id, messageName, parameters, cancellationToken])!;
        return task.GetAwaiter().GetResult();
    }

    /// <summary>
    /// Receives message from the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Tuple<string, AASeqNodes> Receive(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        if (ReceiveMethodInfo is null) { throw new NotSupportedException(); }
        var task = (Task<Tuple<string, AASeqNodes>>)ReceiveMethodInfo.Invoke(Instance, [id, messageName, parameters, cancellationToken])!;
        return task.GetAwaiter().GetResult();
    }

}
