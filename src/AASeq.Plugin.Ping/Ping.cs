namespace AASeq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Ping endpoint.
/// </summary>
internal sealed class Ping : IEndpointPlugin {

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(AASeqNodes configuration) {
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
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task SendAsync(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        var pingOptions = new PingOptions {
            DontFragment = data["DontFragment"].AsBoolean(DontFragment),
            Ttl = data["TTL"].AsInt32(TimeToLive),
        };

        var timeout = data["Timeout"].AsTimeSpan(Timeout);

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
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, CancellationToken cancellationToken) {
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
