using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniFloat64ValueTests {

    [Fact(DisplayName = "TiniFloat64Value: Basic")]
    public void Basic() {
        var text = "42.84";
        Assert.True(TiniFloat64Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniFloat64Value.Parse(text));
    }

    [Fact(DisplayName = "TiniFloat64Value: Exponents")]
    public void Exponents() {
        Assert.Equal("4200", TiniFloat64Value.Parse("42e2"));
    }

    [Fact(DisplayName = "TiniFloat64Value: NaN")]
    public void NaN() {
        Assert.True(double.IsNaN(TiniFloat64Value.Parse("NaN")));
    }

    [Fact(DisplayName = "TiniFloat64Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniFloat64Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniFloat64Value.Parse("A");
        });
    }

}
