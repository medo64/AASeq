namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;
using System.IO;
using System.Text;
using System;

[TestClass]
public sealed class ParseTests {

    [TestMethod]
    public void ParseTests_AlmostNumberInValue() {
        var input = "test1 1.0.0";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 \"1.0.0\"", doc.ToString());

        Assert.ThrowsException<InvalidOperationException>(() => {
            AASeqNodes.Parse(input, AASeqInputOptions.Strict);
        });
    }

    [TestMethod]
    public void ParseTests_DuplicateValue() {
        var input = "test1 value1 value2";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 value2", doc.ToString());

        Assert.ThrowsException<InvalidOperationException>(() => {
            AASeqNodes.Parse(input, AASeqInputOptions.Strict);
        });
    }

    [TestMethod]
    public void ParseTests_DuplicateProperty() {
        var input = "test1 k=v k=v2";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 k=v2", doc.ToString());

        Assert.ThrowsException<InvalidOperationException>(() => {
            AASeqNodes.Parse(input, AASeqInputOptions.Strict);
        });
    }

}
