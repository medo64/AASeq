using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueSizeTests {

    [Fact(DisplayName = "ValueSize: Basic")]
    public void Basic() {
        var text = "42";
        Assert.True(ValueSize.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueSize.Parse(text));
    }

    [Fact(DisplayName = "ValueSize: Base (1)")]
    public void Base1() {
        Assert.Equal("42", ValueSize.Parse("42"));
    }

    [Fact(DisplayName = "ValueSize: Base (2)")]
    public void Base2() {
        Assert.Equal("43", ValueSize.Parse("42.84"));
    }

    [Fact(DisplayName = "ValueSize: Base (3)")]
    public void Base3() {
        Assert.Equal("43", ValueSize.Parse(" 42.84"));
    }


    [Fact(DisplayName = "ValueSize: Kilo (1)")]
    public void Kilo1() {
        Assert.Equal("42000", ValueSize.Parse("42K"));
    }

    [Fact(DisplayName = "ValueSize: Kilo (2)")]
    public void Kilo2() {
        Assert.Equal("42840", ValueSize.Parse("42.84 k"));
    }

    [Fact(DisplayName = "ValueSize: Kilo (3)")]
    public void Kilo3() {
        Assert.Equal("1", ValueSize.Parse(" 0.0001 k "));
    }

    [Fact(DisplayName = "ValueSize: Kibi (1)")]
    public void Kibi1() {
        Assert.Equal("43008", ValueSize.Parse("42Ki"));
    }

    [Fact(DisplayName = "ValueSize: Kibi (2)")]
    public void Kibi2() {
        Assert.Equal("43868", ValueSize.Parse("42.84ki"));
    }

    [Fact(DisplayName = "ValueSize: Kibi (3)")]
    public void Kibi3() {
        Assert.Equal("1", ValueSize.Parse(" 0.000001 ki "));
    }


    [Fact(DisplayName = "ValueSize: Mega (1)")]
    public void Mega1() {
        Assert.Equal("42000000", ValueSize.Parse("42m"));
    }

    [Fact(DisplayName = "ValueSize: Mega (2)")]
    public void Mega2() {
        Assert.Equal("42840000", ValueSize.Parse("42.84 M"));
    }

    [Fact(DisplayName = "ValueSize: Mega (3)")]
    public void Mega3() {
        Assert.Equal("1", ValueSize.Parse(" 0.0000001 M "));
    }

    [Fact(DisplayName = "ValueSize: Mebi (1)")]
    public void Mebi1() {
        Assert.Equal("44040192", ValueSize.Parse("42Mi"));
    }

    [Fact(DisplayName = "ValueSize: Mebi (2)")]
    public void Mebi2() {
        Assert.Equal("44920996", ValueSize.Parse("42.84Mi"));
    }

    [Fact(DisplayName = "ValueSize: Mebi (3)")]
    public void Mebi3() {
        Assert.Equal("1", ValueSize.Parse(" 0.0000001 Mi "));
    }


    [Fact(DisplayName = "ValueSize: Giga (1)")]
    public void Giga1() {
        Assert.Equal("42000000000", ValueSize.Parse("42 G"));
    }

    [Fact(DisplayName = "ValueSize: Giga (2)")]
    public void Giga2() {
        Assert.Equal("42840000000", ValueSize.Parse("42.84g"));
    }

    [Fact(DisplayName = "ValueSize: Giga (3)")]
    public void Giga3() {
        Assert.Equal("1", ValueSize.Parse("  0.0000000001 G "));
    }

    [Fact(DisplayName = "ValueSize: Gibi (1)")]
    public void Gibi1() {
        Assert.Equal("45097156608", ValueSize.Parse("42 Gi"));
    }

    [Fact(DisplayName = "ValueSize: Gibi (2)")]
    public void Gibi2() {
        Assert.Equal("45999099740", ValueSize.Parse("42.84Gi"));
    }

    [Fact(DisplayName = "ValueSize: Gibi (3)")]
    public void Gibi3() {
        Assert.Equal("1", ValueSize.Parse("  0.0000000001 GI "));
    }


    [Fact(DisplayName = "ValueSize: Tera (1)")]
    public void Tera1() {
        Assert.Equal("42000000000000", ValueSize.Parse("42T"));
    }

    [Fact(DisplayName = "ValueSize: Tera (2)")]
    public void Tera2() {
        Assert.Equal("42840000000000", ValueSize.Parse("42.84 T"));
    }

    [Fact(DisplayName = "ValueSize: Tera (3)")]
    public void Tera3() {
        Assert.Equal("1", ValueSize.Parse("  0.0000000000001 T "));
    }

    [Fact(DisplayName = "ValueSize: Tebi (1)")]
    public void Tebi1() {
        Assert.Equal("46179488366592", ValueSize.Parse("42Ti"));
    }

    [Fact(DisplayName = "ValueSize: Tebi (2)")]
    public void Tebi2() {
        Assert.Equal("47103078133924", ValueSize.Parse("42.84Ti"));
    }

    [Fact(DisplayName = "ValueSize: Tebi (3)")]
    public void Tebi3() {
        Assert.Equal("1", ValueSize.Parse("  0.0000000000001 Ti "));
    }


    [Fact(DisplayName = "ValueSize: Peta (1)")]
    public void Peta1() {
        Assert.Equal("42000000000000000", ValueSize.Parse("42p"));
    }

    [Fact(DisplayName = "ValueSize: Peta (2)")]
    public void Peta2() {
        Assert.Equal("42840000000000000", ValueSize.Parse("42.84 P"));
    }

    [Fact(DisplayName = "ValueSize: Peta (3)")]
    public void Peta3() {
        Assert.Equal("1", ValueSize.Parse("  0.0000000000000001 P "));
    }

    [Fact(DisplayName = "ValueSize: Pebi (1)")]
    public void Pebi1() {
        Assert.Equal("47287796087390208", ValueSize.Parse("42pi"));
    }

    [Fact(DisplayName = "ValueSize: Pebi (2)")]
    public void Pebi2() {
        Assert.Equal("48233552009138012", ValueSize.Parse("42.84PI"));
    }

    [Fact(DisplayName = "ValueSize: Pebi (3)")]
    public void Pebi3() {
        Assert.Equal("1", ValueSize.Parse("  0.0000000000000001 PI "));
    }


    [Fact(DisplayName = "ValueSize: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueSize.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueSize.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueSize: Out of range")]
    public void OutOfRange() {
        Assert.True(ValueSize.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.False(ValueSize.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.True(ValueSize.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.False(ValueSize.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
        Assert.False(ValueSize.TryParse("42E", out var _));
    }


    [Fact(DisplayName = "ValueSize: ToKiloString")]
    public void ToKiloString() {
        Assert.Equal("0.042k", ValueSize.Parse("42").ToKiloString());
        Assert.Equal("0.0420k", ValueSize.Parse("42").ToKiloString("0.0000"));
        Assert.Equal("420k", ValueSize.Parse("420000").ToKiloString());
        Assert.Equal("420.0000k", ValueSize.Parse("420000").ToKiloString("0.0000"));
        Assert.Equal("4200000k", ValueSize.Parse("4200000000").ToKiloString());
        Assert.Equal("4200000.0000k", ValueSize.Parse("4200000000").ToKiloString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToKibiString")]
    public void ToKibiString() {
        Assert.Equal("0.041015625Ki", ValueSize.Parse("42").ToKibiString());
        Assert.Equal("0.0410Ki", ValueSize.Parse("42").ToKibiString("0.0000"));
        Assert.Equal("410.15625Ki", ValueSize.Parse("420000").ToKibiString());
        Assert.Equal("410.1563Ki", ValueSize.Parse("420000").ToKibiString("0.0000"));
        Assert.Equal("4101562.5Ki", ValueSize.Parse("4200000000").ToKibiString());
        Assert.Equal("4101562.5000Ki", ValueSize.Parse("4200000000").ToKibiString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToMegatring")]
    public void ToMegaString() {
        Assert.Equal("0.42M", ValueSize.Parse("420000").ToMegaString());
        Assert.Equal("0.4200M", ValueSize.Parse("420000").ToMegaString("0.0000"));
        Assert.Equal("4200M", ValueSize.Parse("4200000000").ToMegaString());
        Assert.Equal("4200.0000M", ValueSize.Parse("4200000000").ToMegaString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToMebiString")]
    public void ToMebiString() {
        Assert.Equal("0.400543212890625Mi", ValueSize.Parse("420000").ToMebiString());
        Assert.Equal("0.4005Mi", ValueSize.Parse("420000").ToMebiString("0.0000"));
        Assert.Equal("4005.43212890625Mi", ValueSize.Parse("4200000000").ToMebiString());
        Assert.Equal("4005.4321Mi", ValueSize.Parse("4200000000").ToMebiString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToGigaString")]
    public void ToGigaString() {
        Assert.Equal("0.00042G", ValueSize.Parse("420000").ToGigaString());
        Assert.Equal("0.0004G", ValueSize.Parse("420000").ToGigaString("0.0000"));
        Assert.Equal("4.2G", ValueSize.Parse("4200000000").ToGigaString());
        Assert.Equal("4.2000G", ValueSize.Parse("4200000000").ToGigaString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToGibiString")]
    public void ToGibiString() {
        Assert.Equal("0.000391155481338501Gi", ValueSize.Parse("420000").ToGibiString());
        Assert.Equal("0.0004Gi", ValueSize.Parse("420000").ToGibiString("0.0000"));
        Assert.Equal("3.9115548133850098Gi", ValueSize.Parse("4200000000").ToGibiString());
        Assert.Equal("3.9116Gi", ValueSize.Parse("4200000000").ToGibiString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToTeraString")]
    public void ToTeraString() {
        Assert.Equal("0.00044T", ValueSize.Parse("440000000").ToTeraString());
        Assert.Equal("0.0004T", ValueSize.Parse("440000000").ToTeraString("0.0000"));
        Assert.Equal("4.4T", ValueSize.Parse("4400000000000").ToTeraString());
        Assert.Equal("4.4000T", ValueSize.Parse("4400000000000").ToTeraString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToTebiString")]
    public void ToTebiString() {
        Assert.Equal("0.0004001776687800884Ti", ValueSize.Parse("440000000").ToTebiString());
        Assert.Equal("0.0004Ti", ValueSize.Parse("440000000").ToTebiString("0.0000"));
        Assert.Equal("4.001776687800884Ti", ValueSize.Parse("4400000000000").ToTebiString());
        Assert.Equal("4.0018Ti", ValueSize.Parse("4400000000000").ToTebiString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToPetaString")]
    public void ToPetaString() {
        Assert.Equal("0.00046P", ValueSize.Parse("460000000000").ToPetaString());
        Assert.Equal("0.0005P", ValueSize.Parse("460000000000").ToPetaString("0.0000"));
        Assert.Equal("4.6P", ValueSize.Parse("4600000000000000").ToPetaString());
        Assert.Equal("4.6000P", ValueSize.Parse("4600000000000000").ToPetaString("0.0000"));
    }

    [Fact(DisplayName = "ValueSize: ToPebiString")]
    public void ToPebiString() {
        Assert.Equal("0.0004085620730620576Pi", ValueSize.Parse("460000000000").ToPebiString());
        Assert.Equal("0.0004Pi", ValueSize.Parse("460000000000").ToPebiString("0.0000"));
        Assert.Equal("4.085620730620576Pi", ValueSize.Parse("4600000000000000").ToPebiString());
        Assert.Equal("4.0856Pi", ValueSize.Parse("4600000000000000").ToPebiString("0.0000"));
    }


    [Fact(DisplayName = "ValueSize: ToScaledSIString")]
    public void ToScaledSIString() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString());
        Assert.Equal("12", ValueSize.Parse("12").ToScaledSIString());
        Assert.Equal("123", ValueSize.Parse("123").ToScaledSIString());
        Assert.Equal("1.23k", ValueSize.Parse("1234").ToScaledSIString());
        Assert.Equal("12.3k", ValueSize.Parse("12345").ToScaledSIString());
        Assert.Equal("123k", ValueSize.Parse("123456").ToScaledSIString());
        Assert.Equal("1.23M", ValueSize.Parse("1234567").ToScaledSIString());
        Assert.Equal("12.3M", ValueSize.Parse("12345678").ToScaledSIString());
        Assert.Equal("123M", ValueSize.Parse("123456789").ToScaledSIString());
        Assert.Equal("1.23G", ValueSize.Parse("1234567890").ToScaledSIString());
        Assert.Equal("12.3G", ValueSize.Parse("12345678901").ToScaledSIString());
        Assert.Equal("123G", ValueSize.Parse("123456789012").ToScaledSIString());
        Assert.Equal("1.23T", ValueSize.Parse("1234567890123").ToScaledSIString());
        Assert.Equal("12.3T", ValueSize.Parse("12345678901234").ToScaledSIString());
        Assert.Equal("123T", ValueSize.Parse("123456789012345").ToScaledSIString());
        Assert.Equal("1.23P", ValueSize.Parse("1234567890123456").ToScaledSIString());
        Assert.Equal("12.3P", ValueSize.Parse("12345678901234567").ToScaledSIString());
        Assert.Equal("123P", ValueSize.Parse("123456789012345678").ToScaledSIString());
        Assert.Equal("1230P", ValueSize.Parse("1234567890123456789").ToScaledSIString());
        Assert.Equal("12300P", ValueSize.Parse("12345678901234567890").ToScaledSIString());
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (1)")]
    public void ToScaledSIString1() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString(1));
        Assert.Equal("10", ValueSize.Parse("12").ToScaledSIString(1));
        Assert.Equal("100", ValueSize.Parse("123").ToScaledSIString(1));
        Assert.Equal("1k", ValueSize.Parse("1234").ToScaledSIString(1));
        Assert.Equal("10k", ValueSize.Parse("12345").ToScaledSIString(1));
        Assert.Equal("100k", ValueSize.Parse("123456").ToScaledSIString(1));
        Assert.Equal("1M", ValueSize.Parse("1234567").ToScaledSIString(1));
        Assert.Equal("10M", ValueSize.Parse("12345678").ToScaledSIString(1));
        Assert.Equal("100M", ValueSize.Parse("123456789").ToScaledSIString(1));
        Assert.Equal("1G", ValueSize.Parse("1234567890").ToScaledSIString(1));
        Assert.Equal("10G", ValueSize.Parse("12345678901").ToScaledSIString(1));
        Assert.Equal("100G", ValueSize.Parse("123456789012").ToScaledSIString(1));
        Assert.Equal("1T", ValueSize.Parse("1234567890123").ToScaledSIString(1));
        Assert.Equal("10T", ValueSize.Parse("12345678901234").ToScaledSIString(1));
        Assert.Equal("100T", ValueSize.Parse("123456789012345").ToScaledSIString(1));
        Assert.Equal("1P", ValueSize.Parse("1234567890123456").ToScaledSIString(1));
        Assert.Equal("10P", ValueSize.Parse("12345678901234567").ToScaledSIString(1));
        Assert.Equal("100P", ValueSize.Parse("123456789012345678").ToScaledSIString(1));
        Assert.Equal("1000P", ValueSize.Parse("1234567890123456789").ToScaledSIString(1));
        Assert.Equal("10000P", ValueSize.Parse("12345678901234567890").ToScaledSIString(1));
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (3)")]
    public void ToScaledSIString3() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString(3));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledSIString(3));
        Assert.Equal("123", ValueSize.Parse("123").ToScaledSIString(3));
        Assert.Equal("1.23k", ValueSize.Parse("1234").ToScaledSIString(3));
        Assert.Equal("12.3k", ValueSize.Parse("12345").ToScaledSIString(3));
        Assert.Equal("123k", ValueSize.Parse("123456").ToScaledSIString(3));
        Assert.Equal("1.23M", ValueSize.Parse("1234567").ToScaledSIString(3));
        Assert.Equal("12.3M", ValueSize.Parse("12345678").ToScaledSIString(3));
        Assert.Equal("123M", ValueSize.Parse("123456789").ToScaledSIString(3));
        Assert.Equal("1.23G", ValueSize.Parse("1234567890").ToScaledSIString(3));
        Assert.Equal("12.3G", ValueSize.Parse("12345678901").ToScaledSIString(3));
        Assert.Equal("123G", ValueSize.Parse("123456789012").ToScaledSIString(3));
        Assert.Equal("1.23T", ValueSize.Parse("1234567890123").ToScaledSIString(3));
        Assert.Equal("12.3T", ValueSize.Parse("12345678901234").ToScaledSIString(3));
        Assert.Equal("123T", ValueSize.Parse("123456789012345").ToScaledSIString(3));
        Assert.Equal("1.23P", ValueSize.Parse("1234567890123456").ToScaledSIString(3));
        Assert.Equal("12.3P", ValueSize.Parse("12345678901234567").ToScaledSIString(3));
        Assert.Equal("123P", ValueSize.Parse("123456789012345678").ToScaledSIString(3));
        Assert.Equal("1230P", ValueSize.Parse("1234567890123456789").ToScaledSIString(3));
        Assert.Equal("12300P", ValueSize.Parse("12345678901234567890").ToScaledSIString(3));
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (4)")]
    public void ToScaledSIString4() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString(4));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledSIString(4));
        Assert.Equal("123", ValueSize.Parse("123").ToScaledSIString(4));
        Assert.Equal("1.234k", ValueSize.Parse("1234").ToScaledSIString(4));
        Assert.Equal("12.35k", ValueSize.Parse("12345").ToScaledSIString(4));
        Assert.Equal("123.5k", ValueSize.Parse("123456").ToScaledSIString(4));
        Assert.Equal("1.235M", ValueSize.Parse("1234567").ToScaledSIString(4));
        Assert.Equal("12.35M", ValueSize.Parse("12345678").ToScaledSIString(4));
        Assert.Equal("123.5M", ValueSize.Parse("123456789").ToScaledSIString(4));
        Assert.Equal("1.235G", ValueSize.Parse("1234567890").ToScaledSIString(4));
        Assert.Equal("12.35G", ValueSize.Parse("12345678901").ToScaledSIString(4));
        Assert.Equal("123.5G", ValueSize.Parse("123456789012").ToScaledSIString(4));
        Assert.Equal("1.235T", ValueSize.Parse("1234567890123").ToScaledSIString(4));
        Assert.Equal("12.35T", ValueSize.Parse("12345678901234").ToScaledSIString(4));
        Assert.Equal("123.5T", ValueSize.Parse("123456789012345").ToScaledSIString(4));
        Assert.Equal("1.235P", ValueSize.Parse("1234567890123456").ToScaledSIString(4));
        Assert.Equal("12.35P", ValueSize.Parse("12345678901234567").ToScaledSIString(4));
        Assert.Equal("123.5P", ValueSize.Parse("123456789012345678").ToScaledSIString(4));
        Assert.Equal("1235P", ValueSize.Parse("1234567890123456789").ToScaledSIString(4));
        Assert.Equal("12350P", ValueSize.Parse("12345678901234567890").ToScaledSIString(4));
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (5)")]
    public void ToScaledSIString5() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString(5));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledSIString(5));
        Assert.Equal("123", ValueSize.Parse("123").ToScaledSIString(5));
        Assert.Equal("1.234k", ValueSize.Parse("1234").ToScaledSIString(5));
        Assert.Equal("12.345k", ValueSize.Parse("12345").ToScaledSIString(5));
        Assert.Equal("123.46k", ValueSize.Parse("123456").ToScaledSIString(5));
        Assert.Equal("1.2346M", ValueSize.Parse("1234567").ToScaledSIString(5));
        Assert.Equal("12.346M", ValueSize.Parse("12345678").ToScaledSIString(5));
        Assert.Equal("123.46M", ValueSize.Parse("123456789").ToScaledSIString(5));
        Assert.Equal("1.2346G", ValueSize.Parse("1234567890").ToScaledSIString(5));
        Assert.Equal("12.346G", ValueSize.Parse("12345678901").ToScaledSIString(5));
        Assert.Equal("123.46G", ValueSize.Parse("123456789012").ToScaledSIString(5));
        Assert.Equal("1.2346T", ValueSize.Parse("1234567890123").ToScaledSIString(5));
        Assert.Equal("12.346T", ValueSize.Parse("12345678901234").ToScaledSIString(5));
        Assert.Equal("123.46T", ValueSize.Parse("123456789012345").ToScaledSIString(5));
        Assert.Equal("1.2346P", ValueSize.Parse("1234567890123456").ToScaledSIString(5));
        Assert.Equal("12.346P", ValueSize.Parse("12345678901234567").ToScaledSIString(5));
        Assert.Equal("123.46P", ValueSize.Parse("123456789012345678").ToScaledSIString(5));
        Assert.Equal("1234.6P", ValueSize.Parse("1234567890123456789").ToScaledSIString(5));
        Assert.Equal("12346P", ValueSize.Parse("12345678901234567890").ToScaledSIString(5));
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (6)")]
    public void ToScaledSIString6() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString(6));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledSIString(6));
        Assert.Equal("123", ValueSize.Parse("123").ToScaledSIString(6));
        Assert.Equal("1.234k", ValueSize.Parse("1234").ToScaledSIString(6));
        Assert.Equal("12.345k", ValueSize.Parse("12345").ToScaledSIString(6));
        Assert.Equal("123.456k", ValueSize.Parse("123456").ToScaledSIString(6));
        Assert.Equal("1.23457M", ValueSize.Parse("1234567").ToScaledSIString(6));
        Assert.Equal("12.3457M", ValueSize.Parse("12345678").ToScaledSIString(6));
        Assert.Equal("123.457M", ValueSize.Parse("123456789").ToScaledSIString(6));
        Assert.Equal("1.23457G", ValueSize.Parse("1234567890").ToScaledSIString(6));
        Assert.Equal("12.3457G", ValueSize.Parse("12345678901").ToScaledSIString(6));
        Assert.Equal("123.457G", ValueSize.Parse("123456789012").ToScaledSIString(6));
        Assert.Equal("1.23457T", ValueSize.Parse("1234567890123").ToScaledSIString(6));
        Assert.Equal("12.3457T", ValueSize.Parse("12345678901234").ToScaledSIString(6));
        Assert.Equal("123.457T", ValueSize.Parse("123456789012345").ToScaledSIString(6));
        Assert.Equal("1.23457P", ValueSize.Parse("1234567890123456").ToScaledSIString(6));
        Assert.Equal("12.3457P", ValueSize.Parse("12345678901234567").ToScaledSIString(6));
        Assert.Equal("123.457P", ValueSize.Parse("123456789012345678").ToScaledSIString(6));
        Assert.Equal("1234.57P", ValueSize.Parse("1234567890123456789").ToScaledSIString(6));
        Assert.Equal("12345.7P", ValueSize.Parse("12345678901234567890").ToScaledSIString(6));
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (7)")]
    public void ToScaledSIString7() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString(7));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledSIString(7));
        Assert.Equal("123", ValueSize.Parse("123").ToScaledSIString(7));
        Assert.Equal("1.234k", ValueSize.Parse("1234").ToScaledSIString(7));
        Assert.Equal("12.345k", ValueSize.Parse("12345").ToScaledSIString(7));
        Assert.Equal("123.456k", ValueSize.Parse("123456").ToScaledSIString(7));
        Assert.Equal("1.234567M", ValueSize.Parse("1234567").ToScaledSIString(7));
        Assert.Equal("12.34568M", ValueSize.Parse("12345678").ToScaledSIString(7));
        Assert.Equal("123.4568M", ValueSize.Parse("123456789").ToScaledSIString(7));
        Assert.Equal("1.234568G", ValueSize.Parse("1234567890").ToScaledSIString(7));
        Assert.Equal("12.34568G", ValueSize.Parse("12345678901").ToScaledSIString(7));
        Assert.Equal("123.4568G", ValueSize.Parse("123456789012").ToScaledSIString(7));
        Assert.Equal("1.234568T", ValueSize.Parse("1234567890123").ToScaledSIString(7));
        Assert.Equal("12.34568T", ValueSize.Parse("12345678901234").ToScaledSIString(7));
        Assert.Equal("123.4568T", ValueSize.Parse("123456789012345").ToScaledSIString(7));
        Assert.Equal("1.234568P", ValueSize.Parse("1234567890123456").ToScaledSIString(7));
        Assert.Equal("12.34568P", ValueSize.Parse("12345678901234567").ToScaledSIString(7));
        Assert.Equal("123.4568P", ValueSize.Parse("123456789012345678").ToScaledSIString(7));
        Assert.Equal("1234.568P", ValueSize.Parse("1234567890123456789").ToScaledSIString(7));
        Assert.Equal("12345.68P", ValueSize.Parse("12345678901234567890").ToScaledSIString(7));
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (8)")]
    public void ToScaledSIString8() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString(8));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledSIString(8));
        Assert.Equal("123", ValueSize.Parse("123").ToScaledSIString(8));
        Assert.Equal("1.234k", ValueSize.Parse("1234").ToScaledSIString(8));
        Assert.Equal("12.345k", ValueSize.Parse("12345").ToScaledSIString(8));
        Assert.Equal("123.456k", ValueSize.Parse("123456").ToScaledSIString(8));
        Assert.Equal("1.234567M", ValueSize.Parse("1234567").ToScaledSIString(8));
        Assert.Equal("12.345678M", ValueSize.Parse("12345678").ToScaledSIString(8));
        Assert.Equal("123.45679M", ValueSize.Parse("123456789").ToScaledSIString(8));
        Assert.Equal("1.2345679G", ValueSize.Parse("1234567890").ToScaledSIString(8));
        Assert.Equal("12.345679G", ValueSize.Parse("12345678901").ToScaledSIString(8));
        Assert.Equal("123.45679G", ValueSize.Parse("123456789012").ToScaledSIString(8));
        Assert.Equal("1.2345679T", ValueSize.Parse("1234567890123").ToScaledSIString(8));
        Assert.Equal("12.345679T", ValueSize.Parse("12345678901234").ToScaledSIString(8));
        Assert.Equal("123.45679T", ValueSize.Parse("123456789012345").ToScaledSIString(8));
        Assert.Equal("1.2345679P", ValueSize.Parse("1234567890123456").ToScaledSIString(8));
        Assert.Equal("12.345679P", ValueSize.Parse("12345678901234567").ToScaledSIString(8));
        Assert.Equal("123.45679P", ValueSize.Parse("123456789012345678").ToScaledSIString(8));
        Assert.Equal("1234.5679P", ValueSize.Parse("1234567890123456789").ToScaledSIString(8));
        Assert.Equal("12345.679P", ValueSize.Parse("12345678901234567890").ToScaledSIString(8));
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (9)")]
    public void ToScaledSIString9() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString(9));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledSIString(9));
        Assert.Equal("123", ValueSize.Parse("123").ToScaledSIString(9));
        Assert.Equal("1.234k", ValueSize.Parse("1234").ToScaledSIString(9));
        Assert.Equal("12.345k", ValueSize.Parse("12345").ToScaledSIString(9));
        Assert.Equal("123.456k", ValueSize.Parse("123456").ToScaledSIString(9));
        Assert.Equal("1.234567M", ValueSize.Parse("1234567").ToScaledSIString(9));
        Assert.Equal("12.345678M", ValueSize.Parse("12345678").ToScaledSIString(9));
        Assert.Equal("123.456789M", ValueSize.Parse("123456789").ToScaledSIString(9));
        Assert.Equal("1.23456789G", ValueSize.Parse("1234567890").ToScaledSIString(9));
        Assert.Equal("12.3456789G", ValueSize.Parse("12345678901").ToScaledSIString(9));
        Assert.Equal("123.456789G", ValueSize.Parse("123456789012").ToScaledSIString(9));
        Assert.Equal("1.23456789T", ValueSize.Parse("1234567890123").ToScaledSIString(9));
        Assert.Equal("12.3456789T", ValueSize.Parse("12345678901234").ToScaledSIString(9));
        Assert.Equal("123.456789T", ValueSize.Parse("123456789012345").ToScaledSIString(9));
        Assert.Equal("1.23456789P", ValueSize.Parse("1234567890123456").ToScaledSIString(9));
        Assert.Equal("12.3456789P", ValueSize.Parse("12345678901234567").ToScaledSIString(9));
        Assert.Equal("123.456789P", ValueSize.Parse("123456789012345678").ToScaledSIString(9));
        Assert.Equal("1234.56789P", ValueSize.Parse("1234567890123456789").ToScaledSIString(9));
        Assert.Equal("12345.6789P", ValueSize.Parse("12345678901234567890").ToScaledSIString(9));
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (10)")]
    public void ToScaledSIString10() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledSIString());
        Assert.Equal("10", ValueSize.Parse("10").ToScaledSIString());
        Assert.Equal("102", ValueSize.Parse("102").ToScaledSIString());
        Assert.Equal("1.02k", ValueSize.Parse("1023").ToScaledSIString());
        Assert.Equal("10.2k", ValueSize.Parse("10234").ToScaledSIString());
        Assert.Equal("102k", ValueSize.Parse("102345").ToScaledSIString());
        Assert.Equal("1.02M", ValueSize.Parse("1023456").ToScaledSIString());
        Assert.Equal("10.2M", ValueSize.Parse("10234567").ToScaledSIString());
        Assert.Equal("102M", ValueSize.Parse("102345678").ToScaledSIString());
        Assert.Equal("1.02G", ValueSize.Parse("1023456789").ToScaledSIString());
        Assert.Equal("10.2G", ValueSize.Parse("10234567890").ToScaledSIString());
        Assert.Equal("102G", ValueSize.Parse("102345678901").ToScaledSIString());
        Assert.Equal("1.02T", ValueSize.Parse("1023456789012").ToScaledSIString());
        Assert.Equal("10.2T", ValueSize.Parse("10234567890123").ToScaledSIString());
        Assert.Equal("102T", ValueSize.Parse("102345678901234").ToScaledSIString());
        Assert.Equal("1.02P", ValueSize.Parse("1023456789012345").ToScaledSIString());
        Assert.Equal("10.2P", ValueSize.Parse("10234567890123456").ToScaledSIString());
        Assert.Equal("102P", ValueSize.Parse("102345678901234567").ToScaledSIString());
        Assert.Equal("1020P", ValueSize.Parse("1023456789012345678").ToScaledSIString());
        Assert.Equal("10200P", ValueSize.Parse("10234567890123456789").ToScaledSIString());
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (11)")]
    public void ToScaledSIString11() {
        Assert.Equal("3", ValueSize.Parse("3").ToScaledSIString());
        Assert.Equal("31", ValueSize.Parse("31").ToScaledSIString());
        Assert.Equal("314", ValueSize.Parse("314").ToScaledSIString());
        Assert.Equal("3.14k", ValueSize.Parse("3141").ToScaledSIString());
        Assert.Equal("31.4k", ValueSize.Parse("31415").ToScaledSIString());
        Assert.Equal("314k", ValueSize.Parse("314159").ToScaledSIString());
        Assert.Equal("3.14M", ValueSize.Parse("3141592").ToScaledSIString());
        Assert.Equal("31.4M", ValueSize.Parse("31415926").ToScaledSIString());
        Assert.Equal("314M", ValueSize.Parse("314159265").ToScaledSIString());
        Assert.Equal("3.14G", ValueSize.Parse("3141592653").ToScaledSIString());
        Assert.Equal("31.4G", ValueSize.Parse("31415926535").ToScaledSIString());
        Assert.Equal("314G", ValueSize.Parse("314159265358").ToScaledSIString());
        Assert.Equal("3.14T", ValueSize.Parse("3141592653589").ToScaledSIString());
        Assert.Equal("31.4T", ValueSize.Parse("31415926535897").ToScaledSIString());
        Assert.Equal("314T", ValueSize.Parse("314159265358979").ToScaledSIString());
        Assert.Equal("3.14P", ValueSize.Parse("3141592653589793").ToScaledSIString());
        Assert.Equal("31.4P", ValueSize.Parse("31415926535897932").ToScaledSIString());
        Assert.Equal("314P", ValueSize.Parse("314159265358979323").ToScaledSIString());
        Assert.Equal("3140P", ValueSize.Parse("3141592653589793238").ToScaledSIString());
    }

    [Fact(DisplayName = "ValueSize: ToScaledSIString (12)")]
    public void ToScaledSIString12() {
        Assert.Equal("2", ValueSize.Parse("2").ToScaledSIString());
        Assert.Equal("27", ValueSize.Parse("27").ToScaledSIString());
        Assert.Equal("271", ValueSize.Parse("271").ToScaledSIString());
        Assert.Equal("2.72k", ValueSize.Parse("2718").ToScaledSIString());
        Assert.Equal("27.2k", ValueSize.Parse("27182").ToScaledSIString());
        Assert.Equal("272k", ValueSize.Parse("271828").ToScaledSIString());
        Assert.Equal("2.72M", ValueSize.Parse("2718281").ToScaledSIString());
        Assert.Equal("27.2M", ValueSize.Parse("27182818").ToScaledSIString());
        Assert.Equal("272M", ValueSize.Parse("271828182").ToScaledSIString());
        Assert.Equal("2.72G", ValueSize.Parse("2718281828").ToScaledSIString());
        Assert.Equal("27.2G", ValueSize.Parse("27182818284").ToScaledSIString());
        Assert.Equal("272G", ValueSize.Parse("271828182845").ToScaledSIString());
        Assert.Equal("2.72T", ValueSize.Parse("2718281828459").ToScaledSIString());
        Assert.Equal("27.2T", ValueSize.Parse("27182818284590").ToScaledSIString());
        Assert.Equal("272T", ValueSize.Parse("271828182845904").ToScaledSIString());
        Assert.Equal("2.72P", ValueSize.Parse("2718281828459045").ToScaledSIString());
        Assert.Equal("27.2P", ValueSize.Parse("27182818284590452").ToScaledSIString());
        Assert.Equal("272P", ValueSize.Parse ("271828182845904523").ToScaledSIString());
        Assert.Equal("2720P", ValueSize.Parse("2718281828459045235").ToScaledSIString());
    }


    [Fact(DisplayName = "ValueSize: ToScaledBinaryString")]
    public void ToScaledBinaryString() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledBinaryString());
        Assert.Equal("12", ValueSize.Parse("12").ToScaledBinaryString());
        Assert.Equal("123", ValueSize.Parse("123").ToScaledBinaryString());
        Assert.Equal("1.21Ki", ValueSize.Parse("1234").ToScaledBinaryString());
        Assert.Equal("12.1Ki", ValueSize.Parse("12345").ToScaledBinaryString());
        Assert.Equal("121Ki", ValueSize.Parse("123456").ToScaledBinaryString());
        Assert.Equal("1.18Mi", ValueSize.Parse("1234567").ToScaledBinaryString());
        Assert.Equal("11.8Mi", ValueSize.Parse("12345678").ToScaledBinaryString());
        Assert.Equal("118Mi", ValueSize.Parse("123456789").ToScaledBinaryString());
        Assert.Equal("1.15Gi", ValueSize.Parse("1234567890").ToScaledBinaryString());
        Assert.Equal("11.5Gi", ValueSize.Parse("12345678901").ToScaledBinaryString());
        Assert.Equal("115Gi", ValueSize.Parse("123456789012").ToScaledBinaryString());
        Assert.Equal("1.12Ti", ValueSize.Parse("1234567890123").ToScaledBinaryString());
        Assert.Equal("11.2Ti", ValueSize.Parse("12345678901234").ToScaledBinaryString());
        Assert.Equal("112Ti", ValueSize.Parse("123456789012345").ToScaledBinaryString());
        Assert.Equal("1.10Pi", ValueSize.Parse("1234567890123456").ToScaledBinaryString());
        Assert.Equal("11.0Pi", ValueSize.Parse("12345678901234567").ToScaledBinaryString());
        Assert.Equal("110Pi", ValueSize.Parse("123456789012345678").ToScaledBinaryString());
        Assert.Equal("1100Pi", ValueSize.Parse("1234567890123456789").ToScaledBinaryString());
        Assert.Equal("11000Pi", ValueSize.Parse("12345678901234567890").ToScaledBinaryString());
    }

    [Fact(DisplayName = "ValueSize: ToScaledBinaryString (1)")]
    public void ToScaledBinaryString1() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledBinaryString(1));
        Assert.Equal("10", ValueSize.Parse("12").ToScaledBinaryString(1));
        Assert.Equal("100", ValueSize.Parse("123").ToScaledBinaryString(1));
        Assert.Equal("1Ki", ValueSize.Parse("1234").ToScaledBinaryString(1));
        Assert.Equal("10Ki", ValueSize.Parse("12345").ToScaledBinaryString(1));
        Assert.Equal("100Ki", ValueSize.Parse("123456").ToScaledBinaryString(1));
        Assert.Equal("1Mi", ValueSize.Parse("1234567").ToScaledBinaryString(1));
        Assert.Equal("10Mi", ValueSize.Parse("12345678").ToScaledBinaryString(1));
        Assert.Equal("100Mi", ValueSize.Parse("123456789").ToScaledBinaryString(1));
        Assert.Equal("1Gi", ValueSize.Parse("1234567890").ToScaledBinaryString(1));
        Assert.Equal("10Gi", ValueSize.Parse("12345678901").ToScaledBinaryString(1));
        Assert.Equal("100Gi", ValueSize.Parse("123456789012").ToScaledBinaryString(1));
        Assert.Equal("1Ti", ValueSize.Parse("1234567890123").ToScaledBinaryString(1));
        Assert.Equal("10Ti", ValueSize.Parse("12345678901234").ToScaledBinaryString(1));
        Assert.Equal("100Ti", ValueSize.Parse("123456789012345").ToScaledBinaryString(1));
        Assert.Equal("1Pi", ValueSize.Parse("1234567890123456").ToScaledBinaryString(1));
        Assert.Equal("10Pi", ValueSize.Parse("12345678901234567").ToScaledBinaryString(1));
        Assert.Equal("100Pi", ValueSize.Parse("123456789012345678").ToScaledBinaryString(1));
        Assert.Equal("1000Pi", ValueSize.Parse("1234567890123456789").ToScaledBinaryString(1));
        Assert.Equal("10000Pi", ValueSize.Parse("12345678901234567890").ToScaledBinaryString(1));
    }

    [Fact(DisplayName = "ValueSize: ToScaledBinaryString (2)")]
    public void ToScaledBinaryString2() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledBinaryString(2));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledBinaryString(2));
        Assert.Equal("120", ValueSize.Parse("123").ToScaledBinaryString(2));
        Assert.Equal("1.2Ki", ValueSize.Parse("1234").ToScaledBinaryString(2));
        Assert.Equal("12Ki", ValueSize.Parse("12345").ToScaledBinaryString(2));
        Assert.Equal("120Ki", ValueSize.Parse("123456").ToScaledBinaryString(2));
        Assert.Equal("1.2Mi", ValueSize.Parse("1234567").ToScaledBinaryString(2));
        Assert.Equal("12Mi", ValueSize.Parse("12345678").ToScaledBinaryString(2));
        Assert.Equal("120Mi", ValueSize.Parse("123456789").ToScaledBinaryString(2));
        Assert.Equal("1.1Gi", ValueSize.Parse("1234567890").ToScaledBinaryString(2));
        Assert.Equal("11Gi", ValueSize.Parse("12345678901").ToScaledBinaryString(2));
        Assert.Equal("110Gi", ValueSize.Parse("123456789012").ToScaledBinaryString(2));
        Assert.Equal("1.1Ti", ValueSize.Parse("1234567890123").ToScaledBinaryString(2));
        Assert.Equal("11Ti", ValueSize.Parse("12345678901234").ToScaledBinaryString(2));
        Assert.Equal("110Ti", ValueSize.Parse("123456789012345").ToScaledBinaryString(2));
        Assert.Equal("1.1Pi", ValueSize.Parse("1234567890123456").ToScaledBinaryString(2));
        Assert.Equal("11Pi", ValueSize.Parse("12345678901234567").ToScaledBinaryString(2));
        Assert.Equal("110Pi", ValueSize.Parse("123456789012345678").ToScaledBinaryString(2));
        Assert.Equal("1100Pi", ValueSize.Parse("1234567890123456789").ToScaledBinaryString(2));
        Assert.Equal("11000Pi", ValueSize.Parse("12345678901234567890").ToScaledBinaryString(2));
    }

    [Fact(DisplayName = "ValueSize: ToScaledBinaryString (3)")]
    public void ToScaledBinaryString3() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledBinaryString(3));
        Assert.Equal("12", ValueSize.Parse("12").ToScaledBinaryString(3));
        Assert.Equal("123", ValueSize.Parse("123").ToScaledBinaryString(3));
        Assert.Equal("1.21Ki", ValueSize.Parse("1234").ToScaledBinaryString(3));
        Assert.Equal("12.1Ki", ValueSize.Parse("12345").ToScaledBinaryString(3));
        Assert.Equal("121Ki", ValueSize.Parse("123456").ToScaledBinaryString(3));
        Assert.Equal("1.18Mi", ValueSize.Parse("1234567").ToScaledBinaryString(3));
        Assert.Equal("11.8Mi", ValueSize.Parse("12345678").ToScaledBinaryString(3));
        Assert.Equal("118Mi", ValueSize.Parse("123456789").ToScaledBinaryString(3));
        Assert.Equal("1.15Gi", ValueSize.Parse("1234567890").ToScaledBinaryString(3));
        Assert.Equal("11.5Gi", ValueSize.Parse("12345678901").ToScaledBinaryString(3));
        Assert.Equal("115Gi", ValueSize.Parse("123456789012").ToScaledBinaryString(3));
        Assert.Equal("1.12Ti", ValueSize.Parse("1234567890123").ToScaledBinaryString(3));
        Assert.Equal("11.2Ti", ValueSize.Parse("12345678901234").ToScaledBinaryString(3));
        Assert.Equal("112Ti", ValueSize.Parse("123456789012345").ToScaledBinaryString(3));
        Assert.Equal("1.10Pi", ValueSize.Parse("1234567890123456").ToScaledBinaryString(3));
        Assert.Equal("11.0Pi", ValueSize.Parse("12345678901234567").ToScaledBinaryString(3));
        Assert.Equal("110Pi", ValueSize.Parse("123456789012345678").ToScaledBinaryString(3));
        Assert.Equal("1100Pi", ValueSize.Parse("1234567890123456789").ToScaledBinaryString(3));
        Assert.Equal("11000Pi", ValueSize.Parse("12345678901234567890").ToScaledBinaryString(3));
    }

    [Fact(DisplayName = "ValueSize: ToScaledBinaryString (10)")]
    public void ToScaledBinaryString10() {
        Assert.Equal("1", ValueSize.Parse("1").ToScaledBinaryString());
        Assert.Equal("10", ValueSize.Parse("10").ToScaledBinaryString());
        Assert.Equal("102", ValueSize.Parse("102").ToScaledBinaryString());
        Assert.Equal("1020", ValueSize.Parse("1023").ToScaledBinaryString());
        Assert.Equal("9.99Ki", ValueSize.Parse("10234").ToScaledBinaryString());
        Assert.Equal("99.9Ki", ValueSize.Parse("102345").ToScaledBinaryString());
        Assert.Equal("999Ki", ValueSize.Parse("1023456").ToScaledBinaryString());
        Assert.Equal("9.76Mi", ValueSize.Parse("10234567").ToScaledBinaryString());
        Assert.Equal("97.6Mi", ValueSize.Parse("102345678").ToScaledBinaryString());
        Assert.Equal("976Mi", ValueSize.Parse("1023456789").ToScaledBinaryString());
        Assert.Equal("9.53Gi", ValueSize.Parse("10234567890").ToScaledBinaryString());
        Assert.Equal("95.3Gi", ValueSize.Parse("102345678901").ToScaledBinaryString());
        Assert.Equal("953Gi", ValueSize.Parse("1023456789012").ToScaledBinaryString());
        Assert.Equal("9.31Ti", ValueSize.Parse("10234567890123").ToScaledBinaryString());
        Assert.Equal("93.1Ti", ValueSize.Parse("102345678901234").ToScaledBinaryString());
        Assert.Equal("931Ti", ValueSize.Parse("1023456789012345").ToScaledBinaryString());
        Assert.Equal("9.09Pi", ValueSize.Parse("10234567890123456").ToScaledBinaryString());
        Assert.Equal("90.9Pi", ValueSize.Parse("102345678901234567").ToScaledBinaryString());
        Assert.Equal("909Pi", ValueSize.Parse("1023456789012345678").ToScaledBinaryString());
        Assert.Equal("9090Pi", ValueSize.Parse("10234567890123456789").ToScaledBinaryString());
    }

    [Fact(DisplayName = "ValueSize: ToScaledBinaryString (11)")]
    public void ToScaledBinaryString11() {
        Assert.Equal("3", ValueSize.Parse("3").ToScaledBinaryString());
        Assert.Equal("31", ValueSize.Parse("31").ToScaledBinaryString());
        Assert.Equal("314", ValueSize.Parse("314").ToScaledBinaryString());
        Assert.Equal("3.07Ki", ValueSize.Parse("3141").ToScaledBinaryString());
        Assert.Equal("30.7Ki", ValueSize.Parse("31415").ToScaledBinaryString());
        Assert.Equal("307Ki", ValueSize.Parse("314159").ToScaledBinaryString());
        Assert.Equal("3.00Mi", ValueSize.Parse("3141592").ToScaledBinaryString());
        Assert.Equal("30.0Mi", ValueSize.Parse("31415926").ToScaledBinaryString());
        Assert.Equal("300Mi", ValueSize.Parse("314159265").ToScaledBinaryString());
        Assert.Equal("2.93Gi", ValueSize.Parse("3141592653").ToScaledBinaryString());
        Assert.Equal("29.3Gi", ValueSize.Parse("31415926535").ToScaledBinaryString());
        Assert.Equal("293Gi", ValueSize.Parse("314159265358").ToScaledBinaryString());
        Assert.Equal("2.86Ti", ValueSize.Parse("3141592653589").ToScaledBinaryString());
        Assert.Equal("28.6Ti", ValueSize.Parse("31415926535897").ToScaledBinaryString());
        Assert.Equal("286Ti", ValueSize.Parse("314159265358979").ToScaledBinaryString());
        Assert.Equal("2.79Pi", ValueSize.Parse("3141592653589793").ToScaledBinaryString());
        Assert.Equal("27.9Pi", ValueSize.Parse("31415926535897932").ToScaledBinaryString());
        Assert.Equal("279Pi", ValueSize.Parse("314159265358979323").ToScaledBinaryString());
        Assert.Equal("2790Pi", ValueSize.Parse("3141592653589793238").ToScaledBinaryString());
    }

    [Fact(DisplayName = "ValueSize: ToScaledBinaryString (12)")]
    public void ToScaledBinaryString12() {
        Assert.Equal("2", ValueSize.Parse("2").ToScaledBinaryString());
        Assert.Equal("27", ValueSize.Parse("27").ToScaledBinaryString());
        Assert.Equal("271", ValueSize.Parse("271").ToScaledBinaryString());
        Assert.Equal("2.65Ki", ValueSize.Parse("2718").ToScaledBinaryString());
        Assert.Equal("26.5Ki", ValueSize.Parse("27182").ToScaledBinaryString());
        Assert.Equal("265Ki", ValueSize.Parse("271828").ToScaledBinaryString());
        Assert.Equal("2.59Mi", ValueSize.Parse("2718281").ToScaledBinaryString());
        Assert.Equal("25.9Mi", ValueSize.Parse("27182818").ToScaledBinaryString());
        Assert.Equal("259Mi", ValueSize.Parse("271828182").ToScaledBinaryString());
        Assert.Equal("2.53Gi", ValueSize.Parse("2718281828").ToScaledBinaryString());
        Assert.Equal("25.3Gi", ValueSize.Parse("27182818284").ToScaledBinaryString());
        Assert.Equal("253Gi", ValueSize.Parse("271828182845").ToScaledBinaryString());
        Assert.Equal("2.47Ti", ValueSize.Parse("2718281828459").ToScaledBinaryString());
        Assert.Equal("24.7Ti", ValueSize.Parse("27182818284590").ToScaledBinaryString());
        Assert.Equal("247Ti", ValueSize.Parse("271828182845904").ToScaledBinaryString());
        Assert.Equal("2.41Pi", ValueSize.Parse("2718281828459045").ToScaledBinaryString());
        Assert.Equal("24.1Pi", ValueSize.Parse("27182818284590452").ToScaledBinaryString());
        Assert.Equal("241Pi", ValueSize.Parse("271828182845904523").ToScaledBinaryString());
        Assert.Equal("2410Pi", ValueSize.Parse("2718281828459045235").ToScaledBinaryString());
    }

}
