namespace AASeq;

/// <summary>
/// Endpoint plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface IEndpointPlugin : IEndpointPluginInstance {

    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static abstract IEndpointPlugin GetInstance(AASeqNodes configuration);

}
