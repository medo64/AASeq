using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueUInt8Tests {

    [Fact(DisplayName = "ValueUInt8: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueUInt8.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueUInt8.Parse(text));
    }

    [Fact(DisplayName = "ValueUInt8: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueUInt8.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueUInt8.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueUInt8: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueUInt8.TryParse(((decimal)byte.MinValue).ToString("0"), out var _));
        Assert.False(ValueUInt8.TryParse(((decimal)byte.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueUInt8.TryParse(((decimal)byte.MaxValue).ToString("0"), out var _));
        Assert.False(ValueUInt8.TryParse(((decimal)byte.MaxValue + 1).ToString("0"), out var _));
    }

}
