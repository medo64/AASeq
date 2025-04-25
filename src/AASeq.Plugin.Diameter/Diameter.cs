namespace AASeqPlugin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using AASeq;

/// <summary>
/// Diameter endpoint.
/// </summary>
internal sealed class Diameter : IEndpointPlugin {

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(AASeqNodes configuration) {
        return new Diameter(configuration);
    }


    private Diameter(AASeqNodes configuration) {
    }


    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task SendAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

}
