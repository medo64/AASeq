using System;
using System.Net;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAInt8Value_Tests {

    [TestMethod]
    public void AAInt8Value_Basic() {
        var text = "42";
        Assert.IsTrue(AAInt8Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAInt8Value.Parse(text));
        Assert.AreEqual(result, AAInt8Value.Parse(text));
        Assert.AreEqual(result, SByte.Parse(text));
    }

    [TestMethod]
    public void AAInt8Value_FailedParse() {
        Assert.IsFalse(AAInt8Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAInt8Value.Parse("A");
        });
    }

    [TestMethod]
    public void AAInt8Value_OutOfRange() {
        Assert.IsTrue(AAInt8Value.TryParse(((decimal)sbyte.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AAInt8Value.TryParse(((decimal)sbyte.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AAInt8Value.TryParse(((decimal)sbyte.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AAInt8Value.TryParse(((decimal)sbyte.MaxValue + 1).ToString("0"), out var _));
    }

}
