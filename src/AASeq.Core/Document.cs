using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AASeq;

/// <summary>
/// AASeq document
/// </summary>
public class Document {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="stream">Input stream.</param>
    /// <exception cref="ArgumentNullException">Stream cannot be null.</exception>
    public Document(Stream stream) {
        if (stream == null) { throw new ArgumentNullException(nameof(stream), "Stream cannot be null."); }

        using var bStream = new BufferedStream(stream);
        var lines = DocumentLine.Parse(bStream);

    }



    [DebuggerDisplay("{Number}: {Text}")]
    internal readonly struct DocumentLine {

        public DocumentLine(int number, byte[] bytes) {
            Number = number;
            Text = Encoding.UTF8.GetString(bytes);
        }

        public int Number { get; }
        public string Text { get; }

        public static IReadOnlyList<DocumentLine> Parse(Stream stream) {
            var lines = new List<DocumentLine>();

            var lineNumber = 1;
            var lineBytes = new List<byte>();
            var hadCr = false;
            while (true) {
                var b = stream.ReadByte();
                if (b == -1) {  // nothing remains
                    if ((lineBytes.Count > 0) || hadCr) {
                        lines.Add(new DocumentLine(lineNumber, lineBytes.ToArray()));
                    }
                    break;
                } else if (b == 10) {
                    lines.Add(new DocumentLine(lineNumber, lineBytes.ToArray()));
                    lineNumber++;
                    lineBytes.Clear();
                    hadCr = false;
                } else if (b == 13) {
                    if (hadCr) {  // two CRs in row
                        lines.Add(new DocumentLine(lineNumber, lineBytes.ToArray()));
                        lineNumber++;
                        lineBytes.Clear();
                    }
                    hadCr = true;
                } else {
                    if (hadCr) {
                        lines.Add(new DocumentLine(lineNumber, lineBytes.ToArray()));
                        lineNumber++;
                        lineBytes.Clear();
                        hadCr = false;
                    }
                    lineBytes.Add((byte)b);
                }
            }

            return lines.AsReadOnly();
        }

    }
}
