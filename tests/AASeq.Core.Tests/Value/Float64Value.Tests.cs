using AASeq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class Float64Value_Tests {

    [TestMethod]
    public void Float64Value_Basic() {
        var text = "42.84";
        Assert.IsTrue(Float64Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, Float64Value.Parse(text));
        Assert.AreEqual(result, Float64Value.Parse(text));
        Assert.AreEqual(result, Double.Parse(text));
    }

    [TestMethod]
    public void Float64Value_Exponents() {
        Assert.AreEqual("4200", Float64Value.Parse("42e2"));
    }

    [TestMethod]
    public void Float64Value_NaN() {
        Assert.IsTrue(double.IsNaN(Float64Value.Parse("NaN")));
    }

    [TestMethod]
    public void Float64Value_FailedParse() {
        Assert.IsFalse(Float64Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            Float64Value.Parse("A");
        });
    }

}
