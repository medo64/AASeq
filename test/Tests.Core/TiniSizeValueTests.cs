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


    [Fact(DisplayName = "TiniSizeValue: ToKiloString")]
    public void ToKiloString() {
        Assert.Equal("0.042k", TiniSizeValue.Parse("42").ToKiloString());
        Assert.Equal("0.0420k", TiniSizeValue.Parse("42").ToKiloString("0.0000"));
        Assert.Equal("420k", TiniSizeValue.Parse("420000").ToKiloString());
        Assert.Equal("420.0000k", TiniSizeValue.Parse("420000").ToKiloString("0.0000"));
        Assert.Equal("4200000k", TiniSizeValue.Parse("4200000000").ToKiloString());
        Assert.Equal("4200000.0000k", TiniSizeValue.Parse("4200000000").ToKiloString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToKibiString")]
    public void ToKibiString() {
        Assert.Equal("0.041015625Ki", TiniSizeValue.Parse("42").ToKibiString());
        Assert.Equal("0.0410Ki", TiniSizeValue.Parse("42").ToKibiString("0.0000"));
        Assert.Equal("410.15625Ki", TiniSizeValue.Parse("420000").ToKibiString());
        Assert.Equal("410.1563Ki", TiniSizeValue.Parse("420000").ToKibiString("0.0000"));
        Assert.Equal("4101562.5Ki", TiniSizeValue.Parse("4200000000").ToKibiString());
        Assert.Equal("4101562.5000Ki", TiniSizeValue.Parse("4200000000").ToKibiString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToMegatring")]
    public void ToMegaString() {
        Assert.Equal("0.42M", TiniSizeValue.Parse("420000").ToMegaString());
        Assert.Equal("0.4200M", TiniSizeValue.Parse("420000").ToMegaString("0.0000"));
        Assert.Equal("4200M", TiniSizeValue.Parse("4200000000").ToMegaString());
        Assert.Equal("4200.0000M", TiniSizeValue.Parse("4200000000").ToMegaString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToMebiString")]
    public void ToMebiString() {
        Assert.Equal("0.400543212890625Mi", TiniSizeValue.Parse("420000").ToMebiString());
        Assert.Equal("0.4005Mi", TiniSizeValue.Parse("420000").ToMebiString("0.0000"));
        Assert.Equal("4005.43212890625Mi", TiniSizeValue.Parse("4200000000").ToMebiString());
        Assert.Equal("4005.4321Mi", TiniSizeValue.Parse("4200000000").ToMebiString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToGigaString")]
    public void ToGigaString() {
        Assert.Equal("0.00042G", TiniSizeValue.Parse("420000").ToGigaString());
        Assert.Equal("0.0004G", TiniSizeValue.Parse("420000").ToGigaString("0.0000"));
        Assert.Equal("4.2G", TiniSizeValue.Parse("4200000000").ToGigaString());
        Assert.Equal("4.2000G", TiniSizeValue.Parse("4200000000").ToGigaString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToGibiString")]
    public void ToGibiString() {
        Assert.Equal("0.000391155481338501Gi", TiniSizeValue.Parse("420000").ToGibiString());
        Assert.Equal("0.0004Gi", TiniSizeValue.Parse("420000").ToGibiString("0.0000"));
        Assert.Equal("3.9115548133850098Gi", TiniSizeValue.Parse("4200000000").ToGibiString());
        Assert.Equal("3.9116Gi", TiniSizeValue.Parse("4200000000").ToGibiString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToTeraString")]
    public void ToTeraString() {
        Assert.Equal("0.00044T", TiniSizeValue.Parse("440000000").ToTeraString());
        Assert.Equal("0.0004T", TiniSizeValue.Parse("440000000").ToTeraString("0.0000"));
        Assert.Equal("4.4T", TiniSizeValue.Parse("4400000000000").ToTeraString());
        Assert.Equal("4.4000T", TiniSizeValue.Parse("4400000000000").ToTeraString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToTebiString")]
    public void ToTebiString() {
        Assert.Equal("0.0004001776687800884Ti", TiniSizeValue.Parse("440000000").ToTebiString());
        Assert.Equal("0.0004Ti", TiniSizeValue.Parse("440000000").ToTebiString("0.0000"));
        Assert.Equal("4.001776687800884Ti", TiniSizeValue.Parse("4400000000000").ToTebiString());
        Assert.Equal("4.0018Ti", TiniSizeValue.Parse("4400000000000").ToTebiString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToPetaString")]
    public void ToPetaString() {
        Assert.Equal("0.00046P", TiniSizeValue.Parse("460000000000").ToPetaString());
        Assert.Equal("0.0005P", TiniSizeValue.Parse("460000000000").ToPetaString("0.0000"));
        Assert.Equal("4.6P", TiniSizeValue.Parse("4600000000000000").ToPetaString());
        Assert.Equal("4.6000P", TiniSizeValue.Parse("4600000000000000").ToPetaString("0.0000"));
    }

    [Fact(DisplayName = "TiniSizeValue: ToPebiString")]
    public void ToPebiString() {
        Assert.Equal("0.0004085620730620576Pi", TiniSizeValue.Parse("460000000000").ToPebiString());
        Assert.Equal("0.0004Pi", TiniSizeValue.Parse("460000000000").ToPebiString("0.0000"));
        Assert.Equal("4.085620730620576Pi", TiniSizeValue.Parse("4600000000000000").ToPebiString());
        Assert.Equal("4.0856Pi", TiniSizeValue.Parse("4600000000000000").ToPebiString("0.0000"));
    }


    [Fact(DisplayName = "TiniSizeValue: ToScaledUnitString")]
    public void ToScaledUnitString() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledUnitString());
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledUnitString());
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledUnitString());
        Assert.Equal("1.23k", TiniSizeValue.Parse("1234").ToScaledUnitString());
        Assert.Equal("12.3k", TiniSizeValue.Parse("12345").ToScaledUnitString());
        Assert.Equal("123k", TiniSizeValue.Parse("123456").ToScaledUnitString());
        Assert.Equal("1.23M", TiniSizeValue.Parse("1234567").ToScaledUnitString());
        Assert.Equal("12.3M", TiniSizeValue.Parse("12345678").ToScaledUnitString());
        Assert.Equal("123M", TiniSizeValue.Parse("123456789").ToScaledUnitString());
        Assert.Equal("1.23G", TiniSizeValue.Parse("1234567890").ToScaledUnitString());
        Assert.Equal("12.3G", TiniSizeValue.Parse("12345678901").ToScaledUnitString());
        Assert.Equal("123G", TiniSizeValue.Parse("123456789012").ToScaledUnitString());
        Assert.Equal("1.23T", TiniSizeValue.Parse("1234567890123").ToScaledUnitString());
        Assert.Equal("12.3T", TiniSizeValue.Parse("12345678901234").ToScaledUnitString());
        Assert.Equal("123T", TiniSizeValue.Parse("123456789012345").ToScaledUnitString());
        Assert.Equal("1.23P", TiniSizeValue.Parse("1234567890123456").ToScaledUnitString());
        Assert.Equal("12.3P", TiniSizeValue.Parse("12345678901234567").ToScaledUnitString());
        Assert.Equal("123P", TiniSizeValue.Parse("123456789012345678").ToScaledUnitString());
        Assert.Equal("1230P", TiniSizeValue.Parse("1234567890123456789").ToScaledUnitString());
        Assert.Equal("12300P", TiniSizeValue.Parse("12345678901234567890").ToScaledUnitString());
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledBinaryUnitString")]
    public void ToScaledBinaryUnitString() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledBinaryUnitString());
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledBinaryUnitString());
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledBinaryUnitString());
        Assert.Equal("1.21Ki", TiniSizeValue.Parse("1234").ToScaledBinaryUnitString());
        Assert.Equal("12.1Ki", TiniSizeValue.Parse("12345").ToScaledBinaryUnitString());
        Assert.Equal("121Ki", TiniSizeValue.Parse("123456").ToScaledBinaryUnitString());
        Assert.Equal("1.18Mi", TiniSizeValue.Parse("1234567").ToScaledBinaryUnitString());
        Assert.Equal("11.8Mi", TiniSizeValue.Parse("12345678").ToScaledBinaryUnitString());
        Assert.Equal("118Mi", TiniSizeValue.Parse("123456789").ToScaledBinaryUnitString());
        Assert.Equal("1.15Gi", TiniSizeValue.Parse("1234567890").ToScaledBinaryUnitString());
        Assert.Equal("11.5Gi", TiniSizeValue.Parse("12345678901").ToScaledBinaryUnitString());
        Assert.Equal("115Gi", TiniSizeValue.Parse("123456789012").ToScaledBinaryUnitString());
        Assert.Equal("1.12Ti", TiniSizeValue.Parse("1234567890123").ToScaledBinaryUnitString());
        Assert.Equal("11.2Ti", TiniSizeValue.Parse("12345678901234").ToScaledBinaryUnitString());
        Assert.Equal("112Ti", TiniSizeValue.Parse("123456789012345").ToScaledBinaryUnitString());
        Assert.Equal("1.10Pi", TiniSizeValue.Parse("1234567890123456").ToScaledBinaryUnitString());
        Assert.Equal("11.0Pi", TiniSizeValue.Parse("12345678901234567").ToScaledBinaryUnitString());
        Assert.Equal("110Pi", TiniSizeValue.Parse("123456789012345678").ToScaledBinaryUnitString());
        Assert.Equal("1100Pi", TiniSizeValue.Parse("1234567890123456789").ToScaledBinaryUnitString());
        Assert.Equal("11000Pi", TiniSizeValue.Parse("12345678901234567890").ToScaledBinaryUnitString());
    }

}
