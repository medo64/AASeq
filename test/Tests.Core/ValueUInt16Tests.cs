using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueUInt16Tests {

    [Fact(DisplayName = "ValueUInt16: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueUInt16.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueUInt16.Parse(text));
    }

    [Fact(DisplayName = "ValueUInt16: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueUInt16.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueUInt16.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueUInt16: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueUInt16.TryParse(((decimal)ushort.MinValue).ToString("0"), out var _));
        Assert.False(ValueUInt16.TryParse(((decimal)ushort.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueUInt16.TryParse(((decimal)ushort.MaxValue).ToString("0"), out var _));
        Assert.False(ValueUInt16.TryParse(((decimal)ushort.MaxValue + 1).ToString("0"), out var _));
    }

}
