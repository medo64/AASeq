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
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task SendAsync(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken);

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, CancellationToken cancellationToken);

}
