namespace AASeq;
using System;

/// <summary>
/// Options for text output.
/// </summary>
public sealed record AASeqOutputOptions {

    /// <summary>
    /// If true, new lines will be avoided when possible.
    /// </summary>
    public bool CompactRepresentation { get; set; }

    /// <summary>
    /// If true, extra empty line is written between main nodes.
    /// </summary>
    public bool ExtraEmptyRootNodeLines { get; set; }

    /// <summary>
    /// Gets/sets executable name for output (hash-bang).
    /// </summary>
    public string? HeaderExecutable { get; set; }

    private string _newLine = Environment.NewLine;
    /// <summary>
    /// Gets/sets the new line character.
    /// </summary>
    public string NewLine {
        get { return _newLine; }
        set {
            if (_newLine.Equals("\n", StringComparison.Ordinal)
                || _newLine.Equals("\r\n", StringComparison.Ordinal)
                || _newLine.Equals("\r", StringComparison.Ordinal)) {
                _newLine = value;
            } else {
                throw new ArgumentOutOfRangeException(nameof(value), "New line must be either \\n, \\r\\n, or \\r.");
            }
        }
    }

    /// <summary>
    /// If true, type annotation be omitted.
    /// </summary>
    public bool NoTypeAnnotation { get; set; }

    /// <summary>
    /// If true, the last new line character will be omitted.
    /// </summary>
    public bool SkipLastNewLine { get; set; }

    /// <summary>
    /// Default output options.
    /// </summary>
    public static AASeqOutputOptions Default => new();

    /// <summary>
    /// Compact output representation.
    /// </summary>
    public static AASeqOutputOptions Compact => new() { CompactRepresentation = true };

}
