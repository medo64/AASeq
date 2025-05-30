namespace AASeq;
using System;

/// <summary>
/// Options for text output.
/// </summary>
public sealed record AASeqOutputOptions {

    /// <summary>
    /// ANSI color number to use for Node name.
    /// </summary>
    public int? AnsiColorName { get; init; }

    /// <summary>
    /// ANSI color number to use for Node property name.
    /// </summary>
    public int? AnsiColorPropertyName { get; init; }

    /// <summary>
    /// ANSI color number to use for Node property value.
    /// </summary>
    public int? AnsiColorPropertyValue { get; init; }

    /// <summary>
    /// ANSI color number to use for symbols (i.e., equals, braces, and semicolon).
    /// </summary>
    public int? AnsiColorSymbols { get; init; }

    /// <summary>
    /// ANSI color number to use for Node type annotation.
    /// </summary>
    public int? AnsiColorType { get; init; }

    /// <summary>
    /// ANSI color number to use for Node value.
    /// </summary>
    public int? AnsiColorValue { get; init; }


    /// <summary>
    /// If true, new lines will be avoided when possible.
    /// </summary>
    public bool CompactRepresentation { get; init; }

    /// <summary>
    /// If true, extra empty line is written between main nodes.
    /// </summary>
    public bool ExtraEmptyRootNodeLines { get; init; }

    /// <summary>
    /// Gets/sets executable name for output (hash-bang).
    /// </summary>
    public string? HeaderExecutable { get; init; }

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
    public bool NoTypeAnnotation { get; init; }

    /// <summary>
    /// If true, the last new line character will be omitted.
    /// </summary>
    public bool SkipLastNewLine { get; init; }


    /// <summary>
    /// Default output options.
    /// </summary>
    public static AASeqOutputOptions Default => new();

    /// <summary>
    /// Compact output representation.
    /// </summary>
    public static AASeqOutputOptions Compact => new() { CompactRepresentation = true };

}
