using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueFloat64Tests {

    [Fact(DisplayName = "TiniValueFloat64: Basic")]
    public void Basic() {
        var text = "42.84";
        Assert.True(TiniValueFloat64.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueFloat64.Parse(text));
    }

    [Fact(DisplayName = "TiniValueFloat64: Exponents")]
    public void Exponents() {
        Assert.Equal("4200", TiniValueFloat64.Parse("42e2"));
    }

    [Fact(DisplayName = "TiniValueFloat64: NaN")]
    public void NaN() {
        Assert.True(double.IsNaN(TiniValueFloat64.Parse("NaN")));
    }

    [Fact(DisplayName = "TiniValueFloat64: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueFloat64.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueFloat64.Parse("A");
        });
    }

}
