namespace AASeq.EndpointPlugins;
using System;
using System.Diagnostics;

/// <summary>
/// Default local endpoint.
/// It's special so don't mind missing IEndpointPlugin.
/// </summary>
[DebuggerDisplay("Me")]
internal sealed class Me : IEndpointPlugin {

    internal Me() {
    }


    /// <summary>
    /// Sends the message as returns ID for the answer.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public Guid Send(string messageName, AASeqNodes data) {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Returns the received message.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    public AASeqNodes Receive(Guid id, out string messageName) {
        throw new NotSupportedException();
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin GetInstance(AASeqNodes configuration) {
        return new Me();
    }

}
