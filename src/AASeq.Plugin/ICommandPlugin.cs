namespace AASeq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

/// <summary>
/// Command plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface ICommandPlugin {

    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static abstract ICommandPlugin GetInstance();


    /// <summary>
    /// Returns true, if command was successfully executed.
    /// </summary>
    /// <param name="data">Data.</param>
    public bool TryExecute(AASeqNodes data);

}
