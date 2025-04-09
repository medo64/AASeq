namespace AASeq.EndpointPlugins;
using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Ping endpoint.
/// </summary>
internal sealed class Ping : IClientEndpointPlugin {

    private Ping(AASeqNodes configuration) {
        //TODO
    }


    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public bool TrySend(Guid id, string messageName, AASeqNodes data) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public bool TryReceive(Guid id, [MaybeNullWhen(false)] out string messageName, [MaybeNullWhen(false)] out AASeqNodes data) {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IClientEndpointPlugin GetInstance(AASeqNodes configuration) {
        return new Ping(configuration);
    }

}
