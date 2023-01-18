using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAInt32Value_Tests {

    [TestMethod]
    public void AAInt32Value_Basic() {
        var text = "42";
        Assert.IsTrue(AAInt32Value.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAInt32Value.Parse(text));
        Assert.AreEqual(result, AAInt32Value.Parse(text));
        Assert.AreEqual(result, Int32.Parse(text));
    }

    [TestMethod]
    public void AAInt32Value_FailedParse() {
        Assert.IsFalse(AAInt32Value.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAInt32Value.Parse("A");
        });
    }

    [TestMethod]
    public void AAInt32Value_OutOfRange() {
        Assert.IsTrue(AAInt32Value.TryParse(((decimal)int.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AAInt32Value.TryParse(((decimal)int.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AAInt32Value.TryParse(((decimal)int.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AAInt32Value.TryParse(((decimal)int.MaxValue + 1).ToString("0"), out var _));
    }

}
