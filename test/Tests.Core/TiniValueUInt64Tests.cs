using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueUInt64Tests {

    [Fact(DisplayName = "TiniValueUInt64: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueUInt64.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueUInt64.Parse(text));
    }

    [Fact(DisplayName = "TiniValueUInt64: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueUInt64.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueUInt64.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueUInt64: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueUInt64.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueUInt64.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueUInt64.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueUInt64.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
    }

}
