using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAFloat32Value_Tests {

    [TestMethod]
    public void AAFloat32Value_Basic() {
        var text = "42.84";
        Assert.IsTrue(AAFloat32Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAFloat32Value.Parse(text));
    }

    [TestMethod]
    public void AAFloat32Value_Exponents() {
        Assert.AreEqual("4200", AAFloat32Value.Parse("42e2"));
    }

    [TestMethod]
    public void AAFloat32Value_NaN() {
        Assert.IsTrue(float.IsNaN(AAFloat32Value.Parse("NaN")));
    }

    [TestMethod]
    public void AAFloat32Value_FailedParse() {
        Assert.IsFalse(AAFloat32Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAFloat32Value.Parse("A");
        });
    }

}
