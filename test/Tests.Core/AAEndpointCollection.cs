using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAEndpointCollection_Tests {

    [TestMethod]
    public void AAEndpointCollection_Basic() {
        var c = new AAEndpointCollection {
            new AAEndpoint("Test", "Protocol")
        };
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.AreEqual("Protocol", c[0].PluginName);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAEndpointCollection_DuplicateName() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint("Test", "Protocol"));
        c.Add(new AAEndpoint("Test", "Protocol"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAEndpointCollection_DuplicateCaseInsensitiveName() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint("Test", "Protocol"));
        c.Add(new AAEndpoint("test", "Protocol"));
    }


    [TestMethod]
    public void AAEndpointCollection_LookupByName() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint("Test1", "Dummy"));
        c.Add(new AAEndpoint("Test2", "Dummy"));
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test1", c["test1"].Name);
        Assert.AreEqual("Test2", c["test2"].Name);
    }

    [TestMethod]
    public void AAEndpointCollection_RenameItem() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint("Test1", "Dummy"));
        c.Add(new AAEndpoint("Test2", "Dummy"));
        c[0] = new AAEndpoint("Test", "Dummy");
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.AreEqual("Test2", c[1].Name);
    }

    [TestMethod]
    public void AAEndpointCollection_RenameItemNoChange() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint("Test1", "Dummy"));
        c.Add(new AAEndpoint("Test2", "Dummy"));
        c[0] = new AAEndpoint("Test1", "Dummy");
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test1", c[0].Name);
        Assert.AreEqual("Test2", c[1].Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAEndpointCollection_RenameItemDuplicate() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint("Test1", "Dummy"));
        c.Add(new AAEndpoint("Test2", "Dummy"));
        c[0] = new AAEndpoint("Test2", "Dummy");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AAEndpointCollection_RenameItemNull() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint("Test1", "Dummy"));
        c.Add(new AAEndpoint("Test2", "Dummy"));
        c[0] = null;
    }

    [TestMethod]
    public void AAEndpointCollection_RemoveByName() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint("Test", "Protocol"));
        c.Remove("Test");
        Assert.AreEqual(0, c.Count);
    }

    [TestMethod]
    public void AAEndpointCollection_GetNewName() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint(c.GetUniqueName("Test"), "test"));
        c.Add(new AAEndpoint(c.GetUniqueName("Test"), "test"));
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.AreEqual("Test2", c[1].Name);
    }

    [TestMethod]
    public void AAEndpointCollection_GetNewNameInOne() {
        var c = new AAEndpointCollection();
        c.Add(new AAEndpoint(c.GetUniqueName("Test"), "Dummy"));
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("Test", c[0].Name);
    }

}
