using AASeq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class Float32Value_Tests {

    [TestMethod]
    public void Float32Value_Basic() {
        var text = "42.84";
        Assert.IsTrue(Float32Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, Float32Value.Parse(text));
        Assert.AreEqual(result, Float32Value.Parse(text));
        Assert.AreEqual(result, Single.Parse(text));
    }

    [TestMethod]
    public void Float32Value_Exponents() {
        Assert.AreEqual("4200", Float32Value.Parse("42e2"));
    }

    [TestMethod]
    public void Float32Value_NaN() {
        Assert.IsTrue(float.IsNaN(Float32Value.Parse("NaN")));
    }

    [TestMethod]
    public void Float32Value_FailedParse() {
        Assert.IsFalse(Float32Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            Float32Value.Parse("A");
        });
    }

}
