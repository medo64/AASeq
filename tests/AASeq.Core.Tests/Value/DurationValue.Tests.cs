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
        Assert.AreEqual(text, result);
    }

    [TestMethod]
    public void DurationValue_DaysHoursMinutesAndSeconds() {
        {
            Assert.IsTrue(DurationValue.TryParse("6.02:11:23", out var result));
            Assert.AreEqual("6.02:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("6.2:11:23", out var result));
            Assert.AreEqual("6.02:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("6.2:11:23.548", out var result));
            Assert.AreEqual("6.02:11:23.548", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("06.23:11:23.5481121", out var result));
            Assert.AreEqual("6.23:11:23.5481121", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("6d 2h 11m 23s", out var result));
            Assert.AreEqual("6.02:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("6d2h11m23", out var result));
            Assert.AreEqual("6.02:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("6d2h11m23s548ms", out var result));
            Assert.AreEqual("6.02:11:23.548", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("06d 23h 11m 23s 548ms 112.1us", out var result));
            Assert.AreEqual("6.23:11:23.5481121", result);
        }
    }

    [TestMethod]
    public void DurationValue_HoursMinutesAndSeconds() {
        {
            Assert.IsTrue(DurationValue.TryParse("2:11:23", out var result));
            Assert.AreEqual("02:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("02:11:23", out var result));
            Assert.AreEqual("02:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("2:11:23.10", out var result));
            Assert.AreEqual("02:11:23.1", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("2:11:23.548", out var result));
            Assert.AreEqual("02:11:23.548", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("23:11:23.5481121", out var result));
            Assert.AreEqual("23:11:23.5481121", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("2h 11m 23s", out var result));
            Assert.AreEqual("02:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("02h11m23s", out var result));
            Assert.AreEqual("02:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("2h11m23s100ms", out var result));
            Assert.AreEqual("02:11:23.1", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("2h 11m 23s 548ms", out var result));
            Assert.AreEqual("02:11:23.548", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("23h11m23s548112100ns", out var result));
            Assert.AreEqual("23:11:23.5481121", result);
        }
    }

    [TestMethod]
    public void DurationValue_MinutesAndSeconds() {
        {
            Assert.IsTrue(DurationValue.TryParse("11:23", out var result));
            Assert.AreEqual("00:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("11:23.548", out var result));
            Assert.AreEqual("00:11:23.548", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("11:23.5481121", out var result));
            Assert.AreEqual("00:11:23.5481121", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("11m23s", out var result));
            Assert.AreEqual("00:11:23", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("11m23.548s", out var result));
            Assert.AreEqual("00:11:23.548", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("11m23s548ms", out var result));
            Assert.AreEqual("00:11:23.548", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("11m23.5481121s", out var result));
            Assert.AreEqual("00:11:23.5481121", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("11m23s548112100ns", out var result));
            Assert.AreEqual("00:11:23.5481121", result);
        }
    }

    [TestMethod]
    public void DurationValue_SecondsOnly() {
        {
            Assert.IsTrue(DurationValue.TryParse("0", out var result));
            Assert.AreEqual("00:00:00", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("1", out var result));
            Assert.AreEqual("00:00:01", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("61", out var result));
            Assert.AreEqual("00:01:01", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("0.121", out var result));
            Assert.AreEqual("00:00:00.121", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("0.1218899", out var result));
            Assert.AreEqual("00:00:00.1218899", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("0s", out var result));
            Assert.AreEqual("00:00:00", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("1s", out var result));
            Assert.AreEqual("00:00:01", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("61s", out var result));
            Assert.AreEqual("00:01:01", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("0.121s", out var result));
            Assert.AreEqual("00:00:00.121", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("0s121ms", out var result));
            Assert.AreEqual("00:00:00.121", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("121ms", out var result));
            Assert.AreEqual("00:00:00.121", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("0.1218899s", out var result));
            Assert.AreEqual("00:00:00.1218899", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("0s121889900ns", out var result));
            Assert.AreEqual("00:00:00.1218899", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("121889900ns", out var result));
            Assert.AreEqual("00:00:00.1218899", result);
        }
        {
            Assert.IsTrue(DurationValue.TryParse("121ms889us900ns", out var result));
            Assert.AreEqual("00:00:00.1218899", result);
        }
    }

}
