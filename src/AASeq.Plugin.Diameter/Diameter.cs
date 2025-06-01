namespace AASeqPlugin;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AASeq;
using AASeq.Diameter;
using System.Text;
using System.Globalization;

/// <summary>
/// Diameter endpoint.
/// </summary>
internal sealed class Diameter : IEndpointPlugin, IDisposable {

    private Diameter(ILogger logger, AASeqNodes configuration) {
        Logger = logger;

        string? remoteHost = null;
        int remotePort = 3868;
        if (configuration.TryConsumeNode("Remote", out var remoteNode)) {
            if (remoteNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{remoteNode.Name}'."); }
            var parts = remoteNode.Value.AsString("").Split(':', StringSplitOptions.TrimEntries);
            if (parts.Length == 1) {
                remoteHost = parts[0];
            } else if (parts.Length == 2) {
                remoteHost = parts[0];
                if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out remotePort) || (remotePort is < 1 or > 65535)) {
                    throw new InvalidOperationException($"Cannot convert 'Remote' port value.");
                }
            } else {
                throw new InvalidOperationException($"Cannot convert 'Remote' value.");
            }
        }

        string? localHost = null;
        int localPort = 3868;
        if (configuration.TryConsumeNode("Local", out var localNode)) {
            if (localNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{localNode.Name}'."); }
            var parts = localNode.Value.AsString("").Split(':', StringSplitOptions.TrimEntries);
            if (parts.Length == 1) {
                localHost = parts[0];
            } else if (parts.Length == 2) {
                localHost = parts[0];
                if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out localPort) || (localPort is < 1 or > 65535)) {
                    throw new InvalidOperationException($"Cannot convert 'Local' port value.");
                }
            } else {
                throw new InvalidOperationException($"Cannot convert 'Local' value.");
            }
        }

        int watchdogInterval = 30;
        if (configuration.TryConsumeNode("WatchdogInterval", out var watchdogNode)) {
            if (watchdogNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{watchdogNode.Name}'."); }
            var wdInt = watchdogNode.Value.AsInt32(watchdogInterval);
            if (wdInt is >= 5) {
                watchdogInterval = wdInt;
            } else {
                throw new InvalidOperationException($"Cannot convert 'Local' value.");
            }
        }

        var useNagle = false;
        if (configuration.TryConsumeNode("UseNagle", out var useNagleNode)) {
            if (useNagleNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{useNagleNode.Name}'."); }
            useNagle = useNagleNode.Value.AsBoolean(useNagle);
        }

        if ((remoteHost is not null) && (localHost is null)) {
            var cerNodes = configuration.ConsumeNode("Capability-Exchange-Request")?.Nodes ?? [];
            var dwrNodes = configuration.ConsumeNode("Diameter-Watchdog-Request")?.Nodes ?? [];
            DiameterThread = new DiameterClientThread(this, logger, remoteHost, remotePort, cerNodes, dwrNodes, watchdogInterval, useNagle);
        } else if ((remoteHost is null) && (localHost is not null)) {
            var ceaNodes = configuration.ConsumeNode("Capability-Exchange-Answer")?.Nodes ?? [];
            var dwrNodes = configuration.ConsumeNode("Diameter-Watchdog-Request")?.Nodes ?? [];
            DiameterThread = new DiameterServerThread(this, logger, localHost, localPort, ceaNodes, dwrNodes, watchdogInterval, useNagle);
        } else {
            throw new InvalidOperationException("Either remote or local endpoint must be specified.");
        }

        if (configuration.Count > 0) { logger.LogWarning($"Unrecognized configuration node '{configuration[0].Name}'."); }
    }


    private readonly ILogger Logger;
    private readonly IDiameterThread DiameterThread;
    internal long LastMessageTimestamp = Environment.TickCount64;  // interlocked access - used by *Thread class


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
        Logger.LogTrace($"Starting Diameter endpoint");
        DiameterThread.Start(cancellationToken);
        Logger.LogTrace($"Started Diameter endpoint");
        await Task.CompletedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Sends message to the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<AASeqNodes> SendAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        while (DiameterStream is null) {
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
        var message = DiameterEncoder.Encode(messageName, parameters);
        StorageAwaiting[(message.HopByHopIdentifier, message.EndToEndIdentifier)] = id;
        DiameterStream.WriteMessage(message);
        Interlocked.Exchange(ref LastMessageTimestamp, Environment.TickCount64);
        return DiameterEncoder.Decode(message, out _);
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
