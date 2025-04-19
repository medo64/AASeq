namespace Tests;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

[TestClass]
public sealed class NodesTraverseTests {

    [TestMethod]
    public void NodesTraverse_FindNode() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a"));
        doc[0].Nodes.Add(new AASeqNode("aa"));
        doc[0].Nodes.Add(new AASeqNode("ab"));

        Assert.AreEqual(doc[0].Nodes[0], doc.FindNode("a/aa"));
    }

    [TestMethod]
    public void NodesTraverse_FindNode_NotFound() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a"));
        doc[0].Nodes.Add(new AASeqNode("aa"));
        doc[0].Nodes.Add(new AASeqNode("ab"));

        Assert.IsNull(doc.FindNode("a/ac"));
    }

    [TestMethod]
    public void NodesTraverse_FindNode_FindLast() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", 1));
        doc[0].Nodes.Add(new AASeqNode("aa", 11));
        doc[0].Nodes.Add(new AASeqNode("ab", 12));
        doc[0].Nodes.Add(new AASeqNode("aa", 13));
        doc[0].Nodes.Add(new AASeqNode("ab", 14));

        Assert.AreEqual(doc[0].Nodes[2], doc.FindNode("a/aa"));
        Assert.AreEqual(doc[0].Nodes[3], doc.FindNode("a/ab"));
    }

    [TestMethod]
    public void NodesTraverse_FindNode_FindLast_DifferentCase() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", 1));
        doc[0].Nodes.Add(new AASeqNode("aa", 11));
        doc[0].Nodes.Add(new AASeqNode("ab", 12));
        doc[0].Nodes.Add(new AASeqNode("aa", 13));
        doc[0].Nodes.Add(new AASeqNode("ab", 14));

        Assert.AreEqual(doc[0].Nodes[2], doc.FindNode("A/AA"));
        Assert.AreEqual(doc[0].Nodes[3], doc.FindNode("A/AB"));
    }


    [TestMethod]
    public void NodesTraverse_FindValue() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", 1));
        doc[0].Nodes.Add(new AASeqNode("aa", 11));
        doc[0].Nodes.Add(new AASeqNode("ab", 12));

        Assert.AreEqual(11, (int)doc.FindValue("a/aa"));
    }

    [TestMethod]
    public void NodesTraverse_FindValue_NotFound() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a"));
        doc[0].Nodes.Add(new AASeqNode("aa"));
        doc[0].Nodes.Add(new AASeqNode("ab"));

        Assert.IsNull(doc.FindValue("a/ac"));
    }

    [TestMethod]
    public void NodesTraverse_FindValue_FindLast() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", 1));
        doc[0].Nodes.Add(new AASeqNode("aa", 11));
        doc[0].Nodes.Add(new AASeqNode("ab", 12));
        doc[0].Nodes.Add(new AASeqNode("aa", 13));
        doc[0].Nodes.Add(new AASeqNode("ab", 14));

        Assert.AreEqual(13, (int)doc.FindValue("a/aa"));
        Assert.AreEqual(14, (int)doc.FindValue("a/ab"));
    }

    [TestMethod]
    public void NodesTraverse_FindValue_FindLast_DifferentCase() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", 1));
        doc[0].Nodes.Add(new AASeqNode("aa", 11));
        doc[0].Nodes.Add(new AASeqNode("ab", 12));
        doc[0].Nodes.Add(new AASeqNode("aa", 13));
        doc[0].Nodes.Add(new AASeqNode("ab", 14));

        Assert.AreEqual(13, (int)doc.FindValue("A/AA"));
        Assert.AreEqual(14, (int)doc.FindValue("A/AB"));
    }


    [TestMethod]
    public void NodesTraverse_Empty() {
        var document = new AASeqNodes();

        Assert.IsTrue(document["test"].IsNull);
        Assert.AreEqual("", document.ToString());
    }

    [TestMethod]
    public void NodesTraverse_Basic() {
        var document = new AASeqNodes();

        document["test"] = "value";
        Assert.AreEqual("value", document["test"].RawValue.ToString());

        Assert.AreEqual("test value", document.ToString());
    }

    [TestMethod]
    public void NodesTraverse_TwoLevel() {
        var document = new AASeqNodes();

        document["test1/test2"] = "value";
        Assert.AreEqual("value", (String)document["test1/test2"]);

        Assert.AreEqual("test1 { test2 value }", document.ToString());
    }

    [TestMethod]
    public void NodesTraverse_TwoThreeLevel() {
        var document = new AASeqNodes();
        document.Add("test1");
        document[0].Nodes.Add(new AASeqNode("test2", "value"));

        document["test1/test2/test3"] = "value3";
        Assert.AreEqual("value3", document["test1/test2/test3"].AsString());

        Assert.AreEqual("test1 { test2 value { test3 value3 } }", document.ToString());
    }

    [TestMethod]
    public void NodesTraverse_TwoThreeLevel_DifferentCase() {
        var document = new AASeqNodes();
        document.Add("test1");
        document[0].Nodes.Add(new AASeqNode("test2", "value"));

        document["TEST1/TEST2/test3"] = "value3";
        Assert.AreEqual("value3", document["test1/test2/TEST3"].AsString());

        Assert.AreEqual("test1 { test2 value { test3 value3 } }", document.ToString());
    }

    [TestMethod]
    public void NodesTraverse_TwoThreeLevel_DifferentCase_Multiset() {
        var document = new AASeqNodes();
        document.Add("test1");
        document[0].Nodes.Add(new AASeqNode("test2", "value"));

        document["TEST1/TEST2/test3"] = "value3";
        document["TEST1/TEST2/test3"] = "value4";
        Assert.AreEqual("value4", document["test1/test2/TEST3"].AsString());

        Assert.AreEqual("test1 { test2 value { test3 value4 } }", document.ToString());
    }


    [TestMethod]
    public void NodesTraverse_GetValue() {
        var doc = AASeqNodes.Parse("node1 { node2 value2 { node3 3 { node4 4.1 } } }");

        Assert.AreEqual("value2", doc["node1/node2"].AsString());
        Assert.AreEqual(3, doc["node1/node2/node3"].AsInt32());
        Assert.AreEqual(4.1, doc["node1/node2/node3/node4"].AsDouble());
        Assert.AreEqual(5, doc["node1/node2/node3/node4/node5"].AsInt32(5));
    }

}
