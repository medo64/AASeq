namespace AASeq.EndpointPlugins;
using System;
using System.Diagnostics;

/// <summary>
/// Empty endpoint.
/// It will respond with the same data as in the request.
/// </summary>
[DebuggerDisplay("Dummy")]
internal sealed class Dummy : IEndpointPlugin {

    private Dummy() {
    }


    /// <summary>
    /// Sends the message as returns ID for the answer.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public Guid Send(string messageName, AASeqNodes data) {
        return Guid.Empty;
    }

    /// <summary>
    /// Returns the received message.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    public AASeqNodes Receive(Guid id, out string messageName) {
        messageName = string.Empty;
        return AASeqNodes.Empty;
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin GetInstance(AASeqNodes configuration) {
        return new Dummy();
    }

}
