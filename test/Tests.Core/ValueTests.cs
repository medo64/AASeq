using System;
using System.Net;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueTests {

    #region Implicit

    [Fact(DisplayName = "Value: Implicit Boolean")]
    public void ImplicitBoolean() {
        Value x = true;
        Assert.IsType<ValueBoolean>(x);
    }

    [Fact(DisplayName = "Value: Implicit Int8")]
    public void ImplicitInt8() {
        Value x = (sbyte)42;
        Assert.IsType<ValueInt8>(x);
    }

    [Fact(DisplayName = "Value: Implicit Int16")]
    public void ImplicitInt16() {
        Value x = (short)42;
        Assert.IsType<ValueInt16>(x);
    }

    [Fact(DisplayName = "Value: Implicit Int32")]
    public void ImplicitInt32() {
        Value x = 42;
        Assert.IsType<ValueInt32>(x);
    }

    [Fact(DisplayName = "Value: Implicit Int64")]
    public void ImplicitInt64() {
        Value x = (long)42;
        Assert.IsType<ValueInt64>(x);
    }

    [Fact(DisplayName = "Value: Implicit UInt8")]
    public void ImplicitUInt8() {
        Value x = (byte)42;
        Assert.IsType<ValueUInt8>(x);
    }

    [Fact(DisplayName = "Value: Implicit UInt16")]
    public void ImplicitUInt16() {
        Value x = (ushort)42;
        Assert.IsType<ValueUInt16>(x);
    }

    [Fact(DisplayName = "Value: Implicit UInt32")]
    public void ImplicitUInt32() {
        Value x = (uint)42;
        Assert.IsType<ValueUInt32>(x);
    }

    [Fact(DisplayName = "Value: Implicit UInt64")]
    public void ImplicitUInt64() {
        Value x = (ulong)42;
        Assert.IsType<ValueUInt64>(x);
    }

    [Fact(DisplayName = "Value: Implicit Float32")]
    public void ImplicitFloat32() {
        Value x = (float)42;
        Assert.IsType<ValueFloat32>(x);
    }

    [Fact(DisplayName = "Value: Implicit Float64")]
    public void ImplicitFloat64() {
        Value x = (double)42;
        Assert.IsType<ValueFloat64>(x);
    }

    [Fact(DisplayName = "Value: Implicit String")]
    public void ImplicitString() {
        Value x = "A";
        Assert.IsType<ValueString>(x);
    }

    [Fact(DisplayName = "Value: Implicit Binary (byte[])")]
    public void ImplicitBinary1() {
        Value x = new byte[] { 0x42 };
        Assert.IsType<ValueBinary>(x);
    }

    [Fact(DisplayName = "Value: Implicit Binary (ReadOnlyMemory)")]
    public void ImplicitBinary2() {
        Value x = new ReadOnlyMemory<Byte>( new byte[] { 0x42 });
        Assert.IsType<ValueBinary>(x);
    }

    [Fact(DisplayName = "Value: Implicit DateTime")]
    public void ImplicitDateTime() {
        Value x = new DateTimeOffset(1997, 4, 1, 23, 11, 54, 565, new TimeSpan(0, 0, 0));
        Assert.IsType<ValueDateTime>(x);
    }

    [Fact(DisplayName = "Value: Implicit DateTime (2)")]
    public void ImplicitDateTime2() {
        Value x = new DateTime(1997, 4, 1, 23, 11, 54, 565, DateTimeKind.Local);
        Assert.IsType<ValueDateTime>(x);
    }

    [Fact(DisplayName = "Value: Implicit Date")]
    public void ImplicitDate() {
        Value x = new DateOnly(1997, 4, 1);
        Assert.IsType<ValueDate>(x);
    }

    [Fact(DisplayName = "Value: Implicit Time")]
    public void ImplicitTime() {
        Value x = new TimeOnly(23, 11, 54, 565);
        Assert.IsType<ValueTime>(x);
    }

    [Fact(DisplayName = "Value: Implicit Duration")]
    public void ImplicitDuration() {
        Value x = new TimeSpan(23, 11, 54, 565);
        Assert.IsType<ValueDuration>(x);
    }

    [Fact(DisplayName = "Value: Implicit IPAddress")]
    public void ImplicitIPAddress() {
        Value x = IPAddress.Parse("ff08::152");
        Assert.IsType<ValueIPAddress>(x);
    }

    #endregion Implicit


    #region AsObject

    [Fact(DisplayName = "Value: AsBooleanObject")]
    public void AsObjectBoolean() {
        Value x = true;
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

    [Fact(DisplayName = "Value: AsInt8Object")]
    public void AsInt8Object() {
        Value x = (sbyte)32;
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

    [Fact(DisplayName = "Value: AsInt16Object")]
    public void AsInt16Object() {
        Value x = (short)32;
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

    [Fact(DisplayName = "Value: AsInt32Object")]
    public void AsInt32Object() {
        Value x = 32;
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

    [Fact(DisplayName = "Value: AsInt64Object")]
    public void AsInt64Object() {
        Value x = (long)32;
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

    [Fact(DisplayName = "Value: AsUInt8Object")]
    public void AsUInt8Object() {
        Value x = (byte)32;
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

    [Fact(DisplayName = "Value: AsUInt16Object")]
    public void AsUInt16Object() {
        Value x = (ushort)32;
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

    [Fact(DisplayName = "Value: AsUInt32Object")]
    public void AsUInt32Object() {
        Value x = (uint)32;
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

    [Fact(DisplayName = "Value: AsUInt64Object")]
    public void AsUInt64Object() {
        Value x = (ulong)32;
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

    [Fact(DisplayName = "Value: AsFloat32Object")]
    public void AsFloat32Object() {
        Value x = (float)32;
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

    [Fact(DisplayName = "Value: AsFloat64Object")]
    public void AsFloat64Object() {
        Value x = (double)32;
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

    [Fact(DisplayName = "Value: AsStringObject")]
    public void AsStringObject() {
        Value x = "A";
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

    [Fact(DisplayName = "Value: AsBinaryObject")]
    public void AsBinaryObject() {
        Value x = Array.Empty<byte>();
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

    [Fact(DisplayName = "Value: AsDateTimeObject")]
    public void AsDateTimeObject() {
        Value x = new DateTimeOffset(2005, 2, 1, 11, 43, 33, 787, new TimeSpan(2, 30, 0));
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

    [Fact(DisplayName = "Value: AsDateTimeObject (2)")]
    public void AsDateTimeObject2() {
        Value x = new DateTime(2005, 2, 1, 11, 43, 33, 787, DateTimeKind.Local);
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

    [Fact(DisplayName = "Value: AsDateObject")]
    public void AsDateObject() {
        Value x = new DateOnly(2005, 2, 1);
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

    [Fact(DisplayName = "Value: AsTimeObject")]
    public void AsTimeObject() {
        Value x = new TimeOnly(11, 43, 33, 787);
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

    [Fact(DisplayName = "Value: AsDurationObject")]
    public void AsDurationObject() {
        Value x = new TimeSpan(11, 43, 33, 787);
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

    [Fact(DisplayName = "Value: AsIPAddressObject (1)")]
    public void AsIPAddressObject1() {
        Value x = IPAddress.Parse("ff04::152");
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

    [Fact(DisplayName = "Value: AsIPAddressObject (2)")]
    public void AsIPAddressObject2() {
        Value x = IPAddress.Parse("239.192.111.17");
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

    [Fact(DisplayName = "Value: AsIPv6AddressObject")]
    public void AsIPv6AddressObject() {
        Value x = ValueIPv6Address.Parse("ff04::152");
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

    [Fact(DisplayName = "Value: AsIPv4AddressObject")]
    public void AsIPv4AddressObject() {
        Value x = ValueIPv4Address.Parse("239.192.111.17");
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

    [Fact(DisplayName = "Value: AsSizeObject")]
    public void AsSizeObject() {
        Value x = new ValueSize(42);
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

    [Fact(DisplayName = "Value: AsBoolean")]
    public void AsBoolean() {
        Value x = true;
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

    [Fact(DisplayName = "Value: AsInt8")]
    public void AsInt8() {
        Value x = (sbyte)42;
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

    [Fact(DisplayName = "Value: AsInt16")]
    public void AsInt16() {
        Value x = (short)42;
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

    [Fact(DisplayName = "Value: AsInt32")]
    public void AsInt32() {
        Value x = 42;
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

    [Fact(DisplayName = "Value: AsInt64")]
    public void AsInt64() {
        Value x = (long)42;
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

    [Fact(DisplayName = "Value: AsUInt8")]
    public void AsUInt8() {
        Value x = (byte)42;
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

    [Fact(DisplayName = "Value: AsUInt16")]
    public void AsUInt16() {
        Value x = (ushort)42;
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

    [Fact(DisplayName = "Value: AsUInt32")]
    public void AsUInt32() {
        Value x = (uint)42;
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

    [Fact(DisplayName = "Value: AsUInt64")]
    public void AsUInt64() {
        Value x = (ulong)42;
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

    [Fact(DisplayName = "Value: AsFloat32")]
    public void AsFloat32() {
        Value x = (float)42.2;
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

    [Fact(DisplayName = "Value: AsFloat64")]
    public void AsFloat64() {
        Value x = (double)42.2;
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

    [Fact(DisplayName = "Value: AsString")]
    public void AsString() {
        Value x = "ABC";
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

    [Fact(DisplayName = "Value: AsDateTime")]
    public void AsDateTime() {
        Value x = new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0));
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

    [Fact(DisplayName = "Value: AsDateTime (2)")]
    public void AsDateTime2() {
        Value x = new DateTime(1999, 1, 2, 4, 12, 44, 469, DateTimeKind.Utc);
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

    [Fact(DisplayName = "Value: AsDate")]
    public void AsDate() {
        Value x = new DateOnly(1999, 1, 2);
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

    [Fact(DisplayName = "Value: AsTime")]
    public void AsTime() {
        Value x = new TimeOnly(4, 12, 44, 469);
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

    [Fact(DisplayName = "Value: AsDuration")]
    public void AsDuration() {
        Value x = new TimeSpan(4, 22, 12, 44, 469);
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

    [Fact(DisplayName = "Value: AsIPAddress")]
    public void AsIPAddress() {
        Value x = IPAddress.Parse("ff08::152");
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

    [Fact(DisplayName = "Value: AsIPAddress (2)")]
    public void AsIPAddress2() {
        Value x = IPAddress.Parse("239.192.111.17");
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

    [Fact(DisplayName = "Value: AsIPv6Address")]
    public void AsIPv6Address() {
        Value x = ValueIPv6Address.Parse("ff08::152");
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

    [Fact(DisplayName = "Value: AsIPv4Address")]
    public void AsIPv4Address() {
        Value x = ValueIPv4Address.Parse("239.192.111.17");
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

    [Fact(DisplayName = "Value: AsSize")]
    public void AsSize() {
        Value x = new ValueSize(42);
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
