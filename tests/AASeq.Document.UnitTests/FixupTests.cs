namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;
using System.IO;
using System.Text;
using System;

[TestClass]
public sealed class FixupTests {

    [TestMethod]
    public void Fixup_NoQuotesNeeded() {
        var input = "test1 \"diameter.aaseq.com\"";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 diameter.aaseq.com", doc.ToString());
    }

    [TestMethod]
    public void Fixup_NumberlikeNeedsQuotes() {
        var input = "test1 \"1.0.0\"";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 \"1.0.0\"", doc.ToString());
    }

}
