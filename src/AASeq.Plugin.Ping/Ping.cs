namespace AASeq.EndpointPlugins;
using System;

/// <summary>
/// Ping endpoint.
/// </summary>
internal sealed class Ping : IEndpointPlugin {

    private Ping(AASeqNodes configuration) {
    }


    /// <summary>
    /// Sends the message.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public void Send(string messageName, AASeqNodes data) {
        // TODO
    }

    /// <summary>
    /// Receives the message.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="expectedData">Expected data.</param>
    public AASeqNodes Receive(string messageName, AASeqNodes expectedData) {
        // TODO
        throw new NotSupportedException();
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin GetInstance(AASeqNodes configuration) {
        return new Ping(configuration);
    }

}
