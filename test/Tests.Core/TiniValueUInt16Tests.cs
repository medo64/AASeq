using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueUInt16Tests {

    [Fact(DisplayName = "TiniValueUInt16: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueUInt16.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueUInt16.Parse(text));
    }

    [Fact(DisplayName = "TiniValueUInt16: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueUInt16.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueUInt16.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueUInt16: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueUInt16.TryParse(((decimal)ushort.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueUInt16.TryParse(((decimal)ushort.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueUInt16.TryParse(((decimal)ushort.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueUInt16.TryParse(((decimal)ushort.MaxValue + 1).ToString("0"), out var _));
    }

}
