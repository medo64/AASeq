namespace Tests;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

[TestClass]
public sealed class NodeTests {

    [TestMethod]
    public void Node_Basic() {
        var node = new AASeqNode("node");
    }

    [TestMethod]
    public void Node_Null() {
        Assert.ThrowsException<ArgumentNullException>(() => new AASeqNode(null));
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("a ")]
    [DataRow("a\a")]
    [DataRow("a\b")]
    [DataRow("a\n")]
    [DataRow("a\r")]
    [DataRow("a\t")]
    [DataRow("a\v")]
    [DataRow("a{")]
    [DataRow("a}")]
    [DataRow("a(")]
    [DataRow("a)")]
    [DataRow("a\\")]
    [DataRow("a/")]
    [DataRow("a\"")]
    [DataRow("a=")]
    [DataRow("a;")]
    [DataRow("a#")]
    public void Node_InvalidNames(string name) {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AASeqNode(name));
    }

}
