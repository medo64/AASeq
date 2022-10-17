namespace AASeq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

internal static class Parsing {

    #region Document
    #endregion Document


    #region Fields
    #endregion Fields


    #region Sections

    public static IReadOnlyList<ParsedSection> ParseSections(IReadOnlyList<ParsedLine> lines) {
        var sections = new List<ParsedSection>();
        ParsedLine? sectionHeader = null;
        var sectionLines = new List<ParsedLine>();

        foreach (var line in lines) {
            bool isHeader = false;
            foreach (var ch in line.Text) {
                if (!char.IsWhiteSpace(ch)) {  // ignore whitespace
                    isHeader = (ch == '[');
                    break;
                }
            }
            if (isHeader) {
                if (sectionHeader != null) {  // already have something
                    sections.Add(new ParsedSection(sectionHeader, sectionLines.AsReadOnly()));
                    sectionLines = new List<ParsedLine>();
                }
                sectionHeader = line;
            } else if (sectionHeader != null) {
                sectionLines.Add(line);
            } else if (!line.IsEmpty) {  // ignore only empty lines before the first section
                throw new FormatException($"Content outside of section in the line {line.LineNumber}: {line.Text}.");
            }
        }
        if (sectionHeader != null) {  // already have something
            sections.Add(new ParsedSection(sectionHeader, sectionLines.AsReadOnly()));
        }

        return sections.AsReadOnly();
    }

    internal sealed record ParsedSection {

        public ParsedSection(ParsedLine headerLine, IReadOnlyList<ParsedLine> contentLines) {
            HeaderLine = headerLine;
            ContentLines = contentLines;
        }

        public ParsedLine HeaderLine { get; }
        public IReadOnlyList<ParsedLine> ContentLines { get; }

    }

    #endregion


    #region Lines

    public static IReadOnlyList<ParsedLine> ParseLines(Stream stream) {
        var lines = new List<ParsedLine>();

        var lineNumber = 1;
        var sbLine = new StringBuilder();
        var hadCr = false;

        using var textStream = new StreamReader(stream, Encoding.UTF8);
        foreach (var ch in textStream.ReadToEnd()) {
            if (hadCr) {  // maybe CRLF
                if (ch is '\r') {  // specal case for double CR
                    lines.Add(new ParsedLine(lineNumber, ""));
                    lineNumber++;
                } else {
                    if (ch is not '\n') {  // was CR
                        sbLine.Append(ch);
                    }
                    hadCr = false;
                }
            } else if (ch is '\r') {  // CR or CRLF
                lines.Add(new ParsedLine(lineNumber, sbLine.ToString()));
                sbLine.Length = 0;
                lineNumber++;
                hadCr = true;
            } else if (ch is '\n') {  // LF
                lines.Add(new ParsedLine(lineNumber, sbLine.ToString()));
                sbLine.Length = 0;
                lineNumber++;
            } else {
                sbLine.Append(ch);
            }
        }
        lines.Add(new ParsedLine(lineNumber, sbLine.ToString()));

        return lines.AsReadOnly();
    }


    internal sealed record ParsedLine {

        public ParsedLine(int lineNumber, string text) {
            LineNumber = lineNumber;
            Text = text;
        }

        public int LineNumber { get; }
        public string Text { get; }

        private bool? _isEmpty;
        public bool IsEmpty {
            get {
                if (_isEmpty == null) {
                    var hasAnything = false;
                    var hadSlash = false;
                    foreach (var ch in Text) {
                        if (!char.IsWhiteSpace(ch)) {
                            if (ch == '#') { break; }  // nothing to see here, just a comment
                            if (ch == '/') {
                                if (hadSlash) { hadSlash = false; break; }  // nothing to see here, just a comment
                                hadSlash = true;
                                continue;
                            }
                            hasAnything = true;
                            break;
                        }
                    }
                    _isEmpty = !(hasAnything || hadSlash);
                }
                return _isEmpty.Value;
            }
        }

        public static implicit operator string(ParsedLine line) => line.Text;
        public static implicit operator int(ParsedLine line) => line.LineNumber;

    }

    #endregion Lines

}
