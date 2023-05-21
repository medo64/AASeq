namespace AASeq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

[DebuggerDisplay("{Number}: {Text}")]
internal readonly struct DocumentLine {

    public DocumentLine(int number, byte[] bytes, EolCharacter eol) {
        Number = number;
        Text = Encoding.UTF8.GetString(bytes);
        Eol = eol;
    }

    public int Number { get; }
    public string Text { get; }
    public EolCharacter Eol { get; }

    public static IReadOnlyList<DocumentLine> GetLines(Stream stream) {
        var lines = new List<DocumentLine>();

        var lineNumber = 1;
        var lineBytes = new List<byte>();
        var hadCr = false;
        while (true) {
            var b = stream.ReadByte();
            if (b == -1) {  // nothing remains
                if ((lineBytes.Count > 0) || hadCr) {
                    lines.Add(new DocumentLine(
                        lineNumber,
                        lineBytes.ToArray(),
                        hadCr ? EolCharacter.Cr : EolCharacter.None
                    ));
                }
                break;
            } else if (b == 10) {
                lines.Add(new DocumentLine(
                    lineNumber,
                    lineBytes.ToArray(),
                    hadCr ? EolCharacter.CrLf : EolCharacter.Lf
                ));
                lineNumber++;
                lineBytes.Clear();
                hadCr = false;
            } else if (b == 13) {
                if (hadCr) {  // two CRs in row
                    lines.Add(new DocumentLine(
                        lineNumber,
                        lineBytes.ToArray(),
                        EolCharacter.Cr
                    ));
                    lineNumber++;
                    lineBytes.Clear();
                }
                hadCr = true;
            } else {
                if (hadCr) {
                    lines.Add(new DocumentLine(
                        lineNumber,
                        lineBytes.ToArray(),
                        EolCharacter.Cr
                    ));
                    lineNumber++;
                    lineBytes.Clear();
                    hadCr = false;
                }
                lineBytes.Add((byte)b);
            }
        }

        return lines.AsReadOnly();
    }


    internal enum EolCharacter {
        None = 0,
        Lf = 1,
        CrLf = 2,
        Cr = 3
    }

}
