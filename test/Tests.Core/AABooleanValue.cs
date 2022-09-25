using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AABooleanValue_Tests {

    [TestMethod]
    public void AABooleanValue_Basic() {
        var text = "True";
        Assert.IsTrue(AABooleanValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AABooleanValue.Parse(text));
    }

    [TestMethod]
    public void AABooleanValue_True() {
        Assert.IsTrue(AABooleanValue.Parse("True"));
        Assert.IsTrue(AABooleanValue.Parse("T"));
        Assert.IsTrue(AABooleanValue.Parse("Yes"));
        Assert.IsTrue(AABooleanValue.Parse("Y"));
        Assert.IsTrue(AABooleanValue.Parse("+"));
    }

    [TestMethod]
    public void AABooleanValue_False() {
        Assert.IsFalse(AABooleanValue.Parse("False"));
        Assert.IsFalse(AABooleanValue.Parse("F"));
        Assert.IsFalse(AABooleanValue.Parse("No"));
        Assert.IsFalse(AABooleanValue.Parse("N"));
        Assert.IsFalse(AABooleanValue.Parse("-"));
    }

    [TestMethod]
    public void AABooleanValue_FailedParse() {
        Assert.IsFalse(AABooleanValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AABooleanValue.Parse("A");
        });
    }

}
