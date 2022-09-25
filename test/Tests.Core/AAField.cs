using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAField_Tests {

    [TestMethod]
    public void AAField_Basic() {
        var field = new AAField("Test");
        Assert.AreEqual("Test", field.Name);
        Assert.IsFalse(field.IsHeader);
        Assert.IsInstanceOfType(field.Value, typeof(AANullValue));
        Assert.AreEqual(0, field.Tags.Count);
        Assert.IsTrue(field.Equals("Test"));
    }

    [TestMethod]
    public void AAField_Equality() {
        Assert.AreEqual(new AAField("Test"), new AAField("Test"));
        Assert.AreEqual(new AAField("Test", true), new AAField("Test", true));
        Assert.AreEqual(new AAField("Test", 42), new AAField("Test", 42));
        Assert.AreNotEqual(new AAField("Test"), new AAField("TestB"));
        Assert.AreNotEqual(new AAField("Test", true), new AAField("Test", false));
        Assert.AreNotEqual(new AAField("Test", 42), new AAField("Test", 41));
    }

    [TestMethod]
    public void AAField_Header() {
        var field = new AAField(".Test");
        Assert.IsTrue(field.IsHeader);
    }

    [TestMethod]
    public void AAField_InvalidNames() {
        Assert.ThrowsException<ArgumentNullException>(() => new AAField(null));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField(""));  // cannot be empty
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField("."));  // cannot have only dot
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField("A "));  // no spaces allowed
    }

}
