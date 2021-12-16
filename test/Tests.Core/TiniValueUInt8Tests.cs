using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueUInt8Tests {

    [Fact(DisplayName = "TiniValueUInt8: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueUInt8.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueUInt8.Parse(text));
    }

    [Fact(DisplayName = "TiniValueUInt8: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueUInt8.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueUInt8.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueUInt8: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueUInt8.TryParse(((decimal)byte.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueUInt8.TryParse(((decimal)byte.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueUInt8.TryParse(((decimal)byte.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueUInt8.TryParse(((decimal)byte.MaxValue + 1).ToString("0"), out var _));
    }

}
