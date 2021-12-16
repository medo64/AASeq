using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueInt16Tests {

    [Fact(DisplayName = "TiniValueInt16: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueInt16.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueInt16.Parse(text));
    }

    [Fact(DisplayName = "TiniValueInt16: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueInt16.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueInt16.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueInt16: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueInt16.TryParse(((decimal)short.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueInt16.TryParse(((decimal)short.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueInt16.TryParse(((decimal)short.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueInt16.TryParse(((decimal)short.MaxValue + 1).ToString("0"), out var _));
    }

}
