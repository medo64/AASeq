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
    public void Send(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        var pingOptions = new PingOptions {
            DontFragment = data["DontFragment"].AsBoolean(DontFragment),
            Ttl = data["TTL"].AsInt32(TimeToLive),
        };

        var timeout = data["Timeout"].AsTimeSpan(Timeout);
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
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public void Receive(Guid id, out string messageName, out AASeqNodes data, CancellationToken cancellationToken) {
        while (!cancellationToken.IsCancellationRequested) {
            if (Storage.Remove(id, out var value)) {
                messageName = value.Item1;
                data = value.Item2;
                return;
            } else {
                Thread.Sleep(1);
            }
        }
        throw new InvalidOperationException("No reply.");
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(AASeqNodes configuration) {
        return new Ping(configuration);
    }

    /// <summary>
    /// Returns validated configuration.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    public static AASeqNodes ValidateConfiguration(AASeqNodes configuration) {
        return [
            configuration.FindNode("DontFragment") ?? new AASeqNode("DontFragment", DefaultDontFragment),
            configuration.FindNode("Host") ?? new AASeqNode("Host", DefaultHost),
            configuration.FindNode("Timeout") ?? new AASeqNode("Timeout", DefaultTimeout),
            configuration.FindNode("TTL") ?? configuration.FindNode("TimeToLive") ?? new AASeqNode("TTL", DefaultTTL),
        ];
    }

    /// <summary>
    /// Returns validated data.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="data">Data.</param>
    public static AASeqNodes ValidateData(string message, AASeqNodes data) {
        return message switch {
            "Send" => [
                        data.FindNode("DontFragment"),
                        data.FindNode("Timeout"),
                        data.FindNode("TTL") ?? data.FindNode("TimeToLive"),
                      ],
            "Reply" => [
                        data.FindNode("Status"),
                        data.FindNode("RoundtripTime"),
                      ],
            _ => throw new ArgumentOutOfRangeException(nameof(message), $"Unknown message: {message}"),
        };
    }

    private const bool DefaultDontFragment = false;
    private const string DefaultHost = "localhost";
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(1);
    private const int DefaultTTL = 64;

}
