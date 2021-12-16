using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueUInt32Tests {

    [Fact(DisplayName = "TiniValueUInt32: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueUInt32.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueUInt32.Parse(text));
    }

    [Fact(DisplayName = "TiniValueUInt32: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueUInt32.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueUInt32.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueUInt32: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueUInt32.TryParse(((decimal)uint.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueUInt32.TryParse(((decimal)uint.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueUInt32.TryParse(((decimal)uint.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueUInt32.TryParse(((decimal)uint.MaxValue + 1).ToString("0"), out var _));
    }

}
