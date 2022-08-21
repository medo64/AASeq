using System;
using AASeq;
using Xunit;

namespace Tests.Core;

public class ValueUInt32Tests {

    [Fact(DisplayName = "ValueUInt32: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueUInt32.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueUInt32.Parse(text));
    }

    [Fact(DisplayName = "ValueUInt32: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueUInt32.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueUInt32.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueUInt32: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueUInt32.TryParse(((decimal)uint.MinValue).ToString("0"), out var _));
        Assert.False(ValueUInt32.TryParse(((decimal)uint.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueUInt32.TryParse(((decimal)uint.MaxValue).ToString("0"), out var _));
        Assert.False(ValueUInt32.TryParse(((decimal)uint.MaxValue + 1).ToString("0"), out var _));
    }

}
