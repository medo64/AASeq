using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAInt64Value_Tests {

    [TestMethod]
    public void AAInt64Value_Basic() {
        var text = "42";
        Assert.IsTrue(AAInt64Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAInt64Value.Parse(text));
    }

    [TestMethod]
    public void AAInt64Value_FailedParse() {
        Assert.IsFalse(AAInt64Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAInt64Value.Parse("A");
        });
    }

    [TestMethod]
    public void AAInt64Value_OutOfRange() {
        Assert.IsTrue(AAInt64Value.TryParse(((decimal)long.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AAInt64Value.TryParse(((decimal)long.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AAInt64Value.TryParse(((decimal)long.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AAInt64Value.TryParse(((decimal)long.MaxValue + 1).ToString("0"), out var _));
    }

}
