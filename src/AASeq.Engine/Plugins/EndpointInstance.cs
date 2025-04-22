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
    /// <param name="sendMethodInfo">Reflection data for Send method.</param>
    /// <param name="receiveMethodInfo">Reflection data for ReceiveMethod method.</param>
    internal EndpointInstance(Object instance, MethodInfo sendMethodInfo, MethodInfo receiveMethodInfo)
        : base(instance) {
        SendMethodInfo = sendMethodInfo;
        ReceiveMethodInfo = receiveMethodInfo;
    }


    private readonly MethodInfo SendMethodInfo;
    private readonly MethodInfo ReceiveMethodInfo;


    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public void Send(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        if (SendMethodInfo is null) { throw new NotSupportedException(); }
        try {
            SendMethodInfo.Invoke(Instance, [id, messageName, data, cancellationToken]);
        } catch (TargetInvocationException ex) {
            throw ex.InnerException is null ? ex : ex.InnerException;
        }
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public void Receive(Guid id, [MaybeNullWhen(false)] ref string messageName, [MaybeNullWhen(false)] out AASeqNodes data, CancellationToken cancellationToken) {
        if (ReceiveMethodInfo is null) { throw new NotSupportedException(); }
        try {
            var parameters = new object?[] { id, messageName, null, cancellationToken };
            ReceiveMethodInfo.Invoke(Instance, parameters);
            messageName = (string)parameters[1]!;
            data = (AASeqNodes)parameters[2]!;
        } catch (TargetInvocationException ex) {
            throw ex.InnerException is null ? ex : ex.InnerException;
        }
    }

}
