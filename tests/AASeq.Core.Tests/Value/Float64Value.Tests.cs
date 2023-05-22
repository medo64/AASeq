using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class Float64Value_Tests {

    [TestMethod]
    public void Float64Value_Basic() {
        var text = "42.84";
        Assert.IsTrue(Float64Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
        Assert.AreEqual(result, Double.Parse(text));
    }

    [TestMethod]
    public void Float64Value_Exponents() {
        Assert.IsTrue(Float64Value.TryParse("42e2", out var result));
        Assert.AreEqual("4200", result);
    }

    [TestMethod]
    public void Float64Value_NaN() {
        Assert.IsTrue(Float64Value.TryParse("NaN", out var result));
        Assert.IsTrue(double.IsNaN(result));
    }

    [TestMethod]
    public void Float64Value_FailedParse() {
        Assert.IsFalse(Float64Value.TryParse("A", out var _));
    }

}
