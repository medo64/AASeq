using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class UInt16Value_Tests {

    [TestMethod]
    public void UInt16Value_Basic() {
        var text = "42";
        Assert.IsTrue(UInt16Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, UInt16Value.Parse(text));
        Assert.AreEqual(result, UInt16Value.Parse(text));
        Assert.AreEqual(result, UInt16.Parse(text));
    }

    [TestMethod]
    public void UInt16Value_FailedParse() {
        Assert.IsFalse(UInt16Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            UInt16Value.Parse("A");
        });
    }

    [TestMethod]
    public void UInt16Value_OutOfRange() {
        Assert.IsTrue(UInt16Value.TryParse(((decimal)ushort.MinValue).ToString("0"), out var _));
        Assert.IsFalse(UInt16Value.TryParse(((decimal)ushort.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(UInt16Value.TryParse(((decimal)ushort.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(UInt16Value.TryParse(((decimal)ushort.MaxValue + 1).ToString("0"), out var _));
    }

}
