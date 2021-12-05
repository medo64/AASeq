using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniInt16ValueTests {

    [Fact(DisplayName = "TiniInt16Value: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniInt16Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniInt16Value.Parse(text));
    }

    [Fact(DisplayName = "TiniInt16Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniInt16Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniInt16Value.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniInt16Value: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniInt16Value.TryParse(((decimal)short.MinValue).ToString("0"), out var _));
        Assert.False(TiniInt16Value.TryParse(((decimal)short.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniInt16Value.TryParse(((decimal)short.MaxValue).ToString("0"), out var _));
        Assert.False(TiniInt16Value.TryParse(((decimal)short.MaxValue + 1).ToString("0"), out var _));
    }

}
