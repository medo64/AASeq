using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class Tag_Tests {

    [TestMethod]
    public void Tag_Basic() {
        var tag = new Tag("Test");
        Assert.AreEqual("Test", tag.Name);
        Assert.IsTrue(tag.State);
        Assert.IsFalse(tag.IsSystem);
    }

    [TestMethod]
    public void Tag_BasicNegative() {
        var tag = new Tag("Test", false);
        Assert.AreEqual("Test", tag.Name);
        Assert.IsFalse(tag.State);
        Assert.IsFalse(tag.IsSystem);
    }

    [TestMethod]
    public void Tag_System() {
        var tag = new Tag("@Test");
        Assert.AreEqual("Test", tag.Name);
        Assert.IsTrue(tag.State);
        Assert.IsTrue(tag.IsSystem);
    }

    [TestMethod]
    public void Tag_Equality() {
        Assert.AreEqual(new Tag("Test"), new Tag("test"));
        Assert.AreEqual(new Tag("Test"), new Tag("test", true));
        Assert.AreEqual(new Tag("Test", true), new Tag("test", true));
        Assert.AreEqual(new Tag("Test", false), new Tag("test", false));
        Assert.AreEqual(new Tag("@Test", false), new Tag("@test", false));
        Assert.AreNotEqual(new Tag("Test", true), new Tag("Test", false));
        Assert.AreNotEqual(new Tag("A"), new Tag("B"));
        Assert.AreNotEqual(new Tag("@A"), new Tag("A"));
    }


    [TestMethod]
    public void Tag_InvalidNames() {
        Assert.ThrowsException<ArgumentNullException>(() => new Tag(null));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Tag(""));  // cannot be empty
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Tag("@"));  // cannot have only at sign
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Tag("A "));  // no spaces allowed
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Tag("1A"));  // cannot start with number
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Tag("@1A"));  // cannot start with a number
    }

}
