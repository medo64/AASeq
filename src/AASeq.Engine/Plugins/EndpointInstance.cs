namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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
        try {
            var task = SendAsync(id, messageName, data, cancellationToken);
            task.Wait(cancellationToken);
        } catch (TargetInvocationException ex) {
            throw ex.InnerException is null ? ex : ex.InnerException;
        }
    }

    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task SendAsync(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        if (SendMethodInfo is null) { throw new NotSupportedException(); }
        var task = (Task)SendMethodInfo.Invoke(Instance, [id, messageName, data, cancellationToken])!;
        await task.ConfigureAwait(false);
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public void Receive(Guid id, ref string messageName, out AASeqNodes data, CancellationToken cancellationToken) {
        try {
            var task = ReceiveAsync(id, messageName, cancellationToken);
            task.Wait(cancellationToken);
            messageName = task.Result.Item1;
            data = task.Result.Item2;
        } catch (TargetInvocationException ex) {
            throw ex.InnerException is null ? ex : ex.InnerException;
        }
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, CancellationToken cancellationToken) {
        if (ReceiveMethodInfo is null) { throw new NotSupportedException(); }
        var task = (Task<Tuple<string, AASeqNodes>>)ReceiveMethodInfo.Invoke(Instance, [id, messageName, cancellationToken])!;
        return await task.ConfigureAwait(false);
    }

}
