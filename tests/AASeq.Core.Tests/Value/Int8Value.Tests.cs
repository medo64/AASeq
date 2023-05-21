using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class Int8Value_Tests {

    [TestMethod]
    public void Int8Value_Basic() {
        var text = "42";
        Assert.IsTrue(Int8Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, Int8Value.Parse(text));
        Assert.AreEqual(result, Int8Value.Parse(text));
        Assert.AreEqual(result, SByte.Parse(text));
    }

    [TestMethod]
    public void Int8Value_FailedParse() {
        Assert.IsFalse(Int8Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            Int8Value.Parse("A");
        });
    }

    [TestMethod]
    public void Int8Value_OutOfRange() {
        Assert.IsTrue(Int8Value.TryParse(((decimal)sbyte.MinValue).ToString("0"), out var _));
        Assert.IsFalse(Int8Value.TryParse(((decimal)sbyte.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(Int8Value.TryParse(((decimal)sbyte.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(Int8Value.TryParse(((decimal)sbyte.MaxValue + 1).ToString("0"), out var _));
    }

}
