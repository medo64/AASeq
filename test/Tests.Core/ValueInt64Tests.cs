using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueInt64Tests {

    [Fact(DisplayName = "ValueInt64: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueInt64.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueInt64.Parse(text));
    }

    [Fact(DisplayName = "ValueInt64: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueInt64.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueInt64.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueInt64: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueInt64.TryParse(((decimal)long.MinValue).ToString("0"), out var _));
        Assert.False(ValueInt64.TryParse(((decimal)long.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueInt64.TryParse(((decimal)long.MaxValue).ToString("0"), out var _));
        Assert.False(ValueInt64.TryParse(((decimal)long.MaxValue + 1).ToString("0"), out var _));
    }

}
