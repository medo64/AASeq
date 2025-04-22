namespace AASeq.Plugins.Standard;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Replies to any request with the same data.
/// It will respond with the same data as in the request.
/// </summary>
[DebuggerDisplay("Echo")]
internal sealed class Echo : IEndpointPlugin {

    private Echo(AASeqNodes configuration) {
        Delay = configuration["Delay"].AsTimeSpan(DefaultDelay);
    }


    private readonly TimeSpan Delay;
    private readonly ConcurrentDictionary<Guid, (string, AASeqNodes)> Storage = [];


    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task SendAsync(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        Storage[id] = (messageName, data);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, CancellationToken cancellationToken) {
        if (Storage.Remove(id, out var value)) {
            messageName = value.Item1;
            var data = value.Item2;
            if (Delay.Ticks > 0) {
                await Task.Delay(Delay, cancellationToken).ConfigureAwait(false);
            }
            await Task.FromResult(new Tuple<string, AASeqNodes>(messageName, data)).ConfigureAwait(false);
        }

        throw new InvalidOperationException("Message not received.");
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(AASeqNodes configuration) {
        return new Echo(configuration);
    }

    /// <summary>
    /// Returns validated configuration.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    public static AASeqNodes ValidateConfiguration(AASeqNodes configuration) {
        return [
            configuration.FindNode("Delay")?? new AASeqNode("Delay", DefaultDelay),
        ];
    }

    /// <summary>
    /// Returns validated data.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="data">Data.</param>
    public static AASeqNodes ValidateData(string message, AASeqNodes data) {
        return data;
    }


    private static readonly TimeSpan DefaultDelay = TimeSpan.Zero;

}
