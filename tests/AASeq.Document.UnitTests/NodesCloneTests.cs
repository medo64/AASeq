namespace Tests;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

[TestClass]
public sealed class NodesCloneTests {

    [TestMethod]
    public void NodesClone_Basic() {
        var document = new AASeqNodes();
        document.Add("test1");
        document[0].Nodes.Add(new AASeqNode("test2", "value"));

        var clone = document.Clone();

        Assert.AreEqual("test1 { test2 value }", clone.ToString());
    }

    [TestMethod]
    public void NodesClone_Multilevel() {
        var text = "test1 p1=v1 { test2 p2=v2 { test3a value3a; test3b p3b=v3b; test3c } }";
        var document = AASeqNodes.Parse(text);
        var clone = document.Clone();
        Assert.AreEqual(text, clone.ToString());
    }

}
