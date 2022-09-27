using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAField_Tests {

    [TestMethod]
    public void AAField_Basic() {
        var field = new AAField("Test", 42);
        Assert.AreEqual("Test", field.Name);
        Assert.IsFalse(field.IsHeader);
        Assert.IsInstanceOfType(field.Value, typeof(AAInt32Value));
        Assert.AreEqual(0, field.Tags.Count);
    }

    [TestMethod]
    public void AAField_Header() {
        var field = new AAField(".Test", "");
        Assert.IsTrue(field.IsHeader);
    }

    [TestMethod]
    public void AAField_InvalidNames() {
        Assert.ThrowsException<ArgumentNullException>(() => new AAField(null, ""));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField("", ""));  // cannot be empty
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField(".", ""));  // cannot have only dot
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField("A ", ""));  // no spaces allowed
        Assert.ThrowsException<ArgumentNullException>(() => new AAField("A", default(AAFieldCollection)));
    }

}
