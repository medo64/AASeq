using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueInt16Tests {

    [Fact(DisplayName = "ValueInt16: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueInt16.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueInt16.Parse(text));
    }

    [Fact(DisplayName = "ValueInt16: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueInt16.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueInt16.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueInt16: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueInt16.TryParse(((decimal)short.MinValue).ToString("0"), out var _));
        Assert.False(ValueInt16.TryParse(((decimal)short.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueInt16.TryParse(((decimal)short.MaxValue).ToString("0"), out var _));
        Assert.False(ValueInt16.TryParse(((decimal)short.MaxValue + 1).ToString("0"), out var _));
    }

}
