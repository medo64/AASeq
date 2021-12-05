using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniUInt16ValueTests {

    [Fact(DisplayName = "TiniUInt16Value: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniUInt16Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniUInt16Value.Parse(text));
    }

    [Fact(DisplayName = "TiniUInt16Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniUInt16Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniUInt16Value.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniUInt16Value: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniUInt16Value.TryParse(((decimal)ushort.MinValue).ToString("0"), out var _));
        Assert.False(TiniUInt16Value.TryParse(((decimal)ushort.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniUInt16Value.TryParse(((decimal)ushort.MaxValue).ToString("0"), out var _));
        Assert.False(TiniUInt16Value.TryParse(((decimal)ushort.MaxValue + 1).ToString("0"), out var _));
    }

}
