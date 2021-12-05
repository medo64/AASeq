using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniUInt32ValueTests {

    [Fact(DisplayName = "TiniUInt32Value: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniUInt32Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniUInt32Value.Parse(text));
    }

    [Fact(DisplayName = "TiniUInt32Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniUInt32Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniUInt32Value.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniUInt32Value: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniUInt32Value.TryParse(((decimal)uint.MinValue).ToString("0"), out var _));
        Assert.False(TiniUInt32Value.TryParse(((decimal)uint.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniUInt32Value.TryParse(((decimal)uint.MaxValue).ToString("0"), out var _));
        Assert.False(TiniUInt32Value.TryParse(((decimal)uint.MaxValue + 1).ToString("0"), out var _));
    }

}
