namespace AASeq;

/// <summary>
/// Endpoint plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface IEndpointPlugin : IEndpointPluginInstance {

    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static abstract IEndpointPlugin CreateInstance(AASeqNodes configuration);

    /// <summary>
    /// Returns validated configuration.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    public static abstract AASeqNodes ValidateConfiguration(AASeqNodes configuration);

    /// <summary>
    /// Returns validated data.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="data">Data.</param>
    public static abstract AASeqNodes ValidateData(string message, AASeqNodes data);

}
