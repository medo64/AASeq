using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class Int64Value_Tests {

    [TestMethod]
    public void Int64Value_Basic() {
        var text = "42";
        Assert.IsTrue(Int64Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, Int64Value.Parse(text));
        Assert.AreEqual(result, Int64Value.Parse(text));
        Assert.AreEqual(result, Int64.Parse(text));
    }

    [TestMethod]
    public void Int64Value_FailedParse() {
        Assert.IsFalse(Int64Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            Int64Value.Parse("A");
        });
    }

    [TestMethod]
    public void Int64Value_OutOfRange() {
        Assert.IsTrue(Int64Value.TryParse(((decimal)long.MinValue).ToString("0"), out var _));
        Assert.IsFalse(Int64Value.TryParse(((decimal)long.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(Int64Value.TryParse(((decimal)long.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(Int64Value.TryParse(((decimal)long.MaxValue + 1).ToString("0"), out var _));
    }

}
