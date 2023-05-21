using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class UInt8Value_Tests {

    [TestMethod]
    public void UInt8Value_Basic() {
        var text = "42";
        Assert.IsTrue(UInt8Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, UInt8Value.Parse(text));
        Assert.AreEqual(result, UInt8Value.Parse(text));
        Assert.AreEqual(result, Byte.Parse(text));
    }

    [TestMethod]
    public void UInt8Value_FailedParse() {
        Assert.IsFalse(UInt8Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            UInt8Value.Parse("A");
        });
    }

    [TestMethod]
    public void UInt8Value_OutOfRange() {
        Assert.IsTrue(UInt8Value.TryParse(((decimal)byte.MinValue).ToString("0"), out var _));
        Assert.IsFalse(UInt8Value.TryParse(((decimal)byte.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(UInt8Value.TryParse(((decimal)byte.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(UInt8Value.TryParse(((decimal)byte.MaxValue + 1).ToString("0"), out var _));
    }

}
