using System;
using System.Globalization;
using System.Net;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueStringTests {

    [Fact(DisplayName = "StringValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueString.TryParse(null, out var _));
        Assert.Throws<FormatException>(() => {
            ValueString.Parse(null);
        });
    }

    [Fact(DisplayName = "StringValue: AsBoolean")]
    public void AsBoolean() {
        Assert.True(new ValueString("TRUE").AsBoolean());
        Assert.True(new ValueString("T").AsBoolean());
        Assert.True(new ValueString("Y").AsBoolean());
        Assert.True(new ValueString("YES").AsBoolean());
        Assert.True(new ValueString("+").AsBoolean());
        Assert.False(new ValueString("false").AsBoolean());
        Assert.False(new ValueString("f").AsBoolean());
        Assert.False(new ValueString("n").AsBoolean());
        Assert.False(new ValueString("No").AsBoolean());
        Assert.False(new ValueString("-").AsBoolean());
        Assert.Null(new ValueString("?").AsBoolean());
    }

    [Fact(DisplayName = "StringValue: AsInt8")]
    public void AsInt8() {
        Assert.Equal((sbyte)42, new ValueString("42").AsInt8());
        Assert.Equal((sbyte)-42, new ValueString("-42").AsInt8());
        Assert.Null(new ValueString("128").AsInt8());
        Assert.Null(new ValueString("-129").AsInt8());
    }

    [Fact(DisplayName = "StringValue: AsInt16")]
    public void AsInt16() {
        Assert.Equal((short)42, new ValueString("42").AsInt16());
        Assert.Equal((short)-42, new ValueString("-42").AsInt16());
        Assert.Equal((short)128, new ValueString("128").AsInt16());
        Assert.Equal((short)-129, new ValueString("-129").AsInt16());
        Assert.Null(new ValueString((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
        Assert.Null(new ValueString((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt16());
    }

    [Fact(DisplayName = "StringValue: AsInt32")]
    public void AsInt32() {
        Assert.Equal(42, new ValueString("42").AsInt32());
        Assert.Equal(-42, new ValueString("-42").AsInt32());
        Assert.Equal(128, new ValueString("128").AsInt32());
        Assert.Equal(-129, new ValueString("-129").AsInt32());
        Assert.Equal(short.MinValue - 1, new ValueString((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.Equal(short.MaxValue + 1, new ValueString((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt32());
        Assert.Null(new ValueString(((long)int.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
        Assert.Null(new ValueString(((long)int.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt32());
    }

    [Fact(DisplayName = "StringValue: AsInt64")]
    public void AsInt64() {
        Assert.Equal(42, new ValueString("42").AsInt64());
        Assert.Equal(-42, new ValueString("-42").AsInt64());
        Assert.Equal(128, new ValueString("128").AsInt64());
        Assert.Equal(-129, new ValueString("-129").AsInt64());
        Assert.Equal((long)(short.MinValue) - 1, new ValueString((short.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(short.MaxValue) + 1, new ValueString((short.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(int.MinValue) - 1, new ValueString(((long)int.MinValue - 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Equal((long)(int.MaxValue) + 1, new ValueString(((long)int.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsInt64());
        Assert.Null(new ValueString(((double)long.MinValue - 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
        Assert.Null(new ValueString(((double)long.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsInt64());
    }

    [Fact(DisplayName = "StringValue: AsUInt8")]
    public void AsUInt8() {
        Assert.Equal((byte)255, new ValueString("255").AsUInt8());
        Assert.Equal((byte)0, new ValueString("0").AsUInt8());
        Assert.Null(new ValueString("256").AsUInt8());
        Assert.Null(new ValueString("-1").AsUInt8());
    }

    [Fact(DisplayName = "StringValue: AsUInt16")]
    public void AsUInt16() {
        Assert.Equal((ushort)255, new ValueString("255").AsUInt16());
        Assert.Equal((ushort)0, new ValueString("0").AsUInt16());
        Assert.Equal(ushort.MaxValue, new ValueString(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt16());
        Assert.Null(new ValueString("-1").AsUInt16());
        Assert.Null(new ValueString((ushort.MaxValue + 1).ToString(CultureInfo.InvariantCulture)).AsUInt16());
    }

    [Fact(DisplayName = "StringValue: AsUInt32")]
    public void AsUInt32() {
        Assert.Equal((uint)255, new ValueString("255").AsUInt32());
        Assert.Equal((uint)0, new ValueString("0").AsUInt32());
        Assert.Equal(ushort.MaxValue, new ValueString(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.Equal(uint.MaxValue, new ValueString(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
        Assert.Null(new ValueString("-1").AsUInt32());
        Assert.Null(new ValueString(((long)uint.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt32());
    }

    [Fact(DisplayName = "StringValue: AsUInt64")]
    public void AsUInt64() {
        Assert.Equal((uint)255, new ValueString("255").AsUInt64());
        Assert.Equal((uint)0, new ValueString("0").AsUInt64());
        Assert.Equal(ushort.MaxValue, new ValueString(ushort.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Equal(uint.MaxValue, new ValueString(uint.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Equal(ulong.MaxValue, new ValueString(ulong.MaxValue.ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
        Assert.Null(new ValueString("-1").AsUInt64());
        Assert.Null(new ValueString(((double)ulong.MaxValue + 1).ToString("0", CultureInfo.InvariantCulture)).AsUInt64());
    }

    [Fact(DisplayName = "StringValue: AsFloat32")]
    public void AsFloat32() {
        Assert.Equal(42.0f, new ValueString("42.0").AsFloat32());
        Assert.Equal(-42.0f, new ValueString("-42.0").AsFloat32());
        Assert.Equal(42.0f, new ValueString("4.2e1").AsFloat32());
        Assert.True(float.IsNaN(new ValueString("NaN").AsFloat32(0)));
        Assert.Null(new ValueString("OFF").AsFloat32());
        Assert.Null(new ValueString("").AsFloat32());
    }

    [Fact(DisplayName = "StringValue: AsFloat64")]
    public void AsFloat64() {
        Assert.Equal(42.0, new ValueString("42.0").AsFloat64());
        Assert.Equal(-42.0, new ValueString("-42.0").AsFloat64());
        Assert.Equal(42.0, new ValueString("4.2e1").AsFloat64());
        Assert.True(double.IsNaN(new ValueString("NaN").AsFloat64(0)));
        Assert.Null(new ValueString("OFF").AsFloat64());
        Assert.Null(new ValueString("").AsFloat64());
    }

    [Fact(DisplayName = "StringValue: AsString")]
    public void AsString() {
        Assert.Equal("42", new ValueString("42").AsString());
    }

    [Fact(DisplayName = "StringValue: AsDateTime")]
    public void AsDateTime() {
        var now = DateTimeOffset.Now;
        Assert.Equal(now, new ValueString(now.ToString("o")).AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(0, 0, 0)), new ValueString("1929-01-07 13:45:23.122Z").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 122, new TimeSpan(1, 0, 0)), new ValueString("1929-01-07 13:45:23.122+01:00").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, 0, new TimeSpan(2, 0, 0)), new ValueString("1929-01-07 13:45:23+02:00").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 23, new TimeSpan(0, 0, 0)).AddTicks(1234567), new ValueString("1929-01-07 13:45:23.1234567Z").AsDateTime());
        Assert.Equal(new DateTimeOffset(1929, 01, 07, 13, 45, 0, 0, new TimeSpan(-2, 0, 0)), new ValueString("1929-01-07 13:45-02:00").AsDateTime());
    }

    [Fact(DisplayName = "StringValue: AsDate")]
    public void AsDate() {
        Assert.Equal(new DateOnly(1929, 01, 07), new ValueString("1929-01-07").AsDate());
    }

    [Fact(DisplayName = "StringValue: AsTime")]
    public void AsTime() {
        Assert.Equal(new TimeOnly(19, 51, 37), new ValueString("19:51:37").AsTime());
        Assert.Equal(new TimeOnly(19, 51, 00), new ValueString("19:51").AsTime());
    }

    [Fact(DisplayName = "StringValue: AsIPAddress")]
    public void AsIPAddress() {
        Assert.Equal(IPAddress.IPv6Any , new ValueString("::").AsIPAddress());
        Assert.Equal(IPAddress.Any, new ValueString("0.0.0.0").AsIPAddress());
        Assert.Equal(IPAddress.IPv6Loopback, new ValueString("::1").AsIPAddress());
        Assert.Equal(IPAddress.Loopback, new ValueString("127.0.0.1").AsIPAddress());
        Assert.Equal(IPAddress.Parse("ff08::152"), new ValueString("ff08::152").AsIPAddress());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), new ValueString("239.192.111.17").AsIPAddress());
        Assert.Equal(IPAddress.Parse("ff18::5e:40:6f:11"), new ValueString("ff18::5e:40:6f:11").AsIPAddress());
    }

    [Fact(DisplayName = "StringValue: AsIPv6Address")]
    public void AsIPv6Address() {
        Assert.Equal(IPAddress.IPv6Any, new ValueString("::").AsIPv6Address());
        Assert.Null(new ValueString("0.0.0.0").AsIPv6Address());
        Assert.Equal(IPAddress.IPv6Loopback, new ValueString("::1").AsIPv6Address());
        Assert.Null(new ValueString("127.0.0.1").AsIPv6Address());
        Assert.Equal(IPAddress.Parse("ff08::152"), new ValueString("ff08::152").AsIPv6Address());
        Assert.Null(new ValueString("239.192.111.17").AsIPv6Address());
        Assert.Equal(IPAddress.Parse("ff18::5e:40:6f:11"), new ValueString("ff18::5e:40:6f:11").AsIPv6Address());
    }

    [Fact(DisplayName = "StringValue: AsIPv4Address")]
    public void AsIPv4Address() {
        Assert.Null(new ValueString("::").AsIPv4Address());
        Assert.Equal(IPAddress.Any, new ValueString("0.0.0.0").AsIPv4Address());
        Assert.Null(new ValueString("::1").AsIPv4Address());
        Assert.Equal(IPAddress.Loopback, new ValueString("127.0.0.1").AsIPv4Address());
        Assert.Null(new ValueString("ff08::152").AsIPv4Address());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), new ValueString("239.192.111.17").AsIPv4Address());
        Assert.Null(new ValueString("ff18::5e:40:6f:11").AsIPv4Address());
    }

}
