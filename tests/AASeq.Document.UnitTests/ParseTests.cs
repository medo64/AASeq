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


    [TestMethod]
    public void ParseTests_ByteArray_Hex() {
        var input = "test1 (hex)\"414243\"";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 (hex)\"414243\"", doc.ToString());
        Assert.AreEqual("41-42-43", BitConverter.ToString(doc[0].Value.AsByteArray()));
    }

    [TestMethod]
    public void ParseTests_ByteArray_Base64() {
        var input = "test1 (base64)\"QUJD\"";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 (hex)\"414243\"", doc.ToString());
        Assert.AreEqual("41-42-43", BitConverter.ToString(doc[0].Value.AsByteArray()));
    }

    [TestMethod]
    public void ParseTests_ByteArray_Text() {
        var input = "test1 \"ABC\"";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 ABC", doc.ToString());
        Assert.AreEqual("41-42-43", BitConverter.ToString(doc[0].Value.AsByteArray()));
    }


    [TestMethod]
    public void ParseTests_ByteArray_Hex_Empty() {
        var input = "test1 (hex)\"\"";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 (hex)\"\"", doc.ToString());
        Assert.AreEqual("", BitConverter.ToString(doc[0].Value.AsByteArray()));
    }

    [TestMethod]
    public void ParseTests_ByteArray_Base64_Empty() {
        var input = "test1 (base64)\"\"";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 (hex)\"\"", doc.ToString());
        Assert.AreEqual("", BitConverter.ToString(doc[0].Value.AsByteArray()));
    }

    [TestMethod]
    public void ParseTests_ByteArray_Text_Empty() {
        var input = "test1 \"\"";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 \"\"", doc.ToString());
        Assert.AreEqual("", BitConverter.ToString(doc[0].Value.AsByteArray()));
    }


    [TestMethod]
    public void ParseTests_ByteArray_Hex_NoQuotesInInput() {
        var input = "test1 (hex)414243";
        var doc = AASeqNodes.Parse(input, AASeqInputOptions.Permissive);
        Assert.AreEqual("test1 (hex)\"414243\"", doc.ToString());
        Assert.AreEqual("41-42-43", BitConverter.ToString(doc[0].Value.AsByteArray()));
    }

}
