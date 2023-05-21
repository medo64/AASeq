using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class UInt64Value_Tests {

    [TestMethod]
    public void UInt64Value_Basic() {
        var text = "42";
        Assert.IsTrue(UInt64Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, UInt64Value.Parse(text));
        Assert.AreEqual(result, UInt64Value.Parse(text));
        Assert.AreEqual(result, UInt64.Parse(text));
    }

    [TestMethod]
    public void UInt64Value_FailedParse() {
        Assert.IsFalse(UInt64Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            UInt64Value.Parse("A");
        });
    }

    [TestMethod]
    public void UInt64Value_OutOfRange() {
        Assert.IsTrue(UInt64Value.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.IsFalse(UInt64Value.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(UInt64Value.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(UInt64Value.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
    }

}
