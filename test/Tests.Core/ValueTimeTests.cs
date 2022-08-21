using System;
using AASeq;
using Xunit;

namespace Tests.Core;

public class ValueTimeTests {

    [Fact(DisplayName = "TimeValue: Basic")]
    public void Basic() {
        var text = "23:20:59";
        Assert.True(ValueTime.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueTime.Parse(text));
    }

    [Fact(DisplayName = "TimeValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueTime.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueTime.Parse("A");
        });
    }

    [Fact(DisplayName = "TimeValue: Minutes-only")]
    public void MinutesOnly() {
        Assert.Equal("23:20:00", ValueTime.Parse("23:20"));
    }

    [Fact(DisplayName = "TimeValue: Milliseconds")]
    public void Milliseconds() {
        Assert.Equal("23:23:12.564", ValueTime.Parse("23:23:12.564"));
    }

    [Fact(DisplayName = "TimeValue: Nanos")]
    public void Nanos() {
        Assert.Equal("23:23:12.5643442", ValueTime.Parse("23:23:12.5643442"));
    }

    [Fact(DisplayName = "TimeValue: Nanos With Zeroes")]
    public void NanosWithZeroes() {
        Assert.Equal("23:23:12.56434", ValueTime.Parse("23:23:12.5643400"));
        Assert.Equal("23:23:12", ValueTime.Parse("23:23:12.0000000"));
    }

}
