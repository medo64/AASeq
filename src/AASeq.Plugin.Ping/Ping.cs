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

    private Ping(ILogger logger, AASeqNodes configuration) {
        Logger = logger;

        if (configuration.TryConsumeNode("DontFragment", out var dfNode)) {
            if (dfNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{dfNode.Name}'."); }
            var dfValue = dfNode.Value.AsBoolean();
            if (dfValue != null) {
                DontFragment = dfValue.Value;
            } else {
                throw new InvalidOperationException($"Cannot convert 'DontFragment' value.");
            }
        }
        if (configuration.TryConsumeNode("Host", out var hostNode)) {
            if (hostNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{hostNode.Name}'."); }
            var hostValue = hostNode.Value.AsString();
            if (!string.IsNullOrWhiteSpace(hostValue)) {
                Host = hostValue;
            } else {
                throw new InvalidOperationException($"Cannot convert 'Host' value.");
            }
        }
        if (configuration.TryConsumeNode("Timeout", out var timeoutNode)) {
            if (timeoutNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{timeoutNode.Name}'."); }
            var timeoutValue = timeoutNode.Value.AsTimeSpan();
            if ((timeoutValue != null) && (timeoutValue.Value.TotalSeconds > 0)) {
                Timeout = timeoutValue.Value;
            } else {
                throw new InvalidOperationException($"Cannot convert 'Timeout' value.");
            }
        }
        if (configuration.TryConsumeNode("TTL", out var ttlNode)) {
            if (ttlNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{ttlNode.Name}'."); }
            var ttlValue = ttlNode.Value.AsInt32();
            if ((ttlValue != null) && (ttlValue.Value is > 0 and < 255)) {
                TimeToLive = ttlValue.Value;
            } else {
                throw new InvalidOperationException($"Cannot convert 'TTL' value.");
            }
        }

        if (configuration.Count > 0) { logger.LogWarning($"Unrecognized configuration node '{configuration[0].Name}'."); }
    }

    private readonly ILogger Logger;
    private readonly ConcurrentDictionary<Guid, Task<AASeqNodes>> Storage = [];

    private readonly bool DontFragment = false;
    private readonly string Host = "localhost";
    private readonly TimeSpan Timeout = TimeSpan.FromSeconds(1);
    private readonly int TimeToLive = 64;


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
        await Task.CompletedTask;  // just to keep the compiler happy

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
        var task = Task<AASeqNodes>.Run(() => {
            using var ping = new System.Net.NetworkInformation.Ping();
            var reply = ping.Send(Host, timeout, null, pingOptions);
            var data = new AASeqNodes {
                new AASeqNode("Address", reply.Address.ToString()),
                new AASeqNode("Status", reply.Status.ToString()),
            };
            if (reply.Status == IPStatus.Success) {
                data.Add(new AASeqNode("RoundtripTime", TimeSpan.FromMilliseconds(reply.RoundtripTime)));
            }
            return data;
        }, CancellationToken.None);
        Storage[id] = task;
#pragma warning restore CS4014

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
        if (!messageName.Equals("Reply", StringComparison.OrdinalIgnoreCase)) { throw new InvalidOperationException("Unknown expected message name."); }

        if (Storage.Remove(id, out var task)) {
            var nodes = await task.WaitAsync(cancellationToken).ConfigureAwait(false);
            return new Tuple<string, AASeqNodes>("Reply", nodes);
        }
        throw new InvalidOperationException("No reply.");
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(ILogger logger, AASeqNodes configuration) {
        return new Ping(logger, configuration);
    }

}
