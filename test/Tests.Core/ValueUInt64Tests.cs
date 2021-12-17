using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueUInt64Tests {

    [Fact(DisplayName = "ValueUInt64: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueUInt64.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueUInt64.Parse(text));
    }

    [Fact(DisplayName = "ValueUInt64: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueUInt64.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueUInt64.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueUInt64: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueUInt64.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.False(ValueUInt64.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueUInt64.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.False(ValueUInt64.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
    }

}
