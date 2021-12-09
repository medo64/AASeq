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


    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString")]
    public void ToScaledSIString() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString());
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledSIString());
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledSIString());
        Assert.Equal("1.23k", TiniSizeValue.Parse("1234").ToScaledSIString());
        Assert.Equal("12.3k", TiniSizeValue.Parse("12345").ToScaledSIString());
        Assert.Equal("123k", TiniSizeValue.Parse("123456").ToScaledSIString());
        Assert.Equal("1.23M", TiniSizeValue.Parse("1234567").ToScaledSIString());
        Assert.Equal("12.3M", TiniSizeValue.Parse("12345678").ToScaledSIString());
        Assert.Equal("123M", TiniSizeValue.Parse("123456789").ToScaledSIString());
        Assert.Equal("1.23G", TiniSizeValue.Parse("1234567890").ToScaledSIString());
        Assert.Equal("12.3G", TiniSizeValue.Parse("12345678901").ToScaledSIString());
        Assert.Equal("123G", TiniSizeValue.Parse("123456789012").ToScaledSIString());
        Assert.Equal("1.23T", TiniSizeValue.Parse("1234567890123").ToScaledSIString());
        Assert.Equal("12.3T", TiniSizeValue.Parse("12345678901234").ToScaledSIString());
        Assert.Equal("123T", TiniSizeValue.Parse("123456789012345").ToScaledSIString());
        Assert.Equal("1.23P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString());
        Assert.Equal("12.3P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString());
        Assert.Equal("123P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString());
        Assert.Equal("1230P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString());
        Assert.Equal("12300P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString());
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (1)")]
    public void ToScaledSIString1() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString(1));
        Assert.Equal("10", TiniSizeValue.Parse("12").ToScaledSIString(1));
        Assert.Equal("100", TiniSizeValue.Parse("123").ToScaledSIString(1));
        Assert.Equal("1k", TiniSizeValue.Parse("1234").ToScaledSIString(1));
        Assert.Equal("10k", TiniSizeValue.Parse("12345").ToScaledSIString(1));
        Assert.Equal("100k", TiniSizeValue.Parse("123456").ToScaledSIString(1));
        Assert.Equal("1M", TiniSizeValue.Parse("1234567").ToScaledSIString(1));
        Assert.Equal("10M", TiniSizeValue.Parse("12345678").ToScaledSIString(1));
        Assert.Equal("100M", TiniSizeValue.Parse("123456789").ToScaledSIString(1));
        Assert.Equal("1G", TiniSizeValue.Parse("1234567890").ToScaledSIString(1));
        Assert.Equal("10G", TiniSizeValue.Parse("12345678901").ToScaledSIString(1));
        Assert.Equal("100G", TiniSizeValue.Parse("123456789012").ToScaledSIString(1));
        Assert.Equal("1T", TiniSizeValue.Parse("1234567890123").ToScaledSIString(1));
        Assert.Equal("10T", TiniSizeValue.Parse("12345678901234").ToScaledSIString(1));
        Assert.Equal("100T", TiniSizeValue.Parse("123456789012345").ToScaledSIString(1));
        Assert.Equal("1P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString(1));
        Assert.Equal("10P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString(1));
        Assert.Equal("100P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString(1));
        Assert.Equal("1000P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString(1));
        Assert.Equal("10000P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString(1));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (3)")]
    public void ToScaledSIString3() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString(3));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledSIString(3));
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledSIString(3));
        Assert.Equal("1.23k", TiniSizeValue.Parse("1234").ToScaledSIString(3));
        Assert.Equal("12.3k", TiniSizeValue.Parse("12345").ToScaledSIString(3));
        Assert.Equal("123k", TiniSizeValue.Parse("123456").ToScaledSIString(3));
        Assert.Equal("1.23M", TiniSizeValue.Parse("1234567").ToScaledSIString(3));
        Assert.Equal("12.3M", TiniSizeValue.Parse("12345678").ToScaledSIString(3));
        Assert.Equal("123M", TiniSizeValue.Parse("123456789").ToScaledSIString(3));
        Assert.Equal("1.23G", TiniSizeValue.Parse("1234567890").ToScaledSIString(3));
        Assert.Equal("12.3G", TiniSizeValue.Parse("12345678901").ToScaledSIString(3));
        Assert.Equal("123G", TiniSizeValue.Parse("123456789012").ToScaledSIString(3));
        Assert.Equal("1.23T", TiniSizeValue.Parse("1234567890123").ToScaledSIString(3));
        Assert.Equal("12.3T", TiniSizeValue.Parse("12345678901234").ToScaledSIString(3));
        Assert.Equal("123T", TiniSizeValue.Parse("123456789012345").ToScaledSIString(3));
        Assert.Equal("1.23P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString(3));
        Assert.Equal("12.3P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString(3));
        Assert.Equal("123P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString(3));
        Assert.Equal("1230P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString(3));
        Assert.Equal("12300P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString(3));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (4)")]
    public void ToScaledSIString4() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString(4));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledSIString(4));
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledSIString(4));
        Assert.Equal("1.234k", TiniSizeValue.Parse("1234").ToScaledSIString(4));
        Assert.Equal("12.35k", TiniSizeValue.Parse("12345").ToScaledSIString(4));
        Assert.Equal("123.5k", TiniSizeValue.Parse("123456").ToScaledSIString(4));
        Assert.Equal("1.235M", TiniSizeValue.Parse("1234567").ToScaledSIString(4));
        Assert.Equal("12.35M", TiniSizeValue.Parse("12345678").ToScaledSIString(4));
        Assert.Equal("123.5M", TiniSizeValue.Parse("123456789").ToScaledSIString(4));
        Assert.Equal("1.235G", TiniSizeValue.Parse("1234567890").ToScaledSIString(4));
        Assert.Equal("12.35G", TiniSizeValue.Parse("12345678901").ToScaledSIString(4));
        Assert.Equal("123.5G", TiniSizeValue.Parse("123456789012").ToScaledSIString(4));
        Assert.Equal("1.235T", TiniSizeValue.Parse("1234567890123").ToScaledSIString(4));
        Assert.Equal("12.35T", TiniSizeValue.Parse("12345678901234").ToScaledSIString(4));
        Assert.Equal("123.5T", TiniSizeValue.Parse("123456789012345").ToScaledSIString(4));
        Assert.Equal("1.235P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString(4));
        Assert.Equal("12.35P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString(4));
        Assert.Equal("123.5P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString(4));
        Assert.Equal("1235P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString(4));
        Assert.Equal("12350P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString(4));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (5)")]
    public void ToScaledSIString5() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString(5));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledSIString(5));
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledSIString(5));
        Assert.Equal("1.234k", TiniSizeValue.Parse("1234").ToScaledSIString(5));
        Assert.Equal("12.345k", TiniSizeValue.Parse("12345").ToScaledSIString(5));
        Assert.Equal("123.46k", TiniSizeValue.Parse("123456").ToScaledSIString(5));
        Assert.Equal("1.2346M", TiniSizeValue.Parse("1234567").ToScaledSIString(5));
        Assert.Equal("12.346M", TiniSizeValue.Parse("12345678").ToScaledSIString(5));
        Assert.Equal("123.46M", TiniSizeValue.Parse("123456789").ToScaledSIString(5));
        Assert.Equal("1.2346G", TiniSizeValue.Parse("1234567890").ToScaledSIString(5));
        Assert.Equal("12.346G", TiniSizeValue.Parse("12345678901").ToScaledSIString(5));
        Assert.Equal("123.46G", TiniSizeValue.Parse("123456789012").ToScaledSIString(5));
        Assert.Equal("1.2346T", TiniSizeValue.Parse("1234567890123").ToScaledSIString(5));
        Assert.Equal("12.346T", TiniSizeValue.Parse("12345678901234").ToScaledSIString(5));
        Assert.Equal("123.46T", TiniSizeValue.Parse("123456789012345").ToScaledSIString(5));
        Assert.Equal("1.2346P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString(5));
        Assert.Equal("12.346P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString(5));
        Assert.Equal("123.46P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString(5));
        Assert.Equal("1234.6P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString(5));
        Assert.Equal("12346P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString(5));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (6)")]
    public void ToScaledSIString6() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString(6));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledSIString(6));
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledSIString(6));
        Assert.Equal("1.234k", TiniSizeValue.Parse("1234").ToScaledSIString(6));
        Assert.Equal("12.345k", TiniSizeValue.Parse("12345").ToScaledSIString(6));
        Assert.Equal("123.456k", TiniSizeValue.Parse("123456").ToScaledSIString(6));
        Assert.Equal("1.23457M", TiniSizeValue.Parse("1234567").ToScaledSIString(6));
        Assert.Equal("12.3457M", TiniSizeValue.Parse("12345678").ToScaledSIString(6));
        Assert.Equal("123.457M", TiniSizeValue.Parse("123456789").ToScaledSIString(6));
        Assert.Equal("1.23457G", TiniSizeValue.Parse("1234567890").ToScaledSIString(6));
        Assert.Equal("12.3457G", TiniSizeValue.Parse("12345678901").ToScaledSIString(6));
        Assert.Equal("123.457G", TiniSizeValue.Parse("123456789012").ToScaledSIString(6));
        Assert.Equal("1.23457T", TiniSizeValue.Parse("1234567890123").ToScaledSIString(6));
        Assert.Equal("12.3457T", TiniSizeValue.Parse("12345678901234").ToScaledSIString(6));
        Assert.Equal("123.457T", TiniSizeValue.Parse("123456789012345").ToScaledSIString(6));
        Assert.Equal("1.23457P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString(6));
        Assert.Equal("12.3457P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString(6));
        Assert.Equal("123.457P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString(6));
        Assert.Equal("1234.57P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString(6));
        Assert.Equal("12345.7P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString(6));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (7)")]
    public void ToScaledSIString7() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString(7));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledSIString(7));
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledSIString(7));
        Assert.Equal("1.234k", TiniSizeValue.Parse("1234").ToScaledSIString(7));
        Assert.Equal("12.345k", TiniSizeValue.Parse("12345").ToScaledSIString(7));
        Assert.Equal("123.456k", TiniSizeValue.Parse("123456").ToScaledSIString(7));
        Assert.Equal("1.234567M", TiniSizeValue.Parse("1234567").ToScaledSIString(7));
        Assert.Equal("12.34568M", TiniSizeValue.Parse("12345678").ToScaledSIString(7));
        Assert.Equal("123.4568M", TiniSizeValue.Parse("123456789").ToScaledSIString(7));
        Assert.Equal("1.234568G", TiniSizeValue.Parse("1234567890").ToScaledSIString(7));
        Assert.Equal("12.34568G", TiniSizeValue.Parse("12345678901").ToScaledSIString(7));
        Assert.Equal("123.4568G", TiniSizeValue.Parse("123456789012").ToScaledSIString(7));
        Assert.Equal("1.234568T", TiniSizeValue.Parse("1234567890123").ToScaledSIString(7));
        Assert.Equal("12.34568T", TiniSizeValue.Parse("12345678901234").ToScaledSIString(7));
        Assert.Equal("123.4568T", TiniSizeValue.Parse("123456789012345").ToScaledSIString(7));
        Assert.Equal("1.234568P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString(7));
        Assert.Equal("12.34568P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString(7));
        Assert.Equal("123.4568P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString(7));
        Assert.Equal("1234.568P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString(7));
        Assert.Equal("12345.68P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString(7));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (8)")]
    public void ToScaledSIString8() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString(8));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledSIString(8));
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledSIString(8));
        Assert.Equal("1.234k", TiniSizeValue.Parse("1234").ToScaledSIString(8));
        Assert.Equal("12.345k", TiniSizeValue.Parse("12345").ToScaledSIString(8));
        Assert.Equal("123.456k", TiniSizeValue.Parse("123456").ToScaledSIString(8));
        Assert.Equal("1.234567M", TiniSizeValue.Parse("1234567").ToScaledSIString(8));
        Assert.Equal("12.345678M", TiniSizeValue.Parse("12345678").ToScaledSIString(8));
        Assert.Equal("123.45679M", TiniSizeValue.Parse("123456789").ToScaledSIString(8));
        Assert.Equal("1.2345679G", TiniSizeValue.Parse("1234567890").ToScaledSIString(8));
        Assert.Equal("12.345679G", TiniSizeValue.Parse("12345678901").ToScaledSIString(8));
        Assert.Equal("123.45679G", TiniSizeValue.Parse("123456789012").ToScaledSIString(8));
        Assert.Equal("1.2345679T", TiniSizeValue.Parse("1234567890123").ToScaledSIString(8));
        Assert.Equal("12.345679T", TiniSizeValue.Parse("12345678901234").ToScaledSIString(8));
        Assert.Equal("123.45679T", TiniSizeValue.Parse("123456789012345").ToScaledSIString(8));
        Assert.Equal("1.2345679P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString(8));
        Assert.Equal("12.345679P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString(8));
        Assert.Equal("123.45679P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString(8));
        Assert.Equal("1234.5679P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString(8));
        Assert.Equal("12345.679P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString(8));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (9)")]
    public void ToScaledSIString9() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString(9));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledSIString(9));
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledSIString(9));
        Assert.Equal("1.234k", TiniSizeValue.Parse("1234").ToScaledSIString(9));
        Assert.Equal("12.345k", TiniSizeValue.Parse("12345").ToScaledSIString(9));
        Assert.Equal("123.456k", TiniSizeValue.Parse("123456").ToScaledSIString(9));
        Assert.Equal("1.234567M", TiniSizeValue.Parse("1234567").ToScaledSIString(9));
        Assert.Equal("12.345678M", TiniSizeValue.Parse("12345678").ToScaledSIString(9));
        Assert.Equal("123.456789M", TiniSizeValue.Parse("123456789").ToScaledSIString(9));
        Assert.Equal("1.23456789G", TiniSizeValue.Parse("1234567890").ToScaledSIString(9));
        Assert.Equal("12.3456789G", TiniSizeValue.Parse("12345678901").ToScaledSIString(9));
        Assert.Equal("123.456789G", TiniSizeValue.Parse("123456789012").ToScaledSIString(9));
        Assert.Equal("1.23456789T", TiniSizeValue.Parse("1234567890123").ToScaledSIString(9));
        Assert.Equal("12.3456789T", TiniSizeValue.Parse("12345678901234").ToScaledSIString(9));
        Assert.Equal("123.456789T", TiniSizeValue.Parse("123456789012345").ToScaledSIString(9));
        Assert.Equal("1.23456789P", TiniSizeValue.Parse("1234567890123456").ToScaledSIString(9));
        Assert.Equal("12.3456789P", TiniSizeValue.Parse("12345678901234567").ToScaledSIString(9));
        Assert.Equal("123.456789P", TiniSizeValue.Parse("123456789012345678").ToScaledSIString(9));
        Assert.Equal("1234.56789P", TiniSizeValue.Parse("1234567890123456789").ToScaledSIString(9));
        Assert.Equal("12345.6789P", TiniSizeValue.Parse("12345678901234567890").ToScaledSIString(9));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (10)")]
    public void ToScaledSIString10() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledSIString());
        Assert.Equal("10", TiniSizeValue.Parse("10").ToScaledSIString());
        Assert.Equal("102", TiniSizeValue.Parse("102").ToScaledSIString());
        Assert.Equal("1.02k", TiniSizeValue.Parse("1023").ToScaledSIString());
        Assert.Equal("10.2k", TiniSizeValue.Parse("10234").ToScaledSIString());
        Assert.Equal("102k", TiniSizeValue.Parse("102345").ToScaledSIString());
        Assert.Equal("1.02M", TiniSizeValue.Parse("1023456").ToScaledSIString());
        Assert.Equal("10.2M", TiniSizeValue.Parse("10234567").ToScaledSIString());
        Assert.Equal("102M", TiniSizeValue.Parse("102345678").ToScaledSIString());
        Assert.Equal("1.02G", TiniSizeValue.Parse("1023456789").ToScaledSIString());
        Assert.Equal("10.2G", TiniSizeValue.Parse("10234567890").ToScaledSIString());
        Assert.Equal("102G", TiniSizeValue.Parse("102345678901").ToScaledSIString());
        Assert.Equal("1.02T", TiniSizeValue.Parse("1023456789012").ToScaledSIString());
        Assert.Equal("10.2T", TiniSizeValue.Parse("10234567890123").ToScaledSIString());
        Assert.Equal("102T", TiniSizeValue.Parse("102345678901234").ToScaledSIString());
        Assert.Equal("1.02P", TiniSizeValue.Parse("1023456789012345").ToScaledSIString());
        Assert.Equal("10.2P", TiniSizeValue.Parse("10234567890123456").ToScaledSIString());
        Assert.Equal("102P", TiniSizeValue.Parse("102345678901234567").ToScaledSIString());
        Assert.Equal("1020P", TiniSizeValue.Parse("1023456789012345678").ToScaledSIString());
        Assert.Equal("10200P", TiniSizeValue.Parse("10234567890123456789").ToScaledSIString());
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (11)")]
    public void ToScaledSIString11() {
        Assert.Equal("3", TiniSizeValue.Parse("3").ToScaledSIString());
        Assert.Equal("31", TiniSizeValue.Parse("31").ToScaledSIString());
        Assert.Equal("314", TiniSizeValue.Parse("314").ToScaledSIString());
        Assert.Equal("3.14k", TiniSizeValue.Parse("3141").ToScaledSIString());
        Assert.Equal("31.4k", TiniSizeValue.Parse("31415").ToScaledSIString());
        Assert.Equal("314k", TiniSizeValue.Parse("314159").ToScaledSIString());
        Assert.Equal("3.14M", TiniSizeValue.Parse("3141592").ToScaledSIString());
        Assert.Equal("31.4M", TiniSizeValue.Parse("31415926").ToScaledSIString());
        Assert.Equal("314M", TiniSizeValue.Parse("314159265").ToScaledSIString());
        Assert.Equal("3.14G", TiniSizeValue.Parse("3141592653").ToScaledSIString());
        Assert.Equal("31.4G", TiniSizeValue.Parse("31415926535").ToScaledSIString());
        Assert.Equal("314G", TiniSizeValue.Parse("314159265358").ToScaledSIString());
        Assert.Equal("3.14T", TiniSizeValue.Parse("3141592653589").ToScaledSIString());
        Assert.Equal("31.4T", TiniSizeValue.Parse("31415926535897").ToScaledSIString());
        Assert.Equal("314T", TiniSizeValue.Parse("314159265358979").ToScaledSIString());
        Assert.Equal("3.14P", TiniSizeValue.Parse("3141592653589793").ToScaledSIString());
        Assert.Equal("31.4P", TiniSizeValue.Parse("31415926535897932").ToScaledSIString());
        Assert.Equal("314P", TiniSizeValue.Parse("314159265358979323").ToScaledSIString());
        Assert.Equal("3140P", TiniSizeValue.Parse("3141592653589793238").ToScaledSIString());
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledSIString (12)")]
    public void ToScaledSIString12() {
        Assert.Equal("2", TiniSizeValue.Parse("2").ToScaledSIString());
        Assert.Equal("27", TiniSizeValue.Parse("27").ToScaledSIString());
        Assert.Equal("271", TiniSizeValue.Parse("271").ToScaledSIString());
        Assert.Equal("2.72k", TiniSizeValue.Parse("2718").ToScaledSIString());
        Assert.Equal("27.2k", TiniSizeValue.Parse("27182").ToScaledSIString());
        Assert.Equal("272k", TiniSizeValue.Parse("271828").ToScaledSIString());
        Assert.Equal("2.72M", TiniSizeValue.Parse("2718281").ToScaledSIString());
        Assert.Equal("27.2M", TiniSizeValue.Parse("27182818").ToScaledSIString());
        Assert.Equal("272M", TiniSizeValue.Parse("271828182").ToScaledSIString());
        Assert.Equal("2.72G", TiniSizeValue.Parse("2718281828").ToScaledSIString());
        Assert.Equal("27.2G", TiniSizeValue.Parse("27182818284").ToScaledSIString());
        Assert.Equal("272G", TiniSizeValue.Parse("271828182845").ToScaledSIString());
        Assert.Equal("2.72T", TiniSizeValue.Parse("2718281828459").ToScaledSIString());
        Assert.Equal("27.2T", TiniSizeValue.Parse("27182818284590").ToScaledSIString());
        Assert.Equal("272T", TiniSizeValue.Parse("271828182845904").ToScaledSIString());
        Assert.Equal("2.72P", TiniSizeValue.Parse("2718281828459045").ToScaledSIString());
        Assert.Equal("27.2P", TiniSizeValue.Parse("27182818284590452").ToScaledSIString());
        Assert.Equal("272P", TiniSizeValue.Parse ("271828182845904523").ToScaledSIString());
        Assert.Equal("2720P", TiniSizeValue.Parse("2718281828459045235").ToScaledSIString());
    }


    [Fact(DisplayName = "TiniSizeValue: ToScaledBinaryString")]
    public void ToScaledBinaryString() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledBinaryString());
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledBinaryString());
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledBinaryString());
        Assert.Equal("1.21Ki", TiniSizeValue.Parse("1234").ToScaledBinaryString());
        Assert.Equal("12.1Ki", TiniSizeValue.Parse("12345").ToScaledBinaryString());
        Assert.Equal("121Ki", TiniSizeValue.Parse("123456").ToScaledBinaryString());
        Assert.Equal("1.18Mi", TiniSizeValue.Parse("1234567").ToScaledBinaryString());
        Assert.Equal("11.8Mi", TiniSizeValue.Parse("12345678").ToScaledBinaryString());
        Assert.Equal("118Mi", TiniSizeValue.Parse("123456789").ToScaledBinaryString());
        Assert.Equal("1.15Gi", TiniSizeValue.Parse("1234567890").ToScaledBinaryString());
        Assert.Equal("11.5Gi", TiniSizeValue.Parse("12345678901").ToScaledBinaryString());
        Assert.Equal("115Gi", TiniSizeValue.Parse("123456789012").ToScaledBinaryString());
        Assert.Equal("1.12Ti", TiniSizeValue.Parse("1234567890123").ToScaledBinaryString());
        Assert.Equal("11.2Ti", TiniSizeValue.Parse("12345678901234").ToScaledBinaryString());
        Assert.Equal("112Ti", TiniSizeValue.Parse("123456789012345").ToScaledBinaryString());
        Assert.Equal("1.10Pi", TiniSizeValue.Parse("1234567890123456").ToScaledBinaryString());
        Assert.Equal("11.0Pi", TiniSizeValue.Parse("12345678901234567").ToScaledBinaryString());
        Assert.Equal("110Pi", TiniSizeValue.Parse("123456789012345678").ToScaledBinaryString());
        Assert.Equal("1100Pi", TiniSizeValue.Parse("1234567890123456789").ToScaledBinaryString());
        Assert.Equal("11000Pi", TiniSizeValue.Parse("12345678901234567890").ToScaledBinaryString());
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledBinaryString (1)")]
    public void ToScaledBinaryString1() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledBinaryString(1));
        Assert.Equal("10", TiniSizeValue.Parse("12").ToScaledBinaryString(1));
        Assert.Equal("100", TiniSizeValue.Parse("123").ToScaledBinaryString(1));
        Assert.Equal("1Ki", TiniSizeValue.Parse("1234").ToScaledBinaryString(1));
        Assert.Equal("10Ki", TiniSizeValue.Parse("12345").ToScaledBinaryString(1));
        Assert.Equal("100Ki", TiniSizeValue.Parse("123456").ToScaledBinaryString(1));
        Assert.Equal("1Mi", TiniSizeValue.Parse("1234567").ToScaledBinaryString(1));
        Assert.Equal("10Mi", TiniSizeValue.Parse("12345678").ToScaledBinaryString(1));
        Assert.Equal("100Mi", TiniSizeValue.Parse("123456789").ToScaledBinaryString(1));
        Assert.Equal("1Gi", TiniSizeValue.Parse("1234567890").ToScaledBinaryString(1));
        Assert.Equal("10Gi", TiniSizeValue.Parse("12345678901").ToScaledBinaryString(1));
        Assert.Equal("100Gi", TiniSizeValue.Parse("123456789012").ToScaledBinaryString(1));
        Assert.Equal("1Ti", TiniSizeValue.Parse("1234567890123").ToScaledBinaryString(1));
        Assert.Equal("10Ti", TiniSizeValue.Parse("12345678901234").ToScaledBinaryString(1));
        Assert.Equal("100Ti", TiniSizeValue.Parse("123456789012345").ToScaledBinaryString(1));
        Assert.Equal("1Pi", TiniSizeValue.Parse("1234567890123456").ToScaledBinaryString(1));
        Assert.Equal("10Pi", TiniSizeValue.Parse("12345678901234567").ToScaledBinaryString(1));
        Assert.Equal("100Pi", TiniSizeValue.Parse("123456789012345678").ToScaledBinaryString(1));
        Assert.Equal("1000Pi", TiniSizeValue.Parse("1234567890123456789").ToScaledBinaryString(1));
        Assert.Equal("10000Pi", TiniSizeValue.Parse("12345678901234567890").ToScaledBinaryString(1));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledBinaryString (2)")]
    public void ToScaledBinaryString2() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledBinaryString(2));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledBinaryString(2));
        Assert.Equal("120", TiniSizeValue.Parse("123").ToScaledBinaryString(2));
        Assert.Equal("1.2Ki", TiniSizeValue.Parse("1234").ToScaledBinaryString(2));
        Assert.Equal("12Ki", TiniSizeValue.Parse("12345").ToScaledBinaryString(2));
        Assert.Equal("120Ki", TiniSizeValue.Parse("123456").ToScaledBinaryString(2));
        Assert.Equal("1.2Mi", TiniSizeValue.Parse("1234567").ToScaledBinaryString(2));
        Assert.Equal("12Mi", TiniSizeValue.Parse("12345678").ToScaledBinaryString(2));
        Assert.Equal("120Mi", TiniSizeValue.Parse("123456789").ToScaledBinaryString(2));
        Assert.Equal("1.1Gi", TiniSizeValue.Parse("1234567890").ToScaledBinaryString(2));
        Assert.Equal("11Gi", TiniSizeValue.Parse("12345678901").ToScaledBinaryString(2));
        Assert.Equal("110Gi", TiniSizeValue.Parse("123456789012").ToScaledBinaryString(2));
        Assert.Equal("1.1Ti", TiniSizeValue.Parse("1234567890123").ToScaledBinaryString(2));
        Assert.Equal("11Ti", TiniSizeValue.Parse("12345678901234").ToScaledBinaryString(2));
        Assert.Equal("110Ti", TiniSizeValue.Parse("123456789012345").ToScaledBinaryString(2));
        Assert.Equal("1.1Pi", TiniSizeValue.Parse("1234567890123456").ToScaledBinaryString(2));
        Assert.Equal("11Pi", TiniSizeValue.Parse("12345678901234567").ToScaledBinaryString(2));
        Assert.Equal("110Pi", TiniSizeValue.Parse("123456789012345678").ToScaledBinaryString(2));
        Assert.Equal("1100Pi", TiniSizeValue.Parse("1234567890123456789").ToScaledBinaryString(2));
        Assert.Equal("11000Pi", TiniSizeValue.Parse("12345678901234567890").ToScaledBinaryString(2));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledBinaryString (3)")]
    public void ToScaledBinaryString3() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledBinaryString(3));
        Assert.Equal("12", TiniSizeValue.Parse("12").ToScaledBinaryString(3));
        Assert.Equal("123", TiniSizeValue.Parse("123").ToScaledBinaryString(3));
        Assert.Equal("1.21Ki", TiniSizeValue.Parse("1234").ToScaledBinaryString(3));
        Assert.Equal("12.1Ki", TiniSizeValue.Parse("12345").ToScaledBinaryString(3));
        Assert.Equal("121Ki", TiniSizeValue.Parse("123456").ToScaledBinaryString(3));
        Assert.Equal("1.18Mi", TiniSizeValue.Parse("1234567").ToScaledBinaryString(3));
        Assert.Equal("11.8Mi", TiniSizeValue.Parse("12345678").ToScaledBinaryString(3));
        Assert.Equal("118Mi", TiniSizeValue.Parse("123456789").ToScaledBinaryString(3));
        Assert.Equal("1.15Gi", TiniSizeValue.Parse("1234567890").ToScaledBinaryString(3));
        Assert.Equal("11.5Gi", TiniSizeValue.Parse("12345678901").ToScaledBinaryString(3));
        Assert.Equal("115Gi", TiniSizeValue.Parse("123456789012").ToScaledBinaryString(3));
        Assert.Equal("1.12Ti", TiniSizeValue.Parse("1234567890123").ToScaledBinaryString(3));
        Assert.Equal("11.2Ti", TiniSizeValue.Parse("12345678901234").ToScaledBinaryString(3));
        Assert.Equal("112Ti", TiniSizeValue.Parse("123456789012345").ToScaledBinaryString(3));
        Assert.Equal("1.10Pi", TiniSizeValue.Parse("1234567890123456").ToScaledBinaryString(3));
        Assert.Equal("11.0Pi", TiniSizeValue.Parse("12345678901234567").ToScaledBinaryString(3));
        Assert.Equal("110Pi", TiniSizeValue.Parse("123456789012345678").ToScaledBinaryString(3));
        Assert.Equal("1100Pi", TiniSizeValue.Parse("1234567890123456789").ToScaledBinaryString(3));
        Assert.Equal("11000Pi", TiniSizeValue.Parse("12345678901234567890").ToScaledBinaryString(3));
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledBinaryString (10)")]
    public void ToScaledBinaryString10() {
        Assert.Equal("1", TiniSizeValue.Parse("1").ToScaledBinaryString());
        Assert.Equal("10", TiniSizeValue.Parse("10").ToScaledBinaryString());
        Assert.Equal("102", TiniSizeValue.Parse("102").ToScaledBinaryString());
        Assert.Equal("1020", TiniSizeValue.Parse("1023").ToScaledBinaryString());
        Assert.Equal("9.99Ki", TiniSizeValue.Parse("10234").ToScaledBinaryString());
        Assert.Equal("99.9Ki", TiniSizeValue.Parse("102345").ToScaledBinaryString());
        Assert.Equal("999Ki", TiniSizeValue.Parse("1023456").ToScaledBinaryString());
        Assert.Equal("9.76Mi", TiniSizeValue.Parse("10234567").ToScaledBinaryString());
        Assert.Equal("97.6Mi", TiniSizeValue.Parse("102345678").ToScaledBinaryString());
        Assert.Equal("976Mi", TiniSizeValue.Parse("1023456789").ToScaledBinaryString());
        Assert.Equal("9.53Gi", TiniSizeValue.Parse("10234567890").ToScaledBinaryString());
        Assert.Equal("95.3Gi", TiniSizeValue.Parse("102345678901").ToScaledBinaryString());
        Assert.Equal("953Gi", TiniSizeValue.Parse("1023456789012").ToScaledBinaryString());
        Assert.Equal("9.31Ti", TiniSizeValue.Parse("10234567890123").ToScaledBinaryString());
        Assert.Equal("93.1Ti", TiniSizeValue.Parse("102345678901234").ToScaledBinaryString());
        Assert.Equal("931Ti", TiniSizeValue.Parse("1023456789012345").ToScaledBinaryString());
        Assert.Equal("9.09Pi", TiniSizeValue.Parse("10234567890123456").ToScaledBinaryString());
        Assert.Equal("90.9Pi", TiniSizeValue.Parse("102345678901234567").ToScaledBinaryString());
        Assert.Equal("909Pi", TiniSizeValue.Parse("1023456789012345678").ToScaledBinaryString());
        Assert.Equal("9090Pi", TiniSizeValue.Parse("10234567890123456789").ToScaledBinaryString());
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledBinaryString (11)")]
    public void ToScaledBinaryString11() {
        Assert.Equal("3", TiniSizeValue.Parse("3").ToScaledBinaryString());
        Assert.Equal("31", TiniSizeValue.Parse("31").ToScaledBinaryString());
        Assert.Equal("314", TiniSizeValue.Parse("314").ToScaledBinaryString());
        Assert.Equal("3.07Ki", TiniSizeValue.Parse("3141").ToScaledBinaryString());
        Assert.Equal("30.7Ki", TiniSizeValue.Parse("31415").ToScaledBinaryString());
        Assert.Equal("307Ki", TiniSizeValue.Parse("314159").ToScaledBinaryString());
        Assert.Equal("3.00Mi", TiniSizeValue.Parse("3141592").ToScaledBinaryString());
        Assert.Equal("30.0Mi", TiniSizeValue.Parse("31415926").ToScaledBinaryString());
        Assert.Equal("300Mi", TiniSizeValue.Parse("314159265").ToScaledBinaryString());
        Assert.Equal("2.93Gi", TiniSizeValue.Parse("3141592653").ToScaledBinaryString());
        Assert.Equal("29.3Gi", TiniSizeValue.Parse("31415926535").ToScaledBinaryString());
        Assert.Equal("293Gi", TiniSizeValue.Parse("314159265358").ToScaledBinaryString());
        Assert.Equal("2.86Ti", TiniSizeValue.Parse("3141592653589").ToScaledBinaryString());
        Assert.Equal("28.6Ti", TiniSizeValue.Parse("31415926535897").ToScaledBinaryString());
        Assert.Equal("286Ti", TiniSizeValue.Parse("314159265358979").ToScaledBinaryString());
        Assert.Equal("2.79Pi", TiniSizeValue.Parse("3141592653589793").ToScaledBinaryString());
        Assert.Equal("27.9Pi", TiniSizeValue.Parse("31415926535897932").ToScaledBinaryString());
        Assert.Equal("279Pi", TiniSizeValue.Parse("314159265358979323").ToScaledBinaryString());
        Assert.Equal("2790Pi", TiniSizeValue.Parse("3141592653589793238").ToScaledBinaryString());
    }

    [Fact(DisplayName = "TiniSizeValue: ToScaledBinaryString (12)")]
    public void ToScaledBinaryString12() {
        Assert.Equal("2", TiniSizeValue.Parse("2").ToScaledBinaryString());
        Assert.Equal("27", TiniSizeValue.Parse("27").ToScaledBinaryString());
        Assert.Equal("271", TiniSizeValue.Parse("271").ToScaledBinaryString());
        Assert.Equal("2.65Ki", TiniSizeValue.Parse("2718").ToScaledBinaryString());
        Assert.Equal("26.5Ki", TiniSizeValue.Parse("27182").ToScaledBinaryString());
        Assert.Equal("265Ki", TiniSizeValue.Parse("271828").ToScaledBinaryString());
        Assert.Equal("2.59Mi", TiniSizeValue.Parse("2718281").ToScaledBinaryString());
        Assert.Equal("25.9Mi", TiniSizeValue.Parse("27182818").ToScaledBinaryString());
        Assert.Equal("259Mi", TiniSizeValue.Parse("271828182").ToScaledBinaryString());
        Assert.Equal("2.53Gi", TiniSizeValue.Parse("2718281828").ToScaledBinaryString());
        Assert.Equal("25.3Gi", TiniSizeValue.Parse("27182818284").ToScaledBinaryString());
        Assert.Equal("253Gi", TiniSizeValue.Parse("271828182845").ToScaledBinaryString());
        Assert.Equal("2.47Ti", TiniSizeValue.Parse("2718281828459").ToScaledBinaryString());
        Assert.Equal("24.7Ti", TiniSizeValue.Parse("27182818284590").ToScaledBinaryString());
        Assert.Equal("247Ti", TiniSizeValue.Parse("271828182845904").ToScaledBinaryString());
        Assert.Equal("2.41Pi", TiniSizeValue.Parse("2718281828459045").ToScaledBinaryString());
        Assert.Equal("24.1Pi", TiniSizeValue.Parse("27182818284590452").ToScaledBinaryString());
        Assert.Equal("241Pi", TiniSizeValue.Parse("271828182845904523").ToScaledBinaryString());
        Assert.Equal("2410Pi", TiniSizeValue.Parse("2718281828459045235").ToScaledBinaryString());
    }

}
