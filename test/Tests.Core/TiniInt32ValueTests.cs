using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniInt32ValueTests {

    [Fact(DisplayName = "TiniInt32Value: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniInt32Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniInt32Value.Parse(text));
    }

    [Fact(DisplayName = "TiniInt32Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniInt32Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniInt32Value.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniInt32Value: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniInt32Value.TryParse(((decimal)int.MinValue).ToString("0"), out var _));
        Assert.False(TiniInt32Value.TryParse(((decimal)int.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniInt32Value.TryParse(((decimal)int.MaxValue).ToString("0"), out var _));
        Assert.False(TiniInt32Value.TryParse(((decimal)int.MaxValue + 1).ToString("0"), out var _));
    }

}
