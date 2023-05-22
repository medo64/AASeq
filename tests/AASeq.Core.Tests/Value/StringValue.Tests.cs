using System;
using System.Globalization;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class StringValue_Tests {

    [TestMethod]
    public void StringValue_Basic() {
        var text = "HG2G";
        Assert.IsTrue(StringValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
        Assert.AreEqual(result, text);
    }

    [TestMethod]
    public void StringValue_FailedParse() {
        Assert.IsFalse(StringValue.TryParse(null, out var _));
    }

    [TestMethod]
    public void StringValue_AsBoolean() {
        Assert.AreEqual(true, new StringValue("TRUE").AsBoolean());
        Assert.AreEqual(true, new StringValue("T").AsBoolean());
        Assert.AreEqual(true, new StringValue("Y").AsBoolean());
        Assert.AreEqual(true, new StringValue("YES").AsBoolean());
        Assert.AreEqual(true, new StringValue("+").AsBoolean());
        Assert.AreEqual(false, new StringValue("false").AsBoolean());
        Assert.AreEqual(false, new StringValue("f").AsBoolean());
        Assert.AreEqual(false, new StringValue("n").AsBoolean());
        Assert.AreEqual(false, new StringValue("No").AsBoolean());
        Assert.AreEqual(false, new StringValue("-").AsBoolean());
        Assert.IsNull(new StringValue("?").AsBoolean());
    }

    [TestMethod]
    public void StringValue_AsSByte() {
        Assert.AreEqual((sbyte)42, new StringValue("42").AsSByte());
        Assert.AreEqual((sbyte)-42, new StringValue("-42").AsSByte());
        Assert.IsNull(new StringValue("128").AsSByte());
        Assert.IsNull(new StringValue("-129").AsSByte());
    }

    [TestMethod]
    public void StringValue_AsInt16() {
        Assert.AreEqual((short)42, new StringValue("42").AsInt16());
        Assert.AreEqual((short)-42, new StringValue("-42").AsInt16());
        Assert.AreEqual((short)128, new StringValue("128").AsInt16());
        Assert.AreEqual((short)-129, new StringValue("-129").AsInt16());
        Assert.IsNull(new StringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
        Assert.IsNull(new StringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
    }

    [TestMethod]
    public void StringValue_AsInt32() {
        Assert.AreEqual(42, new StringValue("42").AsInt32());
        Assert.AreEqual(-42, new StringValue("-42").AsInt32());
        Assert.AreEqual(128, new StringValue("128").AsInt32());
        Assert.AreEqual(-129, new StringValue("-129").AsInt32());
        Assert.AreEqual(short.MinValue - 1, new StringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.AreEqual(short.MaxValue + 1, new StringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.IsNull(new StringValue(((long)int.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
        Assert.IsNull(new StringValue(((long)int.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
    }

    [TestMethod]
    public void StringValue_AsInt64() {
        Assert.AreEqual(42, new StringValue("42").AsInt64());
        Assert.AreEqual(-42, new StringValue("-42").AsInt64());
        Assert.AreEqual(128, new StringValue("128").AsInt64());
        Assert.AreEqual(-129, new StringValue("-129").AsInt64());
        Assert.AreEqual((long)(short.MinValue) - 1, new StringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.AreEqual((long)(short.MaxValue) + 1, new StringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.AreEqual((long)(int.MinValue) - 1, new StringValue(((long)int.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.AreEqual((long)(int.MaxValue) + 1, new StringValue(((long)int.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.IsNull(new StringValue(((double)long.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
        Assert.IsNull(new StringValue(((double)long.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
    }

    [TestMethod]
    public void StringValue_AsByte() {
        Assert.AreEqual((byte)255, new StringValue("255").AsByte());
        Assert.AreEqual((byte)0, new StringValue("0").AsByte());
        Assert.IsNull(new StringValue("256").AsByte());
        Assert.IsNull(new StringValue("-1").AsByte());
    }

    [TestMethod]
    public void StringValue_AsUInt16() {
        Assert.AreEqual((ushort)255, new StringValue("255").AsUInt16());
        Assert.AreEqual((ushort)0, new StringValue("0").AsUInt16());
        Assert.AreEqual(ushort.MaxValue, new StringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt16());
        Assert.IsNull(new StringValue("-1").AsUInt16());
        Assert.IsNull(new StringValue((ushort.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsUInt16());
    }

    [TestMethod]
    public void StringValue_AsUInt32() {
        Assert.AreEqual((uint)255, new StringValue("255").AsUInt32());
        Assert.AreEqual((uint)0, new StringValue("0").AsUInt32());
        Assert.AreEqual(ushort.MaxValue, new StringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.AreEqual(uint.MaxValue, new StringValue(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.IsNull(new StringValue("-1").AsUInt32());
        Assert.IsNull(new StringValue(((long)uint.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
    }

    [TestMethod]
    public void StringValue_AsUInt64() {
        Assert.AreEqual((uint)255, new StringValue("255").AsUInt64());
        Assert.AreEqual((uint)0, new StringValue("0").AsUInt64());
        Assert.AreEqual(ushort.MaxValue, new StringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.AreEqual(uint.MaxValue, new StringValue(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.AreEqual(ulong.MaxValue, new StringValue(ulong.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.IsNull(new StringValue("-1").AsUInt64());
        Assert.IsNull(new StringValue(((double)ulong.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
    }

    [TestMethod]
    public void StringValue_AsSingle() {
        Assert.AreEqual(42.0f, new StringValue("42.0").AsSingle());
        Assert.AreEqual(-42.0f, new StringValue("-42.0").AsSingle());
        Assert.AreEqual(42.0f, new StringValue("4.2e1").AsSingle());
        Assert.IsTrue(float.IsNaN(new StringValue("NaN").AsSingle(0)));
        Assert.IsNull(new StringValue("OFF").AsSingle());
        Assert.IsNull(new StringValue("").AsSingle());
    }

    [TestMethod]
    public void StringValue_AsDouble() {
        Assert.AreEqual(42.0, new StringValue("42.0").AsDouble());
        Assert.AreEqual(-42.0, new StringValue("-42.0").AsDouble());
        Assert.AreEqual(42.0, new StringValue("4.2e1").AsDouble());
        Assert.IsTrue(double.IsNaN(new StringValue("NaN").AsDouble(0)));
        Assert.IsNull(new StringValue("OFF").AsDouble());
        Assert.IsNull(new StringValue("").AsDouble());
    }

    [TestMethod]
    public void StringValue_AsString() {
        Assert.AreEqual("42", new StringValue("42").AsString());
    }

    [TestMethod]
    public void StringValue_AsDateTime() {
        var now = DateTimeOffset.Now;
        Assert.AreEqual(now, new StringValue(now.ToString("o")).AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(0, 0, 0)), new StringValue("1929-01-07 13:45:23.122Z").AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(1, 0, 0)), new StringValue("1929-01-07 13:45:23.122+01:00").AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 0, new TimeSpan(2, 0, 0)), new StringValue("1929-01-07 13:45:23+02:00").AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 23, new TimeSpan(0, 0, 0)).AddTicks(1234567), new StringValue("1929-01-07 13:45:23.1234567Z").AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1929, 01, 07, 13, 45, 0, 0, new TimeSpan(-2, 0, 0)), new StringValue("1929-01-07 13:45-02:00").AsDateTimeOffset());
    }

    [TestMethod]
    public void StringValue_AsDate() {
        Assert.AreEqual(new DateOnly(1929, 01, 07), new StringValue("1929-01-07").AsDateOnly());
    }

    [TestMethod]
    public void StringValue_AsTime() {
        Assert.AreEqual(new TimeOnly(19, 51, 37), new StringValue("19:51:37").AsTimeOnly());
        Assert.AreEqual(new TimeOnly(19, 51, 00), new StringValue("19:51").AsTimeOnly());
    }

}
