using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class UInt32Value_Tests {

    [TestMethod]
    public void UInt32Value_Basic() {
        var text = "42";
        Assert.IsTrue(UInt32Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, UInt32Value.Parse(text));
        Assert.AreEqual(result, UInt32Value.Parse(text));
        Assert.AreEqual(result, UInt32.Parse(text));
    }

    [TestMethod]
    public void UInt32Value_FailedParse() {
        Assert.IsFalse(UInt32Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            UInt32Value.Parse("A");
        });
    }

    [TestMethod]
    public void UInt32Value_OutOfRange() {
        Assert.IsTrue(UInt32Value.TryParse(((decimal)uint.MinValue).ToString("0"), out var _));
        Assert.IsFalse(UInt32Value.TryParse(((decimal)uint.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(UInt32Value.TryParse(((decimal)uint.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(UInt32Value.TryParse(((decimal)uint.MaxValue + 1).ToString("0"), out var _));
    }

}
