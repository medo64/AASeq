namespace AASeq;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Command plugin instance interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface ICommandPluginInstance {

    /// <summary>
    /// Returns true, if command was successfully executed.
    /// </summary>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task ExecuteAsync(AASeqNodes parameters, CancellationToken cancellationToken);

}
