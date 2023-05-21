using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class TimeValue_Tests {

    [TestMethod]
    public void TimeValue_Basic() {
        var text = "23:20:59";
        Assert.IsTrue(TimeValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, TimeValue.Parse(text));
        Assert.AreEqual(result, TimeValue.Parse(text));
        Assert.AreEqual(result, TimeOnly.Parse(text));
    }

    [TestMethod]
    public void TimeValue_FailedParse() {
        Assert.IsFalse(TimeValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            TimeValue.Parse("A");
        });
    }

    [TestMethod]
    public void TimeValue_MinutesOnly() {
        Assert.AreEqual("23:20:00", TimeValue.Parse("23:20"));
    }

    [TestMethod]
    public void TimeValue_Milliseconds() {
        Assert.AreEqual("23:23:12.564", TimeValue.Parse("23:23:12.564"));
    }

    [TestMethod]
    public void TimeValue_Nanos() {
        Assert.AreEqual("23:23:12.5643442", TimeValue.Parse("23:23:12.5643442"));
    }

    [TestMethod]
    public void TimeValue_NanosWithZeroes() {
        Assert.AreEqual("23:23:12.56434", TimeValue.Parse("23:23:12.5643400"));
        Assert.AreEqual("23:23:12", TimeValue.Parse("23:23:12.0000000"));
    }

}
