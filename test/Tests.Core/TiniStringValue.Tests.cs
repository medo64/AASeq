using System;
using System.Globalization;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniStringValueTests {

    [Fact(DisplayName = "TiniStringValue: AsBoolean")]
    public void AsBoolean() {
        Assert.True(new TiniStringValue("TRUE").AsBoolean());
        Assert.True(new TiniStringValue("T").AsBoolean());
        Assert.True(new TiniStringValue("Y").AsBoolean());
        Assert.True(new TiniStringValue("YES").AsBoolean());
        Assert.True(new TiniStringValue("+").AsBoolean());
        Assert.False(new TiniStringValue("false").AsBoolean());
        Assert.False(new TiniStringValue("f").AsBoolean());
        Assert.False(new TiniStringValue("n").AsBoolean());
        Assert.False(new TiniStringValue("No").AsBoolean());
        Assert.False(new TiniStringValue("-").AsBoolean());
        Assert.Null(new TiniStringValue("?").AsBoolean());
    }

    [Fact(DisplayName = "TiniStringValue: AsInt8")]
    public void AsInt8() {
        Assert.Equal((sbyte)42, new TiniStringValue("42").AsInt8());
        Assert.Equal((sbyte)-42, new TiniStringValue("-42").AsInt8());
        Assert.Null(new TiniStringValue("128").AsInt8());
        Assert.Null(new TiniStringValue("-129").AsInt8());
    }

    [Fact(DisplayName = "TiniStringValue: AsInt16")]
    public void AsInt16() {
        Assert.Equal((short)42, new TiniStringValue("42").AsInt16());
        Assert.Equal((short)-42, new TiniStringValue("-42").AsInt16());
        Assert.Equal((short)128, new TiniStringValue("128").AsInt16());
        Assert.Equal((short)-129, new TiniStringValue("-129").AsInt16());
        Assert.Null(new TiniStringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
        Assert.Null(new TiniStringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
    }

    [Fact(DisplayName = "TiniStringValue: AsInt32")]
    public void AsInt32() {
        Assert.Equal(42, new TiniStringValue("42").AsInt32());
        Assert.Equal(-42, new TiniStringValue("-42").AsInt32());
        Assert.Equal(128, new TiniStringValue("128").AsInt32());
        Assert.Equal(-129, new TiniStringValue("-129").AsInt32());
        Assert.Equal(short.MinValue - 1, new TiniStringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.Equal(short.MaxValue + 1, new TiniStringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.Null(new TiniStringValue(((long)int.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
        Assert.Null(new TiniStringValue(((long)int.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
    }

    [Fact(DisplayName = "TiniStringValue: AsInt64")]
    public void AsInt64() {
        Assert.Equal(42, new TiniStringValue("42").AsInt64());
        Assert.Equal(-42, new TiniStringValue("-42").AsInt64());
        Assert.Equal(128, new TiniStringValue("128").AsInt64());
        Assert.Equal(-129, new TiniStringValue("-129").AsInt64());
        Assert.Equal((long)(short.MinValue) - 1, new TiniStringValue((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(short.MaxValue) + 1, new TiniStringValue((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(int.MinValue) - 1, new TiniStringValue(((long)int.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(int.MaxValue) + 1, new TiniStringValue(((long)int.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Null(new TiniStringValue(((double)long.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
        Assert.Null(new TiniStringValue(((double)long.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
    }

    [Fact(DisplayName = "TiniStringValue: AsUInt8")]
    public void AsUInt8() {
        Assert.Equal((byte)255, new TiniStringValue("255").AsUInt8());
        Assert.Equal((byte)0, new TiniStringValue("0").AsUInt8());
        Assert.Null(new TiniStringValue("256").AsUInt8());
        Assert.Null(new TiniStringValue("-1").AsUInt8());
    }

    [Fact(DisplayName = "TiniStringValue: AsUInt16")]
    public void AsUInt16() {
        Assert.Equal((ushort)255, new TiniStringValue("255").AsUInt16());
        Assert.Equal((ushort)0, new TiniStringValue("0").AsUInt16());
        Assert.Equal(ushort.MaxValue, new TiniStringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt16());
        Assert.Null(new TiniStringValue("-1").AsUInt16());
        Assert.Null(new TiniStringValue((ushort.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsUInt16());
    }

    [Fact(DisplayName = "TiniStringValue: AsUInt32")]
    public void AsUInt32() {
        Assert.Equal((uint)255, new TiniStringValue("255").AsUInt32());
        Assert.Equal((uint)0, new TiniStringValue("0").AsUInt32());
        Assert.Equal(ushort.MaxValue, new TiniStringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.Equal(uint.MaxValue, new TiniStringValue(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.Null(new TiniStringValue("-1").AsUInt32());
        Assert.Null(new TiniStringValue(((long)uint.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
    }

    [Fact(DisplayName = "TiniStringValue: AsUInt64")]
    public void AsUInt64() {
        Assert.Equal((uint)255, new TiniStringValue("255").AsUInt64());
        Assert.Equal((uint)0, new TiniStringValue("0").AsUInt64());
        Assert.Equal(ushort.MaxValue, new TiniStringValue(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Equal(uint.MaxValue, new TiniStringValue(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Equal(ulong.MaxValue, new TiniStringValue(ulong.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Null(new TiniStringValue("-1").AsUInt64());
        Assert.Null(new TiniStringValue(((double)ulong.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
    }

    [Fact(DisplayName = "TiniStringValue: AsFloat32")]
    public void AsFloat32() {
        Assert.Equal(42.0f, new TiniStringValue("42.0").AsFloat32());
        Assert.Equal(-42.0f, new TiniStringValue("-42.0").AsFloat32());
        Assert.Equal(42.0f, new TiniStringValue("4.2e1").AsFloat32());
        Assert.True(float.IsNaN(new TiniStringValue("NaN").AsFloat32(0)));
        Assert.Null(new TiniStringValue("OFF").AsFloat32());
        Assert.Null(new TiniStringValue("").AsFloat32());
    }

    [Fact(DisplayName = "TiniStringValue: AsFloat64")]
    public void AsFloat64() {
        Assert.Equal(42.0, new TiniStringValue("42.0").AsFloat64());
        Assert.Equal(-42.0, new TiniStringValue("-42.0").AsFloat64());
        Assert.Equal(42.0, new TiniStringValue("4.2e1").AsFloat64());
        Assert.True(double.IsNaN(new TiniStringValue("NaN").AsFloat64(0)));
        Assert.Null(new TiniStringValue("OFF").AsFloat64());
        Assert.Null(new TiniStringValue("").AsFloat64());
    }

    [Fact(DisplayName = "TiniStringValue: AsString")]
    public void AsString() {
        Assert.Equal("42", new TiniStringValue("42").AsString());
    }

    [Fact(DisplayName = "TiniStringValue: AsDateTime")]
    public void AsDateTime() {
        var now = DateTimeOffset.Now;
        Assert.Equal(now, new TiniStringValue(now.ToString("o")).AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(0, 0, 0)), new TiniStringValue("1929-01-07 13:45:23.122").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(1, 0, 0)), new TiniStringValue("1929-01-07 13:45:23.122+01:00").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 0, new TimeSpan(2, 0, 0)), new TiniStringValue("1929-01-07 13:45:23+02:00").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, new TimeSpan(0, 0, 0)).AddTicks(1234567), new TiniStringValue("1929-01-07 13:45:23.1234567Z").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 0, 0, new TimeSpan(-2, 0, 0)), new TiniStringValue("1929-01-07 13:45-02:00").AsDateTime());
    }

    [Fact(DisplayName = "TiniStringValue: AsDate")]
    public void AsDate() {
        Assert.Equal(new DateOnly(1929, 01, 07), new TiniStringValue("1929-01-07").AsDate());
    }

    [Fact(DisplayName = "TiniStringValue: AsTime")]
    public void AsTime() {
        Assert.Equal(new TimeOnly(19, 51, 37), new TiniStringValue("19:51:37").AsTime());
        Assert.Equal(new TimeOnly(19, 51, 00), new TiniStringValue("19:51").AsTime());
    }

}
