using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class SizeValue_Tests {

    [TestMethod]
    public void SizeValue_Basic() {
        var text = "42";
        Assert.IsTrue(SizeValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, SizeValue.Parse(text));
        Assert.AreEqual(result, SizeValue.Parse(text));
        Assert.AreEqual(result, UInt64.Parse(text));
    }

    [TestMethod]
    public void SizeValue_Base1() {
        Assert.AreEqual("42", SizeValue.Parse("42"));
    }

    [TestMethod]
    public void SizeValue_Base2() {
        Assert.AreEqual("43", SizeValue.Parse("42.84"));
    }

    [TestMethod]
    public void SizeValue_Base3() {
        Assert.AreEqual("43", SizeValue.Parse(" 42.84"));
    }


    [TestMethod]
    public void SizeValue_Kilo1() {
        Assert.AreEqual("42000", SizeValue.Parse("42K"));
    }

    [TestMethod]
    public void SizeValue_Kilo2() {
        Assert.AreEqual("42840", SizeValue.Parse("42.84 k"));
    }

    [TestMethod]
    public void SizeValue_Kilo3() {
        Assert.AreEqual("1", SizeValue.Parse(" 0.0001 k "));
    }

    [TestMethod]
    public void SizeValue_Kibi1() {
        Assert.AreEqual("43008", SizeValue.Parse("42Ki"));
    }

    [TestMethod]
    public void SizeValue_Kibi2() {
        Assert.AreEqual("43868", SizeValue.Parse("42.84ki"));
    }

    [TestMethod]
    public void SizeValue_Kibi3() {
        Assert.AreEqual("1", SizeValue.Parse(" 0.000001 ki "));
    }


    [TestMethod]
    public void SizeValue_Mega1() {
        Assert.AreEqual("42000000", SizeValue.Parse("42m"));
    }

    [TestMethod]
    public void SizeValue_Mega2() {
        Assert.AreEqual("42840000", SizeValue.Parse("42.84 M"));
    }

    [TestMethod]
    public void SizeValue_Mega3() {
        Assert.AreEqual("1", SizeValue.Parse(" 0.0000001 M "));
    }

    [TestMethod]
    public void SizeValue_Mebi1() {
        Assert.AreEqual("44040192", SizeValue.Parse("42Mi"));
    }

    [TestMethod]
    public void SizeValue_Mebi2() {
        Assert.AreEqual("44920996", SizeValue.Parse("42.84Mi"));
    }

    [TestMethod]
    public void SizeValue_Mebi3() {
        Assert.AreEqual("1", SizeValue.Parse(" 0.0000001 Mi "));
    }


    [TestMethod]
    public void SizeValue_Giga1() {
        Assert.AreEqual("42000000000", SizeValue.Parse("42 G"));
    }

    [TestMethod]
    public void SizeValue_Giga2() {
        Assert.AreEqual("42840000000", SizeValue.Parse("42.84g"));
    }

    [TestMethod]
    public void SizeValue_Giga3() {
        Assert.AreEqual("1", SizeValue.Parse("  0.0000000001 G "));
    }

    [TestMethod]
    public void SizeValue_Gibi1() {
        Assert.AreEqual("45097156608", SizeValue.Parse("42 Gi"));
    }

    [TestMethod]
    public void SizeValue_Gibi2() {
        Assert.AreEqual("45999099740", SizeValue.Parse("42.84Gi"));
    }

    [TestMethod]
    public void SizeValue_Gibi3() {
        Assert.AreEqual("1", SizeValue.Parse("  0.0000000001 GI "));
    }


    [TestMethod]
    public void SizeValue_Tera1() {
        Assert.AreEqual("42000000000000", SizeValue.Parse("42T"));
    }

    [TestMethod]
    public void SizeValue_Tera2() {
        Assert.AreEqual("42840000000000", SizeValue.Parse("42.84 T"));
    }

    [TestMethod]
    public void SizeValue_Tera3() {
        Assert.AreEqual("1", SizeValue.Parse("  0.0000000000001 T "));
    }

    [TestMethod]
    public void SizeValue_Tebi1() {
        Assert.AreEqual("46179488366592", SizeValue.Parse("42Ti"));
    }

    [TestMethod]
    public void SizeValue_Tebi2() {
        Assert.AreEqual("47103078133924", SizeValue.Parse("42.84Ti"));
    }

    [TestMethod]
    public void SizeValue_Tebi3() {
        Assert.AreEqual("1", SizeValue.Parse("  0.0000000000001 Ti "));
    }


    [TestMethod]
    public void SizeValue_Peta1() {
        Assert.AreEqual("42000000000000000", SizeValue.Parse("42p"));
    }

    [TestMethod]
    public void SizeValue_Peta2() {
        Assert.AreEqual("42840000000000000", SizeValue.Parse("42.84 P"));
    }

    [TestMethod]
    public void SizeValue_Peta3() {
        Assert.AreEqual("1", SizeValue.Parse("  0.0000000000000001 P "));
    }

    [TestMethod]
    public void SizeValue_Pebi1() {
        Assert.AreEqual("47287796087390208", SizeValue.Parse("42pi"));
    }

    [TestMethod]
    public void SizeValue_Pebi2() {
        Assert.AreEqual("48233552009138012", SizeValue.Parse("42.84PI"));
    }

    [TestMethod]
    public void SizeValue_Pebi3() {
        Assert.AreEqual("1", SizeValue.Parse("  0.0000000000000001 PI "));
    }


    [TestMethod]
    public void SizeValue_FailedParse() {
        Assert.IsFalse(SizeValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            SizeValue.Parse("A");
        });
    }

    [TestMethod]
    public void SizeValue_OutOfRange() {
        Assert.IsTrue(SizeValue.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.IsFalse(SizeValue.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(SizeValue.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(SizeValue.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
        Assert.IsFalse(SizeValue.TryParse("42E", out var _));
    }


    [TestMethod]
    public void SizeValue_ToKiloString() {
        Assert.AreEqual("0.042k", SizeValue.Parse("42").ToKiloString());
        Assert.AreEqual("0.0420k", SizeValue.Parse("42").ToKiloString("0.0000"));
        Assert.AreEqual("420k", SizeValue.Parse("420000").ToKiloString());
        Assert.AreEqual("420.0000k", SizeValue.Parse("420000").ToKiloString("0.0000"));
        Assert.AreEqual("4200000k", SizeValue.Parse("4200000000").ToKiloString());
        Assert.AreEqual("4200000.0000k", SizeValue.Parse("4200000000").ToKiloString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToKibiString() {
        Assert.AreEqual("0.041015625Ki", SizeValue.Parse("42").ToKibiString());
        Assert.AreEqual("0.0410Ki", SizeValue.Parse("42").ToKibiString("0.0000"));
        Assert.AreEqual("410.15625Ki", SizeValue.Parse("420000").ToKibiString());
        Assert.AreEqual("410.1563Ki", SizeValue.Parse("420000").ToKibiString("0.0000"));
        Assert.AreEqual("4101562.5Ki", SizeValue.Parse("4200000000").ToKibiString());
        Assert.AreEqual("4101562.5000Ki", SizeValue.Parse("4200000000").ToKibiString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToMegaString() {
        Assert.AreEqual("0.42M", SizeValue.Parse("420000").ToMegaString());
        Assert.AreEqual("0.4200M", SizeValue.Parse("420000").ToMegaString("0.0000"));
        Assert.AreEqual("4200M", SizeValue.Parse("4200000000").ToMegaString());
        Assert.AreEqual("4200.0000M", SizeValue.Parse("4200000000").ToMegaString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToMebiString() {
        Assert.AreEqual("0.400543212890625Mi", SizeValue.Parse("420000").ToMebiString());
        Assert.AreEqual("0.4005Mi", SizeValue.Parse("420000").ToMebiString("0.0000"));
        Assert.AreEqual("4005.43212890625Mi", SizeValue.Parse("4200000000").ToMebiString());
        Assert.AreEqual("4005.4321Mi", SizeValue.Parse("4200000000").ToMebiString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToGigaString() {
        Assert.AreEqual("0.00042G", SizeValue.Parse("420000").ToGigaString());
        Assert.AreEqual("0.0004G", SizeValue.Parse("420000").ToGigaString("0.0000"));
        Assert.AreEqual("4.2G", SizeValue.Parse("4200000000").ToGigaString());
        Assert.AreEqual("4.2000G", SizeValue.Parse("4200000000").ToGigaString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToGibiString() {
        Assert.AreEqual("0.000391155481338501Gi", SizeValue.Parse("420000").ToGibiString());
        Assert.AreEqual("0.0004Gi", SizeValue.Parse("420000").ToGibiString("0.0000"));
        Assert.AreEqual("3.9115548133850098Gi", SizeValue.Parse("4200000000").ToGibiString());
        Assert.AreEqual("3.9116Gi", SizeValue.Parse("4200000000").ToGibiString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToTeraString() {
        Assert.AreEqual("0.00044T", SizeValue.Parse("440000000").ToTeraString());
        Assert.AreEqual("0.0004T", SizeValue.Parse("440000000").ToTeraString("0.0000"));
        Assert.AreEqual("4.4T", SizeValue.Parse("4400000000000").ToTeraString());
        Assert.AreEqual("4.4000T", SizeValue.Parse("4400000000000").ToTeraString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToTebiString() {
        Assert.AreEqual("0.0004001776687800884Ti", SizeValue.Parse("440000000").ToTebiString());
        Assert.AreEqual("0.0004Ti", SizeValue.Parse("440000000").ToTebiString("0.0000"));
        Assert.AreEqual("4.001776687800884Ti", SizeValue.Parse("4400000000000").ToTebiString());
        Assert.AreEqual("4.0018Ti", SizeValue.Parse("4400000000000").ToTebiString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToPetaString() {
        Assert.AreEqual("0.00046P", SizeValue.Parse("460000000000").ToPetaString());
        Assert.AreEqual("0.0005P", SizeValue.Parse("460000000000").ToPetaString("0.0000"));
        Assert.AreEqual("4.6P", SizeValue.Parse("4600000000000000").ToPetaString());
        Assert.AreEqual("4.6000P", SizeValue.Parse("4600000000000000").ToPetaString("0.0000"));
    }

    [TestMethod]
    public void SizeValue_ToPebiString() {
        Assert.AreEqual("0.0004085620730620576Pi", SizeValue.Parse("460000000000").ToPebiString());
        Assert.AreEqual("0.0004Pi", SizeValue.Parse("460000000000").ToPebiString("0.0000"));
        Assert.AreEqual("4.085620730620576Pi", SizeValue.Parse("4600000000000000").ToPebiString());
        Assert.AreEqual("4.0856Pi", SizeValue.Parse("4600000000000000").ToPebiString("0.0000"));
    }


    [TestMethod]
    public void SizeValue_ToScaledSIString() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString());
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledSIString());
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledSIString());
        Assert.AreEqual("1.23k", SizeValue.Parse("1234").ToScaledSIString());
        Assert.AreEqual("12.3k", SizeValue.Parse("12345").ToScaledSIString());
        Assert.AreEqual("123k", SizeValue.Parse("123456").ToScaledSIString());
        Assert.AreEqual("1.23M", SizeValue.Parse("1234567").ToScaledSIString());
        Assert.AreEqual("12.3M", SizeValue.Parse("12345678").ToScaledSIString());
        Assert.AreEqual("123M", SizeValue.Parse("123456789").ToScaledSIString());
        Assert.AreEqual("1.23G", SizeValue.Parse("1234567890").ToScaledSIString());
        Assert.AreEqual("12.3G", SizeValue.Parse("12345678901").ToScaledSIString());
        Assert.AreEqual("123G", SizeValue.Parse("123456789012").ToScaledSIString());
        Assert.AreEqual("1.23T", SizeValue.Parse("1234567890123").ToScaledSIString());
        Assert.AreEqual("12.3T", SizeValue.Parse("12345678901234").ToScaledSIString());
        Assert.AreEqual("123T", SizeValue.Parse("123456789012345").ToScaledSIString());
        Assert.AreEqual("1.23P", SizeValue.Parse("1234567890123456").ToScaledSIString());
        Assert.AreEqual("12.3P", SizeValue.Parse("12345678901234567").ToScaledSIString());
        Assert.AreEqual("123P", SizeValue.Parse("123456789012345678").ToScaledSIString());
        Assert.AreEqual("1230P", SizeValue.Parse("1234567890123456789").ToScaledSIString());
        Assert.AreEqual("12300P", SizeValue.Parse("12345678901234567890").ToScaledSIString());
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString1() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString(1));
        Assert.AreEqual("10", SizeValue.Parse("12").ToScaledSIString(1));
        Assert.AreEqual("100", SizeValue.Parse("123").ToScaledSIString(1));
        Assert.AreEqual("1k", SizeValue.Parse("1234").ToScaledSIString(1));
        Assert.AreEqual("10k", SizeValue.Parse("12345").ToScaledSIString(1));
        Assert.AreEqual("100k", SizeValue.Parse("123456").ToScaledSIString(1));
        Assert.AreEqual("1M", SizeValue.Parse("1234567").ToScaledSIString(1));
        Assert.AreEqual("10M", SizeValue.Parse("12345678").ToScaledSIString(1));
        Assert.AreEqual("100M", SizeValue.Parse("123456789").ToScaledSIString(1));
        Assert.AreEqual("1G", SizeValue.Parse("1234567890").ToScaledSIString(1));
        Assert.AreEqual("10G", SizeValue.Parse("12345678901").ToScaledSIString(1));
        Assert.AreEqual("100G", SizeValue.Parse("123456789012").ToScaledSIString(1));
        Assert.AreEqual("1T", SizeValue.Parse("1234567890123").ToScaledSIString(1));
        Assert.AreEqual("10T", SizeValue.Parse("12345678901234").ToScaledSIString(1));
        Assert.AreEqual("100T", SizeValue.Parse("123456789012345").ToScaledSIString(1));
        Assert.AreEqual("1P", SizeValue.Parse("1234567890123456").ToScaledSIString(1));
        Assert.AreEqual("10P", SizeValue.Parse("12345678901234567").ToScaledSIString(1));
        Assert.AreEqual("100P", SizeValue.Parse("123456789012345678").ToScaledSIString(1));
        Assert.AreEqual("1000P", SizeValue.Parse("1234567890123456789").ToScaledSIString(1));
        Assert.AreEqual("10000P", SizeValue.Parse("12345678901234567890").ToScaledSIString(1));
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString3() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString(3));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledSIString(3));
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledSIString(3));
        Assert.AreEqual("1.23k", SizeValue.Parse("1234").ToScaledSIString(3));
        Assert.AreEqual("12.3k", SizeValue.Parse("12345").ToScaledSIString(3));
        Assert.AreEqual("123k", SizeValue.Parse("123456").ToScaledSIString(3));
        Assert.AreEqual("1.23M", SizeValue.Parse("1234567").ToScaledSIString(3));
        Assert.AreEqual("12.3M", SizeValue.Parse("12345678").ToScaledSIString(3));
        Assert.AreEqual("123M", SizeValue.Parse("123456789").ToScaledSIString(3));
        Assert.AreEqual("1.23G", SizeValue.Parse("1234567890").ToScaledSIString(3));
        Assert.AreEqual("12.3G", SizeValue.Parse("12345678901").ToScaledSIString(3));
        Assert.AreEqual("123G", SizeValue.Parse("123456789012").ToScaledSIString(3));
        Assert.AreEqual("1.23T", SizeValue.Parse("1234567890123").ToScaledSIString(3));
        Assert.AreEqual("12.3T", SizeValue.Parse("12345678901234").ToScaledSIString(3));
        Assert.AreEqual("123T", SizeValue.Parse("123456789012345").ToScaledSIString(3));
        Assert.AreEqual("1.23P", SizeValue.Parse("1234567890123456").ToScaledSIString(3));
        Assert.AreEqual("12.3P", SizeValue.Parse("12345678901234567").ToScaledSIString(3));
        Assert.AreEqual("123P", SizeValue.Parse("123456789012345678").ToScaledSIString(3));
        Assert.AreEqual("1230P", SizeValue.Parse("1234567890123456789").ToScaledSIString(3));
        Assert.AreEqual("12300P", SizeValue.Parse("12345678901234567890").ToScaledSIString(3));
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString4() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString(4));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledSIString(4));
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledSIString(4));
        Assert.AreEqual("1.234k", SizeValue.Parse("1234").ToScaledSIString(4));
        Assert.AreEqual("12.35k", SizeValue.Parse("12345").ToScaledSIString(4));
        Assert.AreEqual("123.5k", SizeValue.Parse("123456").ToScaledSIString(4));
        Assert.AreEqual("1.235M", SizeValue.Parse("1234567").ToScaledSIString(4));
        Assert.AreEqual("12.35M", SizeValue.Parse("12345678").ToScaledSIString(4));
        Assert.AreEqual("123.5M", SizeValue.Parse("123456789").ToScaledSIString(4));
        Assert.AreEqual("1.235G", SizeValue.Parse("1234567890").ToScaledSIString(4));
        Assert.AreEqual("12.35G", SizeValue.Parse("12345678901").ToScaledSIString(4));
        Assert.AreEqual("123.5G", SizeValue.Parse("123456789012").ToScaledSIString(4));
        Assert.AreEqual("1.235T", SizeValue.Parse("1234567890123").ToScaledSIString(4));
        Assert.AreEqual("12.35T", SizeValue.Parse("12345678901234").ToScaledSIString(4));
        Assert.AreEqual("123.5T", SizeValue.Parse("123456789012345").ToScaledSIString(4));
        Assert.AreEqual("1.235P", SizeValue.Parse("1234567890123456").ToScaledSIString(4));
        Assert.AreEqual("12.35P", SizeValue.Parse("12345678901234567").ToScaledSIString(4));
        Assert.AreEqual("123.5P", SizeValue.Parse("123456789012345678").ToScaledSIString(4));
        Assert.AreEqual("1235P", SizeValue.Parse("1234567890123456789").ToScaledSIString(4));
        Assert.AreEqual("12350P", SizeValue.Parse("12345678901234567890").ToScaledSIString(4));
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString5() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString(5));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledSIString(5));
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledSIString(5));
        Assert.AreEqual("1.234k", SizeValue.Parse("1234").ToScaledSIString(5));
        Assert.AreEqual("12.345k", SizeValue.Parse("12345").ToScaledSIString(5));
        Assert.AreEqual("123.46k", SizeValue.Parse("123456").ToScaledSIString(5));
        Assert.AreEqual("1.2346M", SizeValue.Parse("1234567").ToScaledSIString(5));
        Assert.AreEqual("12.346M", SizeValue.Parse("12345678").ToScaledSIString(5));
        Assert.AreEqual("123.46M", SizeValue.Parse("123456789").ToScaledSIString(5));
        Assert.AreEqual("1.2346G", SizeValue.Parse("1234567890").ToScaledSIString(5));
        Assert.AreEqual("12.346G", SizeValue.Parse("12345678901").ToScaledSIString(5));
        Assert.AreEqual("123.46G", SizeValue.Parse("123456789012").ToScaledSIString(5));
        Assert.AreEqual("1.2346T", SizeValue.Parse("1234567890123").ToScaledSIString(5));
        Assert.AreEqual("12.346T", SizeValue.Parse("12345678901234").ToScaledSIString(5));
        Assert.AreEqual("123.46T", SizeValue.Parse("123456789012345").ToScaledSIString(5));
        Assert.AreEqual("1.2346P", SizeValue.Parse("1234567890123456").ToScaledSIString(5));
        Assert.AreEqual("12.346P", SizeValue.Parse("12345678901234567").ToScaledSIString(5));
        Assert.AreEqual("123.46P", SizeValue.Parse("123456789012345678").ToScaledSIString(5));
        Assert.AreEqual("1234.6P", SizeValue.Parse("1234567890123456789").ToScaledSIString(5));
        Assert.AreEqual("12346P", SizeValue.Parse("12345678901234567890").ToScaledSIString(5));
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString6() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString(6));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledSIString(6));
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledSIString(6));
        Assert.AreEqual("1.234k", SizeValue.Parse("1234").ToScaledSIString(6));
        Assert.AreEqual("12.345k", SizeValue.Parse("12345").ToScaledSIString(6));
        Assert.AreEqual("123.456k", SizeValue.Parse("123456").ToScaledSIString(6));
        Assert.AreEqual("1.23457M", SizeValue.Parse("1234567").ToScaledSIString(6));
        Assert.AreEqual("12.3457M", SizeValue.Parse("12345678").ToScaledSIString(6));
        Assert.AreEqual("123.457M", SizeValue.Parse("123456789").ToScaledSIString(6));
        Assert.AreEqual("1.23457G", SizeValue.Parse("1234567890").ToScaledSIString(6));
        Assert.AreEqual("12.3457G", SizeValue.Parse("12345678901").ToScaledSIString(6));
        Assert.AreEqual("123.457G", SizeValue.Parse("123456789012").ToScaledSIString(6));
        Assert.AreEqual("1.23457T", SizeValue.Parse("1234567890123").ToScaledSIString(6));
        Assert.AreEqual("12.3457T", SizeValue.Parse("12345678901234").ToScaledSIString(6));
        Assert.AreEqual("123.457T", SizeValue.Parse("123456789012345").ToScaledSIString(6));
        Assert.AreEqual("1.23457P", SizeValue.Parse("1234567890123456").ToScaledSIString(6));
        Assert.AreEqual("12.3457P", SizeValue.Parse("12345678901234567").ToScaledSIString(6));
        Assert.AreEqual("123.457P", SizeValue.Parse("123456789012345678").ToScaledSIString(6));
        Assert.AreEqual("1234.57P", SizeValue.Parse("1234567890123456789").ToScaledSIString(6));
        Assert.AreEqual("12345.7P", SizeValue.Parse("12345678901234567890").ToScaledSIString(6));
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString7() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString(7));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledSIString(7));
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledSIString(7));
        Assert.AreEqual("1.234k", SizeValue.Parse("1234").ToScaledSIString(7));
        Assert.AreEqual("12.345k", SizeValue.Parse("12345").ToScaledSIString(7));
        Assert.AreEqual("123.456k", SizeValue.Parse("123456").ToScaledSIString(7));
        Assert.AreEqual("1.234567M", SizeValue.Parse("1234567").ToScaledSIString(7));
        Assert.AreEqual("12.34568M", SizeValue.Parse("12345678").ToScaledSIString(7));
        Assert.AreEqual("123.4568M", SizeValue.Parse("123456789").ToScaledSIString(7));
        Assert.AreEqual("1.234568G", SizeValue.Parse("1234567890").ToScaledSIString(7));
        Assert.AreEqual("12.34568G", SizeValue.Parse("12345678901").ToScaledSIString(7));
        Assert.AreEqual("123.4568G", SizeValue.Parse("123456789012").ToScaledSIString(7));
        Assert.AreEqual("1.234568T", SizeValue.Parse("1234567890123").ToScaledSIString(7));
        Assert.AreEqual("12.34568T", SizeValue.Parse("12345678901234").ToScaledSIString(7));
        Assert.AreEqual("123.4568T", SizeValue.Parse("123456789012345").ToScaledSIString(7));
        Assert.AreEqual("1.234568P", SizeValue.Parse("1234567890123456").ToScaledSIString(7));
        Assert.AreEqual("12.34568P", SizeValue.Parse("12345678901234567").ToScaledSIString(7));
        Assert.AreEqual("123.4568P", SizeValue.Parse("123456789012345678").ToScaledSIString(7));
        Assert.AreEqual("1234.568P", SizeValue.Parse("1234567890123456789").ToScaledSIString(7));
        Assert.AreEqual("12345.68P", SizeValue.Parse("12345678901234567890").ToScaledSIString(7));
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString8() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString(8));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledSIString(8));
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledSIString(8));
        Assert.AreEqual("1.234k", SizeValue.Parse("1234").ToScaledSIString(8));
        Assert.AreEqual("12.345k", SizeValue.Parse("12345").ToScaledSIString(8));
        Assert.AreEqual("123.456k", SizeValue.Parse("123456").ToScaledSIString(8));
        Assert.AreEqual("1.234567M", SizeValue.Parse("1234567").ToScaledSIString(8));
        Assert.AreEqual("12.345678M", SizeValue.Parse("12345678").ToScaledSIString(8));
        Assert.AreEqual("123.45679M", SizeValue.Parse("123456789").ToScaledSIString(8));
        Assert.AreEqual("1.2345679G", SizeValue.Parse("1234567890").ToScaledSIString(8));
        Assert.AreEqual("12.345679G", SizeValue.Parse("12345678901").ToScaledSIString(8));
        Assert.AreEqual("123.45679G", SizeValue.Parse("123456789012").ToScaledSIString(8));
        Assert.AreEqual("1.2345679T", SizeValue.Parse("1234567890123").ToScaledSIString(8));
        Assert.AreEqual("12.345679T", SizeValue.Parse("12345678901234").ToScaledSIString(8));
        Assert.AreEqual("123.45679T", SizeValue.Parse("123456789012345").ToScaledSIString(8));
        Assert.AreEqual("1.2345679P", SizeValue.Parse("1234567890123456").ToScaledSIString(8));
        Assert.AreEqual("12.345679P", SizeValue.Parse("12345678901234567").ToScaledSIString(8));
        Assert.AreEqual("123.45679P", SizeValue.Parse("123456789012345678").ToScaledSIString(8));
        Assert.AreEqual("1234.5679P", SizeValue.Parse("1234567890123456789").ToScaledSIString(8));
        Assert.AreEqual("12345.679P", SizeValue.Parse("12345678901234567890").ToScaledSIString(8));
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString9() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString(9));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledSIString(9));
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledSIString(9));
        Assert.AreEqual("1.234k", SizeValue.Parse("1234").ToScaledSIString(9));
        Assert.AreEqual("12.345k", SizeValue.Parse("12345").ToScaledSIString(9));
        Assert.AreEqual("123.456k", SizeValue.Parse("123456").ToScaledSIString(9));
        Assert.AreEqual("1.234567M", SizeValue.Parse("1234567").ToScaledSIString(9));
        Assert.AreEqual("12.345678M", SizeValue.Parse("12345678").ToScaledSIString(9));
        Assert.AreEqual("123.456789M", SizeValue.Parse("123456789").ToScaledSIString(9));
        Assert.AreEqual("1.23456789G", SizeValue.Parse("1234567890").ToScaledSIString(9));
        Assert.AreEqual("12.3456789G", SizeValue.Parse("12345678901").ToScaledSIString(9));
        Assert.AreEqual("123.456789G", SizeValue.Parse("123456789012").ToScaledSIString(9));
        Assert.AreEqual("1.23456789T", SizeValue.Parse("1234567890123").ToScaledSIString(9));
        Assert.AreEqual("12.3456789T", SizeValue.Parse("12345678901234").ToScaledSIString(9));
        Assert.AreEqual("123.456789T", SizeValue.Parse("123456789012345").ToScaledSIString(9));
        Assert.AreEqual("1.23456789P", SizeValue.Parse("1234567890123456").ToScaledSIString(9));
        Assert.AreEqual("12.3456789P", SizeValue.Parse("12345678901234567").ToScaledSIString(9));
        Assert.AreEqual("123.456789P", SizeValue.Parse("123456789012345678").ToScaledSIString(9));
        Assert.AreEqual("1234.56789P", SizeValue.Parse("1234567890123456789").ToScaledSIString(9));
        Assert.AreEqual("12345.6789P", SizeValue.Parse("12345678901234567890").ToScaledSIString(9));
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString10() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledSIString());
        Assert.AreEqual("10", SizeValue.Parse("10").ToScaledSIString());
        Assert.AreEqual("102", SizeValue.Parse("102").ToScaledSIString());
        Assert.AreEqual("1.02k", SizeValue.Parse("1023").ToScaledSIString());
        Assert.AreEqual("10.2k", SizeValue.Parse("10234").ToScaledSIString());
        Assert.AreEqual("102k", SizeValue.Parse("102345").ToScaledSIString());
        Assert.AreEqual("1.02M", SizeValue.Parse("1023456").ToScaledSIString());
        Assert.AreEqual("10.2M", SizeValue.Parse("10234567").ToScaledSIString());
        Assert.AreEqual("102M", SizeValue.Parse("102345678").ToScaledSIString());
        Assert.AreEqual("1.02G", SizeValue.Parse("1023456789").ToScaledSIString());
        Assert.AreEqual("10.2G", SizeValue.Parse("10234567890").ToScaledSIString());
        Assert.AreEqual("102G", SizeValue.Parse("102345678901").ToScaledSIString());
        Assert.AreEqual("1.02T", SizeValue.Parse("1023456789012").ToScaledSIString());
        Assert.AreEqual("10.2T", SizeValue.Parse("10234567890123").ToScaledSIString());
        Assert.AreEqual("102T", SizeValue.Parse("102345678901234").ToScaledSIString());
        Assert.AreEqual("1.02P", SizeValue.Parse("1023456789012345").ToScaledSIString());
        Assert.AreEqual("10.2P", SizeValue.Parse("10234567890123456").ToScaledSIString());
        Assert.AreEqual("102P", SizeValue.Parse("102345678901234567").ToScaledSIString());
        Assert.AreEqual("1020P", SizeValue.Parse("1023456789012345678").ToScaledSIString());
        Assert.AreEqual("10200P", SizeValue.Parse("10234567890123456789").ToScaledSIString());
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString11() {
        Assert.AreEqual("3", SizeValue.Parse("3").ToScaledSIString());
        Assert.AreEqual("31", SizeValue.Parse("31").ToScaledSIString());
        Assert.AreEqual("314", SizeValue.Parse("314").ToScaledSIString());
        Assert.AreEqual("3.14k", SizeValue.Parse("3141").ToScaledSIString());
        Assert.AreEqual("31.4k", SizeValue.Parse("31415").ToScaledSIString());
        Assert.AreEqual("314k", SizeValue.Parse("314159").ToScaledSIString());
        Assert.AreEqual("3.14M", SizeValue.Parse("3141592").ToScaledSIString());
        Assert.AreEqual("31.4M", SizeValue.Parse("31415926").ToScaledSIString());
        Assert.AreEqual("314M", SizeValue.Parse("314159265").ToScaledSIString());
        Assert.AreEqual("3.14G", SizeValue.Parse("3141592653").ToScaledSIString());
        Assert.AreEqual("31.4G", SizeValue.Parse("31415926535").ToScaledSIString());
        Assert.AreEqual("314G", SizeValue.Parse("314159265358").ToScaledSIString());
        Assert.AreEqual("3.14T", SizeValue.Parse("3141592653589").ToScaledSIString());
        Assert.AreEqual("31.4T", SizeValue.Parse("31415926535897").ToScaledSIString());
        Assert.AreEqual("314T", SizeValue.Parse("314159265358979").ToScaledSIString());
        Assert.AreEqual("3.14P", SizeValue.Parse("3141592653589793").ToScaledSIString());
        Assert.AreEqual("31.4P", SizeValue.Parse("31415926535897932").ToScaledSIString());
        Assert.AreEqual("314P", SizeValue.Parse("314159265358979323").ToScaledSIString());
        Assert.AreEqual("3140P", SizeValue.Parse("3141592653589793238").ToScaledSIString());
    }

    [TestMethod]
    public void SizeValue_ToScaledSIString12() {
        Assert.AreEqual("2", SizeValue.Parse("2").ToScaledSIString());
        Assert.AreEqual("27", SizeValue.Parse("27").ToScaledSIString());
        Assert.AreEqual("271", SizeValue.Parse("271").ToScaledSIString());
        Assert.AreEqual("2.72k", SizeValue.Parse("2718").ToScaledSIString());
        Assert.AreEqual("27.2k", SizeValue.Parse("27182").ToScaledSIString());
        Assert.AreEqual("272k", SizeValue.Parse("271828").ToScaledSIString());
        Assert.AreEqual("2.72M", SizeValue.Parse("2718281").ToScaledSIString());
        Assert.AreEqual("27.2M", SizeValue.Parse("27182818").ToScaledSIString());
        Assert.AreEqual("272M", SizeValue.Parse("271828182").ToScaledSIString());
        Assert.AreEqual("2.72G", SizeValue.Parse("2718281828").ToScaledSIString());
        Assert.AreEqual("27.2G", SizeValue.Parse("27182818284").ToScaledSIString());
        Assert.AreEqual("272G", SizeValue.Parse("271828182845").ToScaledSIString());
        Assert.AreEqual("2.72T", SizeValue.Parse("2718281828459").ToScaledSIString());
        Assert.AreEqual("27.2T", SizeValue.Parse("27182818284590").ToScaledSIString());
        Assert.AreEqual("272T", SizeValue.Parse("271828182845904").ToScaledSIString());
        Assert.AreEqual("2.72P", SizeValue.Parse("2718281828459045").ToScaledSIString());
        Assert.AreEqual("27.2P", SizeValue.Parse("27182818284590452").ToScaledSIString());
        Assert.AreEqual("272P", SizeValue.Parse("271828182845904523").ToScaledSIString());
        Assert.AreEqual("2720P", SizeValue.Parse("2718281828459045235").ToScaledSIString());
    }


    [TestMethod]
    public void SizeValue_ToScaledBinaryString() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledBinaryString());
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledBinaryString());
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledBinaryString());
        Assert.AreEqual("1.21Ki", SizeValue.Parse("1234").ToScaledBinaryString());
        Assert.AreEqual("12.1Ki", SizeValue.Parse("12345").ToScaledBinaryString());
        Assert.AreEqual("121Ki", SizeValue.Parse("123456").ToScaledBinaryString());
        Assert.AreEqual("1.18Mi", SizeValue.Parse("1234567").ToScaledBinaryString());
        Assert.AreEqual("11.8Mi", SizeValue.Parse("12345678").ToScaledBinaryString());
        Assert.AreEqual("118Mi", SizeValue.Parse("123456789").ToScaledBinaryString());
        Assert.AreEqual("1.15Gi", SizeValue.Parse("1234567890").ToScaledBinaryString());
        Assert.AreEqual("11.5Gi", SizeValue.Parse("12345678901").ToScaledBinaryString());
        Assert.AreEqual("115Gi", SizeValue.Parse("123456789012").ToScaledBinaryString());
        Assert.AreEqual("1.12Ti", SizeValue.Parse("1234567890123").ToScaledBinaryString());
        Assert.AreEqual("11.2Ti", SizeValue.Parse("12345678901234").ToScaledBinaryString());
        Assert.AreEqual("112Ti", SizeValue.Parse("123456789012345").ToScaledBinaryString());
        Assert.AreEqual("1.10Pi", SizeValue.Parse("1234567890123456").ToScaledBinaryString());
        Assert.AreEqual("11.0Pi", SizeValue.Parse("12345678901234567").ToScaledBinaryString());
        Assert.AreEqual("110Pi", SizeValue.Parse("123456789012345678").ToScaledBinaryString());
        Assert.AreEqual("1100Pi", SizeValue.Parse("1234567890123456789").ToScaledBinaryString());
        Assert.AreEqual("11000Pi", SizeValue.Parse("12345678901234567890").ToScaledBinaryString());
    }

    [TestMethod]
    public void SizeValue_ToScaledBinaryString1() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledBinaryString(1));
        Assert.AreEqual("10", SizeValue.Parse("12").ToScaledBinaryString(1));
        Assert.AreEqual("100", SizeValue.Parse("123").ToScaledBinaryString(1));
        Assert.AreEqual("1Ki", SizeValue.Parse("1234").ToScaledBinaryString(1));
        Assert.AreEqual("10Ki", SizeValue.Parse("12345").ToScaledBinaryString(1));
        Assert.AreEqual("100Ki", SizeValue.Parse("123456").ToScaledBinaryString(1));
        Assert.AreEqual("1Mi", SizeValue.Parse("1234567").ToScaledBinaryString(1));
        Assert.AreEqual("10Mi", SizeValue.Parse("12345678").ToScaledBinaryString(1));
        Assert.AreEqual("100Mi", SizeValue.Parse("123456789").ToScaledBinaryString(1));
        Assert.AreEqual("1Gi", SizeValue.Parse("1234567890").ToScaledBinaryString(1));
        Assert.AreEqual("10Gi", SizeValue.Parse("12345678901").ToScaledBinaryString(1));
        Assert.AreEqual("100Gi", SizeValue.Parse("123456789012").ToScaledBinaryString(1));
        Assert.AreEqual("1Ti", SizeValue.Parse("1234567890123").ToScaledBinaryString(1));
        Assert.AreEqual("10Ti", SizeValue.Parse("12345678901234").ToScaledBinaryString(1));
        Assert.AreEqual("100Ti", SizeValue.Parse("123456789012345").ToScaledBinaryString(1));
        Assert.AreEqual("1Pi", SizeValue.Parse("1234567890123456").ToScaledBinaryString(1));
        Assert.AreEqual("10Pi", SizeValue.Parse("12345678901234567").ToScaledBinaryString(1));
        Assert.AreEqual("100Pi", SizeValue.Parse("123456789012345678").ToScaledBinaryString(1));
        Assert.AreEqual("1000Pi", SizeValue.Parse("1234567890123456789").ToScaledBinaryString(1));
        Assert.AreEqual("10000Pi", SizeValue.Parse("12345678901234567890").ToScaledBinaryString(1));
    }

    [TestMethod]
    public void SizeValue_ToScaledBinaryString2() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledBinaryString(2));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledBinaryString(2));
        Assert.AreEqual("120", SizeValue.Parse("123").ToScaledBinaryString(2));
        Assert.AreEqual("1.2Ki", SizeValue.Parse("1234").ToScaledBinaryString(2));
        Assert.AreEqual("12Ki", SizeValue.Parse("12345").ToScaledBinaryString(2));
        Assert.AreEqual("120Ki", SizeValue.Parse("123456").ToScaledBinaryString(2));
        Assert.AreEqual("1.2Mi", SizeValue.Parse("1234567").ToScaledBinaryString(2));
        Assert.AreEqual("12Mi", SizeValue.Parse("12345678").ToScaledBinaryString(2));
        Assert.AreEqual("120Mi", SizeValue.Parse("123456789").ToScaledBinaryString(2));
        Assert.AreEqual("1.1Gi", SizeValue.Parse("1234567890").ToScaledBinaryString(2));
        Assert.AreEqual("11Gi", SizeValue.Parse("12345678901").ToScaledBinaryString(2));
        Assert.AreEqual("110Gi", SizeValue.Parse("123456789012").ToScaledBinaryString(2));
        Assert.AreEqual("1.1Ti", SizeValue.Parse("1234567890123").ToScaledBinaryString(2));
        Assert.AreEqual("11Ti", SizeValue.Parse("12345678901234").ToScaledBinaryString(2));
        Assert.AreEqual("110Ti", SizeValue.Parse("123456789012345").ToScaledBinaryString(2));
        Assert.AreEqual("1.1Pi", SizeValue.Parse("1234567890123456").ToScaledBinaryString(2));
        Assert.AreEqual("11Pi", SizeValue.Parse("12345678901234567").ToScaledBinaryString(2));
        Assert.AreEqual("110Pi", SizeValue.Parse("123456789012345678").ToScaledBinaryString(2));
        Assert.AreEqual("1100Pi", SizeValue.Parse("1234567890123456789").ToScaledBinaryString(2));
        Assert.AreEqual("11000Pi", SizeValue.Parse("12345678901234567890").ToScaledBinaryString(2));
    }

    [TestMethod]
    public void SizeValue_ToScaledBinaryString3() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledBinaryString(3));
        Assert.AreEqual("12", SizeValue.Parse("12").ToScaledBinaryString(3));
        Assert.AreEqual("123", SizeValue.Parse("123").ToScaledBinaryString(3));
        Assert.AreEqual("1.21Ki", SizeValue.Parse("1234").ToScaledBinaryString(3));
        Assert.AreEqual("12.1Ki", SizeValue.Parse("12345").ToScaledBinaryString(3));
        Assert.AreEqual("121Ki", SizeValue.Parse("123456").ToScaledBinaryString(3));
        Assert.AreEqual("1.18Mi", SizeValue.Parse("1234567").ToScaledBinaryString(3));
        Assert.AreEqual("11.8Mi", SizeValue.Parse("12345678").ToScaledBinaryString(3));
        Assert.AreEqual("118Mi", SizeValue.Parse("123456789").ToScaledBinaryString(3));
        Assert.AreEqual("1.15Gi", SizeValue.Parse("1234567890").ToScaledBinaryString(3));
        Assert.AreEqual("11.5Gi", SizeValue.Parse("12345678901").ToScaledBinaryString(3));
        Assert.AreEqual("115Gi", SizeValue.Parse("123456789012").ToScaledBinaryString(3));
        Assert.AreEqual("1.12Ti", SizeValue.Parse("1234567890123").ToScaledBinaryString(3));
        Assert.AreEqual("11.2Ti", SizeValue.Parse("12345678901234").ToScaledBinaryString(3));
        Assert.AreEqual("112Ti", SizeValue.Parse("123456789012345").ToScaledBinaryString(3));
        Assert.AreEqual("1.10Pi", SizeValue.Parse("1234567890123456").ToScaledBinaryString(3));
        Assert.AreEqual("11.0Pi", SizeValue.Parse("12345678901234567").ToScaledBinaryString(3));
        Assert.AreEqual("110Pi", SizeValue.Parse("123456789012345678").ToScaledBinaryString(3));
        Assert.AreEqual("1100Pi", SizeValue.Parse("1234567890123456789").ToScaledBinaryString(3));
        Assert.AreEqual("11000Pi", SizeValue.Parse("12345678901234567890").ToScaledBinaryString(3));
    }

    [TestMethod]
    public void SizeValue_ToScaledBinaryString10() {
        Assert.AreEqual("1", SizeValue.Parse("1").ToScaledBinaryString());
        Assert.AreEqual("10", SizeValue.Parse("10").ToScaledBinaryString());
        Assert.AreEqual("102", SizeValue.Parse("102").ToScaledBinaryString());
        Assert.AreEqual("1020", SizeValue.Parse("1023").ToScaledBinaryString());
        Assert.AreEqual("9.99Ki", SizeValue.Parse("10234").ToScaledBinaryString());
        Assert.AreEqual("99.9Ki", SizeValue.Parse("102345").ToScaledBinaryString());
        Assert.AreEqual("999Ki", SizeValue.Parse("1023456").ToScaledBinaryString());
        Assert.AreEqual("9.76Mi", SizeValue.Parse("10234567").ToScaledBinaryString());
        Assert.AreEqual("97.6Mi", SizeValue.Parse("102345678").ToScaledBinaryString());
        Assert.AreEqual("976Mi", SizeValue.Parse("1023456789").ToScaledBinaryString());
        Assert.AreEqual("9.53Gi", SizeValue.Parse("10234567890").ToScaledBinaryString());
        Assert.AreEqual("95.3Gi", SizeValue.Parse("102345678901").ToScaledBinaryString());
        Assert.AreEqual("953Gi", SizeValue.Parse("1023456789012").ToScaledBinaryString());
        Assert.AreEqual("9.31Ti", SizeValue.Parse("10234567890123").ToScaledBinaryString());
        Assert.AreEqual("93.1Ti", SizeValue.Parse("102345678901234").ToScaledBinaryString());
        Assert.AreEqual("931Ti", SizeValue.Parse("1023456789012345").ToScaledBinaryString());
        Assert.AreEqual("9.09Pi", SizeValue.Parse("10234567890123456").ToScaledBinaryString());
        Assert.AreEqual("90.9Pi", SizeValue.Parse("102345678901234567").ToScaledBinaryString());
        Assert.AreEqual("909Pi", SizeValue.Parse("1023456789012345678").ToScaledBinaryString());
        Assert.AreEqual("9090Pi", SizeValue.Parse("10234567890123456789").ToScaledBinaryString());
    }

    [TestMethod]
    public void SizeValue_ToScaledBinaryString11() {
        Assert.AreEqual("3", SizeValue.Parse("3").ToScaledBinaryString());
        Assert.AreEqual("31", SizeValue.Parse("31").ToScaledBinaryString());
        Assert.AreEqual("314", SizeValue.Parse("314").ToScaledBinaryString());
        Assert.AreEqual("3.07Ki", SizeValue.Parse("3141").ToScaledBinaryString());
        Assert.AreEqual("30.7Ki", SizeValue.Parse("31415").ToScaledBinaryString());
        Assert.AreEqual("307Ki", SizeValue.Parse("314159").ToScaledBinaryString());
        Assert.AreEqual("3.00Mi", SizeValue.Parse("3141592").ToScaledBinaryString());
        Assert.AreEqual("30.0Mi", SizeValue.Parse("31415926").ToScaledBinaryString());
        Assert.AreEqual("300Mi", SizeValue.Parse("314159265").ToScaledBinaryString());
        Assert.AreEqual("2.93Gi", SizeValue.Parse("3141592653").ToScaledBinaryString());
        Assert.AreEqual("29.3Gi", SizeValue.Parse("31415926535").ToScaledBinaryString());
        Assert.AreEqual("293Gi", SizeValue.Parse("314159265358").ToScaledBinaryString());
        Assert.AreEqual("2.86Ti", SizeValue.Parse("3141592653589").ToScaledBinaryString());
        Assert.AreEqual("28.6Ti", SizeValue.Parse("31415926535897").ToScaledBinaryString());
        Assert.AreEqual("286Ti", SizeValue.Parse("314159265358979").ToScaledBinaryString());
        Assert.AreEqual("2.79Pi", SizeValue.Parse("3141592653589793").ToScaledBinaryString());
        Assert.AreEqual("27.9Pi", SizeValue.Parse("31415926535897932").ToScaledBinaryString());
        Assert.AreEqual("279Pi", SizeValue.Parse("314159265358979323").ToScaledBinaryString());
        Assert.AreEqual("2790Pi", SizeValue.Parse("3141592653589793238").ToScaledBinaryString());
    }

    [TestMethod]
    public void SizeValue_ToScaledBinaryString12() {
        Assert.AreEqual("2", SizeValue.Parse("2").ToScaledBinaryString());
        Assert.AreEqual("27", SizeValue.Parse("27").ToScaledBinaryString());
        Assert.AreEqual("271", SizeValue.Parse("271").ToScaledBinaryString());
        Assert.AreEqual("2.65Ki", SizeValue.Parse("2718").ToScaledBinaryString());
        Assert.AreEqual("26.5Ki", SizeValue.Parse("27182").ToScaledBinaryString());
        Assert.AreEqual("265Ki", SizeValue.Parse("271828").ToScaledBinaryString());
        Assert.AreEqual("2.59Mi", SizeValue.Parse("2718281").ToScaledBinaryString());
        Assert.AreEqual("25.9Mi", SizeValue.Parse("27182818").ToScaledBinaryString());
        Assert.AreEqual("259Mi", SizeValue.Parse("271828182").ToScaledBinaryString());
        Assert.AreEqual("2.53Gi", SizeValue.Parse("2718281828").ToScaledBinaryString());
        Assert.AreEqual("25.3Gi", SizeValue.Parse("27182818284").ToScaledBinaryString());
        Assert.AreEqual("253Gi", SizeValue.Parse("271828182845").ToScaledBinaryString());
        Assert.AreEqual("2.47Ti", SizeValue.Parse("2718281828459").ToScaledBinaryString());
        Assert.AreEqual("24.7Ti", SizeValue.Parse("27182818284590").ToScaledBinaryString());
        Assert.AreEqual("247Ti", SizeValue.Parse("271828182845904").ToScaledBinaryString());
        Assert.AreEqual("2.41Pi", SizeValue.Parse("2718281828459045").ToScaledBinaryString());
        Assert.AreEqual("24.1Pi", SizeValue.Parse("27182818284590452").ToScaledBinaryString());
        Assert.AreEqual("241Pi", SizeValue.Parse("271828182845904523").ToScaledBinaryString());
        Assert.AreEqual("2410Pi", SizeValue.Parse("2718281828459045235").ToScaledBinaryString());
    }

}
