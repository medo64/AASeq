using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class Int16Value_Tests {

    [TestMethod]
    public void Int16Value_Basic() {
        var text = "42";
        Assert.IsTrue(Int16Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
        Assert.AreEqual(result, Int16.Parse(text));
    }

    [TestMethod]
    public void Int16Value_FailedParse() {
        Assert.IsFalse(Int16Value.TryParse("A", out var _));
    }

    [TestMethod]
    public void Int16Value_OutOfRange() {
        Assert.IsTrue(Int16Value.TryParse(((decimal)short.MinValue).ToString("0"), out var _));
        Assert.IsFalse(Int16Value.TryParse(((decimal)short.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(Int16Value.TryParse(((decimal)short.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(Int16Value.TryParse(((decimal)short.MaxValue + 1).ToString("0"), out var _));
    }

}
