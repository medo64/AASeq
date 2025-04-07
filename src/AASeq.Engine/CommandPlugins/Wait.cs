namespace AASeq.CommandPlugins;
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
    /// Executes the command.
    /// </summary>
    /// <param name="data">Data.</param>
    public void Execute(AASeqNode data) {
        var duration = data.AsTimeSpan(TimeSpan.FromMilliseconds(1000));
        Thread.Sleep((int)duration.TotalMilliseconds);
    }


    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static ICommandPlugin GetInstance() {
        return new Wait();
    }

}
