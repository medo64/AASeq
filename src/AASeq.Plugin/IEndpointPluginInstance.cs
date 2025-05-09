namespace AASeq;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Endpoint plugin instance interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface IEndpointPluginInstance {

    /// <summary>
    /// Starts the endpoint.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task StartAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Sends message to the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task<AASeqNodes> SendAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken);

    /// <summary>
    /// Receives message from the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken);

}
