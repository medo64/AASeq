namespace AASeq.EndpointPlugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

/// <summary>
/// Replies to any request with the same data.
/// It will respond with the same data as in the request.
/// </summary>
[DebuggerDisplay("Echo")]
internal sealed class Echo : IEndpointPlugin {

    private Echo(AASeqNodes configuration) {
        DelayMS = (int)configuration.GetValue("Delay", TimeSpan.Zero).TotalMilliseconds;
    }


    private readonly int DelayMS;
    private readonly Dictionary<Guid, (string, AASeqNodes)> Storage = [];


    /// <summary>
    /// Sends the message as returns ID for the answer.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public Guid Send(string messageName, AASeqNodes data) {
        var guid = Guid.NewGuid();
        Storage.Add(guid, (messageName, data));
        return guid;
    }

    /// <summary>
    /// Returns the received message.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    public AASeqNodes Receive(Guid id, out string messageName) {
        if (Storage.TryGetValue(id, out var value)) {
            Storage.Remove(id);
            messageName = value.Item1;
            var data = value.Item2;
            if (DelayMS > 0) { Thread.Sleep(DelayMS); }
            return data;
        }
        messageName = string.Empty;
        return AASeqNodes.Empty;
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin GetInstance(AASeqNodes configuration) {
        return new Echo(configuration);
    }

}
