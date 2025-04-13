namespace AASeq.Plugins.Standard;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        var duration = data.GetValue("Delay", DefaultDelay);
        Task.Delay(duration, cancellationToken).Wait(cancellationToken);
        return true;
    }


    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static ICommandPlugin CreateInstance() {
        return new Wait();
    }

    /// <summary>
    /// Returns validated data.
    /// </summary>
    /// <param name="data">Data.</param>
    public static AASeqNodes ValidateData(AASeqNodes data) {
        return [
            new AASeqNode("Delay", data.GetValue("Delay", data.GetValue("Value", DefaultDelay))),
        ];
    }


    private static readonly TimeSpan DefaultDelay = TimeSpan.FromMilliseconds(1000);

}
