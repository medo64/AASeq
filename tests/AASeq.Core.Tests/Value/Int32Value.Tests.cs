using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class Int32Value_Tests {

    [TestMethod]
    public void Int32Value_Basic() {
        var text = "42";
        Assert.IsTrue(Int32Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
        Assert.AreEqual(result, Int32.Parse(text));
    }

    [TestMethod]
    public void Int32Value_FailedParse() {
        Assert.IsFalse(Int32Value.TryParse("A", out var _));
    }

    [TestMethod]
    public void Int32Value_OutOfRange() {
        Assert.IsTrue(Int32Value.TryParse(((decimal)int.MinValue).ToString("0"), out var _));
        Assert.IsFalse(Int32Value.TryParse(((decimal)int.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(Int32Value.TryParse(((decimal)int.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(Int32Value.TryParse(((decimal)int.MaxValue + 1).ToString("0"), out var _));
    }

}
