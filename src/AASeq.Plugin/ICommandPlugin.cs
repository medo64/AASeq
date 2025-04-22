namespace AASeq;

/// <summary>
/// Command plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface ICommandPlugin : ICommandPluginInstance {

    /// <summary>
    /// Returns the instance.
    /// </summary>
    public static abstract ICommandPlugin CreateInstance();

}
