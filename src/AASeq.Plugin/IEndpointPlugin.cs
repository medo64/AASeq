namespace AASeq;
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
    /// Sends the message.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    public void Send(string messageName, AASeqNodes data);

    /// <summary>
    /// Receives the message.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="expectedData">Expected data. Only used by dummy protocols.</param>
    public AASeqNodes Receive(string messageName, AASeqNodes expectedData);

}
