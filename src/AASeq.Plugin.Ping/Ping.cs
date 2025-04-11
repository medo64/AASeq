namespace AASeq.EndpointPlugins;
using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Ping endpoint.
/// </summary>
internal sealed class Ping : IEndpointPlugin {

    private Ping(AASeqNodes _) {
        //TODO
    }


    /// <summary>
    /// Returns instance configuration.
    /// </summary>
    public AASeqNodes GetConfiguration() {
        var nodes = new AASeqNodes {
        };
        return nodes;
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
    public static IEndpointPlugin GetInstance(AASeqNodes configuration) {
        return new Ping(configuration);
    }

}
