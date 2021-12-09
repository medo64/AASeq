using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniDurationValueTests {

    [Fact(DisplayName = "TiniDurationValue: Basic")]
    public void Basic() {
        var text = "1.23:11:23.638";
        Assert.True(TiniDurationValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniDurationValue.Parse(text));
    }

    [Fact(DisplayName = "TiniDurationValue: Days, Hours, Minutes and Seconds")]
    public void DaysHoursMinutesAndSeconds() {
        Assert.Equal("6.02:11:23", TiniDurationValue.Parse("6.02:11:23"));
        Assert.Equal("6.02:11:23", TiniDurationValue.Parse("6.2:11:23"));
        Assert.Equal("6.02:11:23.548", TiniDurationValue.Parse("6.2:11:23.548"));
        Assert.Equal("6.23:11:23.5481121", TiniDurationValue.Parse("06.23:11:23.5481121"));

        Assert.Equal("6.02:11:23", TiniDurationValue.Parse("6d 2h 11m 23s"));
        Assert.Equal("6.02:11:23", TiniDurationValue.Parse("6d2h11m23"));
        Assert.Equal("6.02:11:23.548", TiniDurationValue.Parse("6d2h11m23s548ms"));
        Assert.Equal("6.23:11:23.5481121", TiniDurationValue.Parse("06d 23h 11m 23s 548ms 112.1us"));
    }

    [Fact(DisplayName = "TiniDurationValue: Hours, Minutes and Seconds")]
    public void HoursMinutesAndSeconds() {
        Assert.Equal("02:11:23", TiniDurationValue.Parse("2:11:23"));
        Assert.Equal("02:11:23", TiniDurationValue.Parse("02:11:23"));
        Assert.Equal("02:11:23.1", TiniDurationValue.Parse("2:11:23.10"));
        Assert.Equal("02:11:23.548", TiniDurationValue.Parse("2:11:23.548"));
        Assert.Equal("23:11:23.5481121", TiniDurationValue.Parse("23:11:23.5481121"));

        Assert.Equal("02:11:23", TiniDurationValue.Parse("2h 11m 23s"));
        Assert.Equal("02:11:23", TiniDurationValue.Parse("02h11m23s"));
        Assert.Equal("02:11:23.1", TiniDurationValue.Parse("2h11m23s100ms"));
        Assert.Equal("02:11:23.548", TiniDurationValue.Parse("2h 11m 23s 548ms"));
        Assert.Equal("23:11:23.5481121", TiniDurationValue.Parse("23h11m23s548112100ns"));
    }

    [Fact(DisplayName = "TiniDurationValue: Minutes and Seconds")]
    public void MinutesAndSeconds() {
        Assert.Equal("00:11:23", TiniDurationValue.Parse("11:23"));
        Assert.Equal("00:11:23.548", TiniDurationValue.Parse("11:23.548"));
        Assert.Equal("00:11:23.5481121", TiniDurationValue.Parse("11:23.5481121"));

        Assert.Equal("00:11:23", TiniDurationValue.Parse("11m23s"));
        Assert.Equal("00:11:23.548", TiniDurationValue.Parse("11m23.548s"));
        Assert.Equal("00:11:23.548", TiniDurationValue.Parse("11m23s548ms"));
        Assert.Equal("00:11:23.5481121", TiniDurationValue.Parse("11m23.5481121s"));
        Assert.Equal("00:11:23.5481121", TiniDurationValue.Parse("11m23s548112100ns"));
    }

    [Fact(DisplayName = "TiniDurationValue: Seconds-only")]
    public void SecondsOnly() {
        Assert.Equal("00:00:00", TiniDurationValue.Parse("0"));
        Assert.Equal("00:00:01", TiniDurationValue.Parse("1"));
        Assert.Equal("00:01:01", TiniDurationValue.Parse("61"));
        Assert.Equal("00:00:00.121", TiniDurationValue.Parse("0.121"));
        Assert.Equal("00:00:00.1218899", TiniDurationValue.Parse("0.1218899"));

        Assert.Equal("00:00:00", TiniDurationValue.Parse("0s"));
        Assert.Equal("00:00:01", TiniDurationValue.Parse("1s"));
        Assert.Equal("00:01:01", TiniDurationValue.Parse("61s"));
        Assert.Equal("00:00:00.121", TiniDurationValue.Parse("0.121s"));
        Assert.Equal("00:00:00.121", TiniDurationValue.Parse("0s121ms"));
        Assert.Equal("00:00:00.121", TiniDurationValue.Parse("121ms"));
        Assert.Equal("00:00:00.1218899", TiniDurationValue.Parse("0.1218899s"));
        Assert.Equal("00:00:00.1218899", TiniDurationValue.Parse("0s121889900ns"));
        Assert.Equal("00:00:00.1218899", TiniDurationValue.Parse("121889900ns"));
        Assert.Equal("00:00:00.1218899", TiniDurationValue.Parse("121ms889us900ns"));
    }

}
