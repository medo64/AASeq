using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AASizeValue_Tests {

    [TestMethod]
    public void AASizeValue_Basic() {
        var text = "42";
        Assert.IsTrue(AASizeValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AASizeValue.Parse(text));
        Assert.AreEqual(result, AASizeValue.Parse(text));
        Assert.AreEqual(result, UInt64.Parse(text));
    }

    [TestMethod]
    public void AASizeValue_Base1() {
        Assert.AreEqual("42", AASizeValue.Parse("42"));
    }

    [TestMethod]
    public void AASizeValue_Base2() {
        Assert.AreEqual("43", AASizeValue.Parse("42.84"));
    }

    [TestMethod]
    public void AASizeValue_Base3() {
        Assert.AreEqual("43", AASizeValue.Parse(" 42.84"));
    }


    [TestMethod]
    public void AASizeValue_Kilo1() {
        Assert.AreEqual("42000", AASizeValue.Parse("42K"));
    }

    [TestMethod]
    public void AASizeValue_Kilo2() {
        Assert.AreEqual("42840", AASizeValue.Parse("42.84 k"));
    }

    [TestMethod]
    public void AASizeValue_Kilo3() {
        Assert.AreEqual("1", AASizeValue.Parse(" 0.0001 k "));
    }

    [TestMethod]
    public void AASizeValue_Kibi1() {
        Assert.AreEqual("43008", AASizeValue.Parse("42Ki"));
    }

    [TestMethod]
    public void AASizeValue_Kibi2() {
        Assert.AreEqual("43868", AASizeValue.Parse("42.84ki"));
    }

    [TestMethod]
    public void AASizeValue_Kibi3() {
        Assert.AreEqual("1", AASizeValue.Parse(" 0.000001 ki "));
    }


    [TestMethod]
    public void AASizeValue_Mega1() {
        Assert.AreEqual("42000000", AASizeValue.Parse("42m"));
    }

    [TestMethod]
    public void AASizeValue_Mega2() {
        Assert.AreEqual("42840000", AASizeValue.Parse("42.84 M"));
    }

    [TestMethod]
    public void AASizeValue_Mega3() {
        Assert.AreEqual("1", AASizeValue.Parse(" 0.0000001 M "));
    }

    [TestMethod]
    public void AASizeValue_Mebi1() {
        Assert.AreEqual("44040192", AASizeValue.Parse("42Mi"));
    }

    [TestMethod]
    public void AASizeValue_Mebi2() {
        Assert.AreEqual("44920996", AASizeValue.Parse("42.84Mi"));
    }

    [TestMethod]
    public void AASizeValue_Mebi3() {
        Assert.AreEqual("1", AASizeValue.Parse(" 0.0000001 Mi "));
    }


    [TestMethod]
    public void AASizeValue_Giga1() {
        Assert.AreEqual("42000000000", AASizeValue.Parse("42 G"));
    }

    [TestMethod]
    public void AASizeValue_Giga2() {
        Assert.AreEqual("42840000000", AASizeValue.Parse("42.84g"));
    }

    [TestMethod]
    public void AASizeValue_Giga3() {
        Assert.AreEqual("1", AASizeValue.Parse("  0.0000000001 G "));
    }

    [TestMethod]
    public void AASizeValue_Gibi1() {
        Assert.AreEqual("45097156608", AASizeValue.Parse("42 Gi"));
    }

    [TestMethod]
    public void AASizeValue_Gibi2() {
        Assert.AreEqual("45999099740", AASizeValue.Parse("42.84Gi"));
    }

    [TestMethod]
    public void AASizeValue_Gibi3() {
        Assert.AreEqual("1", AASizeValue.Parse("  0.0000000001 GI "));
    }


    [TestMethod]
    public void AASizeValue_Tera1() {
        Assert.AreEqual("42000000000000", AASizeValue.Parse("42T"));
    }

    [TestMethod]
    public void AASizeValue_Tera2() {
        Assert.AreEqual("42840000000000", AASizeValue.Parse("42.84 T"));
    }

    [TestMethod]
    public void AASizeValue_Tera3() {
        Assert.AreEqual("1", AASizeValue.Parse("  0.0000000000001 T "));
    }

    [TestMethod]
    public void AASizeValue_Tebi1() {
        Assert.AreEqual("46179488366592", AASizeValue.Parse("42Ti"));
    }

    [TestMethod]
    public void AASizeValue_Tebi2() {
        Assert.AreEqual("47103078133924", AASizeValue.Parse("42.84Ti"));
    }

    [TestMethod]
    public void AASizeValue_Tebi3() {
        Assert.AreEqual("1", AASizeValue.Parse("  0.0000000000001 Ti "));
    }


    [TestMethod]
    public void AASizeValue_Peta1() {
        Assert.AreEqual("42000000000000000", AASizeValue.Parse("42p"));
    }

    [TestMethod]
    public void AASizeValue_Peta2() {
        Assert.AreEqual("42840000000000000", AASizeValue.Parse("42.84 P"));
    }

    [TestMethod]
    public void AASizeValue_Peta3() {
        Assert.AreEqual("1", AASizeValue.Parse("  0.0000000000000001 P "));
    }

    [TestMethod]
    public void AASizeValue_Pebi1() {
        Assert.AreEqual("47287796087390208", AASizeValue.Parse("42pi"));
    }

    [TestMethod]
    public void AASizeValue_Pebi2() {
        Assert.AreEqual("48233552009138012", AASizeValue.Parse("42.84PI"));
    }

    [TestMethod]
    public void AASizeValue_Pebi3() {
        Assert.AreEqual("1", AASizeValue.Parse("  0.0000000000000001 PI "));
    }


    [TestMethod]
    public void AASizeValue_FailedParse() {
        Assert.IsFalse(AASizeValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AASizeValue.Parse("A");
        });
    }

    [TestMethod]
    public void AASizeValue_OutOfRange() {
        Assert.IsTrue(AASizeValue.TryParse(((decimal)ulong.MinValue).ToString("0"), out var _));
        Assert.IsFalse(AASizeValue.TryParse(((decimal)ulong.MinValue - 1).ToString("0"), out var _));
        Assert.IsTrue(AASizeValue.TryParse(((decimal)ulong.MaxValue).ToString("0"), out var _));
        Assert.IsFalse(AASizeValue.TryParse(((decimal)ulong.MaxValue + 1).ToString("0"), out var _));
        Assert.IsFalse(AASizeValue.TryParse("42E", out var _));
    }


    [TestMethod]
    public void AASizeValue_ToKiloString() {
        Assert.AreEqual("0.042k", AASizeValue.Parse("42").ToKiloString());
        Assert.AreEqual("0.0420k", AASizeValue.Parse("42").ToKiloString("0.0000"));
        Assert.AreEqual("420k", AASizeValue.Parse("420000").ToKiloString());
        Assert.AreEqual("420.0000k", AASizeValue.Parse("420000").ToKiloString("0.0000"));
        Assert.AreEqual("4200000k", AASizeValue.Parse("4200000000").ToKiloString());
        Assert.AreEqual("4200000.0000k", AASizeValue.Parse("4200000000").ToKiloString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToKibiString() {
        Assert.AreEqual("0.041015625Ki", AASizeValue.Parse("42").ToKibiString());
        Assert.AreEqual("0.0410Ki", AASizeValue.Parse("42").ToKibiString("0.0000"));
        Assert.AreEqual("410.15625Ki", AASizeValue.Parse("420000").ToKibiString());
        Assert.AreEqual("410.1563Ki", AASizeValue.Parse("420000").ToKibiString("0.0000"));
        Assert.AreEqual("4101562.5Ki", AASizeValue.Parse("4200000000").ToKibiString());
        Assert.AreEqual("4101562.5000Ki", AASizeValue.Parse("4200000000").ToKibiString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToMegaString() {
        Assert.AreEqual("0.42M", AASizeValue.Parse("420000").ToMegaString());
        Assert.AreEqual("0.4200M", AASizeValue.Parse("420000").ToMegaString("0.0000"));
        Assert.AreEqual("4200M", AASizeValue.Parse("4200000000").ToMegaString());
        Assert.AreEqual("4200.0000M", AASizeValue.Parse("4200000000").ToMegaString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToMebiString() {
        Assert.AreEqual("0.400543212890625Mi", AASizeValue.Parse("420000").ToMebiString());
        Assert.AreEqual("0.4005Mi", AASizeValue.Parse("420000").ToMebiString("0.0000"));
        Assert.AreEqual("4005.43212890625Mi", AASizeValue.Parse("4200000000").ToMebiString());
        Assert.AreEqual("4005.4321Mi", AASizeValue.Parse("4200000000").ToMebiString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToGigaString() {
        Assert.AreEqual("0.00042G", AASizeValue.Parse("420000").ToGigaString());
        Assert.AreEqual("0.0004G", AASizeValue.Parse("420000").ToGigaString("0.0000"));
        Assert.AreEqual("4.2G", AASizeValue.Parse("4200000000").ToGigaString());
        Assert.AreEqual("4.2000G", AASizeValue.Parse("4200000000").ToGigaString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToGibiString() {
        Assert.AreEqual("0.000391155481338501Gi", AASizeValue.Parse("420000").ToGibiString());
        Assert.AreEqual("0.0004Gi", AASizeValue.Parse("420000").ToGibiString("0.0000"));
        Assert.AreEqual("3.9115548133850098Gi", AASizeValue.Parse("4200000000").ToGibiString());
        Assert.AreEqual("3.9116Gi", AASizeValue.Parse("4200000000").ToGibiString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToTeraString() {
        Assert.AreEqual("0.00044T", AASizeValue.Parse("440000000").ToTeraString());
        Assert.AreEqual("0.0004T", AASizeValue.Parse("440000000").ToTeraString("0.0000"));
        Assert.AreEqual("4.4T", AASizeValue.Parse("4400000000000").ToTeraString());
        Assert.AreEqual("4.4000T", AASizeValue.Parse("4400000000000").ToTeraString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToTebiString() {
        Assert.AreEqual("0.0004001776687800884Ti", AASizeValue.Parse("440000000").ToTebiString());
        Assert.AreEqual("0.0004Ti", AASizeValue.Parse("440000000").ToTebiString("0.0000"));
        Assert.AreEqual("4.001776687800884Ti", AASizeValue.Parse("4400000000000").ToTebiString());
        Assert.AreEqual("4.0018Ti", AASizeValue.Parse("4400000000000").ToTebiString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToPetaString() {
        Assert.AreEqual("0.00046P", AASizeValue.Parse("460000000000").ToPetaString());
        Assert.AreEqual("0.0005P", AASizeValue.Parse("460000000000").ToPetaString("0.0000"));
        Assert.AreEqual("4.6P", AASizeValue.Parse("4600000000000000").ToPetaString());
        Assert.AreEqual("4.6000P", AASizeValue.Parse("4600000000000000").ToPetaString("0.0000"));
    }

    [TestMethod]
    public void AASizeValue_ToPebiString() {
        Assert.AreEqual("0.0004085620730620576Pi", AASizeValue.Parse("460000000000").ToPebiString());
        Assert.AreEqual("0.0004Pi", AASizeValue.Parse("460000000000").ToPebiString("0.0000"));
        Assert.AreEqual("4.085620730620576Pi", AASizeValue.Parse("4600000000000000").ToPebiString());
        Assert.AreEqual("4.0856Pi", AASizeValue.Parse("4600000000000000").ToPebiString("0.0000"));
    }


    [TestMethod]
    public void AASizeValue_ToScaledSIString() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString());
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledSIString());
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledSIString());
        Assert.AreEqual("1.23k", AASizeValue.Parse("1234").ToScaledSIString());
        Assert.AreEqual("12.3k", AASizeValue.Parse("12345").ToScaledSIString());
        Assert.AreEqual("123k", AASizeValue.Parse("123456").ToScaledSIString());
        Assert.AreEqual("1.23M", AASizeValue.Parse("1234567").ToScaledSIString());
        Assert.AreEqual("12.3M", AASizeValue.Parse("12345678").ToScaledSIString());
        Assert.AreEqual("123M", AASizeValue.Parse("123456789").ToScaledSIString());
        Assert.AreEqual("1.23G", AASizeValue.Parse("1234567890").ToScaledSIString());
        Assert.AreEqual("12.3G", AASizeValue.Parse("12345678901").ToScaledSIString());
        Assert.AreEqual("123G", AASizeValue.Parse("123456789012").ToScaledSIString());
        Assert.AreEqual("1.23T", AASizeValue.Parse("1234567890123").ToScaledSIString());
        Assert.AreEqual("12.3T", AASizeValue.Parse("12345678901234").ToScaledSIString());
        Assert.AreEqual("123T", AASizeValue.Parse("123456789012345").ToScaledSIString());
        Assert.AreEqual("1.23P", AASizeValue.Parse("1234567890123456").ToScaledSIString());
        Assert.AreEqual("12.3P", AASizeValue.Parse("12345678901234567").ToScaledSIString());
        Assert.AreEqual("123P", AASizeValue.Parse("123456789012345678").ToScaledSIString());
        Assert.AreEqual("1230P", AASizeValue.Parse("1234567890123456789").ToScaledSIString());
        Assert.AreEqual("12300P", AASizeValue.Parse("12345678901234567890").ToScaledSIString());
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString1() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString(1));
        Assert.AreEqual("10", AASizeValue.Parse("12").ToScaledSIString(1));
        Assert.AreEqual("100", AASizeValue.Parse("123").ToScaledSIString(1));
        Assert.AreEqual("1k", AASizeValue.Parse("1234").ToScaledSIString(1));
        Assert.AreEqual("10k", AASizeValue.Parse("12345").ToScaledSIString(1));
        Assert.AreEqual("100k", AASizeValue.Parse("123456").ToScaledSIString(1));
        Assert.AreEqual("1M", AASizeValue.Parse("1234567").ToScaledSIString(1));
        Assert.AreEqual("10M", AASizeValue.Parse("12345678").ToScaledSIString(1));
        Assert.AreEqual("100M", AASizeValue.Parse("123456789").ToScaledSIString(1));
        Assert.AreEqual("1G", AASizeValue.Parse("1234567890").ToScaledSIString(1));
        Assert.AreEqual("10G", AASizeValue.Parse("12345678901").ToScaledSIString(1));
        Assert.AreEqual("100G", AASizeValue.Parse("123456789012").ToScaledSIString(1));
        Assert.AreEqual("1T", AASizeValue.Parse("1234567890123").ToScaledSIString(1));
        Assert.AreEqual("10T", AASizeValue.Parse("12345678901234").ToScaledSIString(1));
        Assert.AreEqual("100T", AASizeValue.Parse("123456789012345").ToScaledSIString(1));
        Assert.AreEqual("1P", AASizeValue.Parse("1234567890123456").ToScaledSIString(1));
        Assert.AreEqual("10P", AASizeValue.Parse("12345678901234567").ToScaledSIString(1));
        Assert.AreEqual("100P", AASizeValue.Parse("123456789012345678").ToScaledSIString(1));
        Assert.AreEqual("1000P", AASizeValue.Parse("1234567890123456789").ToScaledSIString(1));
        Assert.AreEqual("10000P", AASizeValue.Parse("12345678901234567890").ToScaledSIString(1));
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString3() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString(3));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledSIString(3));
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledSIString(3));
        Assert.AreEqual("1.23k", AASizeValue.Parse("1234").ToScaledSIString(3));
        Assert.AreEqual("12.3k", AASizeValue.Parse("12345").ToScaledSIString(3));
        Assert.AreEqual("123k", AASizeValue.Parse("123456").ToScaledSIString(3));
        Assert.AreEqual("1.23M", AASizeValue.Parse("1234567").ToScaledSIString(3));
        Assert.AreEqual("12.3M", AASizeValue.Parse("12345678").ToScaledSIString(3));
        Assert.AreEqual("123M", AASizeValue.Parse("123456789").ToScaledSIString(3));
        Assert.AreEqual("1.23G", AASizeValue.Parse("1234567890").ToScaledSIString(3));
        Assert.AreEqual("12.3G", AASizeValue.Parse("12345678901").ToScaledSIString(3));
        Assert.AreEqual("123G", AASizeValue.Parse("123456789012").ToScaledSIString(3));
        Assert.AreEqual("1.23T", AASizeValue.Parse("1234567890123").ToScaledSIString(3));
        Assert.AreEqual("12.3T", AASizeValue.Parse("12345678901234").ToScaledSIString(3));
        Assert.AreEqual("123T", AASizeValue.Parse("123456789012345").ToScaledSIString(3));
        Assert.AreEqual("1.23P", AASizeValue.Parse("1234567890123456").ToScaledSIString(3));
        Assert.AreEqual("12.3P", AASizeValue.Parse("12345678901234567").ToScaledSIString(3));
        Assert.AreEqual("123P", AASizeValue.Parse("123456789012345678").ToScaledSIString(3));
        Assert.AreEqual("1230P", AASizeValue.Parse("1234567890123456789").ToScaledSIString(3));
        Assert.AreEqual("12300P", AASizeValue.Parse("12345678901234567890").ToScaledSIString(3));
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString4() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString(4));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledSIString(4));
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledSIString(4));
        Assert.AreEqual("1.234k", AASizeValue.Parse("1234").ToScaledSIString(4));
        Assert.AreEqual("12.35k", AASizeValue.Parse("12345").ToScaledSIString(4));
        Assert.AreEqual("123.5k", AASizeValue.Parse("123456").ToScaledSIString(4));
        Assert.AreEqual("1.235M", AASizeValue.Parse("1234567").ToScaledSIString(4));
        Assert.AreEqual("12.35M", AASizeValue.Parse("12345678").ToScaledSIString(4));
        Assert.AreEqual("123.5M", AASizeValue.Parse("123456789").ToScaledSIString(4));
        Assert.AreEqual("1.235G", AASizeValue.Parse("1234567890").ToScaledSIString(4));
        Assert.AreEqual("12.35G", AASizeValue.Parse("12345678901").ToScaledSIString(4));
        Assert.AreEqual("123.5G", AASizeValue.Parse("123456789012").ToScaledSIString(4));
        Assert.AreEqual("1.235T", AASizeValue.Parse("1234567890123").ToScaledSIString(4));
        Assert.AreEqual("12.35T", AASizeValue.Parse("12345678901234").ToScaledSIString(4));
        Assert.AreEqual("123.5T", AASizeValue.Parse("123456789012345").ToScaledSIString(4));
        Assert.AreEqual("1.235P", AASizeValue.Parse("1234567890123456").ToScaledSIString(4));
        Assert.AreEqual("12.35P", AASizeValue.Parse("12345678901234567").ToScaledSIString(4));
        Assert.AreEqual("123.5P", AASizeValue.Parse("123456789012345678").ToScaledSIString(4));
        Assert.AreEqual("1235P", AASizeValue.Parse("1234567890123456789").ToScaledSIString(4));
        Assert.AreEqual("12350P", AASizeValue.Parse("12345678901234567890").ToScaledSIString(4));
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString5() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString(5));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledSIString(5));
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledSIString(5));
        Assert.AreEqual("1.234k", AASizeValue.Parse("1234").ToScaledSIString(5));
        Assert.AreEqual("12.345k", AASizeValue.Parse("12345").ToScaledSIString(5));
        Assert.AreEqual("123.46k", AASizeValue.Parse("123456").ToScaledSIString(5));
        Assert.AreEqual("1.2346M", AASizeValue.Parse("1234567").ToScaledSIString(5));
        Assert.AreEqual("12.346M", AASizeValue.Parse("12345678").ToScaledSIString(5));
        Assert.AreEqual("123.46M", AASizeValue.Parse("123456789").ToScaledSIString(5));
        Assert.AreEqual("1.2346G", AASizeValue.Parse("1234567890").ToScaledSIString(5));
        Assert.AreEqual("12.346G", AASizeValue.Parse("12345678901").ToScaledSIString(5));
        Assert.AreEqual("123.46G", AASizeValue.Parse("123456789012").ToScaledSIString(5));
        Assert.AreEqual("1.2346T", AASizeValue.Parse("1234567890123").ToScaledSIString(5));
        Assert.AreEqual("12.346T", AASizeValue.Parse("12345678901234").ToScaledSIString(5));
        Assert.AreEqual("123.46T", AASizeValue.Parse("123456789012345").ToScaledSIString(5));
        Assert.AreEqual("1.2346P", AASizeValue.Parse("1234567890123456").ToScaledSIString(5));
        Assert.AreEqual("12.346P", AASizeValue.Parse("12345678901234567").ToScaledSIString(5));
        Assert.AreEqual("123.46P", AASizeValue.Parse("123456789012345678").ToScaledSIString(5));
        Assert.AreEqual("1234.6P", AASizeValue.Parse("1234567890123456789").ToScaledSIString(5));
        Assert.AreEqual("12346P", AASizeValue.Parse("12345678901234567890").ToScaledSIString(5));
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString6() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString(6));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledSIString(6));
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledSIString(6));
        Assert.AreEqual("1.234k", AASizeValue.Parse("1234").ToScaledSIString(6));
        Assert.AreEqual("12.345k", AASizeValue.Parse("12345").ToScaledSIString(6));
        Assert.AreEqual("123.456k", AASizeValue.Parse("123456").ToScaledSIString(6));
        Assert.AreEqual("1.23457M", AASizeValue.Parse("1234567").ToScaledSIString(6));
        Assert.AreEqual("12.3457M", AASizeValue.Parse("12345678").ToScaledSIString(6));
        Assert.AreEqual("123.457M", AASizeValue.Parse("123456789").ToScaledSIString(6));
        Assert.AreEqual("1.23457G", AASizeValue.Parse("1234567890").ToScaledSIString(6));
        Assert.AreEqual("12.3457G", AASizeValue.Parse("12345678901").ToScaledSIString(6));
        Assert.AreEqual("123.457G", AASizeValue.Parse("123456789012").ToScaledSIString(6));
        Assert.AreEqual("1.23457T", AASizeValue.Parse("1234567890123").ToScaledSIString(6));
        Assert.AreEqual("12.3457T", AASizeValue.Parse("12345678901234").ToScaledSIString(6));
        Assert.AreEqual("123.457T", AASizeValue.Parse("123456789012345").ToScaledSIString(6));
        Assert.AreEqual("1.23457P", AASizeValue.Parse("1234567890123456").ToScaledSIString(6));
        Assert.AreEqual("12.3457P", AASizeValue.Parse("12345678901234567").ToScaledSIString(6));
        Assert.AreEqual("123.457P", AASizeValue.Parse("123456789012345678").ToScaledSIString(6));
        Assert.AreEqual("1234.57P", AASizeValue.Parse("1234567890123456789").ToScaledSIString(6));
        Assert.AreEqual("12345.7P", AASizeValue.Parse("12345678901234567890").ToScaledSIString(6));
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString7() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString(7));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledSIString(7));
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledSIString(7));
        Assert.AreEqual("1.234k", AASizeValue.Parse("1234").ToScaledSIString(7));
        Assert.AreEqual("12.345k", AASizeValue.Parse("12345").ToScaledSIString(7));
        Assert.AreEqual("123.456k", AASizeValue.Parse("123456").ToScaledSIString(7));
        Assert.AreEqual("1.234567M", AASizeValue.Parse("1234567").ToScaledSIString(7));
        Assert.AreEqual("12.34568M", AASizeValue.Parse("12345678").ToScaledSIString(7));
        Assert.AreEqual("123.4568M", AASizeValue.Parse("123456789").ToScaledSIString(7));
        Assert.AreEqual("1.234568G", AASizeValue.Parse("1234567890").ToScaledSIString(7));
        Assert.AreEqual("12.34568G", AASizeValue.Parse("12345678901").ToScaledSIString(7));
        Assert.AreEqual("123.4568G", AASizeValue.Parse("123456789012").ToScaledSIString(7));
        Assert.AreEqual("1.234568T", AASizeValue.Parse("1234567890123").ToScaledSIString(7));
        Assert.AreEqual("12.34568T", AASizeValue.Parse("12345678901234").ToScaledSIString(7));
        Assert.AreEqual("123.4568T", AASizeValue.Parse("123456789012345").ToScaledSIString(7));
        Assert.AreEqual("1.234568P", AASizeValue.Parse("1234567890123456").ToScaledSIString(7));
        Assert.AreEqual("12.34568P", AASizeValue.Parse("12345678901234567").ToScaledSIString(7));
        Assert.AreEqual("123.4568P", AASizeValue.Parse("123456789012345678").ToScaledSIString(7));
        Assert.AreEqual("1234.568P", AASizeValue.Parse("1234567890123456789").ToScaledSIString(7));
        Assert.AreEqual("12345.68P", AASizeValue.Parse("12345678901234567890").ToScaledSIString(7));
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString8() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString(8));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledSIString(8));
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledSIString(8));
        Assert.AreEqual("1.234k", AASizeValue.Parse("1234").ToScaledSIString(8));
        Assert.AreEqual("12.345k", AASizeValue.Parse("12345").ToScaledSIString(8));
        Assert.AreEqual("123.456k", AASizeValue.Parse("123456").ToScaledSIString(8));
        Assert.AreEqual("1.234567M", AASizeValue.Parse("1234567").ToScaledSIString(8));
        Assert.AreEqual("12.345678M", AASizeValue.Parse("12345678").ToScaledSIString(8));
        Assert.AreEqual("123.45679M", AASizeValue.Parse("123456789").ToScaledSIString(8));
        Assert.AreEqual("1.2345679G", AASizeValue.Parse("1234567890").ToScaledSIString(8));
        Assert.AreEqual("12.345679G", AASizeValue.Parse("12345678901").ToScaledSIString(8));
        Assert.AreEqual("123.45679G", AASizeValue.Parse("123456789012").ToScaledSIString(8));
        Assert.AreEqual("1.2345679T", AASizeValue.Parse("1234567890123").ToScaledSIString(8));
        Assert.AreEqual("12.345679T", AASizeValue.Parse("12345678901234").ToScaledSIString(8));
        Assert.AreEqual("123.45679T", AASizeValue.Parse("123456789012345").ToScaledSIString(8));
        Assert.AreEqual("1.2345679P", AASizeValue.Parse("1234567890123456").ToScaledSIString(8));
        Assert.AreEqual("12.345679P", AASizeValue.Parse("12345678901234567").ToScaledSIString(8));
        Assert.AreEqual("123.45679P", AASizeValue.Parse("123456789012345678").ToScaledSIString(8));
        Assert.AreEqual("1234.5679P", AASizeValue.Parse("1234567890123456789").ToScaledSIString(8));
        Assert.AreEqual("12345.679P", AASizeValue.Parse("12345678901234567890").ToScaledSIString(8));
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString9() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString(9));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledSIString(9));
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledSIString(9));
        Assert.AreEqual("1.234k", AASizeValue.Parse("1234").ToScaledSIString(9));
        Assert.AreEqual("12.345k", AASizeValue.Parse("12345").ToScaledSIString(9));
        Assert.AreEqual("123.456k", AASizeValue.Parse("123456").ToScaledSIString(9));
        Assert.AreEqual("1.234567M", AASizeValue.Parse("1234567").ToScaledSIString(9));
        Assert.AreEqual("12.345678M", AASizeValue.Parse("12345678").ToScaledSIString(9));
        Assert.AreEqual("123.456789M", AASizeValue.Parse("123456789").ToScaledSIString(9));
        Assert.AreEqual("1.23456789G", AASizeValue.Parse("1234567890").ToScaledSIString(9));
        Assert.AreEqual("12.3456789G", AASizeValue.Parse("12345678901").ToScaledSIString(9));
        Assert.AreEqual("123.456789G", AASizeValue.Parse("123456789012").ToScaledSIString(9));
        Assert.AreEqual("1.23456789T", AASizeValue.Parse("1234567890123").ToScaledSIString(9));
        Assert.AreEqual("12.3456789T", AASizeValue.Parse("12345678901234").ToScaledSIString(9));
        Assert.AreEqual("123.456789T", AASizeValue.Parse("123456789012345").ToScaledSIString(9));
        Assert.AreEqual("1.23456789P", AASizeValue.Parse("1234567890123456").ToScaledSIString(9));
        Assert.AreEqual("12.3456789P", AASizeValue.Parse("12345678901234567").ToScaledSIString(9));
        Assert.AreEqual("123.456789P", AASizeValue.Parse("123456789012345678").ToScaledSIString(9));
        Assert.AreEqual("1234.56789P", AASizeValue.Parse("1234567890123456789").ToScaledSIString(9));
        Assert.AreEqual("12345.6789P", AASizeValue.Parse("12345678901234567890").ToScaledSIString(9));
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString10() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledSIString());
        Assert.AreEqual("10", AASizeValue.Parse("10").ToScaledSIString());
        Assert.AreEqual("102", AASizeValue.Parse("102").ToScaledSIString());
        Assert.AreEqual("1.02k", AASizeValue.Parse("1023").ToScaledSIString());
        Assert.AreEqual("10.2k", AASizeValue.Parse("10234").ToScaledSIString());
        Assert.AreEqual("102k", AASizeValue.Parse("102345").ToScaledSIString());
        Assert.AreEqual("1.02M", AASizeValue.Parse("1023456").ToScaledSIString());
        Assert.AreEqual("10.2M", AASizeValue.Parse("10234567").ToScaledSIString());
        Assert.AreEqual("102M", AASizeValue.Parse("102345678").ToScaledSIString());
        Assert.AreEqual("1.02G", AASizeValue.Parse("1023456789").ToScaledSIString());
        Assert.AreEqual("10.2G", AASizeValue.Parse("10234567890").ToScaledSIString());
        Assert.AreEqual("102G", AASizeValue.Parse("102345678901").ToScaledSIString());
        Assert.AreEqual("1.02T", AASizeValue.Parse("1023456789012").ToScaledSIString());
        Assert.AreEqual("10.2T", AASizeValue.Parse("10234567890123").ToScaledSIString());
        Assert.AreEqual("102T", AASizeValue.Parse("102345678901234").ToScaledSIString());
        Assert.AreEqual("1.02P", AASizeValue.Parse("1023456789012345").ToScaledSIString());
        Assert.AreEqual("10.2P", AASizeValue.Parse("10234567890123456").ToScaledSIString());
        Assert.AreEqual("102P", AASizeValue.Parse("102345678901234567").ToScaledSIString());
        Assert.AreEqual("1020P", AASizeValue.Parse("1023456789012345678").ToScaledSIString());
        Assert.AreEqual("10200P", AASizeValue.Parse("10234567890123456789").ToScaledSIString());
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString11() {
        Assert.AreEqual("3", AASizeValue.Parse("3").ToScaledSIString());
        Assert.AreEqual("31", AASizeValue.Parse("31").ToScaledSIString());
        Assert.AreEqual("314", AASizeValue.Parse("314").ToScaledSIString());
        Assert.AreEqual("3.14k", AASizeValue.Parse("3141").ToScaledSIString());
        Assert.AreEqual("31.4k", AASizeValue.Parse("31415").ToScaledSIString());
        Assert.AreEqual("314k", AASizeValue.Parse("314159").ToScaledSIString());
        Assert.AreEqual("3.14M", AASizeValue.Parse("3141592").ToScaledSIString());
        Assert.AreEqual("31.4M", AASizeValue.Parse("31415926").ToScaledSIString());
        Assert.AreEqual("314M", AASizeValue.Parse("314159265").ToScaledSIString());
        Assert.AreEqual("3.14G", AASizeValue.Parse("3141592653").ToScaledSIString());
        Assert.AreEqual("31.4G", AASizeValue.Parse("31415926535").ToScaledSIString());
        Assert.AreEqual("314G", AASizeValue.Parse("314159265358").ToScaledSIString());
        Assert.AreEqual("3.14T", AASizeValue.Parse("3141592653589").ToScaledSIString());
        Assert.AreEqual("31.4T", AASizeValue.Parse("31415926535897").ToScaledSIString());
        Assert.AreEqual("314T", AASizeValue.Parse("314159265358979").ToScaledSIString());
        Assert.AreEqual("3.14P", AASizeValue.Parse("3141592653589793").ToScaledSIString());
        Assert.AreEqual("31.4P", AASizeValue.Parse("31415926535897932").ToScaledSIString());
        Assert.AreEqual("314P", AASizeValue.Parse("314159265358979323").ToScaledSIString());
        Assert.AreEqual("3140P", AASizeValue.Parse("3141592653589793238").ToScaledSIString());
    }

    [TestMethod]
    public void AASizeValue_ToScaledSIString12() {
        Assert.AreEqual("2", AASizeValue.Parse("2").ToScaledSIString());
        Assert.AreEqual("27", AASizeValue.Parse("27").ToScaledSIString());
        Assert.AreEqual("271", AASizeValue.Parse("271").ToScaledSIString());
        Assert.AreEqual("2.72k", AASizeValue.Parse("2718").ToScaledSIString());
        Assert.AreEqual("27.2k", AASizeValue.Parse("27182").ToScaledSIString());
        Assert.AreEqual("272k", AASizeValue.Parse("271828").ToScaledSIString());
        Assert.AreEqual("2.72M", AASizeValue.Parse("2718281").ToScaledSIString());
        Assert.AreEqual("27.2M", AASizeValue.Parse("27182818").ToScaledSIString());
        Assert.AreEqual("272M", AASizeValue.Parse("271828182").ToScaledSIString());
        Assert.AreEqual("2.72G", AASizeValue.Parse("2718281828").ToScaledSIString());
        Assert.AreEqual("27.2G", AASizeValue.Parse("27182818284").ToScaledSIString());
        Assert.AreEqual("272G", AASizeValue.Parse("271828182845").ToScaledSIString());
        Assert.AreEqual("2.72T", AASizeValue.Parse("2718281828459").ToScaledSIString());
        Assert.AreEqual("27.2T", AASizeValue.Parse("27182818284590").ToScaledSIString());
        Assert.AreEqual("272T", AASizeValue.Parse("271828182845904").ToScaledSIString());
        Assert.AreEqual("2.72P", AASizeValue.Parse("2718281828459045").ToScaledSIString());
        Assert.AreEqual("27.2P", AASizeValue.Parse("27182818284590452").ToScaledSIString());
        Assert.AreEqual("272P", AASizeValue.Parse("271828182845904523").ToScaledSIString());
        Assert.AreEqual("2720P", AASizeValue.Parse("2718281828459045235").ToScaledSIString());
    }


    [TestMethod]
    public void AASizeValue_ToScaledBinaryString() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledBinaryString());
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledBinaryString());
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledBinaryString());
        Assert.AreEqual("1.21Ki", AASizeValue.Parse("1234").ToScaledBinaryString());
        Assert.AreEqual("12.1Ki", AASizeValue.Parse("12345").ToScaledBinaryString());
        Assert.AreEqual("121Ki", AASizeValue.Parse("123456").ToScaledBinaryString());
        Assert.AreEqual("1.18Mi", AASizeValue.Parse("1234567").ToScaledBinaryString());
        Assert.AreEqual("11.8Mi", AASizeValue.Parse("12345678").ToScaledBinaryString());
        Assert.AreEqual("118Mi", AASizeValue.Parse("123456789").ToScaledBinaryString());
        Assert.AreEqual("1.15Gi", AASizeValue.Parse("1234567890").ToScaledBinaryString());
        Assert.AreEqual("11.5Gi", AASizeValue.Parse("12345678901").ToScaledBinaryString());
        Assert.AreEqual("115Gi", AASizeValue.Parse("123456789012").ToScaledBinaryString());
        Assert.AreEqual("1.12Ti", AASizeValue.Parse("1234567890123").ToScaledBinaryString());
        Assert.AreEqual("11.2Ti", AASizeValue.Parse("12345678901234").ToScaledBinaryString());
        Assert.AreEqual("112Ti", AASizeValue.Parse("123456789012345").ToScaledBinaryString());
        Assert.AreEqual("1.10Pi", AASizeValue.Parse("1234567890123456").ToScaledBinaryString());
        Assert.AreEqual("11.0Pi", AASizeValue.Parse("12345678901234567").ToScaledBinaryString());
        Assert.AreEqual("110Pi", AASizeValue.Parse("123456789012345678").ToScaledBinaryString());
        Assert.AreEqual("1100Pi", AASizeValue.Parse("1234567890123456789").ToScaledBinaryString());
        Assert.AreEqual("11000Pi", AASizeValue.Parse("12345678901234567890").ToScaledBinaryString());
    }

    [TestMethod]
    public void AASizeValue_ToScaledBinaryString1() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledBinaryString(1));
        Assert.AreEqual("10", AASizeValue.Parse("12").ToScaledBinaryString(1));
        Assert.AreEqual("100", AASizeValue.Parse("123").ToScaledBinaryString(1));
        Assert.AreEqual("1Ki", AASizeValue.Parse("1234").ToScaledBinaryString(1));
        Assert.AreEqual("10Ki", AASizeValue.Parse("12345").ToScaledBinaryString(1));
        Assert.AreEqual("100Ki", AASizeValue.Parse("123456").ToScaledBinaryString(1));
        Assert.AreEqual("1Mi", AASizeValue.Parse("1234567").ToScaledBinaryString(1));
        Assert.AreEqual("10Mi", AASizeValue.Parse("12345678").ToScaledBinaryString(1));
        Assert.AreEqual("100Mi", AASizeValue.Parse("123456789").ToScaledBinaryString(1));
        Assert.AreEqual("1Gi", AASizeValue.Parse("1234567890").ToScaledBinaryString(1));
        Assert.AreEqual("10Gi", AASizeValue.Parse("12345678901").ToScaledBinaryString(1));
        Assert.AreEqual("100Gi", AASizeValue.Parse("123456789012").ToScaledBinaryString(1));
        Assert.AreEqual("1Ti", AASizeValue.Parse("1234567890123").ToScaledBinaryString(1));
        Assert.AreEqual("10Ti", AASizeValue.Parse("12345678901234").ToScaledBinaryString(1));
        Assert.AreEqual("100Ti", AASizeValue.Parse("123456789012345").ToScaledBinaryString(1));
        Assert.AreEqual("1Pi", AASizeValue.Parse("1234567890123456").ToScaledBinaryString(1));
        Assert.AreEqual("10Pi", AASizeValue.Parse("12345678901234567").ToScaledBinaryString(1));
        Assert.AreEqual("100Pi", AASizeValue.Parse("123456789012345678").ToScaledBinaryString(1));
        Assert.AreEqual("1000Pi", AASizeValue.Parse("1234567890123456789").ToScaledBinaryString(1));
        Assert.AreEqual("10000Pi", AASizeValue.Parse("12345678901234567890").ToScaledBinaryString(1));
    }

    [TestMethod]
    public void AASizeValue_ToScaledBinaryString2() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledBinaryString(2));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledBinaryString(2));
        Assert.AreEqual("120", AASizeValue.Parse("123").ToScaledBinaryString(2));
        Assert.AreEqual("1.2Ki", AASizeValue.Parse("1234").ToScaledBinaryString(2));
        Assert.AreEqual("12Ki", AASizeValue.Parse("12345").ToScaledBinaryString(2));
        Assert.AreEqual("120Ki", AASizeValue.Parse("123456").ToScaledBinaryString(2));
        Assert.AreEqual("1.2Mi", AASizeValue.Parse("1234567").ToScaledBinaryString(2));
        Assert.AreEqual("12Mi", AASizeValue.Parse("12345678").ToScaledBinaryString(2));
        Assert.AreEqual("120Mi", AASizeValue.Parse("123456789").ToScaledBinaryString(2));
        Assert.AreEqual("1.1Gi", AASizeValue.Parse("1234567890").ToScaledBinaryString(2));
        Assert.AreEqual("11Gi", AASizeValue.Parse("12345678901").ToScaledBinaryString(2));
        Assert.AreEqual("110Gi", AASizeValue.Parse("123456789012").ToScaledBinaryString(2));
        Assert.AreEqual("1.1Ti", AASizeValue.Parse("1234567890123").ToScaledBinaryString(2));
        Assert.AreEqual("11Ti", AASizeValue.Parse("12345678901234").ToScaledBinaryString(2));
        Assert.AreEqual("110Ti", AASizeValue.Parse("123456789012345").ToScaledBinaryString(2));
        Assert.AreEqual("1.1Pi", AASizeValue.Parse("1234567890123456").ToScaledBinaryString(2));
        Assert.AreEqual("11Pi", AASizeValue.Parse("12345678901234567").ToScaledBinaryString(2));
        Assert.AreEqual("110Pi", AASizeValue.Parse("123456789012345678").ToScaledBinaryString(2));
        Assert.AreEqual("1100Pi", AASizeValue.Parse("1234567890123456789").ToScaledBinaryString(2));
        Assert.AreEqual("11000Pi", AASizeValue.Parse("12345678901234567890").ToScaledBinaryString(2));
    }

    [TestMethod]
    public void AASizeValue_ToScaledBinaryString3() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledBinaryString(3));
        Assert.AreEqual("12", AASizeValue.Parse("12").ToScaledBinaryString(3));
        Assert.AreEqual("123", AASizeValue.Parse("123").ToScaledBinaryString(3));
        Assert.AreEqual("1.21Ki", AASizeValue.Parse("1234").ToScaledBinaryString(3));
        Assert.AreEqual("12.1Ki", AASizeValue.Parse("12345").ToScaledBinaryString(3));
        Assert.AreEqual("121Ki", AASizeValue.Parse("123456").ToScaledBinaryString(3));
        Assert.AreEqual("1.18Mi", AASizeValue.Parse("1234567").ToScaledBinaryString(3));
        Assert.AreEqual("11.8Mi", AASizeValue.Parse("12345678").ToScaledBinaryString(3));
        Assert.AreEqual("118Mi", AASizeValue.Parse("123456789").ToScaledBinaryString(3));
        Assert.AreEqual("1.15Gi", AASizeValue.Parse("1234567890").ToScaledBinaryString(3));
        Assert.AreEqual("11.5Gi", AASizeValue.Parse("12345678901").ToScaledBinaryString(3));
        Assert.AreEqual("115Gi", AASizeValue.Parse("123456789012").ToScaledBinaryString(3));
        Assert.AreEqual("1.12Ti", AASizeValue.Parse("1234567890123").ToScaledBinaryString(3));
        Assert.AreEqual("11.2Ti", AASizeValue.Parse("12345678901234").ToScaledBinaryString(3));
        Assert.AreEqual("112Ti", AASizeValue.Parse("123456789012345").ToScaledBinaryString(3));
        Assert.AreEqual("1.10Pi", AASizeValue.Parse("1234567890123456").ToScaledBinaryString(3));
        Assert.AreEqual("11.0Pi", AASizeValue.Parse("12345678901234567").ToScaledBinaryString(3));
        Assert.AreEqual("110Pi", AASizeValue.Parse("123456789012345678").ToScaledBinaryString(3));
        Assert.AreEqual("1100Pi", AASizeValue.Parse("1234567890123456789").ToScaledBinaryString(3));
        Assert.AreEqual("11000Pi", AASizeValue.Parse("12345678901234567890").ToScaledBinaryString(3));
    }

    [TestMethod]
    public void AASizeValue_ToScaledBinaryString10() {
        Assert.AreEqual("1", AASizeValue.Parse("1").ToScaledBinaryString());
        Assert.AreEqual("10", AASizeValue.Parse("10").ToScaledBinaryString());
        Assert.AreEqual("102", AASizeValue.Parse("102").ToScaledBinaryString());
        Assert.AreEqual("1020", AASizeValue.Parse("1023").ToScaledBinaryString());
        Assert.AreEqual("9.99Ki", AASizeValue.Parse("10234").ToScaledBinaryString());
        Assert.AreEqual("99.9Ki", AASizeValue.Parse("102345").ToScaledBinaryString());
        Assert.AreEqual("999Ki", AASizeValue.Parse("1023456").ToScaledBinaryString());
        Assert.AreEqual("9.76Mi", AASizeValue.Parse("10234567").ToScaledBinaryString());
        Assert.AreEqual("97.6Mi", AASizeValue.Parse("102345678").ToScaledBinaryString());
        Assert.AreEqual("976Mi", AASizeValue.Parse("1023456789").ToScaledBinaryString());
        Assert.AreEqual("9.53Gi", AASizeValue.Parse("10234567890").ToScaledBinaryString());
        Assert.AreEqual("95.3Gi", AASizeValue.Parse("102345678901").ToScaledBinaryString());
        Assert.AreEqual("953Gi", AASizeValue.Parse("1023456789012").ToScaledBinaryString());
        Assert.AreEqual("9.31Ti", AASizeValue.Parse("10234567890123").ToScaledBinaryString());
        Assert.AreEqual("93.1Ti", AASizeValue.Parse("102345678901234").ToScaledBinaryString());
        Assert.AreEqual("931Ti", AASizeValue.Parse("1023456789012345").ToScaledBinaryString());
        Assert.AreEqual("9.09Pi", AASizeValue.Parse("10234567890123456").ToScaledBinaryString());
        Assert.AreEqual("90.9Pi", AASizeValue.Parse("102345678901234567").ToScaledBinaryString());
        Assert.AreEqual("909Pi", AASizeValue.Parse("1023456789012345678").ToScaledBinaryString());
        Assert.AreEqual("9090Pi", AASizeValue.Parse("10234567890123456789").ToScaledBinaryString());
    }

    [TestMethod]
    public void AASizeValue_ToScaledBinaryString11() {
        Assert.AreEqual("3", AASizeValue.Parse("3").ToScaledBinaryString());
        Assert.AreEqual("31", AASizeValue.Parse("31").ToScaledBinaryString());
        Assert.AreEqual("314", AASizeValue.Parse("314").ToScaledBinaryString());
        Assert.AreEqual("3.07Ki", AASizeValue.Parse("3141").ToScaledBinaryString());
        Assert.AreEqual("30.7Ki", AASizeValue.Parse("31415").ToScaledBinaryString());
        Assert.AreEqual("307Ki", AASizeValue.Parse("314159").ToScaledBinaryString());
        Assert.AreEqual("3.00Mi", AASizeValue.Parse("3141592").ToScaledBinaryString());
        Assert.AreEqual("30.0Mi", AASizeValue.Parse("31415926").ToScaledBinaryString());
        Assert.AreEqual("300Mi", AASizeValue.Parse("314159265").ToScaledBinaryString());
        Assert.AreEqual("2.93Gi", AASizeValue.Parse("3141592653").ToScaledBinaryString());
        Assert.AreEqual("29.3Gi", AASizeValue.Parse("31415926535").ToScaledBinaryString());
        Assert.AreEqual("293Gi", AASizeValue.Parse("314159265358").ToScaledBinaryString());
        Assert.AreEqual("2.86Ti", AASizeValue.Parse("3141592653589").ToScaledBinaryString());
        Assert.AreEqual("28.6Ti", AASizeValue.Parse("31415926535897").ToScaledBinaryString());
        Assert.AreEqual("286Ti", AASizeValue.Parse("314159265358979").ToScaledBinaryString());
        Assert.AreEqual("2.79Pi", AASizeValue.Parse("3141592653589793").ToScaledBinaryString());
        Assert.AreEqual("27.9Pi", AASizeValue.Parse("31415926535897932").ToScaledBinaryString());
        Assert.AreEqual("279Pi", AASizeValue.Parse("314159265358979323").ToScaledBinaryString());
        Assert.AreEqual("2790Pi", AASizeValue.Parse("3141592653589793238").ToScaledBinaryString());
    }

    [TestMethod]
    public void AASizeValue_ToScaledBinaryString12() {
        Assert.AreEqual("2", AASizeValue.Parse("2").ToScaledBinaryString());
        Assert.AreEqual("27", AASizeValue.Parse("27").ToScaledBinaryString());
        Assert.AreEqual("271", AASizeValue.Parse("271").ToScaledBinaryString());
        Assert.AreEqual("2.65Ki", AASizeValue.Parse("2718").ToScaledBinaryString());
        Assert.AreEqual("26.5Ki", AASizeValue.Parse("27182").ToScaledBinaryString());
        Assert.AreEqual("265Ki", AASizeValue.Parse("271828").ToScaledBinaryString());
        Assert.AreEqual("2.59Mi", AASizeValue.Parse("2718281").ToScaledBinaryString());
        Assert.AreEqual("25.9Mi", AASizeValue.Parse("27182818").ToScaledBinaryString());
        Assert.AreEqual("259Mi", AASizeValue.Parse("271828182").ToScaledBinaryString());
        Assert.AreEqual("2.53Gi", AASizeValue.Parse("2718281828").ToScaledBinaryString());
        Assert.AreEqual("25.3Gi", AASizeValue.Parse("27182818284").ToScaledBinaryString());
        Assert.AreEqual("253Gi", AASizeValue.Parse("271828182845").ToScaledBinaryString());
        Assert.AreEqual("2.47Ti", AASizeValue.Parse("2718281828459").ToScaledBinaryString());
        Assert.AreEqual("24.7Ti", AASizeValue.Parse("27182818284590").ToScaledBinaryString());
        Assert.AreEqual("247Ti", AASizeValue.Parse("271828182845904").ToScaledBinaryString());
        Assert.AreEqual("2.41Pi", AASizeValue.Parse("2718281828459045").ToScaledBinaryString());
        Assert.AreEqual("24.1Pi", AASizeValue.Parse("27182818284590452").ToScaledBinaryString());
        Assert.AreEqual("241Pi", AASizeValue.Parse("271828182845904523").ToScaledBinaryString());
        Assert.AreEqual("2410Pi", AASizeValue.Parse("2718281828459045235").ToScaledBinaryString());
    }

}
