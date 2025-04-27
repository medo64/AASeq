namespace AASeqPlugin;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AASeq;

/// <summary>
/// Diameter endpoint.
/// </summary>
internal sealed class Diameter : IEndpointPlugin, IDisposable {

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(AASeqNodes configuration) {
        return new Diameter(configuration);
    }


    private Diameter(AASeqNodes configuration) {
        var remoteIP = configuration["Remote"].AsIPAddress();
        var localIP = configuration["Local"].AsIPAddress();

        var remoteEndpoint = remoteIP is null ? configuration["Remote"].AsIPEndPoint() : new IPEndPoint(remoteIP, 3868);
        var localEndpoint = localIP is null ? configuration["Local"].AsIPEndPoint() : new IPEndPoint(localIP, 3868);

        var ceNodes = configuration.FindNode("Capability-Exchange")?.Nodes ?? [];
        var dwNodes = configuration.FindNode("Diameter-Watchdog")?.Nodes ?? [];

        if ((remoteEndpoint is not null) && (localEndpoint is null)) {
            DiameterThread = new DiameterClientThread(remoteEndpoint, ceNodes, dwNodes);
        } else if ((remoteEndpoint is null) && (localEndpoint is not null)) {
            DiameterThread = new DiameterServerThread(localEndpoint, ceNodes, dwNodes);
        } else {
            throw new InvalidOperationException("Either remote or local endpoint must be specified.");
        }
    }


    private readonly IDiameterThread DiameterThread;

    #region IDisposable

    public void Dispose() {
        DiameterThread.Stop();
    }

    #endregion IDisposable


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
