namespace AASeq;
using System;
using System.Globalization;
using System.IO;
using System.Net;

public sealed partial class AASeqNodes : IFormattable {

    /// <summary>
    /// Saves nodes to a stream.
    /// </summary>
    /// <param name="stream">Destination stream that will contain the UTF-8 encoded text.</param>
    public void Save(Stream stream) {
        Save(this, stream);
    }

    /// <summary>
    /// Saves nodes to a stream.
    /// </summary>
    /// <param name="writer">Destination writer.</param>
    public void Save(TextWriter writer) {
        Save(this, writer);
    }

    /// <summary>
    /// Saves nodes to a stream.
    /// </summary>
    /// <param name="stream">Destination stream that will contain the UTF-8 encoded text.</param>
    /// <param name="options">Output options.</param>
    public void Save(Stream stream, AASeqOutputOptions options) {
        Save(this, stream, options);
    }

    /// <summary>
    /// Saves nodes to a stream.
    /// </summary>
    /// <param name="writer">Destination writer.</param>
    /// <param name="options">Output options.</param>
    public void Save(TextWriter writer, AASeqOutputOptions options) {
        Save(this, writer, options);
    }

    /// <summary>
    /// Saves nodes to a file.
    /// </summary>
    /// <param name="filePath">Path to the destination UTF-8 encoded file.</param>
    public void Save(string filePath) {
        Save(this, filePath);
    }

    /// <summary>
    /// Saves nodes to a file.
    /// </summary>
    /// <param name="filePath">Path to the destination UTF-8 encoded file.</param>
    /// <param name="options">Output options.</param>
    public void Save(string filePath, AASeqOutputOptions options) {
        Save(this, filePath, options);
    }


    #region Static Save

    /// <summary>
    /// Saves nodes to a stream.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    /// <param name="stream">Destination stream that will contain the UTF-8 encoded text.</param>
    internal static void Save(AASeqNodes nodes, Stream stream) {
        Save(nodes, stream, AASeqOutputOptions.Default);
    }

    /// <summary>
    /// Saves nodes to a stream.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    /// <param name="writer">Destination writer.</param>
    internal static void Save(AASeqNodes nodes, TextWriter writer) {
        Save(nodes, writer, AASeqOutputOptions.Default);
    }

    /// <summary>
    /// Saves nodes to a stream.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    /// <param name="stream">Destination stream that will contain the UTF-8 encoded text.</param>
    /// <param name="options">Output options.</param>
    internal static void Save(AASeqNodes nodes, Stream stream, AASeqOutputOptions options) {
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentNullException.ThrowIfNull(options);

        stream.SetLength(0);

        using var bufferStream = new BufferedStream(stream, BufferSize);
        using var writerStream = new StreamWriter(bufferStream, Utf8);
        SaveNodes(nodes, writerStream, options, 0);
    }

    /// <summary>
    /// Saves nodes to a stream.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    /// <param name="writer">Destination writer.</param>
    /// <param name="options">Output options.</param>
    internal static void Save(AASeqNodes nodes, TextWriter writer, AASeqOutputOptions options) {
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentNullException.ThrowIfNull(options);

        SaveNodes(nodes, writer, options, 0);
    }

    /// <summary>
    /// Saves nodes to a file.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    /// <param name="filePath">Path to the destination UTF-8 encoded file.</param>
    internal static void Save(AASeqNodes nodes, string filePath) {
        Save(nodes, filePath, AASeqOutputOptions.Default);
    }

    /// <summary>
    /// Saves nodes to a file.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    /// <param name="filePath">Path to the destination UTF-8 encoded file.</param>
    /// <param name="options">Output options.</param>
    internal static void Save(AASeqNodes nodes, string filePath, AASeqOutputOptions options) {
        ArgumentNullException.ThrowIfNull(filePath);
        ArgumentNullException.ThrowIfNull(options);

        using var stream = File.OpenWrite(filePath);
        stream.SetLength(0);
        Save(nodes, stream, options);
    }

    #endregion Static Save


    #region IFormattable

    /// <summary>
    /// Returns text representation of the nodes.
    /// </summary>
    public override string ToString() {
        return ToString(format: null, formatProvider: null);
    }

    /// <summary>
    /// Returns text representation of the nodes.
    /// </summary>
    /// <param name="format">Not used.</param>
    /// <param name="formatProvider">Not used.</param>
    public string ToString(string? format, IFormatProvider? formatProvider) {
        return ToString(this);
    }

    /// <summary>
    /// Returns text representation of the nodes.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    internal static string ToString(AASeqNodes nodes) {
        using var memoryStream = new MemoryStream();
        using (var writerStream = new StreamWriter(memoryStream, Utf8)) {
            SaveNodes(nodes, writerStream, AASeqOutputOptions.Compact, 0);
        }
        return Utf8.GetString(memoryStream.ToArray());
    }

    #endregion IFormattable


    #region Helper

    private static void SaveNodes(AASeqNodes nodes, TextWriter writer, AASeqOutputOptions options, int level) {
        if ((level == 0) && !string.IsNullOrEmpty(options.HeaderExecutable)) {
            writer.Write("#!/usr/bin/env ");
            writer.Write(options.HeaderExecutable);
            writer.Write(options.NewLine);
        }

        for (var i = 0; i < nodes.Count; i++) {
            var extraLine = options.ExtraEmptyRootNodeLines && (level == 0) && ((i > 0) || !string.IsNullOrEmpty(options.HeaderExecutable));
            if (extraLine) { writer.Write(options.NewLine); }

            var node = nodes[i];
            var isLast = i < nodes.Count - 1;
            SaveNode(node, writer, options, level, isLast);
        }
    }

    private static void SaveNode(AASeqNode node, TextWriter writer, AASeqOutputOptions options, int level, bool isLast) {
        var indent = new string(' ', level * 4);
        if ((level > 0) && !options.CompactRepresentation) { writer.Write(indent); }

        writer.Write(node.Name);

        if (node.Value.RawValue is not null) {
            writer.Write(' ');
            if (!options.NoTypeAnnotation) {
                var typeAnnotation = GetTypeAnnotation(node.Value);
                if (typeAnnotation is not null) {
                    writer.Write('(');
                    writer.Write(typeAnnotation);
                    writer.Write(')');
                }
            }
            writer.Write(GetStringFromValue(node.Value));
        }

        if (node.Properties.Count > 0) {
            foreach (var property in node.Properties) {
                writer.Write(' ');
                writer.Write(property.Key);
                writer.Write('=');
                writer.Write(GetPotentiallyQuotedString(property.Value));
            }
        }

        var hasSubnodes = node.Nodes.Count > 0;
        if (hasSubnodes) {
            writer.Write(' ');
            writer.Write('{');
            if (options.CompactRepresentation) {
                writer.Write(' ');
            } else {
                writer.Write(options.NewLine);
            }
            SaveNodes(node.Nodes, writer, options, level + 1);
            if (options.CompactRepresentation) {
                writer.Write(' ');
            }
            if ((level > 0) && !options.CompactRepresentation) { writer.Write(indent); }
            writer.Write('}');
        }

        if (!options.CompactRepresentation) {
            if (level == 0) {
                if (!options.SkipLastNewLine || isLast) {
                    writer.Write(options.NewLine);
                }
            } else {
                writer.Write(options.NewLine);
            }
        } else {  // compact
            if (isLast) {
                if (!hasSubnodes) { writer.Write(';'); }
                writer.Write(' ');
            }
        }
    }

    private static string? GetTypeAnnotation(AASeqValue value) {
        return value.RawValue switch {
            null => null,
            Boolean => null,
            SByte => "i8",
            Byte => "u8",
            Int16 => "i16",
            UInt16 => "u16",
            Int32 => null,
            UInt32 => "u32",
            Int64 => "i64",
            UInt64 => "u64",
            Int128 => "i128",
            UInt128 => "u128",
            Half => "f16",
            Single => "f32",
            Double => null,
            Decimal => "d128",
            DateTimeOffset => "datetime",
            DateOnly => "dateonly",
            TimeOnly => "timeonly",
            TimeSpan => "duration",
            IPAddress => "ip",
            IPEndPoint => "endpoint",
            Uri => "uri",
            Guid => "uuid",
            Byte[] => null,
            String => null,
            _ => throw new InvalidOperationException("Unknown value type."),
        };
    }

    private static string GetStringFromValue(AASeqValue valueObject) {
        return valueObject.RawValue switch {
            null => "null",
            Boolean value => value ? "true" : "false",
            SByte value => value.ToString(CultureInfo.InvariantCulture),
            Byte value => value.ToString(CultureInfo.InvariantCulture),
            Int16 value => value.ToString(CultureInfo.InvariantCulture),
            UInt16 value => value.ToString(CultureInfo.InvariantCulture),
            Int32 value => value.ToString(CultureInfo.InvariantCulture),
            UInt32 value => value.ToString(CultureInfo.InvariantCulture),
            Int64 value => value.ToString(CultureInfo.InvariantCulture),
            UInt64 value => value.ToString(CultureInfo.InvariantCulture),
            Int128 value => value.ToString(CultureInfo.InvariantCulture),
            UInt128 value => value.ToString(CultureInfo.InvariantCulture),
            Half value => Half.IsNaN(value) ? "NaN" : Half.IsPositiveInfinity(value) ? "+Inf" : Half.IsNegativeInfinity(value) ? "-Inf" : value.ToString("0.0##", CultureInfo.InvariantCulture),
            Single value => Single.IsNaN(value) ? "NaN" : Single.IsPositiveInfinity(value) ? "+Inf" : Single.IsNegativeInfinity(value) ? "-Inf" : value.ToString("0.0#####", CultureInfo.InvariantCulture),
            Double value => Double.IsNaN(value) ? "NaN" : Double.IsPositiveInfinity(value) ? "+Inf" : Double.IsNegativeInfinity(value) ? "-Inf" : value.ToString("0.0##############", CultureInfo.InvariantCulture),
            Decimal value => value.ToString("0.0##############", CultureInfo.InvariantCulture),
            DateTimeOffset value => '"' + value.ToString("yyyy-MM-dd'T'HH:mm:ss.FFFFFFFzzz", CultureInfo.InvariantCulture) + '"',
            DateOnly value => '"' + value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + '"',
            TimeOnly value => '"' + value.ToString("HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture) + '"',
            TimeSpan value => '"' + (AASeqValue.TryConvertString(value, out string? valueString) ? valueString : "") + '"',
            IPAddress value => '"' + value.ToString() + '"',
            IPEndPoint value => '"' + value.ToString() + '"',
            Uri value => '"' + value.ToString() + '"',
            Guid value => '"' + value.ToString() + '"',
            Byte[] value => "0x" + (AASeqValue.TryConvertString(value, out String? res) ? res : ""),
            String value => GetPotentiallyQuotedString(value),
            _ => throw new InvalidOperationException("Unknown value type."),
        };
    }

    private static string GetPotentiallyQuotedString(string text) {
        if (text.Length == 0) { return "\"\""; }
        if (text.Equals("null", StringComparison.OrdinalIgnoreCase)
            || text.Equals("false", StringComparison.OrdinalIgnoreCase)
            || text.Equals("true", StringComparison.OrdinalIgnoreCase)
            || text.Equals("+inf", StringComparison.OrdinalIgnoreCase)
            || text.Equals("-inf", StringComparison.OrdinalIgnoreCase)
            || text.Equals("nan", StringComparison.OrdinalIgnoreCase)) {
            return "\"" + text + "\"";
        }

        var sbOutQuote = StringBuilderPool.Get();
        try {
            var dotCount = 0;
            var shouldQuote = (text[0] is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9')
                || ((text.Length > 1) && (text[0] is '+' or '-' or '.') && (text[1] is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9'))
                || ((text.Length > 2) && (text[0] is '+' or '-') && (text[1] is '.') && (text[2] is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9'));
            if (shouldQuote) { sbOutQuote.Append('"'); }
            foreach (var ch in text) {
                if (!shouldQuote) {  // just handle if quote is needed; escaping will be handled later
                    if (char.IsWhiteSpace(ch)) {
                        sbOutQuote.Insert(0, '"');
                        shouldQuote = true;
                    } else if (ch is '.') {
                        dotCount++;
                        if (dotCount > 1) {
                            sbOutQuote.Insert(0, '"');
                            shouldQuote = true;
                        }
                    } else if (ch is '{' or '}' or '(' or ')' or '\\' or '/' or '=' or '"' or ';' or '#') {
                        sbOutQuote.Insert(0, '"');
                        shouldQuote = true;
                    } else if (char.IsControl(ch)) {
                        sbOutQuote.Insert(0, '"');
                        shouldQuote = true;
                    }
                }
                if (char.IsControl(ch)) {
                    switch (ch) {
                        case '\0': sbOutQuote.Append(@"\0"); break;
                        case '\a': sbOutQuote.Append(@"\a"); break;
                        case '\b': sbOutQuote.Append(@"\b"); break;
                        case '\e': sbOutQuote.Append(@"\e"); break;
                        case '\f': sbOutQuote.Append(@"\f"); break;
                        case '\n': sbOutQuote.Append(@"\n"); break;
                        case '\r': sbOutQuote.Append(@"\r"); break;
                        case '\t': sbOutQuote.Append(@"\t"); break;
                        case '\v': sbOutQuote.Append(@"\v"); break;
                        default:
                            var chValue = (int)ch;
                            if (chValue <= 255) {
                                sbOutQuote.Append(@"\x"); sbOutQuote.Append(chValue.ToString("X2", CultureInfo.InvariantCulture));
                            } else if (chValue <= 65535) {
                                sbOutQuote.Append(@"\u"); sbOutQuote.Append(chValue.ToString("X4", CultureInfo.InvariantCulture));
                            } else {
                                sbOutQuote.Append(@"\U"); sbOutQuote.Append(chValue.ToString("X8", CultureInfo.InvariantCulture));
                            }
                            break;
                    }
                } else if (ch is '"') {
                    sbOutQuote.Append(@"\""");
                } else if (ch is '\\') {
                    sbOutQuote.Append(@"\\");
                } else {
                    sbOutQuote.Append(ch);
                }
            }
            if (shouldQuote) { sbOutQuote.Append('"'); }
            return sbOutQuote.ToString();
        } finally {
            sbOutQuote.Length = 0;
            StringBuilderPool.Return(sbOutQuote);
        }
    }

    #endregion Helper

}
