namespace Tests;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

[TestClass]
public sealed class PropertiesTests {

    [TestMethod]
    public void Properties_Sorted() {
        var properties = new AASeqProperties {
            { "d", "4" },
            { "e", "5" },
            { "b", "2" },
            { "a", "1" },
            { "c", "3" }
        };

        var keys = new List<string>(properties.Keys);
        Assert.AreEqual("a", keys[0]);
        Assert.AreEqual("b", keys[1]);
        Assert.AreEqual("c", keys[2]);
        Assert.AreEqual("d", keys[3]);
        Assert.AreEqual("e", keys[4]);

        var values = new List<string>(properties.Values);
        Assert.AreEqual("1", values[0]);
        Assert.AreEqual("2", values[1]);
        Assert.AreEqual("3", values[2]);
        Assert.AreEqual("4", values[3]);
        Assert.AreEqual("5", values[4]);
    }

}
