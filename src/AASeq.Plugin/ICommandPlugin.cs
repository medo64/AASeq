namespace AASeq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

/// <summary>
/// Command plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface ICommandPlugin {

    /// <summary>
    /// Executes the command and returns the executed nodes.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public static abstract Task<AASeqNodes> ExecuteAsync(ILogger logger, AASeqNodes parameters, CancellationToken cancellationToken);

}
