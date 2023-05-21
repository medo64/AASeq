using AASeq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class Float16Value_Tests {

    [TestMethod]
    public void Float16Value_Basic() {
        var text = "42.84";
        Assert.IsTrue(Float16Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, Float16Value.Parse(text));
        Assert.AreEqual(result, Float16Value.Parse(text));
        Assert.AreEqual(result, Half.Parse(text));
    }

    [TestMethod]
    public void Float16Value_Exponents() {
        Assert.AreEqual("4200", Float16Value.Parse("42e2"));
    }

    [TestMethod]
    public void Float16Value_NaN() {
        Assert.IsTrue(Half.IsNaN(Float16Value.Parse("NaN")));
    }

    [TestMethod]
    public void Float16Value_FailedParse() {
        Assert.IsFalse(Float16Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            Float16Value.Parse("A");
        });
    }

}
