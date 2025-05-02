namespace AASeqPlugin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AASeq;

/// <summary>
/// Diameter endpoint.
/// </summary>
internal sealed class Diameter : IEndpointPlugin, IDisposable {

    private Diameter(ILogger logger, AASeqNodes configuration) {
        Logger = logger;
        var remoteIP = configuration["Remote"].AsIPAddress();
        var localIP = configuration["Local"].AsIPAddress();

        var remoteEndpoint = remoteIP is null ? configuration["Remote"].AsIPEndPoint() : new IPEndPoint(remoteIP, 3868);
        var localEndpoint = localIP is null ? configuration["Local"].AsIPEndPoint() : new IPEndPoint(localIP, 3868);

        if ((remoteEndpoint is not null) && (localEndpoint is null)) {
            var cerNodes = configuration.FindNode("Capability-Exchange-Request")?.Nodes ?? [];
            var dwrNodes = configuration.FindNode("Diameter-Watchdog-Request")?.Nodes ?? [];
            DiameterThread = new DiameterClientThread(this, logger, remoteEndpoint, cerNodes, dwrNodes);
        } else if ((remoteEndpoint is null) && (localEndpoint is not null)) {
            var ceaNodes = configuration.FindNode("Capability-Exchange-Answer")?.Nodes ?? [];
            var dwrNodes = configuration.FindNode("Diameter-Watchdog-Request")?.Nodes ?? [];
            DiameterThread = new DiameterServerThread(this, logger, localEndpoint, ceaNodes, dwrNodes);
        } else {
            throw new InvalidOperationException("Either remote or local endpoint must be specified.");
        }
    }


    private readonly ILogger Logger;
    private readonly IDiameterThread DiameterThread;

    /// <summary>
    /// Gets/sets the diameter stream.
    /// </summary>
    internal DiameterStream? DiameterStream { get; set; }

    internal readonly ConcurrentDictionary<(uint, uint), Guid> StorageAwaiting = [];
    internal readonly ConcurrentDictionary<Guid, (string, AASeqNodes)> Storage = [];


    #region IDisposable

    public void Dispose() {
        DiameterThread.Stop();
    }

    #endregion IDisposable


    /// <summary>
    /// Starts the endpoint.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task StartAsync(CancellationToken cancellationToken) {
        Logger.LogTrace($"Starting Diameter endpoint @ {DiameterThread.Endpoint}");
        DiameterThread.Start(cancellationToken);
        Logger.LogTrace($"Started Diameter endpoint @ {DiameterThread.Endpoint}");
        await Task.CompletedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Sends message to the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task SendAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        while (DiameterStream is null) {
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
        var message = DiameterEncoder.Encode(messageName, parameters);
        StorageAwaiting[(message.HopByHopIdentifier, message.EndToEndIdentifier)] = id;
        DiameterStream.WriteMessage(message);
    }

    /// <summary>
    /// Receives message from the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        while (!cancellationToken.IsCancellationRequested) {
            if (Storage.Remove(id, out var value)) {
                messageName = value.Item1;
                var data = value.Item2;
                return await Task.FromResult(new Tuple<string, AASeqNodes>(messageName, data)).ConfigureAwait(false);
            } else {
                await Task.Delay(1, cancellationToken).ConfigureAwait(false);
            }
        }
        throw new InvalidOperationException("No reply.");
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(ILogger logger, AASeqNodes configuration) {
        return new Diameter(logger, configuration);
    }

}
