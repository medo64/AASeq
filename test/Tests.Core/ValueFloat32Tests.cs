using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueFloat32Tests {

    [Fact(DisplayName = "ValueFloat32: Basic")]
    public void Basic() {
        var text = "42.84";
        Assert.True(ValueFloat32.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueFloat32.Parse(text));
    }

    [Fact(DisplayName = "ValueFloat32: Exponents")]
    public void Exponents() {
        Assert.Equal("4200", ValueFloat32.Parse("42e2"));
    }

    [Fact(DisplayName = "ValueFloat32: NaN")]
    public void NaN() {
        Assert.True(float.IsNaN(ValueFloat32.Parse("NaN")));
    }

    [Fact(DisplayName = "ValueFloat32: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueFloat32.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueFloat32.Parse("A");
        });
    }

}
