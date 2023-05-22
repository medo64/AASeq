using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class DateTimeValue_Tests {

    [TestMethod]
    public void DateTimeValue_Basic() {
        var text = "2021-01-14 15:23:55.4523567 +00:00";
        Assert.IsTrue(DateTimeValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
    }

    [TestMethod]
    public void DateTimeValue_FailedParse() {
        Assert.IsFalse(DateTimeValue.TryParse("A", out var _));
    }

    [TestMethod]
    public void DateTimeValue_Iso8601() {
        Assert.IsTrue(DateTimeValue.TryParse("2021-01-14T19:22:44+01:30", out var result));
        Assert.AreEqual("2021-01-14 19:22:44 +01:30", result.ToString());
    }

    [TestMethod]
    public void DateTimeValue_Iso8601Utc() {
        Assert.IsTrue(DateTimeValue.TryParse("20210114T192244Z", out var result));
        Assert.AreEqual("2021-01-14 19:22:44 +00:00", result.ToString());
    }

    [TestMethod]
    public void DateTimeValue_DateOnly() {
        Assert.IsTrue(DateTimeValue.TryParse("2021-01-14", out var result));
        Assert.AreEqual("2021-01-14 00:00:00", result.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

    [TestMethod]
    public void DateTimeValue_TimeOnly() {
        Assert.IsTrue(DateTimeValue.TryParse("23:59:59", out var result));
        Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59", result.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

}
