namespace AASeq;

/// <summary>
/// Path and field pair.
/// </summary>
public sealed class AAFieldNode {

    internal AAFieldNode(string path, AAField field) {
        Path = path;
        Field = field;
    }


    /// <summary>
    /// Gets path to a field.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Gets field.
    /// </summary>
    public AAField Field { get; }


    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    public override string ToString() {
        return Path;
    }

}
