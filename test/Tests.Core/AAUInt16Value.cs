using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAUInt16Value_Tests {

    [TestMethod]
    public void AAUInt16Value_Basic() {
        var text = "42";
        Assert.IsTrue(AAUInt16Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAUInt16Value.Parse(text));
        Assert.AreEqual(result, AAUInt16Value.Parse(text));
        Assert.AreEqual(result, UInt16.Parse(text));
    }

    [TestMethod]
    public void AAUInt16Value_FailedParse() {
        Assert.IsFalse(AAUInt16Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAUInt16Value.Parse("A");
        });
    }

    [TestMethod]
    public void AAUInt16Value_OutOfRange() {
        Assert.IsTrue(AAUInt16Value.TryParse(((decimal)ushort.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AAUInt16Value.TryParse(((decimal)ushort.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AAUInt16Value.TryParse(((decimal)ushort.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AAUInt16Value.TryParse(((decimal)ushort.MaxValue + 1).ToString("0"), out var _));
    }

}
