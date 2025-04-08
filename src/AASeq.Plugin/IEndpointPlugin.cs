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
    /// Sends the message as returns ID for the answer.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public Guid Send(string messageName, AASeqNodes data);

    /// <summary>
    /// Returns the received message.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    public AASeqNodes Receive(Guid id, out string messageName);

}
