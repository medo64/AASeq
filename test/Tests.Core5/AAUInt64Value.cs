using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAUInt64Value_Tests {

    [TestMethod]
    public void AAUInt64Value_Basic() {
        var text = "42";
        Assert.IsTrue(AAUInt64Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAUInt64Value.Parse(text));
        Assert.AreEqual(result, AAUInt64Value.Parse(text));
        Assert.AreEqual(result, UInt64.Parse(text));
    }

    [TestMethod]
    public void AAUInt64Value_FailedParse() {
        Assert.IsFalse(AAUInt64Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAUInt64Value.Parse("A");
        });
    }

    [TestMethod]
    public void AAUInt64Value_OutOfRange() {
        Assert.IsTrue(AAUInt64Value.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AAUInt64Value.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AAUInt64Value.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AAUInt64Value.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
    }

}
