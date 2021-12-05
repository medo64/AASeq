using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniFloat32ValueTests {

    [Fact(DisplayName = "TiniFloat32Value: Basic")]
    public void Basic() {
        var text = "42.84";
        Assert.True(TiniFloat32Value.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniFloat32Value.Parse(text));
    }

    [Fact(DisplayName = "TiniFloat32Value: Exponents")]
    public void Exponents() {
        Assert.Equal("4200", TiniFloat32Value.Parse("42e2"));
    }

    [Fact(DisplayName = "TiniFloat32Value: NaN")]
    public void NaN() {
        Assert.True(float.IsNaN(TiniFloat32Value.Parse("NaN")));
    }

    [Fact(DisplayName = "TiniFloat32Value: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniFloat32Value.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniFloat32Value.Parse("A");
        });
    }

}
