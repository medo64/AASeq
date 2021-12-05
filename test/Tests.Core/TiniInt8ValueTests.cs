using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniInt8ValueTests {

    [Fact(DisplayName = "TiniInt8Value: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniInt8Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniInt8Value.Parse(text));
    }

    [Fact(DisplayName = "TiniInt8Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniInt8Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniInt8Value.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniInt8Value: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniInt8Value.TryParse(((decimal)sbyte.MinValue).ToString("0"), out var _));
        Assert.False(TiniInt8Value.TryParse(((decimal)sbyte.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniInt8Value.TryParse(((decimal)sbyte.MaxValue).ToString("0"), out var _));
        Assert.False(TiniInt8Value.TryParse(((decimal)sbyte.MaxValue + 1).ToString("0"), out var _));
    }

}
