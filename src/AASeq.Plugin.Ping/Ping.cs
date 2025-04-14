namespace AASeq.EndpointPlugins;
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
        DontFragment = configuration.GetValue("DontFragment", DefaultDontFragment);
        Host = configuration.GetValue("Host", DefaultHost);
        Timeout = configuration.GetValue("Timeout", DefaultTimeout);
        TimeToLive = configuration.GetValue("TTL", configuration.GetValue("TimeToLive", DefaultTTL));
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
    public bool TrySend(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        var pingOptions = new PingOptions {
            DontFragment = data.GetValue("DontFragment", DontFragment),
            Ttl = data.GetValue("TTL", TimeToLive),
        };

        var timeout = data.GetValue("Timeout", Timeout);
        Task.Run(() => {
            using var ping = new System.Net.NetworkInformation.Ping();
            var reply = ping.Send(Host, timeout, null, pingOptions);
            var data = new AASeqNodes {
                new AASeqNode("Address", reply.Address.ToString()),
                new AASeqNode("Status", reply.Status.ToString()),
                new AASeqNode("RoundtripTime", TimeSpan.FromMilliseconds(reply.RoundtripTime > 0 ? reply.RoundtripTime : 1)),
            };
            Storage[id] = ("Reply", data);
        }, CancellationToken.None);
        return true;
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public bool TryReceive(Guid id, [MaybeNullWhen(false)] out string messageName, [MaybeNullWhen(false)] out AASeqNodes data, CancellationToken cancellationToken) {
        while (!cancellationToken.IsCancellationRequested) {
            if (Storage.Remove(id, out var value)) {
                messageName = value.Item1;
                data = value.Item2;
                return true;
            } else {
                Thread.Sleep(1);
            }
        }

        messageName = null;
        data = null;
        return false;
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
            "Ping" => [
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
