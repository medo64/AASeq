using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class Float16Value_Tests {

    [TestMethod]
    public void Float16Value_Basic() {
        var text = "42.84";
        Assert.IsTrue(Float16Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
    }

    [TestMethod]
    public void Float16Value_Exponents() {
        Assert.IsTrue(Float16Value.TryParse("42e2", out var result));
        Assert.AreEqual("4200", result);
    }

    [TestMethod]
    public void Float16Value_NaN() {
        Assert.IsTrue(Float16Value.TryParse("NaN", out var result));
        Assert.IsTrue(Half.IsNaN(result));
    }

    [TestMethod]
    public void Float16Value_FailedParse() {
        Assert.IsFalse(Float16Value.TryParse("A", out var _));
    }

}
