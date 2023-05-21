using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class BooleanValue_Tests {

    [TestMethod]
    public void BooleanValue_Basic() {
        var text = "True";
        Assert.IsTrue(BooleanValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, BooleanValue.Parse(text));
        Assert.AreEqual(result, BooleanValue.Parse(text));
        Assert.AreEqual(result, Boolean.Parse(text));
    }

    [TestMethod]
    public void BooleanValue_True() {
        Assert.IsTrue(BooleanValue.Parse("True"));
        Assert.IsTrue(BooleanValue.Parse("T"));
        Assert.IsTrue(BooleanValue.Parse("Yes"));
        Assert.IsTrue(BooleanValue.Parse("Y"));
        Assert.IsTrue(BooleanValue.Parse("+"));
    }

    [TestMethod]
    public void BooleanValue_False() {
        Assert.IsFalse(BooleanValue.Parse("False"));
        Assert.IsFalse(BooleanValue.Parse("F"));
        Assert.IsFalse(BooleanValue.Parse("No"));
        Assert.IsFalse(BooleanValue.Parse("N"));
        Assert.IsFalse(BooleanValue.Parse("-"));
    }

    [TestMethod]
    public void BooleanValue_FailedParse() {
        Assert.IsFalse(BooleanValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            BooleanValue.Parse("A");
        });
    }

}
