using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniDateTimeValueTests {

    [Fact(DisplayName = "TiniDateTimeValue: Basic")]
    public void Basic() {
        var text = "2021-01-14 15:23:55.4523567 +00:00";
        Assert.True(TiniDateTimeValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniDateTimeValue.Parse(text));
    }

    [Fact(DisplayName = "TiniDateTimeValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniDateTimeValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniDateTimeValue.Parse("A");
        });
    }
    [Fact(DisplayName = "TiniDateTimeValue: ISO8601")]
    public void Iso8601() {
        Assert.Equal("2021-01-14 19:22:44 +01:30", TiniDateTimeValue.Parse("2021-01-14T19:22:44+01:30"));
    }

    [Fact(DisplayName = "TiniDateTimeValue: ISO8601 UTC")]
    public void Iso8601Utc() {
        Assert.Equal("2021-01-14 19:22:44 +00:00", TiniDateTimeValue.Parse("20210114T192244Z"));
    }

    [Fact(DisplayName = "TiniDateTimeValue: Date-only")]
    public void DateOnly() {
        Assert.Equal("2021-01-14 00:00:00", TiniDateTimeValue.Parse("2021-01-14").ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

    [Fact(DisplayName = "TiniDateTimeValue: Time-only")]
    public void TimeOnly() {
        Assert.Equal(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59", TiniDateTimeValue.Parse("23:59:59").ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }

}
