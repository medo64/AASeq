using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class DateValue_Tests {

    [TestMethod]
    public void DateValue_Basic() {
        var text = "2021-01-14";
        Assert.IsTrue(DateValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, DateValue.Parse(text));
        Assert.AreEqual(result, DateValue.Parse(text));
        Assert.AreEqual(result, DateOnly.Parse(text));
    }

    [TestMethod]
    public void DateValue_FailedParse() {
        Assert.IsFalse(DateValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            DateValue.Parse("A");
        });
    }

}
