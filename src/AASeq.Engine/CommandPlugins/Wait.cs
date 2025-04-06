namespace AASeq.CommandPlugins;
using System;

/// <summary>
/// Wait plugin command.
/// </summary>
internal sealed class Wait : ICommandPlugin {

    /// <summary>
    /// Creates a new instance. 
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    private Wait(AASeqNodes configuration) {
    }


    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="data">Data.</param>
    public void Execute(AASeqNodes data) {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static ICommandPlugin GetInstance(AASeqNodes configuration) {
        return new Wait(configuration);
    }

}
