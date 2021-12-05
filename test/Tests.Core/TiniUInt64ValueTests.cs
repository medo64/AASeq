using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniUInt64ValueTests {

    [Fact(DisplayName = "TiniUInt64Value: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniUInt64Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniUInt64Value.Parse(text));
    }

    [Fact(DisplayName = "TiniUInt64Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniUInt64Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniUInt64Value.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniUInt64Value: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniUInt64Value.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.False(TiniUInt64Value.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniUInt64Value.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.False(TiniUInt64Value.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
    }

}
