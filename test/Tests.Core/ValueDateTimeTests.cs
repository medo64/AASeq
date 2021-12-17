using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueDateTimeTests {

    [Fact(DisplayName = "ValueDateTime: Basic")]
    public void Basic() {
        var text = "2021-01-14 15:23:55.4523567 +00:00";
        Assert.True(ValueDateTime.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueDateTime.Parse(text));
    }

    [Fact(DisplayName = "ValueDateTime: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueDateTime.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueDateTime.Parse("A");
        });
    }
    [Fact(DisplayName = "ValueDateTime: ISO8601")]
    public void Iso8601() {
        Assert.Equal("2021-01-14 19:22:44 +01:30", ValueDateTime.Parse("2021-01-14T19:22:44+01:30"));
    }

    [Fact(DisplayName = "ValueDateTime: ISO8601 UTC")]
    public void Iso8601Utc() {
        Assert.Equal("2021-01-14 19:22:44 +00:00", ValueDateTime.Parse("20210114T192244Z"));
    }

    [Fact(DisplayName = "ValueDateTime: Date-only")]
    public void DateOnly() {
        Assert.Equal("2021-01-14 00:00:00", ValueDateTime.Parse("2021-01-14").ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

    [Fact(DisplayName = "ValueDateTime: Time-only")]
    public void TimeOnly() {
        Assert.Equal(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59", ValueDateTime.Parse("23:59:59").ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

}
