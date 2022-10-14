using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class AAInteractionCollection_Tests {

    [TestMethod]
    public void AAInteractionCollection_Basic() {
        var c = new AAInteractionCollection();
        c.Add(new AAMessage("Test", new AAEndpoint("S"), new AAEndpoint("D")));
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.AreEqual("S", ((AAMessage)(c[0])).Source.Name);
        Assert.AreEqual("D", ((AAMessage)(c[0])).Destination.Name);
    }

    [TestMethod]
    public void AAInteractionCollection_DuplicateName() {
        var c = new AAInteractionCollection();
        c.Add(new AAMessage("Test", new AAEndpoint("S"), new AAEndpoint("D")));
        c.Add(new AACommand("Test"));
        Assert.AreEqual(2, c.Count);
    }

    [TestMethod]
    public void AAInteractionCollection_DuplicateCaseInsensitiveName() {
        var c = new AAInteractionCollection();
        c.Add(new AAMessage("Test", new AAEndpoint("S"), new AAEndpoint("D")));
        c.Add(new AAMessage("test", new AAEndpoint("S"), new AAEndpoint("D")));
        Assert.AreEqual(2, c.Count);
    }


    [TestMethod]
    public void AAInteractionCollection_LookupByName() {
        var c = new AAInteractionCollection();
        c.Add(new AAMessage("Test1", new AAEndpoint("S"), new AAEndpoint("D")));
        c.Add(new AACommand("Test2"));
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test1", c["test1"].Name);
        Assert.AreEqual("Test2", c["test2"].Name);
    }

    [TestMethod]
    public void AAInteractionCollection_LookupMultipleByName() {
        var c = new AAInteractionCollection();
        c.Add(new AACommand("Test"));
        c.Insert(0, new AACommand("test"));
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("test", c["Test"].Name);
    }

    [TestMethod]
    public void AAInteractionCollection_LookupByNameAfterRemove() {
        var c = new AAInteractionCollection();
        c.Add(new AACommand("Test"));
        c.Add(new AACommand("test"));
        c.RemoveAt(0);
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("test", c["Test"].Name);
    }

    [TestMethod]
    public void AAInteractionCollection_LookupByNameAfterRemoveAll() {
        var c = new AAInteractionCollection();
        c.Add(new AACommand("Test"));
        c.Add(new AAMessage("test", new AAEndpoint("S"), new AAEndpoint("D")));
        c.Remove("TEST");
        Assert.AreEqual(0, c.Count);
    }

    [TestMethod]
    public void AAInteractionCollection_RemoveByName() {
        var c = new AAInteractionCollection();
        c.Add(new AAMessage("Test", new AAEndpoint("S"), new AAEndpoint("D")));
        c.Remove("Test");
        Assert.AreEqual(0, c.Count);
    }

    [TestMethod]
    public void AAInteractionCollection_RemoveMultipleByName() {
        var c = new AAInteractionCollection();
        c.Add(new AAMessage("Test", new AAEndpoint("S"), new AAEndpoint("D")));
        c.Add(new AACommand("test"));
        c.Remove("Test");
        Assert.AreEqual(0, c.Count);
    }


    [TestMethod]
    public void AAInteractionCollection_Clone() {
        var o = new AAInteractionCollection();
        o.Add(new AAMessage("Test", new AAEndpoint("S"), new AAEndpoint("D")));

        var c = o.Clone();
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.AreEqual("S", ((AAMessage)(c[0])).Source.Name);
        Assert.AreEqual("D", ((AAMessage)(c[0])).Destination.Name);
    }

}
