using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniUInt8ValueTests {

    [Fact(DisplayName = "TiniUInt8Value: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniUInt8Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniUInt8Value.Parse(text));
    }

    [Fact(DisplayName = "TiniInt8Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniUInt8Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniUInt8Value.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniInt8Value: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniUInt8Value.TryParse(((decimal)byte.MinValue).ToString("0"), out var _));
        Assert.False(TiniUInt8Value.TryParse(((decimal)byte.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniUInt8Value.TryParse(((decimal)byte.MaxValue).ToString("0"), out var _));
        Assert.False(TiniUInt8Value.TryParse(((decimal)byte.MaxValue + 1).ToString("0"), out var _));
    }

}
