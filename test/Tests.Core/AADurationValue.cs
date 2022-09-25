using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AADurationValue_Tests {

    [TestMethod]
    public void AADurationValue_Basic() {
        var text = "1.23:11:23.638";
        Assert.IsTrue(AADurationValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AADurationValue.Parse(text));
    }

    [TestMethod]
    public void AADurationValue_DaysHoursMinutesAndSeconds() {
        Assert.AreEqual("6.02:11:23", AADurationValue.Parse("6.02:11:23"));
        Assert.AreEqual("6.02:11:23", AADurationValue.Parse("6.2:11:23"));
        Assert.AreEqual("6.02:11:23.548", AADurationValue.Parse("6.2:11:23.548"));
        Assert.AreEqual("6.23:11:23.5481121", AADurationValue.Parse("06.23:11:23.5481121"));

        Assert.AreEqual("6.02:11:23", AADurationValue.Parse("6d 2h 11m 23s"));
        Assert.AreEqual("6.02:11:23", AADurationValue.Parse("6d2h11m23"));
        Assert.AreEqual("6.02:11:23.548", AADurationValue.Parse("6d2h11m23s548ms"));
        Assert.AreEqual("6.23:11:23.5481121", AADurationValue.Parse("06d 23h 11m 23s 548ms 112.1us"));
    }

    [TestMethod]
    public void AADurationValue_HoursMinutesAndSeconds() {
        Assert.AreEqual("02:11:23", AADurationValue.Parse("2:11:23"));
        Assert.AreEqual("02:11:23", AADurationValue.Parse("02:11:23"));
        Assert.AreEqual("02:11:23.1", AADurationValue.Parse("2:11:23.10"));
        Assert.AreEqual("02:11:23.548", AADurationValue.Parse("2:11:23.548"));
        Assert.AreEqual("23:11:23.5481121", AADurationValue.Parse("23:11:23.5481121"));

        Assert.AreEqual("02:11:23", AADurationValue.Parse("2h 11m 23s"));
        Assert.AreEqual("02:11:23", AADurationValue.Parse("02h11m23s"));
        Assert.AreEqual("02:11:23.1", AADurationValue.Parse("2h11m23s100ms"));
        Assert.AreEqual("02:11:23.548", AADurationValue.Parse("2h 11m 23s 548ms"));
        Assert.AreEqual("23:11:23.5481121", AADurationValue.Parse("23h11m23s548112100ns"));
    }

    [TestMethod]
    public void AADurationValue_MinutesAndSeconds() {
        Assert.AreEqual("00:11:23", AADurationValue.Parse("11:23"));
        Assert.AreEqual("00:11:23.548", AADurationValue.Parse("11:23.548"));
        Assert.AreEqual("00:11:23.5481121", AADurationValue.Parse("11:23.5481121"));

        Assert.AreEqual("00:11:23", AADurationValue.Parse("11m23s"));
        Assert.AreEqual("00:11:23.548", AADurationValue.Parse("11m23.548s"));
        Assert.AreEqual("00:11:23.548", AADurationValue.Parse("11m23s548ms"));
        Assert.AreEqual("00:11:23.5481121", AADurationValue.Parse("11m23.5481121s"));
        Assert.AreEqual("00:11:23.5481121", AADurationValue.Parse("11m23s548112100ns"));
    }

    [TestMethod]
    public void AADurationValue_SecondsOnly() {
        Assert.AreEqual("00:00:00", AADurationValue.Parse("0"));
        Assert.AreEqual("00:00:01", AADurationValue.Parse("1"));
        Assert.AreEqual("00:01:01", AADurationValue.Parse("61"));
        Assert.AreEqual("00:00:00.121", AADurationValue.Parse("0.121"));
        Assert.AreEqual("00:00:00.1218899", AADurationValue.Parse("0.1218899"));

        Assert.AreEqual("00:00:00", AADurationValue.Parse("0s"));
        Assert.AreEqual("00:00:01", AADurationValue.Parse("1s"));
        Assert.AreEqual("00:01:01", AADurationValue.Parse("61s"));
        Assert.AreEqual("00:00:00.121", AADurationValue.Parse("0.121s"));
        Assert.AreEqual("00:00:00.121", AADurationValue.Parse("0s121ms"));
        Assert.AreEqual("00:00:00.121", AADurationValue.Parse("121ms"));
        Assert.AreEqual("00:00:00.1218899", AADurationValue.Parse("0.1218899s"));
        Assert.AreEqual("00:00:00.1218899", AADurationValue.Parse("0s121889900ns"));
        Assert.AreEqual("00:00:00.1218899", AADurationValue.Parse("121889900ns"));
        Assert.AreEqual("00:00:00.1218899", AADurationValue.Parse("121ms889us900ns"));
    }

}
