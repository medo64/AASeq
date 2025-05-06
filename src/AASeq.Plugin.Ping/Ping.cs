namespace AASeqPlugin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AASeq;

/// <summary>
/// Ping endpoint.
/// </summary>
internal sealed class Ping : IEndpointPlugin {

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(ILogger logger, AASeqNodes configuration) {
        return new Ping(logger, configuration);
    }


    private Ping(ILogger logger, AASeqNodes configuration) {
        Logger = logger;

        if (configuration.TryConsumeNode("DontFragment", out var dfNode)) {
            if (dfNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{dfNode.Name}'."); }
            var dfValue = dfNode.Value.AsBoolean();
            if (dfValue != null) {
                DontFragment = dfValue.Value;
            } else {
                logger.LogWarning($"Cannot convert 'DontFragment' value.");
            }
        }
        if (configuration.TryConsumeNode("Host", out var hostNode)) {
            if (hostNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{hostNode.Name}'."); }
            var hostValue = hostNode.Value.AsString();
            if (!string.IsNullOrWhiteSpace(hostValue)) {
                Host = hostValue;
            } else {
                logger.LogWarning($"Cannot convert 'Host' value.");
            }
        }
        if (configuration.TryConsumeNode("Timeout", out var timeoutNode)) {
            if (timeoutNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{timeoutNode.Name}'."); }
            var timeoutValue = timeoutNode.Value.AsTimeSpan();
            if ((timeoutValue != null) && (timeoutValue.Value.TotalSeconds > 0)) {
                Timeout = timeoutValue.Value;
            } else {
                logger.LogWarning($"Cannot convert 'Timeout' value.");
            }
        }
        if (configuration.TryConsumeNode("TTL", out var ttlNode)) {
            if (ttlNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{ttlNode.Name}'."); }
            var ttlValue = ttlNode.Value.AsInt32();
            if ((ttlValue != null) && (ttlValue.Value is > 0 and < 255)) {
                TimeToLive = ttlValue.Value;
            } else {
                logger.LogWarning($"Cannot convert 'TTL' value.");
            }
        }

        if (configuration.Count > 0) { logger.LogWarning($"Unrecognized configuration node '{configuration[0].Name}'."); }
    }

    private readonly ILogger Logger;
    private readonly bool DontFragment = false;
    private readonly string Host = "localhost";
    private readonly TimeSpan Timeout = TimeSpan.FromSeconds(1);
    private readonly int TimeToLive = 64;

    private readonly ConcurrentDictionary<Guid, (string, AASeqNodes)> Storage = [];


    /// <summary>
    /// Starts the endpoint.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task StartAsync(CancellationToken cancellationToken) {
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
        var dontFragment = DontFragment;
        if (parameters.TryConsumeNode("DontFragment", out var dfNode)) {
            if (dfNode.Properties.Count > 0) { Logger.LogWarning($"Unrecognized properties on '{dfNode.Name}'."); }
            var dfValue = dfNode.Value.AsBoolean();
            if (dfValue != null) {
                dontFragment = dfValue.Value;
            } else {
                Logger.LogWarning($"Cannot convert 'DontFragment' value.");
            }
        }

        var timeout = Timeout;
        if (parameters.TryConsumeNode("Timeout", out var timeoutNode)) {
            if (timeoutNode.Properties.Count > 0) { Logger.LogWarning($"Unrecognized properties on '{timeoutNode.Name}'."); }
            var timeoutValue = timeoutNode.Value.AsTimeSpan();
            if ((timeoutValue != null) && (timeoutValue.Value.TotalSeconds > 0)) {
                timeout = timeoutValue.Value;
            } else {
                Logger.LogWarning($"Cannot convert 'Timeout' value.");
            }
        }

        var ttl = TimeToLive;
        if (parameters.TryConsumeNode("TTL", out var ttlNode)) {
            if (ttlNode.Properties.Count > 0) { Logger.LogWarning($"Unrecognized properties on '{ttlNode.Name}'."); }
            var ttlValue = ttlNode.Value.AsInt32();
            if ((ttlValue != null) && (ttlValue.Value is > 0 and < 255)) {
                ttl = ttlValue.Value;
            } else {
                Logger.LogWarning($"Cannot convert 'TTL' value.");
            }
        }

        if (parameters.Count > 0) { Logger.LogWarning($"Unrecognized node '{parameters[0].Name}'."); }


        var pingOptions = new PingOptions {
            DontFragment = dontFragment,
            Ttl = ttl,
        };

#pragma warning disable CS4014
        Task.Run(() => {
            using var ping = new System.Net.NetworkInformation.Ping();
            var reply = ping.Send(Host, timeout, null, pingOptions);
            var data = new AASeqNodes {
                new AASeqNode("Address", reply.Address.ToString()),
                new AASeqNode("Status", reply.Status.ToString()),
            };
            if (reply.Status == IPStatus.Success) {
                data.Add(new AASeqNode("RoundtripTime", TimeSpan.FromMilliseconds(reply.RoundtripTime)));
            }
            Storage[id] = ("Reply", data);
        }, CancellationToken.None);
#pragma warning restore CS4014

        await Task.CompletedTask.ConfigureAwait(false);

        var result = new AASeqNodes();
        if (pingOptions.DontFragment != DontFragment) { result.Add(new AASeqNode("DontFragment", pingOptions.DontFragment)); }
        if (pingOptions.Ttl != TimeToLive) { result.Add(new AASeqNode("TTL", pingOptions.Ttl)); }
        if (timeout != Timeout) { result.Add(new AASeqNode("Timeout", timeout)); }
        return result;
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

}
