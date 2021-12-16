using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueSizeTests {

    [Fact(DisplayName = "TiniValueSize: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(TiniValueSize.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueSize.Parse(text));
    }

    [Fact(DisplayName = "TiniValueSize: Base (1)")]
    public void Base1() {
        Assert.Equal("42", TiniValueSize.Parse("42"));
    }

    [Fact(DisplayName = "TiniValueSize: Base (2)")]
    public void Base2() {
        Assert.Equal("43", TiniValueSize.Parse("42.84"));
    }

    [Fact(DisplayName = "TiniValueSize: Base (3)")]
    public void Base3() {
        Assert.Equal("43", TiniValueSize.Parse(" 42.84"));
    }


    [Fact(DisplayName = "TiniValueSize: Kilo (1)")]
    public void Kilo1() {
        Assert.Equal("42000", TiniValueSize.Parse("42K"));
    }

    [Fact(DisplayName = "TiniValueSize: Kilo (2)")]
    public void Kilo2() {
        Assert.Equal("42840", TiniValueSize.Parse("42.84 k"));
    }

    [Fact(DisplayName = "TiniValueSize: Kilo (3)")]
    public void Kilo3() {
        Assert.Equal("1", TiniValueSize.Parse(" 0.0001 k "));
    }

    [Fact(DisplayName = "TiniValueSize: Kibi (1)")]
    public void Kibi1() {
        Assert.Equal("43008", TiniValueSize.Parse("42Ki"));
    }

    [Fact(DisplayName = "TiniValueSize: Kibi (2)")]
    public void Kibi2() {
        Assert.Equal("43868", TiniValueSize.Parse("42.84ki"));
    }

    [Fact(DisplayName = "TiniValueSize: Kibi (3)")]
    public void Kibi3() {
        Assert.Equal("1", TiniValueSize.Parse(" 0.000001 ki "));
    }


    [Fact(DisplayName = "TiniValueSize: Mega (1)")]
    public void Mega1() {
        Assert.Equal("42000000", TiniValueSize.Parse("42m"));
    }

    [Fact(DisplayName = "TiniValueSize: Mega (2)")]
    public void Mega2() {
        Assert.Equal("42840000", TiniValueSize.Parse("42.84 M"));
    }

    [Fact(DisplayName = "TiniValueSize: Mega (3)")]
    public void Mega3() {
        Assert.Equal("1", TiniValueSize.Parse(" 0.0000001 M "));
    }

    [Fact(DisplayName = "TiniValueSize: Mebi (1)")]
    public void Mebi1() {
        Assert.Equal("44040192", TiniValueSize.Parse("42Mi"));
    }

    [Fact(DisplayName = "TiniValueSize: Mebi (2)")]
    public void Mebi2() {
        Assert.Equal("44920996", TiniValueSize.Parse("42.84Mi"));
    }

    [Fact(DisplayName = "TiniValueSize: Mebi (3)")]
    public void Mebi3() {
        Assert.Equal("1", TiniValueSize.Parse(" 0.0000001 Mi "));
    }


    [Fact(DisplayName = "TiniValueSize: Giga (1)")]
    public void Giga1() {
        Assert.Equal("42000000000", TiniValueSize.Parse("42 G"));
    }

    [Fact(DisplayName = "TiniValueSize: Giga (2)")]
    public void Giga2() {
        Assert.Equal("42840000000", TiniValueSize.Parse("42.84g"));
    }

    [Fact(DisplayName = "TiniValueSize: Giga (3)")]
    public void Giga3() {
        Assert.Equal("1", TiniValueSize.Parse("  0.0000000001 G "));
    }

    [Fact(DisplayName = "TiniValueSize: Gibi (1)")]
    public void Gibi1() {
        Assert.Equal("45097156608", TiniValueSize.Parse("42 Gi"));
    }

    [Fact(DisplayName = "TiniValueSize: Gibi (2)")]
    public void Gibi2() {
        Assert.Equal("45999099740", TiniValueSize.Parse("42.84Gi"));
    }

    [Fact(DisplayName = "TiniValueSize: Gibi (3)")]
    public void Gibi3() {
        Assert.Equal("1", TiniValueSize.Parse("  0.0000000001 GI "));
    }


    [Fact(DisplayName = "TiniValueSize: Tera (1)")]
    public void Tera1() {
        Assert.Equal("42000000000000", TiniValueSize.Parse("42T"));
    }

    [Fact(DisplayName = "TiniValueSize: Tera (2)")]
    public void Tera2() {
        Assert.Equal("42840000000000", TiniValueSize.Parse("42.84 T"));
    }

    [Fact(DisplayName = "TiniValueSize: Tera (3)")]
    public void Tera3() {
        Assert.Equal("1", TiniValueSize.Parse("  0.0000000000001 T "));
    }

    [Fact(DisplayName = "TiniValueSize: Tebi (1)")]
    public void Tebi1() {
        Assert.Equal("46179488366592", TiniValueSize.Parse("42Ti"));
    }

    [Fact(DisplayName = "TiniValueSize: Tebi (2)")]
    public void Tebi2() {
        Assert.Equal("47103078133924", TiniValueSize.Parse("42.84Ti"));
    }

    [Fact(DisplayName = "TiniValueSize: Tebi (3)")]
    public void Tebi3() {
        Assert.Equal("1", TiniValueSize.Parse("  0.0000000000001 Ti "));
    }


    [Fact(DisplayName = "TiniValueSize: Peta (1)")]
    public void Peta1() {
        Assert.Equal("42000000000000000", TiniValueSize.Parse("42p"));
    }

    [Fact(DisplayName = "TiniValueSize: Peta (2)")]
    public void Peta2() {
        Assert.Equal("42840000000000000", TiniValueSize.Parse("42.84 P"));
    }

    [Fact(DisplayName = "TiniValueSize: Peta (3)")]
    public void Peta3() {
        Assert.Equal("1", TiniValueSize.Parse("  0.0000000000000001 P "));
    }

    [Fact(DisplayName = "TiniValueSize: Pebi (1)")]
    public void Pebi1() {
        Assert.Equal("47287796087390208", TiniValueSize.Parse("42pi"));
    }

    [Fact(DisplayName = "TiniValueSize: Pebi (2)")]
    public void Pebi2() {
        Assert.Equal("48233552009138012", TiniValueSize.Parse("42.84PI"));
    }

    [Fact(DisplayName = "TiniValueSize: Pebi (3)")]
    public void Pebi3() {
        Assert.Equal("1", TiniValueSize.Parse("  0.0000000000000001 PI "));
    }


    [Fact(DisplayName = "TiniValueSize: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueSize.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueSize.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueSize: Out of range")]
    public void OutOfRange() {
        Assert.True(TiniValueSize.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.False(TiniValueSize.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.True(TiniValueSize.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.False(TiniValueSize.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
        Assert.False(TiniValueSize.TryParse("42E", out var _));
    }


    [Fact(DisplayName = "TiniValueSize: ToKiloString")]
    public void ToKiloString() {
        Assert.Equal("0.042k", TiniValueSize.Parse("42").ToKiloString());
        Assert.Equal("0.0420k", TiniValueSize.Parse("42").ToKiloString("0.0000"));
        Assert.Equal("420k", TiniValueSize.Parse("420000").ToKiloString());
        Assert.Equal("420.0000k", TiniValueSize.Parse("420000").ToKiloString("0.0000"));
        Assert.Equal("4200000k", TiniValueSize.Parse("4200000000").ToKiloString());
        Assert.Equal("4200000.0000k", TiniValueSize.Parse("4200000000").ToKiloString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToKibiString")]
    public void ToKibiString() {
        Assert.Equal("0.041015625Ki", TiniValueSize.Parse("42").ToKibiString());
        Assert.Equal("0.0410Ki", TiniValueSize.Parse("42").ToKibiString("0.0000"));
        Assert.Equal("410.15625Ki", TiniValueSize.Parse("420000").ToKibiString());
        Assert.Equal("410.1563Ki", TiniValueSize.Parse("420000").ToKibiString("0.0000"));
        Assert.Equal("4101562.5Ki", TiniValueSize.Parse("4200000000").ToKibiString());
        Assert.Equal("4101562.5000Ki", TiniValueSize.Parse("4200000000").ToKibiString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToMegatring")]
    public void ToMegaString() {
        Assert.Equal("0.42M", TiniValueSize.Parse("420000").ToMegaString());
        Assert.Equal("0.4200M", TiniValueSize.Parse("420000").ToMegaString("0.0000"));
        Assert.Equal("4200M", TiniValueSize.Parse("4200000000").ToMegaString());
        Assert.Equal("4200.0000M", TiniValueSize.Parse("4200000000").ToMegaString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToMebiString")]
    public void ToMebiString() {
        Assert.Equal("0.400543212890625Mi", TiniValueSize.Parse("420000").ToMebiString());
        Assert.Equal("0.4005Mi", TiniValueSize.Parse("420000").ToMebiString("0.0000"));
        Assert.Equal("4005.43212890625Mi", TiniValueSize.Parse("4200000000").ToMebiString());
        Assert.Equal("4005.4321Mi", TiniValueSize.Parse("4200000000").ToMebiString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToGigaString")]
    public void ToGigaString() {
        Assert.Equal("0.00042G", TiniValueSize.Parse("420000").ToGigaString());
        Assert.Equal("0.0004G", TiniValueSize.Parse("420000").ToGigaString("0.0000"));
        Assert.Equal("4.2G", TiniValueSize.Parse("4200000000").ToGigaString());
        Assert.Equal("4.2000G", TiniValueSize.Parse("4200000000").ToGigaString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToGibiString")]
    public void ToGibiString() {
        Assert.Equal("0.000391155481338501Gi", TiniValueSize.Parse("420000").ToGibiString());
        Assert.Equal("0.0004Gi", TiniValueSize.Parse("420000").ToGibiString("0.0000"));
        Assert.Equal("3.9115548133850098Gi", TiniValueSize.Parse("4200000000").ToGibiString());
        Assert.Equal("3.9116Gi", TiniValueSize.Parse("4200000000").ToGibiString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToTeraString")]
    public void ToTeraString() {
        Assert.Equal("0.00044T", TiniValueSize.Parse("440000000").ToTeraString());
        Assert.Equal("0.0004T", TiniValueSize.Parse("440000000").ToTeraString("0.0000"));
        Assert.Equal("4.4T", TiniValueSize.Parse("4400000000000").ToTeraString());
        Assert.Equal("4.4000T", TiniValueSize.Parse("4400000000000").ToTeraString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToTebiString")]
    public void ToTebiString() {
        Assert.Equal("0.0004001776687800884Ti", TiniValueSize.Parse("440000000").ToTebiString());
        Assert.Equal("0.0004Ti", TiniValueSize.Parse("440000000").ToTebiString("0.0000"));
        Assert.Equal("4.001776687800884Ti", TiniValueSize.Parse("4400000000000").ToTebiString());
        Assert.Equal("4.0018Ti", TiniValueSize.Parse("4400000000000").ToTebiString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToPetaString")]
    public void ToPetaString() {
        Assert.Equal("0.00046P", TiniValueSize.Parse("460000000000").ToPetaString());
        Assert.Equal("0.0005P", TiniValueSize.Parse("460000000000").ToPetaString("0.0000"));
        Assert.Equal("4.6P", TiniValueSize.Parse("4600000000000000").ToPetaString());
        Assert.Equal("4.6000P", TiniValueSize.Parse("4600000000000000").ToPetaString("0.0000"));
    }

    [Fact(DisplayName = "TiniValueSize: ToPebiString")]
    public void ToPebiString() {
        Assert.Equal("0.0004085620730620576Pi", TiniValueSize.Parse("460000000000").ToPebiString());
        Assert.Equal("0.0004Pi", TiniValueSize.Parse("460000000000").ToPebiString("0.0000"));
        Assert.Equal("4.085620730620576Pi", TiniValueSize.Parse("4600000000000000").ToPebiString());
        Assert.Equal("4.0856Pi", TiniValueSize.Parse("4600000000000000").ToPebiString("0.0000"));
    }


    [Fact(DisplayName = "TiniValueSize: ToScaledSIString")]
    public void ToScaledSIString() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString());
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledSIString());
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledSIString());
        Assert.Equal("1.23k", TiniValueSize.Parse("1234").ToScaledSIString());
        Assert.Equal("12.3k", TiniValueSize.Parse("12345").ToScaledSIString());
        Assert.Equal("123k", TiniValueSize.Parse("123456").ToScaledSIString());
        Assert.Equal("1.23M", TiniValueSize.Parse("1234567").ToScaledSIString());
        Assert.Equal("12.3M", TiniValueSize.Parse("12345678").ToScaledSIString());
        Assert.Equal("123M", TiniValueSize.Parse("123456789").ToScaledSIString());
        Assert.Equal("1.23G", TiniValueSize.Parse("1234567890").ToScaledSIString());
        Assert.Equal("12.3G", TiniValueSize.Parse("12345678901").ToScaledSIString());
        Assert.Equal("123G", TiniValueSize.Parse("123456789012").ToScaledSIString());
        Assert.Equal("1.23T", TiniValueSize.Parse("1234567890123").ToScaledSIString());
        Assert.Equal("12.3T", TiniValueSize.Parse("12345678901234").ToScaledSIString());
        Assert.Equal("123T", TiniValueSize.Parse("123456789012345").ToScaledSIString());
        Assert.Equal("1.23P", TiniValueSize.Parse("1234567890123456").ToScaledSIString());
        Assert.Equal("12.3P", TiniValueSize.Parse("12345678901234567").ToScaledSIString());
        Assert.Equal("123P", TiniValueSize.Parse("123456789012345678").ToScaledSIString());
        Assert.Equal("1230P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString());
        Assert.Equal("12300P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString());
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (1)")]
    public void ToScaledSIString1() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString(1));
        Assert.Equal("10", TiniValueSize.Parse("12").ToScaledSIString(1));
        Assert.Equal("100", TiniValueSize.Parse("123").ToScaledSIString(1));
        Assert.Equal("1k", TiniValueSize.Parse("1234").ToScaledSIString(1));
        Assert.Equal("10k", TiniValueSize.Parse("12345").ToScaledSIString(1));
        Assert.Equal("100k", TiniValueSize.Parse("123456").ToScaledSIString(1));
        Assert.Equal("1M", TiniValueSize.Parse("1234567").ToScaledSIString(1));
        Assert.Equal("10M", TiniValueSize.Parse("12345678").ToScaledSIString(1));
        Assert.Equal("100M", TiniValueSize.Parse("123456789").ToScaledSIString(1));
        Assert.Equal("1G", TiniValueSize.Parse("1234567890").ToScaledSIString(1));
        Assert.Equal("10G", TiniValueSize.Parse("12345678901").ToScaledSIString(1));
        Assert.Equal("100G", TiniValueSize.Parse("123456789012").ToScaledSIString(1));
        Assert.Equal("1T", TiniValueSize.Parse("1234567890123").ToScaledSIString(1));
        Assert.Equal("10T", TiniValueSize.Parse("12345678901234").ToScaledSIString(1));
        Assert.Equal("100T", TiniValueSize.Parse("123456789012345").ToScaledSIString(1));
        Assert.Equal("1P", TiniValueSize.Parse("1234567890123456").ToScaledSIString(1));
        Assert.Equal("10P", TiniValueSize.Parse("12345678901234567").ToScaledSIString(1));
        Assert.Equal("100P", TiniValueSize.Parse("123456789012345678").ToScaledSIString(1));
        Assert.Equal("1000P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString(1));
        Assert.Equal("10000P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString(1));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (3)")]
    public void ToScaledSIString3() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString(3));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledSIString(3));
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledSIString(3));
        Assert.Equal("1.23k", TiniValueSize.Parse("1234").ToScaledSIString(3));
        Assert.Equal("12.3k", TiniValueSize.Parse("12345").ToScaledSIString(3));
        Assert.Equal("123k", TiniValueSize.Parse("123456").ToScaledSIString(3));
        Assert.Equal("1.23M", TiniValueSize.Parse("1234567").ToScaledSIString(3));
        Assert.Equal("12.3M", TiniValueSize.Parse("12345678").ToScaledSIString(3));
        Assert.Equal("123M", TiniValueSize.Parse("123456789").ToScaledSIString(3));
        Assert.Equal("1.23G", TiniValueSize.Parse("1234567890").ToScaledSIString(3));
        Assert.Equal("12.3G", TiniValueSize.Parse("12345678901").ToScaledSIString(3));
        Assert.Equal("123G", TiniValueSize.Parse("123456789012").ToScaledSIString(3));
        Assert.Equal("1.23T", TiniValueSize.Parse("1234567890123").ToScaledSIString(3));
        Assert.Equal("12.3T", TiniValueSize.Parse("12345678901234").ToScaledSIString(3));
        Assert.Equal("123T", TiniValueSize.Parse("123456789012345").ToScaledSIString(3));
        Assert.Equal("1.23P", TiniValueSize.Parse("1234567890123456").ToScaledSIString(3));
        Assert.Equal("12.3P", TiniValueSize.Parse("12345678901234567").ToScaledSIString(3));
        Assert.Equal("123P", TiniValueSize.Parse("123456789012345678").ToScaledSIString(3));
        Assert.Equal("1230P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString(3));
        Assert.Equal("12300P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString(3));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (4)")]
    public void ToScaledSIString4() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString(4));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledSIString(4));
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledSIString(4));
        Assert.Equal("1.234k", TiniValueSize.Parse("1234").ToScaledSIString(4));
        Assert.Equal("12.35k", TiniValueSize.Parse("12345").ToScaledSIString(4));
        Assert.Equal("123.5k", TiniValueSize.Parse("123456").ToScaledSIString(4));
        Assert.Equal("1.235M", TiniValueSize.Parse("1234567").ToScaledSIString(4));
        Assert.Equal("12.35M", TiniValueSize.Parse("12345678").ToScaledSIString(4));
        Assert.Equal("123.5M", TiniValueSize.Parse("123456789").ToScaledSIString(4));
        Assert.Equal("1.235G", TiniValueSize.Parse("1234567890").ToScaledSIString(4));
        Assert.Equal("12.35G", TiniValueSize.Parse("12345678901").ToScaledSIString(4));
        Assert.Equal("123.5G", TiniValueSize.Parse("123456789012").ToScaledSIString(4));
        Assert.Equal("1.235T", TiniValueSize.Parse("1234567890123").ToScaledSIString(4));
        Assert.Equal("12.35T", TiniValueSize.Parse("12345678901234").ToScaledSIString(4));
        Assert.Equal("123.5T", TiniValueSize.Parse("123456789012345").ToScaledSIString(4));
        Assert.Equal("1.235P", TiniValueSize.Parse("1234567890123456").ToScaledSIString(4));
        Assert.Equal("12.35P", TiniValueSize.Parse("12345678901234567").ToScaledSIString(4));
        Assert.Equal("123.5P", TiniValueSize.Parse("123456789012345678").ToScaledSIString(4));
        Assert.Equal("1235P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString(4));
        Assert.Equal("12350P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString(4));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (5)")]
    public void ToScaledSIString5() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString(5));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledSIString(5));
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledSIString(5));
        Assert.Equal("1.234k", TiniValueSize.Parse("1234").ToScaledSIString(5));
        Assert.Equal("12.345k", TiniValueSize.Parse("12345").ToScaledSIString(5));
        Assert.Equal("123.46k", TiniValueSize.Parse("123456").ToScaledSIString(5));
        Assert.Equal("1.2346M", TiniValueSize.Parse("1234567").ToScaledSIString(5));
        Assert.Equal("12.346M", TiniValueSize.Parse("12345678").ToScaledSIString(5));
        Assert.Equal("123.46M", TiniValueSize.Parse("123456789").ToScaledSIString(5));
        Assert.Equal("1.2346G", TiniValueSize.Parse("1234567890").ToScaledSIString(5));
        Assert.Equal("12.346G", TiniValueSize.Parse("12345678901").ToScaledSIString(5));
        Assert.Equal("123.46G", TiniValueSize.Parse("123456789012").ToScaledSIString(5));
        Assert.Equal("1.2346T", TiniValueSize.Parse("1234567890123").ToScaledSIString(5));
        Assert.Equal("12.346T", TiniValueSize.Parse("12345678901234").ToScaledSIString(5));
        Assert.Equal("123.46T", TiniValueSize.Parse("123456789012345").ToScaledSIString(5));
        Assert.Equal("1.2346P", TiniValueSize.Parse("1234567890123456").ToScaledSIString(5));
        Assert.Equal("12.346P", TiniValueSize.Parse("12345678901234567").ToScaledSIString(5));
        Assert.Equal("123.46P", TiniValueSize.Parse("123456789012345678").ToScaledSIString(5));
        Assert.Equal("1234.6P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString(5));
        Assert.Equal("12346P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString(5));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (6)")]
    public void ToScaledSIString6() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString(6));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledSIString(6));
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledSIString(6));
        Assert.Equal("1.234k", TiniValueSize.Parse("1234").ToScaledSIString(6));
        Assert.Equal("12.345k", TiniValueSize.Parse("12345").ToScaledSIString(6));
        Assert.Equal("123.456k", TiniValueSize.Parse("123456").ToScaledSIString(6));
        Assert.Equal("1.23457M", TiniValueSize.Parse("1234567").ToScaledSIString(6));
        Assert.Equal("12.3457M", TiniValueSize.Parse("12345678").ToScaledSIString(6));
        Assert.Equal("123.457M", TiniValueSize.Parse("123456789").ToScaledSIString(6));
        Assert.Equal("1.23457G", TiniValueSize.Parse("1234567890").ToScaledSIString(6));
        Assert.Equal("12.3457G", TiniValueSize.Parse("12345678901").ToScaledSIString(6));
        Assert.Equal("123.457G", TiniValueSize.Parse("123456789012").ToScaledSIString(6));
        Assert.Equal("1.23457T", TiniValueSize.Parse("1234567890123").ToScaledSIString(6));
        Assert.Equal("12.3457T", TiniValueSize.Parse("12345678901234").ToScaledSIString(6));
        Assert.Equal("123.457T", TiniValueSize.Parse("123456789012345").ToScaledSIString(6));
        Assert.Equal("1.23457P", TiniValueSize.Parse("1234567890123456").ToScaledSIString(6));
        Assert.Equal("12.3457P", TiniValueSize.Parse("12345678901234567").ToScaledSIString(6));
        Assert.Equal("123.457P", TiniValueSize.Parse("123456789012345678").ToScaledSIString(6));
        Assert.Equal("1234.57P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString(6));
        Assert.Equal("12345.7P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString(6));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (7)")]
    public void ToScaledSIString7() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString(7));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledSIString(7));
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledSIString(7));
        Assert.Equal("1.234k", TiniValueSize.Parse("1234").ToScaledSIString(7));
        Assert.Equal("12.345k", TiniValueSize.Parse("12345").ToScaledSIString(7));
        Assert.Equal("123.456k", TiniValueSize.Parse("123456").ToScaledSIString(7));
        Assert.Equal("1.234567M", TiniValueSize.Parse("1234567").ToScaledSIString(7));
        Assert.Equal("12.34568M", TiniValueSize.Parse("12345678").ToScaledSIString(7));
        Assert.Equal("123.4568M", TiniValueSize.Parse("123456789").ToScaledSIString(7));
        Assert.Equal("1.234568G", TiniValueSize.Parse("1234567890").ToScaledSIString(7));
        Assert.Equal("12.34568G", TiniValueSize.Parse("12345678901").ToScaledSIString(7));
        Assert.Equal("123.4568G", TiniValueSize.Parse("123456789012").ToScaledSIString(7));
        Assert.Equal("1.234568T", TiniValueSize.Parse("1234567890123").ToScaledSIString(7));
        Assert.Equal("12.34568T", TiniValueSize.Parse("12345678901234").ToScaledSIString(7));
        Assert.Equal("123.4568T", TiniValueSize.Parse("123456789012345").ToScaledSIString(7));
        Assert.Equal("1.234568P", TiniValueSize.Parse("1234567890123456").ToScaledSIString(7));
        Assert.Equal("12.34568P", TiniValueSize.Parse("12345678901234567").ToScaledSIString(7));
        Assert.Equal("123.4568P", TiniValueSize.Parse("123456789012345678").ToScaledSIString(7));
        Assert.Equal("1234.568P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString(7));
        Assert.Equal("12345.68P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString(7));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (8)")]
    public void ToScaledSIString8() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString(8));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledSIString(8));
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledSIString(8));
        Assert.Equal("1.234k", TiniValueSize.Parse("1234").ToScaledSIString(8));
        Assert.Equal("12.345k", TiniValueSize.Parse("12345").ToScaledSIString(8));
        Assert.Equal("123.456k", TiniValueSize.Parse("123456").ToScaledSIString(8));
        Assert.Equal("1.234567M", TiniValueSize.Parse("1234567").ToScaledSIString(8));
        Assert.Equal("12.345678M", TiniValueSize.Parse("12345678").ToScaledSIString(8));
        Assert.Equal("123.45679M", TiniValueSize.Parse("123456789").ToScaledSIString(8));
        Assert.Equal("1.2345679G", TiniValueSize.Parse("1234567890").ToScaledSIString(8));
        Assert.Equal("12.345679G", TiniValueSize.Parse("12345678901").ToScaledSIString(8));
        Assert.Equal("123.45679G", TiniValueSize.Parse("123456789012").ToScaledSIString(8));
        Assert.Equal("1.2345679T", TiniValueSize.Parse("1234567890123").ToScaledSIString(8));
        Assert.Equal("12.345679T", TiniValueSize.Parse("12345678901234").ToScaledSIString(8));
        Assert.Equal("123.45679T", TiniValueSize.Parse("123456789012345").ToScaledSIString(8));
        Assert.Equal("1.2345679P", TiniValueSize.Parse("1234567890123456").ToScaledSIString(8));
        Assert.Equal("12.345679P", TiniValueSize.Parse("12345678901234567").ToScaledSIString(8));
        Assert.Equal("123.45679P", TiniValueSize.Parse("123456789012345678").ToScaledSIString(8));
        Assert.Equal("1234.5679P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString(8));
        Assert.Equal("12345.679P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString(8));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (9)")]
    public void ToScaledSIString9() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString(9));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledSIString(9));
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledSIString(9));
        Assert.Equal("1.234k", TiniValueSize.Parse("1234").ToScaledSIString(9));
        Assert.Equal("12.345k", TiniValueSize.Parse("12345").ToScaledSIString(9));
        Assert.Equal("123.456k", TiniValueSize.Parse("123456").ToScaledSIString(9));
        Assert.Equal("1.234567M", TiniValueSize.Parse("1234567").ToScaledSIString(9));
        Assert.Equal("12.345678M", TiniValueSize.Parse("12345678").ToScaledSIString(9));
        Assert.Equal("123.456789M", TiniValueSize.Parse("123456789").ToScaledSIString(9));
        Assert.Equal("1.23456789G", TiniValueSize.Parse("1234567890").ToScaledSIString(9));
        Assert.Equal("12.3456789G", TiniValueSize.Parse("12345678901").ToScaledSIString(9));
        Assert.Equal("123.456789G", TiniValueSize.Parse("123456789012").ToScaledSIString(9));
        Assert.Equal("1.23456789T", TiniValueSize.Parse("1234567890123").ToScaledSIString(9));
        Assert.Equal("12.3456789T", TiniValueSize.Parse("12345678901234").ToScaledSIString(9));
        Assert.Equal("123.456789T", TiniValueSize.Parse("123456789012345").ToScaledSIString(9));
        Assert.Equal("1.23456789P", TiniValueSize.Parse("1234567890123456").ToScaledSIString(9));
        Assert.Equal("12.3456789P", TiniValueSize.Parse("12345678901234567").ToScaledSIString(9));
        Assert.Equal("123.456789P", TiniValueSize.Parse("123456789012345678").ToScaledSIString(9));
        Assert.Equal("1234.56789P", TiniValueSize.Parse("1234567890123456789").ToScaledSIString(9));
        Assert.Equal("12345.6789P", TiniValueSize.Parse("12345678901234567890").ToScaledSIString(9));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (10)")]
    public void ToScaledSIString10() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledSIString());
        Assert.Equal("10", TiniValueSize.Parse("10").ToScaledSIString());
        Assert.Equal("102", TiniValueSize.Parse("102").ToScaledSIString());
        Assert.Equal("1.02k", TiniValueSize.Parse("1023").ToScaledSIString());
        Assert.Equal("10.2k", TiniValueSize.Parse("10234").ToScaledSIString());
        Assert.Equal("102k", TiniValueSize.Parse("102345").ToScaledSIString());
        Assert.Equal("1.02M", TiniValueSize.Parse("1023456").ToScaledSIString());
        Assert.Equal("10.2M", TiniValueSize.Parse("10234567").ToScaledSIString());
        Assert.Equal("102M", TiniValueSize.Parse("102345678").ToScaledSIString());
        Assert.Equal("1.02G", TiniValueSize.Parse("1023456789").ToScaledSIString());
        Assert.Equal("10.2G", TiniValueSize.Parse("10234567890").ToScaledSIString());
        Assert.Equal("102G", TiniValueSize.Parse("102345678901").ToScaledSIString());
        Assert.Equal("1.02T", TiniValueSize.Parse("1023456789012").ToScaledSIString());
        Assert.Equal("10.2T", TiniValueSize.Parse("10234567890123").ToScaledSIString());
        Assert.Equal("102T", TiniValueSize.Parse("102345678901234").ToScaledSIString());
        Assert.Equal("1.02P", TiniValueSize.Parse("1023456789012345").ToScaledSIString());
        Assert.Equal("10.2P", TiniValueSize.Parse("10234567890123456").ToScaledSIString());
        Assert.Equal("102P", TiniValueSize.Parse("102345678901234567").ToScaledSIString());
        Assert.Equal("1020P", TiniValueSize.Parse("1023456789012345678").ToScaledSIString());
        Assert.Equal("10200P", TiniValueSize.Parse("10234567890123456789").ToScaledSIString());
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (11)")]
    public void ToScaledSIString11() {
        Assert.Equal("3", TiniValueSize.Parse("3").ToScaledSIString());
        Assert.Equal("31", TiniValueSize.Parse("31").ToScaledSIString());
        Assert.Equal("314", TiniValueSize.Parse("314").ToScaledSIString());
        Assert.Equal("3.14k", TiniValueSize.Parse("3141").ToScaledSIString());
        Assert.Equal("31.4k", TiniValueSize.Parse("31415").ToScaledSIString());
        Assert.Equal("314k", TiniValueSize.Parse("314159").ToScaledSIString());
        Assert.Equal("3.14M", TiniValueSize.Parse("3141592").ToScaledSIString());
        Assert.Equal("31.4M", TiniValueSize.Parse("31415926").ToScaledSIString());
        Assert.Equal("314M", TiniValueSize.Parse("314159265").ToScaledSIString());
        Assert.Equal("3.14G", TiniValueSize.Parse("3141592653").ToScaledSIString());
        Assert.Equal("31.4G", TiniValueSize.Parse("31415926535").ToScaledSIString());
        Assert.Equal("314G", TiniValueSize.Parse("314159265358").ToScaledSIString());
        Assert.Equal("3.14T", TiniValueSize.Parse("3141592653589").ToScaledSIString());
        Assert.Equal("31.4T", TiniValueSize.Parse("31415926535897").ToScaledSIString());
        Assert.Equal("314T", TiniValueSize.Parse("314159265358979").ToScaledSIString());
        Assert.Equal("3.14P", TiniValueSize.Parse("3141592653589793").ToScaledSIString());
        Assert.Equal("31.4P", TiniValueSize.Parse("31415926535897932").ToScaledSIString());
        Assert.Equal("314P", TiniValueSize.Parse("314159265358979323").ToScaledSIString());
        Assert.Equal("3140P", TiniValueSize.Parse("3141592653589793238").ToScaledSIString());
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledSIString (12)")]
    public void ToScaledSIString12() {
        Assert.Equal("2", TiniValueSize.Parse("2").ToScaledSIString());
        Assert.Equal("27", TiniValueSize.Parse("27").ToScaledSIString());
        Assert.Equal("271", TiniValueSize.Parse("271").ToScaledSIString());
        Assert.Equal("2.72k", TiniValueSize.Parse("2718").ToScaledSIString());
        Assert.Equal("27.2k", TiniValueSize.Parse("27182").ToScaledSIString());
        Assert.Equal("272k", TiniValueSize.Parse("271828").ToScaledSIString());
        Assert.Equal("2.72M", TiniValueSize.Parse("2718281").ToScaledSIString());
        Assert.Equal("27.2M", TiniValueSize.Parse("27182818").ToScaledSIString());
        Assert.Equal("272M", TiniValueSize.Parse("271828182").ToScaledSIString());
        Assert.Equal("2.72G", TiniValueSize.Parse("2718281828").ToScaledSIString());
        Assert.Equal("27.2G", TiniValueSize.Parse("27182818284").ToScaledSIString());
        Assert.Equal("272G", TiniValueSize.Parse("271828182845").ToScaledSIString());
        Assert.Equal("2.72T", TiniValueSize.Parse("2718281828459").ToScaledSIString());
        Assert.Equal("27.2T", TiniValueSize.Parse("27182818284590").ToScaledSIString());
        Assert.Equal("272T", TiniValueSize.Parse("271828182845904").ToScaledSIString());
        Assert.Equal("2.72P", TiniValueSize.Parse("2718281828459045").ToScaledSIString());
        Assert.Equal("27.2P", TiniValueSize.Parse("27182818284590452").ToScaledSIString());
        Assert.Equal("272P", TiniValueSize.Parse ("271828182845904523").ToScaledSIString());
        Assert.Equal("2720P", TiniValueSize.Parse("2718281828459045235").ToScaledSIString());
    }


    [Fact(DisplayName = "TiniValueSize: ToScaledBinaryString")]
    public void ToScaledBinaryString() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledBinaryString());
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledBinaryString());
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledBinaryString());
        Assert.Equal("1.21Ki", TiniValueSize.Parse("1234").ToScaledBinaryString());
        Assert.Equal("12.1Ki", TiniValueSize.Parse("12345").ToScaledBinaryString());
        Assert.Equal("121Ki", TiniValueSize.Parse("123456").ToScaledBinaryString());
        Assert.Equal("1.18Mi", TiniValueSize.Parse("1234567").ToScaledBinaryString());
        Assert.Equal("11.8Mi", TiniValueSize.Parse("12345678").ToScaledBinaryString());
        Assert.Equal("118Mi", TiniValueSize.Parse("123456789").ToScaledBinaryString());
        Assert.Equal("1.15Gi", TiniValueSize.Parse("1234567890").ToScaledBinaryString());
        Assert.Equal("11.5Gi", TiniValueSize.Parse("12345678901").ToScaledBinaryString());
        Assert.Equal("115Gi", TiniValueSize.Parse("123456789012").ToScaledBinaryString());
        Assert.Equal("1.12Ti", TiniValueSize.Parse("1234567890123").ToScaledBinaryString());
        Assert.Equal("11.2Ti", TiniValueSize.Parse("12345678901234").ToScaledBinaryString());
        Assert.Equal("112Ti", TiniValueSize.Parse("123456789012345").ToScaledBinaryString());
        Assert.Equal("1.10Pi", TiniValueSize.Parse("1234567890123456").ToScaledBinaryString());
        Assert.Equal("11.0Pi", TiniValueSize.Parse("12345678901234567").ToScaledBinaryString());
        Assert.Equal("110Pi", TiniValueSize.Parse("123456789012345678").ToScaledBinaryString());
        Assert.Equal("1100Pi", TiniValueSize.Parse("1234567890123456789").ToScaledBinaryString());
        Assert.Equal("11000Pi", TiniValueSize.Parse("12345678901234567890").ToScaledBinaryString());
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledBinaryString (1)")]
    public void ToScaledBinaryString1() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledBinaryString(1));
        Assert.Equal("10", TiniValueSize.Parse("12").ToScaledBinaryString(1));
        Assert.Equal("100", TiniValueSize.Parse("123").ToScaledBinaryString(1));
        Assert.Equal("1Ki", TiniValueSize.Parse("1234").ToScaledBinaryString(1));
        Assert.Equal("10Ki", TiniValueSize.Parse("12345").ToScaledBinaryString(1));
        Assert.Equal("100Ki", TiniValueSize.Parse("123456").ToScaledBinaryString(1));
        Assert.Equal("1Mi", TiniValueSize.Parse("1234567").ToScaledBinaryString(1));
        Assert.Equal("10Mi", TiniValueSize.Parse("12345678").ToScaledBinaryString(1));
        Assert.Equal("100Mi", TiniValueSize.Parse("123456789").ToScaledBinaryString(1));
        Assert.Equal("1Gi", TiniValueSize.Parse("1234567890").ToScaledBinaryString(1));
        Assert.Equal("10Gi", TiniValueSize.Parse("12345678901").ToScaledBinaryString(1));
        Assert.Equal("100Gi", TiniValueSize.Parse("123456789012").ToScaledBinaryString(1));
        Assert.Equal("1Ti", TiniValueSize.Parse("1234567890123").ToScaledBinaryString(1));
        Assert.Equal("10Ti", TiniValueSize.Parse("12345678901234").ToScaledBinaryString(1));
        Assert.Equal("100Ti", TiniValueSize.Parse("123456789012345").ToScaledBinaryString(1));
        Assert.Equal("1Pi", TiniValueSize.Parse("1234567890123456").ToScaledBinaryString(1));
        Assert.Equal("10Pi", TiniValueSize.Parse("12345678901234567").ToScaledBinaryString(1));
        Assert.Equal("100Pi", TiniValueSize.Parse("123456789012345678").ToScaledBinaryString(1));
        Assert.Equal("1000Pi", TiniValueSize.Parse("1234567890123456789").ToScaledBinaryString(1));
        Assert.Equal("10000Pi", TiniValueSize.Parse("12345678901234567890").ToScaledBinaryString(1));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledBinaryString (2)")]
    public void ToScaledBinaryString2() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledBinaryString(2));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledBinaryString(2));
        Assert.Equal("120", TiniValueSize.Parse("123").ToScaledBinaryString(2));
        Assert.Equal("1.2Ki", TiniValueSize.Parse("1234").ToScaledBinaryString(2));
        Assert.Equal("12Ki", TiniValueSize.Parse("12345").ToScaledBinaryString(2));
        Assert.Equal("120Ki", TiniValueSize.Parse("123456").ToScaledBinaryString(2));
        Assert.Equal("1.2Mi", TiniValueSize.Parse("1234567").ToScaledBinaryString(2));
        Assert.Equal("12Mi", TiniValueSize.Parse("12345678").ToScaledBinaryString(2));
        Assert.Equal("120Mi", TiniValueSize.Parse("123456789").ToScaledBinaryString(2));
        Assert.Equal("1.1Gi", TiniValueSize.Parse("1234567890").ToScaledBinaryString(2));
        Assert.Equal("11Gi", TiniValueSize.Parse("12345678901").ToScaledBinaryString(2));
        Assert.Equal("110Gi", TiniValueSize.Parse("123456789012").ToScaledBinaryString(2));
        Assert.Equal("1.1Ti", TiniValueSize.Parse("1234567890123").ToScaledBinaryString(2));
        Assert.Equal("11Ti", TiniValueSize.Parse("12345678901234").ToScaledBinaryString(2));
        Assert.Equal("110Ti", TiniValueSize.Parse("123456789012345").ToScaledBinaryString(2));
        Assert.Equal("1.1Pi", TiniValueSize.Parse("1234567890123456").ToScaledBinaryString(2));
        Assert.Equal("11Pi", TiniValueSize.Parse("12345678901234567").ToScaledBinaryString(2));
        Assert.Equal("110Pi", TiniValueSize.Parse("123456789012345678").ToScaledBinaryString(2));
        Assert.Equal("1100Pi", TiniValueSize.Parse("1234567890123456789").ToScaledBinaryString(2));
        Assert.Equal("11000Pi", TiniValueSize.Parse("12345678901234567890").ToScaledBinaryString(2));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledBinaryString (3)")]
    public void ToScaledBinaryString3() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledBinaryString(3));
        Assert.Equal("12", TiniValueSize.Parse("12").ToScaledBinaryString(3));
        Assert.Equal("123", TiniValueSize.Parse("123").ToScaledBinaryString(3));
        Assert.Equal("1.21Ki", TiniValueSize.Parse("1234").ToScaledBinaryString(3));
        Assert.Equal("12.1Ki", TiniValueSize.Parse("12345").ToScaledBinaryString(3));
        Assert.Equal("121Ki", TiniValueSize.Parse("123456").ToScaledBinaryString(3));
        Assert.Equal("1.18Mi", TiniValueSize.Parse("1234567").ToScaledBinaryString(3));
        Assert.Equal("11.8Mi", TiniValueSize.Parse("12345678").ToScaledBinaryString(3));
        Assert.Equal("118Mi", TiniValueSize.Parse("123456789").ToScaledBinaryString(3));
        Assert.Equal("1.15Gi", TiniValueSize.Parse("1234567890").ToScaledBinaryString(3));
        Assert.Equal("11.5Gi", TiniValueSize.Parse("12345678901").ToScaledBinaryString(3));
        Assert.Equal("115Gi", TiniValueSize.Parse("123456789012").ToScaledBinaryString(3));
        Assert.Equal("1.12Ti", TiniValueSize.Parse("1234567890123").ToScaledBinaryString(3));
        Assert.Equal("11.2Ti", TiniValueSize.Parse("12345678901234").ToScaledBinaryString(3));
        Assert.Equal("112Ti", TiniValueSize.Parse("123456789012345").ToScaledBinaryString(3));
        Assert.Equal("1.10Pi", TiniValueSize.Parse("1234567890123456").ToScaledBinaryString(3));
        Assert.Equal("11.0Pi", TiniValueSize.Parse("12345678901234567").ToScaledBinaryString(3));
        Assert.Equal("110Pi", TiniValueSize.Parse("123456789012345678").ToScaledBinaryString(3));
        Assert.Equal("1100Pi", TiniValueSize.Parse("1234567890123456789").ToScaledBinaryString(3));
        Assert.Equal("11000Pi", TiniValueSize.Parse("12345678901234567890").ToScaledBinaryString(3));
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledBinaryString (10)")]
    public void ToScaledBinaryString10() {
        Assert.Equal("1", TiniValueSize.Parse("1").ToScaledBinaryString());
        Assert.Equal("10", TiniValueSize.Parse("10").ToScaledBinaryString());
        Assert.Equal("102", TiniValueSize.Parse("102").ToScaledBinaryString());
        Assert.Equal("1020", TiniValueSize.Parse("1023").ToScaledBinaryString());
        Assert.Equal("9.99Ki", TiniValueSize.Parse("10234").ToScaledBinaryString());
        Assert.Equal("99.9Ki", TiniValueSize.Parse("102345").ToScaledBinaryString());
        Assert.Equal("999Ki", TiniValueSize.Parse("1023456").ToScaledBinaryString());
        Assert.Equal("9.76Mi", TiniValueSize.Parse("10234567").ToScaledBinaryString());
        Assert.Equal("97.6Mi", TiniValueSize.Parse("102345678").ToScaledBinaryString());
        Assert.Equal("976Mi", TiniValueSize.Parse("1023456789").ToScaledBinaryString());
        Assert.Equal("9.53Gi", TiniValueSize.Parse("10234567890").ToScaledBinaryString());
        Assert.Equal("95.3Gi", TiniValueSize.Parse("102345678901").ToScaledBinaryString());
        Assert.Equal("953Gi", TiniValueSize.Parse("1023456789012").ToScaledBinaryString());
        Assert.Equal("9.31Ti", TiniValueSize.Parse("10234567890123").ToScaledBinaryString());
        Assert.Equal("93.1Ti", TiniValueSize.Parse("102345678901234").ToScaledBinaryString());
        Assert.Equal("931Ti", TiniValueSize.Parse("1023456789012345").ToScaledBinaryString());
        Assert.Equal("9.09Pi", TiniValueSize.Parse("10234567890123456").ToScaledBinaryString());
        Assert.Equal("90.9Pi", TiniValueSize.Parse("102345678901234567").ToScaledBinaryString());
        Assert.Equal("909Pi", TiniValueSize.Parse("1023456789012345678").ToScaledBinaryString());
        Assert.Equal("9090Pi", TiniValueSize.Parse("10234567890123456789").ToScaledBinaryString());
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledBinaryString (11)")]
    public void ToScaledBinaryString11() {
        Assert.Equal("3", TiniValueSize.Parse("3").ToScaledBinaryString());
        Assert.Equal("31", TiniValueSize.Parse("31").ToScaledBinaryString());
        Assert.Equal("314", TiniValueSize.Parse("314").ToScaledBinaryString());
        Assert.Equal("3.07Ki", TiniValueSize.Parse("3141").ToScaledBinaryString());
        Assert.Equal("30.7Ki", TiniValueSize.Parse("31415").ToScaledBinaryString());
        Assert.Equal("307Ki", TiniValueSize.Parse("314159").ToScaledBinaryString());
        Assert.Equal("3.00Mi", TiniValueSize.Parse("3141592").ToScaledBinaryString());
        Assert.Equal("30.0Mi", TiniValueSize.Parse("31415926").ToScaledBinaryString());
        Assert.Equal("300Mi", TiniValueSize.Parse("314159265").ToScaledBinaryString());
        Assert.Equal("2.93Gi", TiniValueSize.Parse("3141592653").ToScaledBinaryString());
        Assert.Equal("29.3Gi", TiniValueSize.Parse("31415926535").ToScaledBinaryString());
        Assert.Equal("293Gi", TiniValueSize.Parse("314159265358").ToScaledBinaryString());
        Assert.Equal("2.86Ti", TiniValueSize.Parse("3141592653589").ToScaledBinaryString());
        Assert.Equal("28.6Ti", TiniValueSize.Parse("31415926535897").ToScaledBinaryString());
        Assert.Equal("286Ti", TiniValueSize.Parse("314159265358979").ToScaledBinaryString());
        Assert.Equal("2.79Pi", TiniValueSize.Parse("3141592653589793").ToScaledBinaryString());
        Assert.Equal("27.9Pi", TiniValueSize.Parse("31415926535897932").ToScaledBinaryString());
        Assert.Equal("279Pi", TiniValueSize.Parse("314159265358979323").ToScaledBinaryString());
        Assert.Equal("2790Pi", TiniValueSize.Parse("3141592653589793238").ToScaledBinaryString());
    }

    [Fact(DisplayName = "TiniValueSize: ToScaledBinaryString (12)")]
    public void ToScaledBinaryString12() {
        Assert.Equal("2", TiniValueSize.Parse("2").ToScaledBinaryString());
        Assert.Equal("27", TiniValueSize.Parse("27").ToScaledBinaryString());
        Assert.Equal("271", TiniValueSize.Parse("271").ToScaledBinaryString());
        Assert.Equal("2.65Ki", TiniValueSize.Parse("2718").ToScaledBinaryString());
        Assert.Equal("26.5Ki", TiniValueSize.Parse("27182").ToScaledBinaryString());
        Assert.Equal("265Ki", TiniValueSize.Parse("271828").ToScaledBinaryString());
        Assert.Equal("2.59Mi", TiniValueSize.Parse("2718281").ToScaledBinaryString());
        Assert.Equal("25.9Mi", TiniValueSize.Parse("27182818").ToScaledBinaryString());
        Assert.Equal("259Mi", TiniValueSize.Parse("271828182").ToScaledBinaryString());
        Assert.Equal("2.53Gi", TiniValueSize.Parse("2718281828").ToScaledBinaryString());
        Assert.Equal("25.3Gi", TiniValueSize.Parse("27182818284").ToScaledBinaryString());
        Assert.Equal("253Gi", TiniValueSize.Parse("271828182845").ToScaledBinaryString());
        Assert.Equal("2.47Ti", TiniValueSize.Parse("2718281828459").ToScaledBinaryString());
        Assert.Equal("24.7Ti", TiniValueSize.Parse("27182818284590").ToScaledBinaryString());
        Assert.Equal("247Ti", TiniValueSize.Parse("271828182845904").ToScaledBinaryString());
        Assert.Equal("2.41Pi", TiniValueSize.Parse("2718281828459045").ToScaledBinaryString());
        Assert.Equal("24.1Pi", TiniValueSize.Parse("27182818284590452").ToScaledBinaryString());
        Assert.Equal("241Pi", TiniValueSize.Parse("271828182845904523").ToScaledBinaryString());
        Assert.Equal("2410Pi", TiniValueSize.Parse("2718281828459045235").ToScaledBinaryString());
    }

}
