namespace AASeq.Plugins.Standard;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Wait plugin command.
/// </summary>
internal sealed class Wait : ICommandPlugin {

    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static ICommandPlugin CreateInstance() {
        return new Wait();
    }


    /// <summary>
    /// Creates a new instance.
    /// </summary>
    private Wait() {
    }


    /// <summary>
    /// Returns true, if command was successfully executed.
    /// </summary>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task ExecuteAsync(AASeqNodes data, CancellationToken cancellationToken) {
        var duration = data["Delay"].AsTimeSpan(DefaultDelay);
        await Task.Delay(duration, cancellationToken).ConfigureAwait(false);
    }


    private static readonly TimeSpan DefaultDelay = TimeSpan.FromMilliseconds(1000);

}
