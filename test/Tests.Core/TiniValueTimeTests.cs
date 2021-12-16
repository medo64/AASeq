using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueTimeTests {

    [Fact(DisplayName = "TiniTimeValue: Basic")]
    public void Basic() {
        var text = "23:20:59";
        Assert.True(TiniValueTime.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueTime.Parse(text));
    }

    [Fact(DisplayName = "TiniTimeValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueTime.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueTime.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniTimeValue: Minutes-only")]
    public void MinutesOnly() {
        Assert.Equal("23:20:00", TiniValueTime.Parse("23:20"));
    }

    [Fact(DisplayName = "TiniTimeValue: Milliseconds")]
    public void Milliseconds() {
        Assert.Equal("23:23:12.564", TiniValueTime.Parse("23:23:12.564"));
    }

    [Fact(DisplayName = "TiniTimeValue: Nanos")]
    public void Nanos() {
        Assert.Equal("23:23:12.5643442", TiniValueTime.Parse("23:23:12.5643442"));
    }

    [Fact(DisplayName = "TiniTimeValue: Nanos With Zeroes")]
    public void NanosWithZeroes() {
        Assert.Equal("23:23:12.56434", TiniValueTime.Parse("23:23:12.5643400"));
        Assert.Equal("23:23:12", TiniValueTime.Parse("23:23:12.0000000"));
    }

}
