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
        Assert.IsType<TiniValueBoolean>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int8")]
    public void ImplicitInt8() {
        TiniValue x = (sbyte)42;
        Assert.IsType<TiniValueInt8>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int16")]
    public void ImplicitInt16() {
        TiniValue x = (short)42;
        Assert.IsType<TiniValueInt16>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int32")]
    public void ImplicitInt32() {
        TiniValue x = 42;
        Assert.IsType<TiniValueInt32>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int64")]
    public void ImplicitInt64() {
        TiniValue x = (long)42;
        Assert.IsType<TiniValueInt64>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt8")]
    public void ImplicitUInt8() {
        TiniValue x = (byte)42;
        Assert.IsType<TiniValueUInt8>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt16")]
    public void ImplicitUInt16() {
        TiniValue x = (ushort)42;
        Assert.IsType<TiniValueUInt16>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt32")]
    public void ImplicitUInt32() {
        TiniValue x = (uint)42;
        Assert.IsType<TiniValueUInt32>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt64")]
    public void ImplicitUInt64() {
        TiniValue x = (ulong)42;
        Assert.IsType<TiniValueUInt64>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Float32")]
    public void ImplicitFloat32() {
        TiniValue x = (float)42;
        Assert.IsType<TiniValueFloat32>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Float64")]
    public void ImplicitFloat64() {
        TiniValue x = (double)42;
        Assert.IsType<TiniValueFloat64>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit String")]
    public void ImplicitString() {
        TiniValue x = "A";
        Assert.IsType<TiniValueString>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Binary (byte[])")]
    public void ImplicitBinary1() {
        TiniValue x = new byte[] { 0x42 };
        Assert.IsType<TiniValueBinary>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Binary (ReadOnlyMemory)")]
    public void ImplicitBinary2() {
        TiniValue x = new ReadOnlyMemory<Byte>( new byte[] { 0x42 });
        Assert.IsType<TiniValueBinary>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit DateTime")]
    public void ImplicitDateTime() {
        TiniValue x = new DateTimeOffset(1997, 4, 1, 23, 11, 54, 565, new TimeSpan(0, 0, 0));
        Assert.IsType<TiniValueDateTime>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit DateTime (2)")]
    public void ImplicitDateTime2() {
        TiniValue x = new DateTime(1997, 4, 1, 23, 11, 54, 565, DateTimeKind.Local);
        Assert.IsType<TiniValueDateTime>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Date")]
    public void ImplicitDate() {
        TiniValue x = new DateOnly(1997, 4, 1);
        Assert.IsType<TiniValueDate>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Time")]
    public void ImplicitTime() {
        TiniValue x = new TimeOnly(23, 11, 54, 565);
        Assert.IsType<TiniValueTime>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Duration")]
    public void ImplicitDuration() {
        TiniValue x = new TimeSpan(23, 11, 54, 565);
        Assert.IsType<TiniValueDuration>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit IPAddress")]
    public void ImplicitIPAddress() {
        TiniValue x = IPAddress.Parse("ff08::152");
        Assert.IsType<TiniValueIPAddress>(x);
    }

    #endregion Implicit


    #region AsObject

    [Fact(DisplayName = "TiniValue: AsBooleanObject")]
    public void AsObjectBoolean() {
        TiniValue x = true;
        Assert.NotNull(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsInt8Object")]
    public void AsInt8Object() {
        TiniValue x = (sbyte)32;
        Assert.Null(x.AsBooleanObject());
        Assert.NotNull(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsInt16Object")]
    public void AsInt16Object() {
        TiniValue x = (short)32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.NotNull(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsInt32Object")]
    public void AsInt32Object() {
        TiniValue x = 32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.NotNull(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsInt64Object")]
    public void AsInt64Object() {
        TiniValue x = (long)32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.NotNull(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsUInt8Object")]
    public void AsUInt8Object() {
        TiniValue x = (byte)32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.NotNull(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsUInt16Object")]
    public void AsUInt16Object() {
        TiniValue x = (ushort)32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.NotNull(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsUInt32Object")]
    public void AsUInt32Object() {
        TiniValue x = (uint)32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.NotNull(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsUInt64Object")]
    public void AsUInt64Object() {
        TiniValue x = (ulong)32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.NotNull(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsFloat32Object")]
    public void AsFloat32Object() {
        TiniValue x = (float)32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.NotNull(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsFloat64Object")]
    public void AsFloat64Object() {
        TiniValue x = (double)32;
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.NotNull(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsStringObject")]
    public void AsStringObject() {
        TiniValue x = "A";
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.NotNull(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsBinaryObject")]
    public void AsBinaryObject() {
        TiniValue x = Array.Empty<byte>();
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.NotNull(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsDateTimeObject")]
    public void AsDateTimeObject() {
        TiniValue x = new DateTimeOffset(2005, 2, 1, 11, 43, 33, 787, new TimeSpan(2, 30, 0));
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.NotNull(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsDateTimeObject (2)")]
    public void AsDateTimeObject2() {
        TiniValue x = new DateTime(2005, 2, 1, 11, 43, 33, 787, DateTimeKind.Local);
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.NotNull(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsDateObject")]
    public void AsDateObject() {
        TiniValue x = new DateOnly(2005, 2, 1);
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.NotNull(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsTimeObject")]
    public void AsTimeObject() {
        TiniValue x = new TimeOnly(11, 43, 33, 787);
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.NotNull(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsDurationObject")]
    public void AsDurationObject() {
        TiniValue x = new TimeSpan(11, 43, 33, 787);
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.NotNull(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsIPAddressObject (1)")]
    public void AsIPAddressObject1() {
        TiniValue x = IPAddress.Parse("ff04::152");
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.NotNull(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsIPAddressObject (2)")]
    public void AsIPAddressObject2() {
        TiniValue x = IPAddress.Parse("239.192.111.17");
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.NotNull(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsIPv6AddressObject")]
    public void AsIPv6AddressObject() {
        TiniValue x = TiniValueIPv6Address.Parse("ff04::152");
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.NotNull(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsIPv4AddressObject")]
    public void AsIPv4AddressObject() {
        TiniValue x = TiniValueIPv4Address.Parse("239.192.111.17");
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.NotNull(x.AsIPv4AddressObject());
        Assert.Null(x.AsSizeObject());
    }

    [Fact(DisplayName = "TiniValue: AsSizeObject")]
    public void AsSizeObject() {
        TiniValue x = new TiniValueSize(42);
        Assert.Null(x.AsBooleanObject());
        Assert.Null(x.AsInt8Object());
        Assert.Null(x.AsInt16Object());
        Assert.Null(x.AsInt32Object());
        Assert.Null(x.AsInt64Object());
        Assert.Null(x.AsUInt8Object());
        Assert.Null(x.AsUInt16Object());
        Assert.Null(x.AsUInt32Object());
        Assert.Null(x.AsUInt64Object());
        Assert.Null(x.AsFloat32Object());
        Assert.Null(x.AsFloat64Object());
        Assert.Null(x.AsStringObject());
        Assert.Null(x.AsBinaryObject());
        Assert.Null(x.AsDateTimeObject());
        Assert.Null(x.AsDateObject());
        Assert.Null(x.AsTimeObject());
        Assert.Null(x.AsDurationObject());
        Assert.Null(x.AsIPAddressObject());
        Assert.Null(x.AsIPv6AddressObject());
        Assert.Null(x.AsIPv4AddressObject());
        Assert.NotNull(x.AsSizeObject());
    }

    #endregion AsObject


    #region AsValue

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
        Assert.Null(x.AsBinary());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
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
        Assert.Equal("2A", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
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
        Assert.Equal("002A", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
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
        Assert.Equal("0000002A", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
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
        Assert.Equal("00000000 0000002A", x.AsBinary().ToHexString());
        Assert.Equal(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.Equal(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
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
        Assert.Equal("2A", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
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
        Assert.Equal("002A", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
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
        Assert.Equal("0000002A", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
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
        Assert.Equal("00000000 0000002A", x.AsBinary().ToHexString());
        Assert.Equal(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.Equal(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
    }

    [Fact(DisplayName = "TiniValue: AsFloat32")]
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
        Assert.Equal("4228CCCD", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
    }

    [Fact(DisplayName = "TiniValue: AsFloat64")]
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
        Assert.Equal("40451999 9999999A", x.AsBinary().ToHexString());
        Assert.Equal(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 200, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.Equal(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
    }

    [Fact(DisplayName = "TiniValue: AsString")]
    public void AsString() {
        TiniValue x = "ABC";
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
        Assert.Equal("ABC", x.AsString());
        Assert.Equal("414243", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
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
        Assert.Null(x.AsBinary());
        Assert.Equal(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0)), x.AsDateTime());
        Assert.Equal(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.Equal(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
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
        Assert.Null(x.AsBinary());
        Assert.Equal(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(0, 0, 0)), x.AsDateTime());
        Assert.Equal(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.Equal(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
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
        Assert.Null(x.AsBinary());
        Assert.Equal(new DateTimeOffset(1999, 1, 2, 0, 0, 0, 0, new TimeSpan(0, 0, 0)), x.AsDateTime());
        Assert.Equal(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
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
        Assert.Equal("04:12:44.469", x.AsString());
        Assert.Null(x.AsBinary());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Equal(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
    }

    [Fact(DisplayName = "TiniValue: AsDuration")]
    public void AsDuration() {
        TiniValue x = new TimeSpan(4, 22, 12, 44, 469);
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Equal(425564, x.AsInt32());
        Assert.Equal(425564, x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Equal(425564u, x.AsUInt32());
        Assert.Equal(425564ul, x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Equal(425564.469, x.AsFloat64());
        Assert.Equal("4.22:12:44.469", x.AsString());
        Assert.Null(x.AsBinary());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Equal(new TimeSpan(4, 22, 12, 44, 469), x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
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
        Assert.Equal("FF080000 00000000 00000000 00000152", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Equal(IPAddress.Parse("ff08::152"), x.AsIPAddress());
        Assert.Equal(IPAddress.Parse("ff08::152"), x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
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
        Assert.Equal("EFC06F11", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), x.AsIPv4Address());
        Assert.Null(x.AsSize());
    }

    [Fact(DisplayName = "TiniValue: AsIPv6Address")]
    public void AsIPv6Address() {
        TiniValue x = TiniValueIPv6Address.Parse("ff08::152");
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
        Assert.Equal("FF080000 00000000 00000000 00000152", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Equal(IPAddress.Parse("ff08::152"), x.AsIPAddress());
        Assert.Equal(IPAddress.Parse("ff08::152"), x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Null(x.AsSize());
    }

    [Fact(DisplayName = "TiniValue: AsIPv4Address")]
    public void AsIPv4Address() {
        TiniValue x = TiniValueIPv4Address.Parse("239.192.111.17");
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
        Assert.Equal("EFC06F11", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Equal(IPAddress.Parse("239.192.111.17"), x.AsIPv4Address());
        Assert.Null(x.AsSize());
    }

    [Fact(DisplayName = "TiniValue: AsSize")]
    public void AsSize() {
        TiniValue x = new TiniValueSize(42);
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Null(x.AsFloat64());
        Assert.Equal("42", x.AsString());
        Assert.Equal("00000000 0000002A", x.AsBinary().ToHexString());
        Assert.Null(x.AsDateTime());
        Assert.Null(x.AsDate());
        Assert.Null(x.AsTime());
        Assert.Null(x.AsDuration());
        Assert.Null(x.AsIPAddress());
        Assert.Null(x.AsIPv6Address());
        Assert.Null(x.AsIPv4Address());
        Assert.Equal(42ul, x.AsSize());
    }

    #endregion AsValue

}
