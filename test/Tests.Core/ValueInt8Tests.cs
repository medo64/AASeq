using System;
using AASeq;
using Xunit;

namespace Tests.Core;

public class ValueInt8Tests {

    [Fact(DisplayName = "ValueInt8: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueInt8.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueInt8.Parse(text));
    }

    [Fact(DisplayName = "ValueInt8: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueInt8.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueInt8.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueInt8: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueInt8.TryParse(((decimal)sbyte.MinValue).ToString("0"), out var _));
        Assert.False(ValueInt8.TryParse(((decimal)sbyte.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueInt8.TryParse(((decimal)sbyte.MaxValue).ToString("0"), out var _));
        Assert.False(ValueInt8.TryParse(((decimal)sbyte.MaxValue + 1).ToString("0"), out var _));
    }

}
