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
        Assert.AreEqual(text, result);
        Assert.AreEqual(result, TimeOnly.Parse(text));
    }

    [TestMethod]
    public void TimeValue_FailedParse() {
        Assert.IsFalse(TimeValue.TryParse("A", out var _));
    }

    [TestMethod]
    public void TimeValue_MinutesOnly() {
        Assert.IsTrue(TimeValue.TryParse("23:20", out var result));
        Assert.AreEqual("23:20:00", result);
    }

    [TestMethod]
    public void TimeValue_Milliseconds() {
        Assert.IsTrue(TimeValue.TryParse("23:23:12.564", out var result));
        Assert.AreEqual("23:23:12.564", result);
    }

    [TestMethod]
    public void TimeValue_Nanos() {
        Assert.IsTrue(TimeValue.TryParse("23:23:12.5643442", out var result));
        Assert.AreEqual("23:23:12.5643442", result);
    }

    [TestMethod]
    public void TimeValue_NanosWithZeroes() {
        {
            Assert.IsTrue(TimeValue.TryParse("23:23:12.5643400", out var result));
            Assert.AreEqual("23:23:12.56434", result);
        }
        {
            Assert.IsTrue(TimeValue.TryParse("23:23:12.0000000", out var result));
            Assert.AreEqual("23:23:12", result);
        }
    }

}
