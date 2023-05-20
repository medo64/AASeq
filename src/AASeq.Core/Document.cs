using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AASeq;

/// <summary>
/// AASeq document.
/// </summary>
public sealed record Document {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="stream">Input stream.</param>
    /// <exception cref="ArgumentNullException">Stream cannot be null.</exception>
    public Document(Stream stream) {
        if (stream == null) { throw new ArgumentNullException(nameof(stream), "Stream cannot be null."); }

        using var bStream = new BufferedStream(stream);
        var lines = Line.GetLines(bStream);
    }



    [DebuggerDisplay("{Number}: {Text}")]
    internal readonly struct Line {

        public Line(int number, byte[] bytes, EolCharacter eol) {
            Number = number;
            Text = Encoding.UTF8.GetString(bytes);
            Eol = eol;
        }

        public int Number { get; }
        public string Text { get; }
        public EolCharacter Eol { get; }

        public static IReadOnlyList<Line> GetLines(Stream stream) {
            var lines = new List<Line>();

            var lineNumber = 1;
            var lineBytes = new List<byte>();
            var hadCr = false;
            while (true) {
                var b = stream.ReadByte();
                if (b == -1) {  // nothing remains
                    if ((lineBytes.Count > 0) || hadCr) {
                        lines.Add(new Line(lineNumber, lineBytes.ToArray(), hadCr ? EolCharacter.Cr : EolCharacter.None));
                    }
                    break;
                } else if (b == 10) {
                    lines.Add(new Line(lineNumber, lineBytes.ToArray(), hadCr ? EolCharacter.CrLf : EolCharacter.Lf));
                    lineNumber++;
                    lineBytes.Clear();
                    hadCr = false;
                } else if (b == 13) {
                    if (hadCr) {  // two CRs in row
                        lines.Add(new Line(lineNumber, lineBytes.ToArray(), EolCharacter.Cr));
                        lineNumber++;
                        lineBytes.Clear();
                    }
                    hadCr = true;
                } else {
                    if (hadCr) {
                        lines.Add(new Line(lineNumber, lineBytes.ToArray(), EolCharacter.Cr));
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

    internal enum EolCharacter {
        None = 0,
        Lf = 1,
        CrLf = 2,
        Cr = 3
    }

}
