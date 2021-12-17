using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueInt32Tests {

    [Fact(DisplayName = "ValueInt32: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueInt32.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueInt32.Parse(text));
    }

    [Fact(DisplayName = "ValueInt32: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueInt32.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueInt32.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueInt32: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueInt32.TryParse(((decimal)int.MinValue).ToString("0"), out var _));
        Assert.False(ValueInt32.TryParse(((decimal)int.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueInt32.TryParse(((decimal)int.MaxValue).ToString("0"), out var _));
        Assert.False(ValueInt32.TryParse(((decimal)int.MaxValue + 1).ToString("0"), out var _));
    }

}
