using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class DateTimeValue_Tests {

    [TestMethod]
    public void DateTimeValue_Basic() {
        var text = "2021-01-14 15:23:55.4523567 +00:00";
        Assert.IsTrue(DateTimeValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, DateTimeValue.Parse(text));
        Assert.AreEqual(result, DateTimeValue.Parse(text));
        Assert.AreEqual(result, DateTimeOffset.Parse(text));
        Assert.AreEqual(result, DateTime.Parse(text));
    }

    [TestMethod]
    public void DateTimeValue_FailedParse() {
        Assert.IsFalse(DateTimeValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            DateTimeValue.Parse("A");
        });
    }

    [TestMethod]
    public void DateTimeValue_Iso8601() {
        Assert.AreEqual("2021-01-14 19:22:44 +01:30", DateTimeValue.Parse("2021-01-14T19:22:44+01:30"));
    }

    [TestMethod]
    public void DateTimeValue_Iso8601Utc() {
        Assert.AreEqual("2021-01-14 19:22:44 +00:00", DateTimeValue.Parse("20210114T192244Z"));
    }

    [TestMethod]
    public void DateTimeValue_DateOnly() {
        Assert.AreEqual("2021-01-14 00:00:00", DateTimeValue.Parse("2021-01-14").ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

    [TestMethod]
    public void DateTimeValue_TimeOnly() {
        Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59", DateTimeValue.Parse("23:59:59").ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

}
