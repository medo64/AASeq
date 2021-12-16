using System;
using System.Globalization;
using System.Net;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueStringTests {

    [Fact(DisplayName = "TiniStringValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueString.TryParse(null, out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueString.Parse(null);
        });
    }

    [Fact(DisplayName = "TiniStringValue: AsBoolean")]
    public void AsBoolean() {
        Assert.True(new TiniValueString("TRUE").AsBoolean());
        Assert.True(new TiniValueString("T").AsBoolean());
        Assert.True(new TiniValueString("Y").AsBoolean());
        Assert.True(new TiniValueString("YES").AsBoolean());
        Assert.True(new TiniValueString("+").AsBoolean());
        Assert.False(new TiniValueString("false").AsBoolean());
        Assert.False(new TiniValueString("f").AsBoolean());
        Assert.False(new TiniValueString("n").AsBoolean());
        Assert.False(new TiniValueString("No").AsBoolean());
        Assert.False(new TiniValueString("-").AsBoolean());
        Assert.Null(new TiniValueString("?").AsBoolean());
    }

    [Fact(DisplayName = "TiniStringValue: AsInt8")]
    public void AsInt8() {
        Assert.Equal((sbyte)42, new TiniValueString("42").AsInt8());
        Assert.Equal((sbyte)-42, new TiniValueString("-42").AsInt8());
        Assert.Null(new TiniValueString("128").AsInt8());
        Assert.Null(new TiniValueString("-129").AsInt8());
    }

    [Fact(DisplayName = "TiniStringValue: AsInt16")]
    public void AsInt16() {
        Assert.Equal((short)42, new TiniValueString("42").AsInt16());
        Assert.Equal((short)-42, new TiniValueString("-42").AsInt16());
        Assert.Equal((short)128, new TiniValueString("128").AsInt16());
        Assert.Equal((short)-129, new TiniValueString("-129").AsInt16());
        Assert.Null(new TiniValueString((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
        Assert.Null(new TiniValueString((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
    }

    [Fact(DisplayName = "TiniStringValue: AsInt32")]
    public void AsInt32() {
        Assert.Equal(42, new TiniValueString("42").AsInt32());
        Assert.Equal(-42, new TiniValueString("-42").AsInt32());
        Assert.Equal(128, new TiniValueString("128").AsInt32());
        Assert.Equal(-129, new TiniValueString("-129").AsInt32());
        Assert.Equal(short.MinValue - 1, new TiniValueString((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.Equal(short.MaxValue + 1, new TiniValueString((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.Null(new TiniValueString(((long)int.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
        Assert.Null(new TiniValueString(((long)int.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
    }

    [Fact(DisplayName = "TiniStringValue: AsInt64")]
    public void AsInt64() {
        Assert.Equal(42, new TiniValueString("42").AsInt64());
        Assert.Equal(-42, new TiniValueString("-42").AsInt64());
        Assert.Equal(128, new TiniValueString("128").AsInt64());
        Assert.Equal(-129, new TiniValueString("-129").AsInt64());
        Assert.Equal((long)(short.MinValue) - 1, new TiniValueString((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(short.MaxValue) + 1, new TiniValueString((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(int.MinValue) - 1, new TiniValueString(((long)int.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(int.MaxValue) + 1, new TiniValueString(((long)int.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Null(new TiniValueString(((double)long.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
        Assert.Null(new TiniValueString(((double)long.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
    }

    [Fact(DisplayName = "TiniStringValue: AsUInt8")]
    public void AsUInt8() {
        Assert.Equal((byte)255, new TiniValueString("255").AsUInt8());
        Assert.Equal((byte)0, new TiniValueString("0").AsUInt8());
        Assert.Null(new TiniValueString("256").AsUInt8());
        Assert.Null(new TiniValueString("-1").AsUInt8());
    }

    [Fact(DisplayName = "TiniStringValue: AsUInt16")]
    public void AsUInt16() {
        Assert.Equal((ushort)255, new TiniValueString("255").AsUInt16());
        Assert.Equal((ushort)0, new TiniValueString("0").AsUInt16());
        Assert.Equal(ushort.MaxValue, new TiniValueString(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt16());
        Assert.Null(new TiniValueString("-1").AsUInt16());
        Assert.Null(new TiniValueString((ushort.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsUInt16());
    }

    [Fact(DisplayName = "TiniStringValue: AsUInt32")]
    public void AsUInt32() {
        Assert.Equal((uint)255, new TiniValueString("255").AsUInt32());
        Assert.Equal((uint)0, new TiniValueString("0").AsUInt32());
        Assert.Equal(ushort.MaxValue, new TiniValueString(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.Equal(uint.MaxValue, new TiniValueString(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.Null(new TiniValueString("-1").AsUInt32());
        Assert.Null(new TiniValueString(((long)uint.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
    }

    [Fact(DisplayName = "TiniStringValue: AsUInt64")]
    public void AsUInt64() {
        Assert.Equal((uint)255, new TiniValueString("255").AsUInt64());
        Assert.Equal((uint)0, new TiniValueString("0").AsUInt64());
        Assert.Equal(ushort.MaxValue, new TiniValueString(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Equal(uint.MaxValue, new TiniValueString(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Equal(ulong.MaxValue, new TiniValueString(ulong.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Null(new TiniValueString("-1").AsUInt64());
        Assert.Null(new TiniValueString(((double)ulong.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
    }

    [Fact(DisplayName = "TiniStringValue: AsFloat32")]
    public void AsFloat32() {
        Assert.Equal(42.0f, new TiniValueString("42.0").AsFloat32());
        Assert.Equal(-42.0f, new TiniValueString("-42.0").AsFloat32());
        Assert.Equal(42.0f, new TiniValueString("4.2e1").AsFloat32());
        Assert.True(float.IsNaN(new TiniValueString("NaN").AsFloat32(0)));
        Assert.Null(new TiniValueString("OFF").AsFloat32());
        Assert.Null(new TiniValueString("").AsFloat32());
    }

    [Fact(DisplayName = "TiniStringValue: AsFloat64")]
    public void AsFloat64() {
        Assert.Equal(42.0, new TiniValueString("42.0").AsFloat64());
        Assert.Equal(-42.0, new TiniValueString("-42.0").AsFloat64());
        Assert.Equal(42.0, new TiniValueString("4.2e1").AsFloat64());
        Assert.True(double.IsNaN(new TiniValueString("NaN").AsFloat64(0)));
        Assert.Null(new TiniValueString("OFF").AsFloat64());
        Assert.Null(new TiniValueString("").AsFloat64());
    }

    [Fact(DisplayName = "TiniStringValue: AsString")]
    public void AsString() {
        Assert.Equal("42", new TiniValueString("42").AsString());
    }

    [Fact(DisplayName = "TiniStringValue: AsDateTime")]
    public void AsDateTime() {
        var now = DateTimeOffset.Now;
        Assert.Equal(now, new TiniValueString(now.ToString("o")).AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(0, 0, 0)), new TiniValueString("1929-01-07 13:45:23.122Z").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(1, 0, 0)), new TiniValueString("1929-01-07 13:45:23.122+01:00").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 0, new TimeSpan(2, 0, 0)), new TiniValueString("1929-01-07 13:45:23+02:00").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, new TimeSpan(0, 0, 0)).AddTicks(1234567), new TiniValueString("1929-01-07 13:45:23.1234567Z").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 0, 0, new TimeSpan(-2, 0, 0)), new TiniValueString("1929-01-07 13:45-02:00").AsDateTime());
    }

    [Fact(DisplayName = "TiniStringValue: AsDate")]
    public void AsDate() {
        Assert.Equal(new DateOnly(1929, 01, 07), new TiniValueString("1929-01-07").AsDate());
    }

    [Fact(DisplayName = "TiniStringValue: AsTime")]
    public void AsTime() {
        Assert.Equal(new TimeOnly(19, 51, 37), new TiniValueString("19:51:37").AsTime());
        Assert.Equal(new TimeOnly(19, 51, 00), new TiniValueString("19:51").AsTime());
    }

    [Fact(DisplayName = "TiniStringValue: AsIPAddress")]
    public void AsIPAddress() {
        Assert.Equal(IPAddress.IPv6Any , new TiniValueString("::").AsIPAddress());
        Assert.Equal(IPAddress.Any, new TiniValueString("0.0.0.0").AsIPAddress());
        Assert.Equal(IPAddress.IPv6Loopback, new TiniValueString("::1").AsIPAddress());
        Assert.Equal(IPAddress.Loopback, new TiniValueString("127.0.0.1").AsIPAddress());
        Assert.Equal(IPAddress.Parse("ff08::152"), new TiniValueString("ff08::152").AsIPAddress());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), new TiniValueString("239.192.111.17").AsIPAddress());
        Assert.Equal(IPAddress.Parse("ff18::5e:40:6f:11"), new TiniValueString("ff18::5e:40:6f:11").AsIPAddress());
    }

    [Fact(DisplayName = "TiniStringValue: AsIPv6Address")]
    public void AsIPv6Address() {
        Assert.Equal(IPAddress.IPv6Any, new TiniValueString("::").AsIPv6Address());
        Assert.Null(new TiniValueString("0.0.0.0").AsIPv6Address());
        Assert.Equal(IPAddress.IPv6Loopback, new TiniValueString("::1").AsIPv6Address());
        Assert.Null(new TiniValueString("127.0.0.1").AsIPv6Address());
        Assert.Equal(IPAddress.Parse("ff08::152"), new TiniValueString("ff08::152").AsIPv6Address());
        Assert.Null(new TiniValueString("239.192.111.17").AsIPv6Address());
        Assert.Equal(IPAddress.Parse("ff18::5e:40:6f:11"), new TiniValueString("ff18::5e:40:6f:11").AsIPv6Address());
    }

    [Fact(DisplayName = "TiniStringValue: AsIPv4Address")]
    public void AsIPv4Address() {
        Assert.Null(new TiniValueString("::").AsIPv4Address());
        Assert.Equal(IPAddress.Any, new TiniValueString("0.0.0.0").AsIPv4Address());
        Assert.Null(new TiniValueString("::1").AsIPv4Address());
        Assert.Equal(IPAddress.Loopback, new TiniValueString("127.0.0.1").AsIPv4Address());
        Assert.Null(new TiniValueString("ff08::152").AsIPv4Address());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), new TiniValueString("239.192.111.17").AsIPv4Address());
        Assert.Null(new TiniValueString("ff18::5e:40:6f:11").AsIPv4Address());
    }

}
