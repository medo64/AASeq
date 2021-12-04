using System;
using System.Net;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueTests {

    #region Implicit

    [Fact(DisplayName = "TiniValue: Implicit Boolean")]
    public void ImplicitBoolean() {
        TiniValue x = true;
        Assert.IsType<TiniBooleanValue>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int8")]
    public void ImplicitInt8() {
        TiniValue x = (sbyte)42;
        Assert.IsType<TiniInt8Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int16")]
    public void ImplicitInt16() {
        TiniValue x = (short)42;
        Assert.IsType<TiniInt16Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int32")]
    public void ImplicitInt32() {
        TiniValue x = 42;
        Assert.IsType<TiniInt32Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int64")]
    public void ImplicitInt64() {
        TiniValue x = (long)42;
        Assert.IsType<TiniInt64Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt8")]
    public void ImplicitUInt8() {
        TiniValue x = (byte)42;
        Assert.IsType<TiniUInt8Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt16")]
    public void ImplicitUInt16() {
        TiniValue x = (ushort)42;
        Assert.IsType<TiniUInt16Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt32")]
    public void ImplicitUInt32() {
        TiniValue x = (uint)42;
        Assert.IsType<TiniUInt32Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt64")]
    public void ImplicitUInt64() {
        TiniValue x = (ulong)42;
        Assert.IsType<TiniUInt64Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Float32")]
    public void ImplicitFloat32() {
        TiniValue x = (float)42;
        Assert.IsType<TiniFloat32Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Float64")]
    public void ImplicitFloat64() {
        TiniValue x = (double)42;
        Assert.IsType<TiniFloat64Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit String")]
    public void ImplicitString() {
        TiniValue x = "A";
        Assert.IsType<TiniStringValue>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit DateTime")]
    public void ImplicitDateTime() {
        TiniValue x = new DateTimeOffset(1997, 4, 1, 23, 11, 54, 565, new TimeSpan(0, 0, 0));
        Assert.IsType<TiniDateTimeValue>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit DateTime (2)")]
    public void ImplicitDateTime2() {
        TiniValue x = new DateTime(1997, 4, 1, 23, 11, 54, 565, DateTimeKind.Local);
        Assert.IsType<TiniDateTimeValue>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Date")]
    public void ImplicitDate() {
        TiniValue x = new DateOnly(1997, 4, 1);
        Assert.IsType<TiniDateValue>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Time")]
    public void ImplicitTime() {
        TiniValue x = new TimeOnly(23, 11, 54, 565);
        Assert.IsType<TiniTimeValue>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit IPAddress")]
    public void ImplicitIPAddress() {
        TiniValue x = IPAddress.Parse("ff08::152");
        Assert.IsType<TiniIPAddressValue>(x);
    }

    #endregion Implicit

    #region AsValue

    [Fact(DisplayName = "TiniValue: AsBooleanValue")]
    public void AsValueBoolean() {
        TiniValue x = true;
        Assert.NotNull(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsInt8Value")]
    public void AsInt8Value() {
        TiniValue x = (sbyte)32;
        Assert.Null(x.AsBooleanValue());
        Assert.NotNull(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsInt16Value")]
    public void AsInt16Value() {
        TiniValue x = (short)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.NotNull(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsInt32Value")]
    public void AsInt32Value() {
        TiniValue x = 32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.NotNull(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsInt64Value")]
    public void AsInt64Value() {
        TiniValue x = (long)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.NotNull(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsUInt8Value")]
    public void AsUInt8Value() {
        TiniValue x = (byte)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.NotNull(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsUInt16Value")]
    public void AsUInt16Value() {
        TiniValue x = (ushort)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.NotNull(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsUInt32Value")]
    public void AsUInt32Value() {
        TiniValue x = (uint)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.NotNull(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsUInt64Value")]
    public void AsUInt64Value() {
        TiniValue x = (ulong)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.NotNull(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsFloat32Value")]
    public void AsFloat32Value() {
        TiniValue x = (float)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.NotNull(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsFloat64Value")]
    public void AsFloat64Value() {
        TiniValue x = (double)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.NotNull(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsStringValue")]
    public void AsStringValue() {
        TiniValue x = "A";
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.NotNull(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsDateTimeValue")]
    public void AsDateTimeValue() {
        TiniValue x = new DateTimeOffset(2005, 2, 1, 11, 43, 33, 787, new TimeSpan(2, 30, 0));
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.NotNull(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsDateTimeValue (2)")]
    public void AsDateTimeValue2() {
        TiniValue x = new DateTime(2005, 2, 1, 11, 43, 33, 787, DateTimeKind.Local);
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.NotNull(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsDateValue")]
    public void AsDateValue() {
        TiniValue x = new DateOnly(2005, 2, 1);
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.NotNull(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsTimeValue")]
    public void AsTimeValue() {
        TiniValue x = new TimeOnly(11, 43, 33, 787);
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.NotNull(x.AsTimeValue());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsIPAddressValue")]
    public void AsIPAddressValue() {
        TiniValue x = IPAddress.Parse("ff04::152");
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
        Assert.Null(x.AsDateTimeValue());
        Assert.Null(x.AsDateValue());
        Assert.Null(x.AsTimeValue());
        Assert.NotNull(x.AsIPAddress());
        Assert.NotNull(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    #endregion AsValue

    #region As

    [Fact(DisplayName = "TiniValue: AsBoolean")]
    public void AsBoolean() {
        TiniValue x = true;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)1, x.AsInt8());
        Assert.Equal((short)1, x.AsInt16());
        Assert.Equal(1, x.AsInt32());
        Assert.Equal(1, x.AsInt64());
        Assert.Equal((byte)1, x.AsUInt8());
        Assert.Equal((ushort)1, x.AsUInt16());
        Assert.Equal((uint)1, x.AsUInt32());
        Assert.Equal((ulong)1, x.AsUInt64());
        Assert.Equal(1.0f, x.AsFloat32());
        Assert.Equal(1.0, x.AsFloat64());
        Assert.Equal("True", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsInt8")]
    public void AsInt8() {
        TiniValue x = (sbyte)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsInt16")]
    public void AsInt16() {
        TiniValue x = (short)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsInt32")]
    public void AsInt32() {
        TiniValue x = 42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsInt64")]
    public void AsInt64() {
        TiniValue x = (long)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Equal(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.Equal(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsUInt8")]
    public void AsUInt8() {
        TiniValue x = (byte)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsUInt16")]
    public void AsUInt16() {
        TiniValue x = (ushort)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsUInt32")]
    public void AsUInt32() {
        TiniValue x = (uint)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsUInt64")]
    public void AsUInt64() {
        TiniValue x = (ulong)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Equal(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.Equal(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: Float32")]
    public void AsFloat32() {
        TiniValue x = (float)42.2;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.2f, x.AsFloat32());
        Assert.Equal(42.2, x.AsFloat64(0), 5);
        Assert.Equal("42.2", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: Float64")]
    public void AsFloat64() {
        TiniValue x = (double)42.2;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.2f, x.AsFloat32());
        Assert.Equal(42.2, x.AsFloat64());
        Assert.Equal("42.2", x.AsString());
        Assert.Equal(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 200, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.Equal(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsString")]
    public void AsString() {
        TiniValue x = "A";
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Null(x.AsInt32());
        Assert.Null(x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Null(x.AsUInt32());
        Assert.Null(x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Null(x.AsFloat64());
        Assert.Equal("A", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsDateTime")]
    public void AsDateTime() {
        TiniValue x = new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0));
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Null(x.AsInt32());
        Assert.Equal(915214364, x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Null(x.AsUInt32());
        Assert.Equal((ulong)915214364, x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Equal(915214364.469, x.AsFloat64());
        Assert.Equal("1999-01-02T04:12:44+10:00", x.AsString());
        Assert.Equal(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0)), x.AsDateTime());
        Assert.Equal(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.Equal(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsDateTime (2)")]
    public void AsDateTime2() {
        TiniValue x = new DateTime(1999, 1, 2, 4, 12, 44, 469, DateTimeKind.Utc);
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Null(x.AsInt32());
        Assert.Equal(915250364, x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Null(x.AsUInt32());
        Assert.Equal((ulong)915250364, x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Equal(915250364.469, x.AsFloat64());
        Assert.Equal("1999-01-02T04:12:44+00:00", x.AsString());
        Assert.Equal(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(0, 0, 0)), x.AsDateTime());
        Assert.Equal(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.Equal(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsDate")]
    public void AsDate() {
        TiniValue x = new DateOnly(1999, 1, 2);
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Null(x.AsInt32());
        Assert.Equal(915235200, x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Null(x.AsUInt32());
        Assert.Equal((ulong)915235200, x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Equal(915235200, x.AsFloat64());
        Assert.Equal("1999-01-02", x.AsString());
        Assert.Equal(new DateTimeOffset(1999, 1, 2, 0, 0, 0, 0, new TimeSpan(0, 0, 0)), x.AsDateTime());
        Assert.Equal(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsTime")]
    public void AsTime() {
        TiniValue x = new TimeOnly(4, 12, 44, 469);
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Null(x.AsInt32());
        Assert.Null(x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Null(x.AsUInt32());
        Assert.Null(x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Null(x.AsFloat64());
        Assert.Equal("04:12:44", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Equal(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsIPAddress")]
    public void AsIPAddress() {
        TiniValue x = IPAddress.Parse("ff08::152");
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Null(x.AsInt32());
        Assert.Null(x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Null(x.AsUInt32());
        Assert.Null(x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Null(x.AsFloat64());
        Assert.Equal("ff08::152", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Equal(IPAddress.Parse("ff08::152"), x.AsIPAddress());
        Assert.Equal(IPAddress.Parse("ff08::152"), x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
    }

    [Fact(DisplayName = "TiniValue: AsIPAddress (2)")]
    public void AsIPAddress2() {
        TiniValue x = IPAddress.Parse("239.192.111.17");
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Null(x.AsInt32());
        Assert.Null(x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Null(x.AsUInt32());
        Assert.Null(x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Null(x.AsFloat64());
        Assert.Equal("239.192.111.17", x.AsString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), x.AsIPv4Address());
    }

    #endregion AsValue

}
