namespace AASeq;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Command plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface ICommandPlugin {

    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static abstract ICommandPlugin GetInstance(AASeqNodes configuration);


    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="data">Data.</param>
    public void Execute(AASeqNodes data);

}
