using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAFloat64Value_Tests {

    [TestMethod]
    public void AAFloat64Value_Basic() {
        var text = "42.84";
        Assert.IsTrue(AAFloat64Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAFloat64Value.Parse(text));
        Assert.AreEqual(result, AAFloat64Value.Parse(text));
        Assert.AreEqual(result, Double.Parse(text));
    }

    [TestMethod]
    public void AAFloat64Value_Exponents() {
        Assert.AreEqual("4200", AAFloat64Value.Parse("42e2"));
    }

    [TestMethod]
    public void AAFloat64Value_NaN() {
        Assert.IsTrue(double.IsNaN(AAFloat64Value.Parse("NaN")));
    }

    [TestMethod]
    public void AAFloat64Value_FailedParse() {
        Assert.IsFalse(AAFloat64Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAFloat64Value.Parse("A");
        });
    }

}
