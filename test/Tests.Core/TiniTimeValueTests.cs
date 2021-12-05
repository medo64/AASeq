using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniTimeValueTests {

    [Fact(DisplayName = "TiniTimeValue: Basic")]
    public void Basic() {
        var text = "23:20:59";
        Assert.True(TiniTimeValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniTimeValue.Parse(text));
    }

    [Fact(DisplayName = "TiniTimeValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniTimeValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniTimeValue.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniTimeValue: Minutes-only")]
    public void MinutesOnly() {
        Assert.Equal("23:20:00", TiniTimeValue.Parse("23:20"));
    }

    [Fact(DisplayName = "TiniTimeValue: Milliseconds")]
    public void Milliseconds() {
        Assert.Equal("23:23:12.564", TiniTimeValue.Parse("23:23:12.564"));
    }

    [Fact(DisplayName = "TiniTimeValue: Nanos")]
    public void Nanos() {
        Assert.Equal("23:23:12.5643442", TiniTimeValue.Parse("23:23:12.5643442"));
    }

    [Fact(DisplayName = "TiniTimeValue: Nanos With Zeroes")]
    public void NanosWithZeroes() {
        Assert.Equal("23:23:12.56434", TiniTimeValue.Parse("23:23:12.5643400"));
        Assert.Equal("23:23:12", TiniTimeValue.Parse("23:23:12.0000000"));
    }

}
