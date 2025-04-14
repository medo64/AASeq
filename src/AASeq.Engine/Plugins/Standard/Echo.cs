namespace AASeq.Plugins.Standard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

/// <summary>
/// Replies to any request with the same data.
/// It will respond with the same data as in the request.
/// </summary>
[DebuggerDisplay("Echo")]
internal sealed class Echo : IEndpointPlugin {

    private Echo(AASeqNodes configuration) {
        DelayMS = (int)configuration.GetValue("Delay", DefaultDelay).TotalMilliseconds;
    }


    private readonly int DelayMS;
    private readonly Dictionary<Guid, (string, AASeqNodes)> Storage = [];


    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public bool TrySend(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        Storage.Add(id, (messageName, data));
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
        if (Storage.TryGetValue(id, out var value)) {
            Storage.Remove(id);
            messageName = value.Item1;
            data = value.Item2;
            if (DelayMS > 0) { Thread.Sleep(DelayMS); }
            return true;
        }

        messageName = null;
        data = null;
        return false;
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
