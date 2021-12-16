using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueInt64Tests {

    [Fact(DisplayName = "TiniValueInt64: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueInt64.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueInt64.Parse(text));
    }

    [Fact(DisplayName = "TiniValueInt64: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueInt64.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueInt64.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueInt64: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueInt64.TryParse(((decimal)long.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueInt64.TryParse(((decimal)long.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueInt64.TryParse(((decimal)long.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueInt64.TryParse(((decimal)long.MaxValue + 1).ToString("0"), out var _));
    }

}
