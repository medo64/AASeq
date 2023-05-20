using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

using static Tests.Helpers;

namespace Tests;

[TestClass]
public class Document_Tests {

    #region Lines

    [TestMethod]
    public void Document_Lines_Empty() {
        var lines = Document.Line.GetLines(GetResourceStream("Document/Empty.aaseq"));
        Assert.AreEqual(0, lines.Count);
    }

    [DataTestMethod]
    [DataRow("Document/Line/EndOfLine.LF.aaseq.bin", 1)]
    [DataRow("Document/Line/EndOfLine.CRLF.aaseq.bin", 2)]
    [DataRow("Document/Line/EndOfLine.CR.aaseq.bin", 3)]
    public void Document_Lines_EndOfLine(string resourceName, int eol) {
        var lines = Document.Line.GetLines(GetResourceStream(resourceName));
        Assert.AreEqual(5, lines.Count);
        Assert.AreEqual((1, "A", (Document.EolCharacter)eol), (lines[0].Number, lines[0].Text, lines[0].Eol));
        Assert.AreEqual((2, "B", (Document.EolCharacter)eol), (lines[1].Number, lines[1].Text, lines[1].Eol));
        Assert.AreEqual((3, "", (Document.EolCharacter)eol), (lines[2].Number, lines[2].Text, lines[2].Eol));
        Assert.AreEqual((4, "", (Document.EolCharacter)eol), (lines[3].Number, lines[3].Text, lines[3].Eol));
        Assert.AreEqual((5, "D", Document.EolCharacter.None), (lines[4].Number, lines[4].Text, lines[4].Eol));
    }

    #endregion Lines

    #region Exceptions

    [TestMethod]
    public void Document_Exception_NullStream() {
        Assert.ThrowsException<ArgumentNullException>(() => {
            _ = new Document(null);
        });
    }

    #endregion Exceptions

}
