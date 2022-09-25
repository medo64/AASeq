using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AATagCollection_Tests {

    [TestMethod]
    public void AATagCollection_Basic() {
        var c = new AATagCollection {
            new AATag("Test"),
            new AATag("Test2", false)
        };
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.IsTrue(c[0].State);
        Assert.AreEqual(true, c.GetState("Test"));
        Assert.AreEqual("Test2", c[1].Name);
        Assert.IsFalse(c[1].State);
        Assert.AreEqual(false, c.GetState("Test2"));
    }

    [TestMethod]
    public void AATagCollection_StateLookup() {
        var c = new AATagCollection {
            new AATag("Test", false)
        };
        Assert.IsNull(c.GetState("XXX"));
    }

    [TestMethod]
    public void AATagCollection_DuplicateName() {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            var c = new AATagCollection {
                new AATag("Test"),
                new AATag("test", false)
            };
        });
    }

    [TestMethod]
    public void AATagCollection_LookupByName() {
        var c = new AATagCollection {
            new AATag("Test1"),
            new AATag("Test2")
        };
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test1", c["test1"].Name);
        Assert.AreEqual("Test2", c["test2"].Name);
    }

    [TestMethod]
    public void AATagCollection_RemoveByName() {
        var c = new AATagCollection {
            new AATag("Test")
        };
        c.Remove("Test");
        Assert.AreEqual(0, c.Count);
    }

}
