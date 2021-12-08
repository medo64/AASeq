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
        Assert.Equal("42", TiniSizeValue.Parse("42"));
    }

    [Fact(DisplayName = "TiniSizeValue: Base (2)")]
    public void Base2() {
        Assert.Equal("43", TiniSizeValue.Parse("42.84"));
    }

    [Fact(DisplayName = "TiniSizeValue: Base (3)")]
    public void Base3() {
        Assert.Equal("43", TiniSizeValue.Parse(" 42.84"));
    }


    [Fact(DisplayName = "TiniSizeValue: Kilo (1)")]
    public void Kilo1() {
        Assert.Equal("42000", TiniSizeValue.Parse("42K"));
    }

    [Fact(DisplayName = "TiniSizeValue: Kilo (2)")]
    public void Kilo2() {
        Assert.Equal("42840", TiniSizeValue.Parse("42.84 k"));
    }

    [Fact(DisplayName = "TiniSizeValue: Kilo (3)")]
    public void Kilo3() {
        Assert.Equal("1", TiniSizeValue.Parse(" 0.0001 k "));
    }

    [Fact(DisplayName = "TiniSizeValue: Kibi (1)")]
    public void Kibi1() {
        Assert.Equal("43008", TiniSizeValue.Parse("42Ki"));
    }

    [Fact(DisplayName = "TiniSizeValue: Kibi (2)")]
    public void Kibi2() {
        Assert.Equal("43868", TiniSizeValue.Parse("42.84ki"));
    }

    [Fact(DisplayName = "TiniSizeValue: Kibi (3)")]
    public void Kibi3() {
        Assert.Equal("1", TiniSizeValue.Parse(" 0.000001 ki "));
    }


    [Fact(DisplayName = "TiniSizeValue: Mega (1)")]
    public void Mega1() {
        Assert.Equal("42000000", TiniSizeValue.Parse("42m"));
    }

    [Fact(DisplayName = "TiniSizeValue: Mega (2)")]
    public void Mega2() {
        Assert.Equal("42840000", TiniSizeValue.Parse("42.84 M"));
    }

    [Fact(DisplayName = "TiniSizeValue: Mega (3)")]
    public void Mega3() {
        Assert.Equal("1", TiniSizeValue.Parse(" 0.0000001 M "));
    }

    [Fact(DisplayName = "TiniSizeValue: Mebi (1)")]
    public void Mebi1() {
        Assert.Equal("44040192", TiniSizeValue.Parse("42Mi"));
    }

    [Fact(DisplayName = "TiniSizeValue: Mebi (2)")]
    public void Mebi2() {
        Assert.Equal("44920996", TiniSizeValue.Parse("42.84Mi"));
    }

    [Fact(DisplayName = "TiniSizeValue: Mebi (3)")]
    public void Mebi3() {
        Assert.Equal("1", TiniSizeValue.Parse(" 0.0000001 Mi "));
    }


    [Fact(DisplayName = "TiniSizeValue: Giga (1)")]
    public void Giga1() {
        Assert.Equal("42000000000", TiniSizeValue.Parse("42 G"));
    }

    [Fact(DisplayName = "TiniSizeValue: Giga (2)")]
    public void Giga2() {
        Assert.Equal("42840000000", TiniSizeValue.Parse("42.84g"));
    }

    [Fact(DisplayName = "TiniSizeValue: Giga (3)")]
    public void Giga3() {
        Assert.Equal("1", TiniSizeValue.Parse("  0.0000000001 G "));
    }

    [Fact(DisplayName = "TiniSizeValue: Gibi (1)")]
    public void Gibi1() {
        Assert.Equal("45097156608", TiniSizeValue.Parse("42 Gi"));
    }

    [Fact(DisplayName = "TiniSizeValue: Gibi (2)")]
    public void Gibi2() {
        Assert.Equal("45999099740", TiniSizeValue.Parse("42.84Gi"));
    }

    [Fact(DisplayName = "TiniSizeValue: Gibi (3)")]
    public void Gibi3() {
        Assert.Equal("1", TiniSizeValue.Parse("  0.0000000001 GI "));
    }


    [Fact(DisplayName = "TiniSizeValue: Tera (1)")]
    public void Tera1() {
        Assert.Equal("42000000000000", TiniSizeValue.Parse("42T"));
    }

    [Fact(DisplayName = "TiniSizeValue: Tera (2)")]
    public void Tera2() {
        Assert.Equal("42840000000000", TiniSizeValue.Parse("42.84 T"));
    }

    [Fact(DisplayName = "TiniSizeValue: Tera (3)")]
    public void Tera3() {
        Assert.Equal("1", TiniSizeValue.Parse("  0.0000000000001 T "));
    }

    [Fact(DisplayName = "TiniSizeValue: Tebi (1)")]
    public void Tebi1() {
        Assert.Equal("46179488366592", TiniSizeValue.Parse("42Ti"));
    }

    [Fact(DisplayName = "TiniSizeValue: Tebi (2)")]
    public void Tebi2() {
        Assert.Equal("47103078133924", TiniSizeValue.Parse("42.84Ti"));
    }

    [Fact(DisplayName = "TiniSizeValue: Tebi (3)")]
    public void Tebi3() {
        Assert.Equal("1", TiniSizeValue.Parse("  0.0000000000001 Ti "));
    }


    [Fact(DisplayName = "TiniSizeValue: Peta (1)")]
    public void Peta1() {
        Assert.Equal("42000000000000000", TiniSizeValue.Parse("42p"));
    }

    [Fact(DisplayName = "TiniSizeValue: Peta (2)")]
    public void Peta2() {
        Assert.Equal("42840000000000000", TiniSizeValue.Parse("42.84 P"));
    }

    [Fact(DisplayName = "TiniSizeValue: Peta (3)")]
    public void Peta3() {
        Assert.Equal("1", TiniSizeValue.Parse("  0.0000000000000001 P "));
    }

    [Fact(DisplayName = "TiniSizeValue: Pebi (1)")]
    public void Pebi1() {
        Assert.Equal("47287796087390208", TiniSizeValue.Parse("42pi"));
    }

    [Fact(DisplayName = "TiniSizeValue: Pebi (2)")]
    public void Pebi2() {
        Assert.Equal("48233552009138012", TiniSizeValue.Parse("42.84PI"));
    }

    [Fact(DisplayName = "TiniSizeValue: Pebi (3)")]
    public void Pebi3() {
        Assert.Equal("1", TiniSizeValue.Parse("  0.0000000000000001 PI "));
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
