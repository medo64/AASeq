using System;
using System.Globalization;
using System.Net;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAStringValue_Tests {

    [TestMethod]
    public void AAStringValue_Basic() {
        var text = "HG2G";
        Assert.IsTrue(AAStringValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAStringValue.Parse(text));
        Assert.AreEqual(result, AAStringValue.Parse(text));
        Assert.AreEqual(result, text);
    }

    [TestMethod]
    public void AAStringValue_FailedParse() {
        Assert.IsFalse(AAStringValue.TryParse(null, out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAStringValue.Parse(null);
        });
    }

    [TestMethod]
    public void AAStringValue_AsBoolean() {
        Assert.AreEqual(true, new AAStringValue("TRUE").AsBoolean());
        Assert.AreEqual(true, new AAStringValue("T").AsBoolean());
        Assert.AreEqual(true, new AAStringValue("Y").AsBoolean());
        Assert.AreEqual(true, new AAStringValue("YES").AsBoolean());
        Assert.AreEqual(true, new AAStringValue("+").AsBoolean());
        Assert.AreEqual(false, new AAStringValue("false").AsBoolean());
        Assert.AreEqual(false, new AAStringValue("f").AsBoolean());
        Assert.AreEqual(false, new AAStringValue("n").AsBoolean());
        Assert.AreEqual(false, new AAStringValue("No").AsBoolean());
        Assert.AreEqual(false, new AAStringValue("-").AsBoolean());
        Assert.IsNull(new AAStringValue("?").AsBoolean());
    }

    [TestMethod]
    public void AAStringValue_AsSByte() {
        Assert.AreEqual((sbyte)42, new AAStringValue("42").AsSByte());
        Assert.AreEqual((sbyte)-42, new AAStringValue("-42").AsSByte());
        Assert.IsNull(new AAStringValue("128").AsSByte());
        Assert.IsNull(new AAStringValue("-129").AsSByte());
    }

    [TestMethod]
    public void AAStringValue_AsInt16() {
        Assert.AreEqual((short)42, new AAStringValue("42").AsInt16());
        Assert.AreEqual((short)-42, new AAStringValue("-42").AsInt16());
        Assert.AreEqual((short)128, new AAStringValue("128").AsInt16());
        Assert.AreEqual((short)-129, new AAStringValue("-129").AsInt16());
        Assert.IsNull(new AAStringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
        Assert.IsNull(new AAStringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
    }

    [TestMethod]
    public void AAStringValue_AsInt32() {
        Assert.AreEqual(42, new AAStringValue("42").AsInt32());
        Assert.AreEqual(-42, new AAStringValue("-42").AsInt32());
        Assert.AreEqual(128, new AAStringValue("128").AsInt32());
        Assert.AreEqual(-129, new AAStringValue("-129").AsInt32());
        Assert.AreEqual(short.MinValue - 1, new AAStringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.AreEqual(short.MaxValue + 1, new AAStringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.IsNull(new AAStringValue(((long)int.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
        Assert.IsNull(new AAStringValue(((long)int.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
    }

    [TestMethod]
    public void AAStringValue_AsInt64() {
        Assert.AreEqual(42, new AAStringValue("42").AsInt64());
        Assert.AreEqual(-42, new AAStringValue("-42").AsInt64());
        Assert.AreEqual(128, new AAStringValue("128").AsInt64());
        Assert.AreEqual(-129, new AAStringValue("-129").AsInt64());
        Assert.AreEqual((long)(short.MinValue) - 1, new AAStringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.AreEqual((long)(short.MaxValue) + 1, new AAStringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.AreEqual((long)(int.MinValue) - 1, new AAStringValue(((long)int.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.AreEqual((long)(int.MaxValue) + 1, new AAStringValue(((long)int.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.IsNull(new AAStringValue(((double)long.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
        Assert.IsNull(new AAStringValue(((double)long.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
    }

    [TestMethod]
    public void AAStringValue_AsByte() {
        Assert.AreEqual((byte)255, new AAStringValue("255").AsByte());
        Assert.AreEqual((byte)0, new AAStringValue("0").AsByte());
        Assert.IsNull(new AAStringValue("256").AsByte());
        Assert.IsNull(new AAStringValue("-1").AsByte());
    }

    [TestMethod]
    public void AAStringValue_AsUInt16() {
        Assert.AreEqual((ushort)255, new AAStringValue("255").AsUInt16());
        Assert.AreEqual((ushort)0, new AAStringValue("0").AsUInt16());
        Assert.AreEqual(ushort.MaxValue, new AAStringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt16());
        Assert.IsNull(new AAStringValue("-1").AsUInt16());
        Assert.IsNull(new AAStringValue((ushort.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsUInt16());
    }

    [TestMethod]
    public void AAStringValue_AsUInt32() {
        Assert.AreEqual((uint)255, new AAStringValue("255").AsUInt32());
        Assert.AreEqual((uint)0, new AAStringValue("0").AsUInt32());
        Assert.AreEqual(ushort.MaxValue, new AAStringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.AreEqual(uint.MaxValue, new AAStringValue(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.IsNull(new AAStringValue("-1").AsUInt32());
        Assert.IsNull(new AAStringValue(((long)uint.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
    }

    [TestMethod]
    public void AAStringValue_AsUInt64() {
        Assert.AreEqual((uint)255, new AAStringValue("255").AsUInt64());
        Assert.AreEqual((uint)0, new AAStringValue("0").AsUInt64());
        Assert.AreEqual(ushort.MaxValue, new AAStringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.AreEqual(uint.MaxValue, new AAStringValue(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.AreEqual(ulong.MaxValue, new AAStringValue(ulong.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.IsNull(new AAStringValue("-1").AsUInt64());
        Assert.IsNull(new AAStringValue(((double)ulong.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
    }

    [TestMethod]
    public void AAStringValue_AsSingle() {
        Assert.AreEqual(42.0f, new AAStringValue("42.0").AsSingle());
        Assert.AreEqual(-42.0f, new AAStringValue("-42.0").AsSingle());
        Assert.AreEqual(42.0f, new AAStringValue("4.2e1").AsSingle());
        Assert.IsTrue(float.IsNaN(new AAStringValue("NaN").AsSingle(0)));
        Assert.IsNull(new AAStringValue("OFF").AsSingle());
        Assert.IsNull(new AAStringValue("").AsSingle());
    }

    [TestMethod]
    public void AAStringValue_AsDouble() {
        Assert.AreEqual(42.0, new AAStringValue("42.0").AsDouble());
        Assert.AreEqual(-42.0, new AAStringValue("-42.0").AsDouble());
        Assert.AreEqual(42.0, new AAStringValue("4.2e1").AsDouble());
        Assert.IsTrue(double.IsNaN(new AAStringValue("NaN").AsDouble(0)));
        Assert.IsNull(new AAStringValue("OFF").AsDouble());
        Assert.IsNull(new AAStringValue("").AsDouble());
    }

    [TestMethod]
    public void AAStringValue_AsString() {
        Assert.AreEqual("42", new AAStringValue("42").AsString());
    }

    [TestMethod]
    public void AAStringValue_AsDateTime() {
        var now = DateTimeOffset.Now;
        Assert.AreEqual(now, new AAStringValue(now.ToString("o")).AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(0, 0, 0)), new AAStringValue("1929-01-07 13:45:23.122Z").AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(1, 0, 0)), new AAStringValue("1929-01-07 13:45:23.122+01:00").AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 0, new TimeSpan(2, 0, 0)), new AAStringValue("1929-01-07 13:45:23+02:00").AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 23, new TimeSpan(0, 0, 0)).AddTicks(1234567), new AAStringValue("1929-01-07 13:45:23.1234567Z").AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 0, 0, new TimeSpan(-2, 0, 0)), new AAStringValue("1929-01-07 13:45-02:00").AsDateTimeOffset());
    }

    [TestMethod]
    public void AAStringValue_AsDate() {
        Assert.AreEqual(new DateOnly(1929, 01, 07), new AAStringValue("1929-01-07").AsDateOnly());
    }

    [TestMethod]
    public void AAStringValue_AsTime() {
        Assert.AreEqual(new TimeOnly(19, 51, 37), new AAStringValue("19:51:37").AsTimeOnly());
        Assert.AreEqual(new TimeOnly(19, 51, 00), new AAStringValue("19:51").AsTimeOnly());
    }

    [TestMethod]
    public void AAStringValue_AsIPAddress() {
        Assert.AreEqual(IPAddress.IPv6Any , new AAStringValue("::").AsIPAddress());
        Assert.AreEqual(IPAddress.Any, new AAStringValue("0.0.0.0").AsIPAddress());
        Assert.AreEqual(IPAddress.IPv6Loopback, new AAStringValue("::1").AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, new AAStringValue("127.0.0.1").AsIPAddress());
        Assert.AreEqual(IPAddress.Parse("ff08::152"), new AAStringValue("ff08::152").AsIPAddress());
        Assert.AreEqual(IPAddress.Parse("239.192.111.17"), new AAStringValue("239.192.111.17").AsIPAddress());
        Assert.AreEqual(IPAddress.Parse("ff18::5e:40:6f:11"), new AAStringValue("ff18::5e:40:6f:11").AsIPAddress());
    }

}
