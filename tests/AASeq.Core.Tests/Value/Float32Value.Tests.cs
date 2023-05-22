using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class Float32Value_Tests {

    [TestMethod]
    public void Float32Value_Basic() {
        var text = "42.84";
        Assert.IsTrue(Float32Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(result, Single.Parse(text));
    }

    [TestMethod]
    public void Float32Value_Exponents() {
        Assert.IsTrue(Float32Value.TryParse("42e2", out var result));
        Assert.AreEqual("4200", result);
    }

    [TestMethod]
    public void Float32Value_NaN() {
        Assert.IsTrue(Float32Value.TryParse("NaN", out var result));
        Assert.IsTrue(float.IsNaN(result));
    }

    [TestMethod]
    public void Float32Value_FailedParse() {
        Assert.IsFalse(Float32Value.TryParse("A", out var _));
    }

}
