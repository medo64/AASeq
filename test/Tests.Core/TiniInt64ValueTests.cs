using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniInt64ValueTests {

    [Fact(DisplayName = "TiniInt64Value: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniInt64Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniInt64Value.Parse(text));
    }

    [Fact(DisplayName = "TiniInt64Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniInt64Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniInt64Value.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniInt64Value: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniInt64Value.TryParse(((decimal)long.MinValue).ToString("0"), out var _));
        Assert.False(TiniInt64Value.TryParse(((decimal)long.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniInt64Value.TryParse(((decimal)long.MaxValue).ToString("0"), out var _));
        Assert.False(TiniInt64Value.TryParse(((decimal)long.MaxValue + 1).ToString("0"), out var _));
    }

}
