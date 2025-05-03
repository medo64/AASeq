namespace AASeq.Plugins.Standard;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

/// <summary>
/// Replies to any request with the same data.
/// It will respond with the same data as in the request.
/// </summary>
[DebuggerDisplay("Echo")]
internal sealed class Echo : IEndpointPlugin {

    private Echo(ILogger logger, AASeqNodes configuration) {
        Logger = logger;
        Delay = configuration["Delay"].AsTimeSpan(DefaultDelay);
    }


    private readonly ILogger Logger;
    private readonly TimeSpan Delay;
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
    public async Task SendAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        Logger.LogTrace($"Storing message {id}");
        Storage[id] = (messageName, parameters);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Receives message from the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        Logger.LogTrace($"Retrieving message {id}");
        if (Storage.Remove(id, out var value)) {
            messageName = value.Item1;
            var data = value.Item2;
            if (Delay.Ticks > 0) {
                await Task.Delay(Delay, cancellationToken).ConfigureAwait(false);
            }
            return new Tuple<string, AASeqNodes>(messageName, data);
        }

        throw new InvalidOperationException("Message not received.");
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(ILogger logger, AASeqNodes configuration) {
        return new Echo(logger, configuration);
    }


    private static readonly TimeSpan DefaultDelay = TimeSpan.Zero;

}
