namespace AASeq;
using Microsoft.Extensions.Logging;

/// <summary>
/// Endpoint plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface IEndpointPlugin : IEndpointPluginInstance {

    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static abstract IEndpointPlugin CreateInstance(ILogger logger, AASeqNodes configuration);

}
