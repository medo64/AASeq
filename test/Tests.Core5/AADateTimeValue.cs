using System;
using System.Globalization;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AADateTimeValue_Tests {

    [TestMethod]
    public void AADateTimeValue_Basic() {
        var text = "2021-01-14 15:23:55.4523567 +00:00";
        Assert.IsTrue(AADateTimeValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AADateTimeValue.Parse(text));
        Assert.AreEqual(result, AADateTimeValue.Parse(text));
        Assert.AreEqual(result, DateTimeOffset.Parse(text));
        Assert.AreEqual(result, DateTime.Parse(text));
    }

    [TestMethod]
    public void AADateTimeValue_FailedParse() {
        Assert.IsFalse(AADateTimeValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AADateTimeValue.Parse("A");
        });
    }

    [TestMethod]
    public void AADateTimeValue_Iso8601() {
        Assert.AreEqual("2021-01-14 19:22:44 +01:30", AADateTimeValue.Parse("2021-01-14T19:22:44+01:30"));
    }

    [TestMethod]
    public void AADateTimeValue_Iso8601Utc() {
        Assert.AreEqual("2021-01-14 19:22:44 +00:00", AADateTimeValue.Parse("20210114T192244Z"));
    }

    [TestMethod]
    public void AADateTimeValue_DateOnly() {
        Assert.AreEqual("2021-01-14 00:00:00", AADateTimeValue.Parse("2021-01-14").ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

    [TestMethod]
    public void AADateTimeValue_TimeOnly() {
        Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59", AADateTimeValue.Parse("23:59:59").ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

}
