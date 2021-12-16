using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueInt32Tests {

    [Fact(DisplayName = "TiniValueInt32: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueInt32.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueInt32.Parse(text));
    }

    [Fact(DisplayName = "TiniValueInt32: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueInt32.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueInt32.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueInt32: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueInt32.TryParse(((decimal)int.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueInt32.TryParse(((decimal)int.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueInt32.TryParse(((decimal)int.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueInt32.TryParse(((decimal)int.MaxValue + 1).ToString("0"), out var _));
    }

}
