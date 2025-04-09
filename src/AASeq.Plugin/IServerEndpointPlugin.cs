namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Endpoint plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface IEndpointPlugin {

    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static abstract IEndpointPlugin GetInstance(AASeqNodes configuration);


    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public bool TryServerReceive(Guid id, [MaybeNullWhen(false)] out string messageName, [MaybeNullWhen(false)] out AASeqNodes data);

    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public bool TryServerSend(Guid id, string messageName, AASeqNodes data);

}
