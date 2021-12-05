using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniBinarySizeValueTests {

    [Fact(DisplayName = "TiniBinarySizeValue: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniBinarySizeValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniBinarySizeValue.Parse(text));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Base (1)")]
    public void Base1() {
        Assert.Equal("42", TiniBinarySizeValue.Parse("42B"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Base (2)")]
    public void Base2() {
        Assert.Equal("42", TiniBinarySizeValue.Parse("42 bit"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Base (3)")]
    public void Base3() {
        Assert.Equal("43", TiniBinarySizeValue.Parse("42.84 bit"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Kilo (1)")]
    public void Kilo1() {
        Assert.Equal("43008", TiniBinarySizeValue.Parse("42K"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Kilo (2)")]
    public void Kilo2() {
        Assert.Equal("43008", TiniBinarySizeValue.Parse("42 KB"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Kilo (3)")]
    public void Kilo3() {
        Assert.Equal("43008", TiniBinarySizeValue.Parse("42 kbit"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Kilo (4)")]
    public void Kilo4() {
        Assert.Equal("43868", TiniBinarySizeValue.Parse("42.84K"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Mega (1)")]
    public void Mega1() {
        Assert.Equal("44040192", TiniBinarySizeValue.Parse("42M"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Mega (2)")]
    public void Mega2() {
        Assert.Equal("44040192", TiniBinarySizeValue.Parse("42MB"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Mega (3)")]
    public void Mega3() {
        Assert.Equal("44040192", TiniBinarySizeValue.Parse("42mbit"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Mega (4)")]
    public void Mega4() {
        Assert.Equal("44920996", TiniBinarySizeValue.Parse("42.84M"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Giga (1)")]
    public void Giga1() {
        Assert.Equal("45097156608", TiniBinarySizeValue.Parse("42 G"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Giga (2)")]
    public void Giga2() {
        Assert.Equal("45097156608", TiniBinarySizeValue.Parse("42 GB"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Giga (3)")]
    public void Giga3() {
        Assert.Equal("45097156608", TiniBinarySizeValue.Parse("42 GBIT"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Giga (4)")]
    public void Giga4() {
        Assert.Equal("45999099740", TiniBinarySizeValue.Parse("42.84G"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Peta (1)")]
    public void Peta1() {
        Assert.Equal("46179488366592", TiniBinarySizeValue.Parse("42p"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Peta (2)")]
    public void Peta2() {
        Assert.Equal("46179488366592", TiniBinarySizeValue.Parse("42pb"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Peta (3)")]
    public void Peta3() {
        Assert.Equal("46179488366592", TiniBinarySizeValue.Parse("42pbit"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Peta (4)")]
    public void Peta4() {
        Assert.Equal("47103078133924", TiniBinarySizeValue.Parse("42.84P"));
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniBinarySizeValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniBinarySizeValue.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniBinarySizeValue: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniBinarySizeValue.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.False(TiniBinarySizeValue.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniBinarySizeValue.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.False(TiniBinarySizeValue.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
        Assert.False(TiniBinarySizeValue.TryParse("42E", out var _));
    }

}
