using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AATag_Tests {

    [TestMethod]
    public void AATag_Basic() {
        var tag = new AATag("Test");
        Assert.AreEqual("Test", tag.Name);
        Assert.IsTrue(tag.State);
    }

    [TestMethod]
    public void AATag_BasicNegative() {
        var tag = new AATag("Test", false);
        Assert.AreEqual("Test", tag.Name);
        Assert.IsFalse(tag.State);
    }

    [TestMethod]
    public void AATag_Equality() {
        Assert.AreEqual(new AATag("Test"), new AATag("Test"));
        Assert.AreEqual(new AATag("Test", true), new AATag("Test", true));
        Assert.AreEqual(new AATag("Test", false), new AATag("Test", false));
        Assert.AreNotEqual(new AATag("Test", true), new AATag("Test", false));
        Assert.AreNotEqual(new AATag("A"), new AATag("B"));
        Assert.AreNotEqual(new AATag("@A"), new AATag("A"));
    }


    [TestMethod]
    public void AATag_InvalidNames() {
        Assert.ThrowsException<ArgumentNullException>(() => new AATag(null));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AATag(""));  // cannot be empty
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AATag("@"));  // cannot have only at sign
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AATag("A "));  // no spaces allowed
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AATag("1A"));  // cannot start with number
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AATag("@1A"));  // cannot start with a number
    }

}
