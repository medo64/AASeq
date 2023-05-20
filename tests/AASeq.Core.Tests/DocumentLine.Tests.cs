using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

using static Tests.Helpers;

namespace Tests;

[TestClass]
public class DocumentLine_Tests {

    [TestMethod]
    public void DocumentLine_Empty() {
        var lines = DocumentLine.GetLines(GetResourceStream("Document/Empty.aaseq"));
        Assert.AreEqual(0, lines.Count);
    }

    [DataTestMethod]
    [DataRow("Document/Lines/EndOfLine.LF.aaseq.bin", 1)]
    [DataRow("Document/Lines/EndOfLine.CRLF.aaseq.bin", 2)]
    [DataRow("Document/Lines/EndOfLine.CR.aaseq.bin", 3)]
    public void DocumentLine_EndOfLine(string resourceName, int eol) {
        var lines = DocumentLine.GetLines(GetResourceStream(resourceName));
        Assert.AreEqual(5, lines.Count);
        Assert.AreEqual((1, "A", (DocumentLine.EolCharacter)eol), (lines[0].Number, lines[0].Text, lines[0].Eol));
        Assert.AreEqual((2, "B", (DocumentLine.EolCharacter)eol), (lines[1].Number, lines[1].Text, lines[1].Eol));
        Assert.AreEqual((3, "", (DocumentLine.EolCharacter)eol), (lines[2].Number, lines[2].Text, lines[2].Eol));
        Assert.AreEqual((4, "", (DocumentLine.EolCharacter)eol), (lines[3].Number, lines[3].Text, lines[3].Eol));
        Assert.AreEqual((5, "D", DocumentLine.EolCharacter.None), (lines[4].Number, lines[4].Text, lines[4].Eol));
    }


    [TestMethod]
    public void DocumentLine_Exception_NullStream() {
        Assert.ThrowsException<ArgumentNullException>(() => {
            _ = new Document(null);
        });
    }

}
