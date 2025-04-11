namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Endpoint plugin instance interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface IEndpointPluginInstance {

    /// <summary>
    /// Returns instance configuration.
    /// </summary>
    public AASeqNodes GetConfiguration();


    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public bool TrySend(Guid id, string messageName, AASeqNodes data);

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public bool TryReceive(Guid id, [MaybeNullWhen(false)] out string messageName, [MaybeNullWhen(false)] out AASeqNodes data);

}
