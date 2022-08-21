using System;
using AASeq;
using Xunit;

namespace Tests.Core;

public class ValueFloat64Tests {

    [Fact(DisplayName = "ValueFloat64: Basic")]
    public void Basic() {
        var text = "42.84";
        Assert.True(ValueFloat64.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueFloat64.Parse(text));
    }

    [Fact(DisplayName = "ValueFloat64: Exponents")]
    public void Exponents() {
        Assert.Equal("4200", ValueFloat64.Parse("42e2"));
    }

    [Fact(DisplayName = "ValueFloat64: NaN")]
    public void NaN() {
        Assert.True(double.IsNaN(ValueFloat64.Parse("NaN")));
    }

    [Fact(DisplayName = "ValueFloat64: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueFloat64.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueFloat64.Parse("A");
        });
    }

}
