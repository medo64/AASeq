using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAInt16Value_Tests {

    [TestMethod]
    public void AAInt16Value_Basic() {
        var text = "42";
        Assert.IsTrue(AAInt16Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAInt16Value.Parse(text));
        Assert.AreEqual(result, AAInt16Value.Parse(text));
        Assert.AreEqual(result, Int16.Parse(text));
    }

    [TestMethod]
    public void AAInt16Value_FailedParse() {
        Assert.IsFalse(AAInt16Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAInt16Value.Parse("A");
        });
    }

    [TestMethod]
    public void AAInt16Value_OutOfRange() {
        Assert.IsTrue(AAInt16Value.TryParse(((decimal)short.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AAInt16Value.TryParse(((decimal)short.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AAInt16Value.TryParse(((decimal)short.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AAInt16Value.TryParse(((decimal)short.MaxValue + 1).ToString("0"), out var _));
    }

}
