using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAUInt32Value_Tests {

    [TestMethod]
    public void AAUInt32Value_Basic() {
        var text = "42";
        Assert.IsTrue(AAUInt32Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAUInt32Value.Parse(text));
    }

    [TestMethod]
    public void AAUInt32Value_FailedParse() {
        Assert.IsFalse(AAUInt32Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAUInt32Value.Parse("A");
        });
    }

    [TestMethod]
    public void AAUInt32Value_OutOfRange() {
        Assert.IsTrue(AAUInt32Value.TryParse(((decimal)uint.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AAUInt32Value.TryParse(((decimal)uint.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AAUInt32Value.TryParse(((decimal)uint.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AAUInt32Value.TryParse(((decimal)uint.MaxValue + 1).ToString("0"), out var _));
    }

}
