namespace Tests;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

[TestClass]
public sealed class ConsumeTests {

    [TestMethod]
    public void Consume_OneNode() {
        var doc = AASeqNodes.Parse("A { B }");
        var node = doc.ConsumeNode("A/B");
        Assert.IsNotNull(node);
        Assert.AreEqual("B", node.Name);
        Assert.AreEqual("A", doc.ToString());
    }

    [TestMethod]
    public void Consume_NoNode() {
        var doc = AASeqNodes.Parse("A { B }");
        var node = doc.ConsumeNode("A/C");
        Assert.IsNull(node);
        Assert.AreEqual("A { B }", doc.ToString());
    }

    [TestMethod]
    public void Consume_Root() {
        var doc = AASeqNodes.Parse("A { B }");
        var node = doc.ConsumeNode("A");
        Assert.IsNotNull(node);
        Assert.AreEqual("A", node.Name);
        Assert.AreEqual("", doc.ToString());
    }

    [TestMethod]
    public void Consume_Property() {
        var doc = AASeqNodes.Parse("A { B Test=X }");
        var value = doc.FindNode("A/B").ConsumeProperty("test");
        Assert.AreEqual("X", value);
        Assert.AreEqual("A { B }", doc.ToString());
    }

    [TestMethod]
    public void Consume_NoProperty() {
        var doc = AASeqNodes.Parse("A { B test=X }");
        var value = doc.FindNode("A/B").ConsumeProperty("othertest");
        Assert.IsNull(value);
        Assert.AreEqual("A { B test=X }", doc.ToString());
    }

}
