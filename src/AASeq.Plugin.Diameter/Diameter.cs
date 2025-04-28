namespace AASeqPlugin;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AASeq;
using Microsoft.Extensions.Logging;

/// <summary>
/// Diameter endpoint.
/// </summary>
internal sealed class Diameter : IEndpointPlugin, IDisposable {

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(ILogger logger, AASeqNodes configuration) {
        return new Diameter(logger, configuration);
    }


    private Diameter(ILogger logger, AASeqNodes configuration) {
        var remoteIP = configuration["Remote"].AsIPAddress();
        var localIP = configuration["Local"].AsIPAddress();

        var remoteEndpoint = remoteIP is null ? configuration["Remote"].AsIPEndPoint() : new IPEndPoint(remoteIP, 3868);
        var localEndpoint = localIP is null ? configuration["Local"].AsIPEndPoint() : new IPEndPoint(localIP, 3868);

        if ((remoteEndpoint is not null) && (localEndpoint is null)) {
            var cerNodes = configuration.FindNode("Capability-Exchange-Request")?.Nodes ?? [];
            var dwrNodes = configuration.FindNode("Diameter-Watchdog-Request")?.Nodes ?? [];
            DiameterThread = new DiameterClientThread(logger, remoteEndpoint, cerNodes, dwrNodes);
        } else if ((remoteEndpoint is null) && (localEndpoint is not null)) {
            var ceaNodes = configuration.FindNode("Capability-Exchange-Answer")?.Nodes ?? [];
            var dwrNodes = configuration.FindNode("Diameter-Watchdog-Request")?.Nodes ?? [];
            DiameterThread = new DiameterServerThread(logger, localEndpoint, ceaNodes, dwrNodes);
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
