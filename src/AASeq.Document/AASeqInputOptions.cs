namespace AASeq;

/// <summary>
/// Options for input.
/// </summary>
public sealed record AASeqInputOptions {

    /// <summary>
    /// If true, dupplicate values will not overwrite previously defined values.
    /// </summary>
    public bool NoDuplicatesValues { get; init; }

    /// <summary>
    /// If true, dupplicate properties will not overwrite previously defined property value.
    /// </summary>
    public bool NoDuplicatesProperties { get; init; }

    /// <summary>
    /// If true, number quoting will be enforced, even on load.
    /// </summary>
    public bool StrictNumberQuoting { get; init; }


    /// <summary>
    /// Default output options.
    /// </summary>
    public static AASeqInputOptions Permissive => new();

    /// <summary>
    /// Compact output representation.
    /// </summary>
    public static AASeqInputOptions Strict => new() { NoDuplicatesProperties = true, NoDuplicatesValues = true, StrictNumberQuoting = true };

}
