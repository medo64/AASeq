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
        var lines = Document.DocumentLine.Parse(GetResourceStream("Document/Empty.aaseq"));
        Assert.AreEqual(0, lines.Count);
    }

    [DataTestMethod]
    [DataRow("Document/EndOfLine.LF.aaseq")]
    [DataRow("Document/EndOfLine.CRLF.aaseq")]
    [DataRow("Document/EndOfLine.CR.aaseq")]
    public void Document_Lines_EndOfLine(string resourceName) {
        var lines = Document.DocumentLine.Parse(GetResourceStream(resourceName));
        Assert.AreEqual(4, lines.Count);
        Assert.AreEqual((1, "A"), (lines[0].Number, lines[0].Text));
        Assert.AreEqual((2, "B"), (lines[1].Number, lines[1].Text));
        Assert.AreEqual((3, ""), (lines[2].Number, lines[2].Text));
        Assert.AreEqual((4, "D"), (lines[3].Number, lines[3].Text));
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
