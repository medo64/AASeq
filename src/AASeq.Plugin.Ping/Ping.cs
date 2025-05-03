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
        return new Ping(configuration);
    }


    private Ping(AASeqNodes configuration) {
        DontFragment = configuration["DontFragment"].AsBoolean(DefaultDontFragment);
        Host = configuration["Host"].AsString(DefaultHost);
        Timeout = configuration["Timeout"].AsTimeSpan(DefaultTimeout);
        TimeToLive = configuration["TTL"].AsInt32(configuration["TimeToLive"].AsInt32(DefaultTTL));
    }

    private readonly bool DontFragment;
    private readonly string Host;
    private readonly TimeSpan Timeout;
    private readonly int TimeToLive;

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
        var pingOptions = new PingOptions {
            DontFragment = parameters["DontFragment"].AsBoolean(DontFragment),
            Ttl = parameters["TTL"].AsInt32(TimeToLive),
        };

        var timeout = parameters["Timeout"].AsTimeSpan(Timeout);

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
        return new AASeqNodes {
            new AASeqNode("DontFragment", pingOptions.DontFragment),
            new AASeqNode("TTL", pingOptions.Ttl),
            new AASeqNode("Timeout", timeout),
        };
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


    private const bool DefaultDontFragment = false;
    private const string DefaultHost = "localhost";
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(1);
    private const int DefaultTTL = 64;

}
