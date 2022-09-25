using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAUInt8Value_Tests {

    [TestMethod]
    public void AAUInt8Value_Basic() {
        var text = "42";
        Assert.IsTrue(AAUInt8Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAUInt8Value.Parse(text));
    }

    [TestMethod]
    public void AAUInt8Value_FailedParse() {
        Assert.IsFalse(AAUInt8Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAUInt8Value.Parse("A");
        });
    }

    [TestMethod]
    public void AAUInt8Value_OutOfRange() {
        Assert.IsTrue(AAUInt8Value.TryParse(((decimal)byte.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AAUInt8Value.TryParse(((decimal)byte.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AAUInt8Value.TryParse(((decimal)byte.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AAUInt8Value.TryParse(((decimal)byte.MaxValue + 1).ToString("0"), out var _));
    }

}
