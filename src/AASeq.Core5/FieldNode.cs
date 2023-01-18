using System;

namespace AASeq;

/// <summary>
/// Path and field pair.
/// </summary>
public class FieldNode {

    internal FieldNode(string path, Field field) {
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
    public Field Field { get; }


    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    public override string ToString() {
        return Path;
    }

}
