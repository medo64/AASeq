using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueInt8Tests {

    [Fact(DisplayName = "TiniValueInt8: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueInt8.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueInt8.Parse(text));
    }

    [Fact(DisplayName = "TiniValueInt8: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueInt8.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueInt8.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueInt8: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueInt8.TryParse(((decimal)sbyte.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueInt8.TryParse(((decimal)sbyte.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueInt8.TryParse(((decimal)sbyte.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueInt8.TryParse(((decimal)sbyte.MaxValue + 1).ToString("0"), out var _));
    }

}
