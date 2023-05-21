using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class DurationValue_Tests {

    [TestMethod]
    public void DurationValue_Basic() {
        var text = "1.23:11:23.638";
        Assert.IsTrue(DurationValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, DurationValue.Parse(text));
        Assert.AreEqual(result, DurationValue.Parse(text));
        Assert.AreEqual(result, TimeSpan.Parse(text));
    }

    [TestMethod]
    public void DurationValue_DaysHoursMinutesAndSeconds() {
        Assert.AreEqual("6.02:11:23", DurationValue.Parse("6.02:11:23"));
        Assert.AreEqual("6.02:11:23", DurationValue.Parse("6.2:11:23"));
        Assert.AreEqual("6.02:11:23.548", DurationValue.Parse("6.2:11:23.548"));
        Assert.AreEqual("6.23:11:23.5481121", DurationValue.Parse("06.23:11:23.5481121"));

        Assert.AreEqual("6.02:11:23", DurationValue.Parse("6d 2h 11m 23s"));
        Assert.AreEqual("6.02:11:23", DurationValue.Parse("6d2h11m23"));
        Assert.AreEqual("6.02:11:23.548", DurationValue.Parse("6d2h11m23s548ms"));
        Assert.AreEqual("6.23:11:23.5481121", DurationValue.Parse("06d 23h 11m 23s 548ms 112.1us"));
    }

    [TestMethod]
    public void DurationValue_HoursMinutesAndSeconds() {
        Assert.AreEqual("02:11:23", DurationValue.Parse("2:11:23"));
        Assert.AreEqual("02:11:23", DurationValue.Parse("02:11:23"));
        Assert.AreEqual("02:11:23.1", DurationValue.Parse("2:11:23.10"));
        Assert.AreEqual("02:11:23.548", DurationValue.Parse("2:11:23.548"));
        Assert.AreEqual("23:11:23.5481121", DurationValue.Parse("23:11:23.5481121"));

        Assert.AreEqual("02:11:23", DurationValue.Parse("2h 11m 23s"));
        Assert.AreEqual("02:11:23", DurationValue.Parse("02h11m23s"));
        Assert.AreEqual("02:11:23.1", DurationValue.Parse("2h11m23s100ms"));
        Assert.AreEqual("02:11:23.548", DurationValue.Parse("2h 11m 23s 548ms"));
        Assert.AreEqual("23:11:23.5481121", DurationValue.Parse("23h11m23s548112100ns"));
    }

    [TestMethod]
    public void DurationValue_MinutesAndSeconds() {
        Assert.AreEqual("00:11:23", DurationValue.Parse("11:23"));
        Assert.AreEqual("00:11:23.548", DurationValue.Parse("11:23.548"));
        Assert.AreEqual("00:11:23.5481121", DurationValue.Parse("11:23.5481121"));

        Assert.AreEqual("00:11:23", DurationValue.Parse("11m23s"));
        Assert.AreEqual("00:11:23.548", DurationValue.Parse("11m23.548s"));
        Assert.AreEqual("00:11:23.548", DurationValue.Parse("11m23s548ms"));
        Assert.AreEqual("00:11:23.5481121", DurationValue.Parse("11m23.5481121s"));
        Assert.AreEqual("00:11:23.5481121", DurationValue.Parse("11m23s548112100ns"));
    }

    [TestMethod]
    public void DurationValue_SecondsOnly() {
        Assert.AreEqual("00:00:00", DurationValue.Parse("0"));
        Assert.AreEqual("00:00:01", DurationValue.Parse("1"));
        Assert.AreEqual("00:01:01", DurationValue.Parse("61"));
        Assert.AreEqual("00:00:00.121", DurationValue.Parse("0.121"));
        Assert.AreEqual("00:00:00.1218899", DurationValue.Parse("0.1218899"));

        Assert.AreEqual("00:00:00", DurationValue.Parse("0s"));
        Assert.AreEqual("00:00:01", DurationValue.Parse("1s"));
        Assert.AreEqual("00:01:01", DurationValue.Parse("61s"));
        Assert.AreEqual("00:00:00.121", DurationValue.Parse("0.121s"));
        Assert.AreEqual("00:00:00.121", DurationValue.Parse("0s121ms"));
        Assert.AreEqual("00:00:00.121", DurationValue.Parse("121ms"));
        Assert.AreEqual("00:00:00.1218899", DurationValue.Parse("0.1218899s"));
        Assert.AreEqual("00:00:00.1218899", DurationValue.Parse("0s121889900ns"));
        Assert.AreEqual("00:00:00.1218899", DurationValue.Parse("121889900ns"));
        Assert.AreEqual("00:00:00.1218899", DurationValue.Parse("121ms889us900ns"));
    }

}
