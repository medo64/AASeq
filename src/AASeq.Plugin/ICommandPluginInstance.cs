namespace AASeq;

/// <summary>
/// Command plugin instance interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface ICommandPluginInstance {

    /// <summary>
    /// Returns true, if command was successfully executed.
    /// </summary>
    /// <param name="data">Data.</param>
    public bool TryExecute(AASeqNodes data);

}
