namespace AASeq.Plugins.Standard;
using System;
using System.Threading;

/// <summary>
/// Wait plugin command.
/// </summary>
internal sealed class Wait : ICommandPlugin {

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
    public bool TryExecute(AASeqNodes data, CancellationToken cancellationToken) {
        var duration = data.GetValue("Value", TimeSpan.FromMilliseconds(1000));
        Thread.Sleep((int)duration.TotalMilliseconds);
        return true;
    }


    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static ICommandPlugin GetInstance() {
        return new Wait();
    }

}
