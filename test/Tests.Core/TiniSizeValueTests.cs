using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniSizeValueTests {

    [Fact(DisplayName = "TiniSizeValue: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniSizeValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniSizeValue.Parse(text));
    }

    [Fact(DisplayName = "TiniSizeValue: Base (1)")]
    public void Base1() {
        Assert.Equal("42", TiniSizeValue.Parse("42B"));
    }

    [Fact(DisplayName = "TiniSizeValue: Base (2)")]
    public void Base2() {
        Assert.Equal("42", TiniSizeValue.Parse("42 bit"));
    }

    [Fact(DisplayName = "TiniSizeValue: Base (3)")]
    public void Base3() {
        Assert.Equal("43", TiniSizeValue.Parse("42.84 bit"));
    }

    [Fact(DisplayName = "TiniSizeValue: Kilo (1)")]
    public void Kilo1() {
        Assert.Equal("42000", TiniSizeValue.Parse("42K"));
    }

    [Fact(DisplayName = "TiniSizeValue: Kilo (2)")]
    public void Kilo2() {
        Assert.Equal("42000", TiniSizeValue.Parse("42 KB"));
    }

    [Fact(DisplayName = "TiniSizeValue: Kilo (3)")]
    public void Kilo3() {
        Assert.Equal("42000", TiniSizeValue.Parse("42 kbit"));
    }

    [Fact(DisplayName = "TiniSizeValue: Kilo (4)")]
    public void Kilo4() {
        Assert.Equal("42840", TiniSizeValue.Parse("42.84 kbit"));
    }

    [Fact(DisplayName = "TiniSizeValue: Mega (1)")]
    public void Mega1() {
        Assert.Equal("42000000", TiniSizeValue.Parse("42M"));
    }

    [Fact(DisplayName = "TiniSizeValue: Mega (2)")]
    public void Mega2() {
        Assert.Equal("42000000", TiniSizeValue.Parse("42MB"));
    }

    [Fact(DisplayName = "TiniSizeValue: Mega (3)")]
    public void Mega3() {
        Assert.Equal("42000000", TiniSizeValue.Parse("42mbit"));
    }

    [Fact(DisplayName = "TiniSizeValue: Mega (4)")]
    public void Mega4() {
        Assert.Equal("42840000", TiniSizeValue.Parse("42.84 Mbit"));
    }

    [Fact(DisplayName = "TiniSizeValue: Giga (1)")]
    public void Giga1() {
        Assert.Equal("42000000000", TiniSizeValue.Parse("42 G"));
    }

    [Fact(DisplayName = "TiniSizeValue: Giga (2)")]
    public void Giga2() {
        Assert.Equal("42000000000", TiniSizeValue.Parse("42 GB"));
    }

    [Fact(DisplayName = "TiniSizeValue: Giga (3)")]
    public void Giga3() {
        Assert.Equal("42000000000", TiniSizeValue.Parse("42 GBIT"));
    }

    [Fact(DisplayName = "TiniSizeValue: Giga (4)")]
    public void Giga4() {
        Assert.Equal("42840000000", TiniSizeValue.Parse("42.84 GB"));
    }

    [Fact(DisplayName = "TiniSizeValue: Peta (1)")]
    public void Peta1() {
        Assert.Equal("42000000000000", TiniSizeValue.Parse("42p"));
    }

    [Fact(DisplayName = "TiniSizeValue: Peta (2)")]
    public void Peta2() {
        Assert.Equal("42000000000000", TiniSizeValue.Parse("42pb"));
    }

    [Fact(DisplayName = "TiniSizeValue: Peta (3)")]
    public void Peta3() {
        Assert.Equal("42000000000000", TiniSizeValue.Parse("42pbit"));
    }

    [Fact(DisplayName = "TiniSizeValue: Peta (4)")]
    public void Peta4() {
        Assert.Equal("42840000000000", TiniSizeValue.Parse("42.84 P"));
    }

    [Fact(DisplayName = "TiniSizeValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniSizeValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniSizeValue.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniSizeValue: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniSizeValue.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.False(TiniSizeValue.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniSizeValue.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.False(TiniSizeValue.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
        Assert.False(TiniSizeValue.TryParse("42E", out var _));
    }

}
