using System;
using System.Net;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAValue_Tests {

    #region Implicit

    [TestMethod]
    public void AAValue_ImplicitBoolean() {
        AAValue x = true;
        Assert.IsInstanceOfType(x, typeof(AABooleanValue));
    }

    [TestMethod]
    public void AAValue_ImplicitInt8() {
        AAValue x = (sbyte)42;
        Assert.IsInstanceOfType(x, typeof(AAInt8Value));
    }

    [TestMethod]
    public void AAValue_ImplicitInt16() {
        AAValue x = (short)42;
        Assert.IsInstanceOfType(x, typeof(AAInt16Value));
    }

    [TestMethod]
    public void AAValue_ImplicitInt32() {
        AAValue x = 42;
        Assert.IsInstanceOfType(x, typeof(AAInt32Value));
    }

    [TestMethod]
    public void AAValue_ImplicitInt64() {
        AAValue x = (long)42;
        Assert.IsInstanceOfType(x, typeof(AAInt64Value));
    }

    [TestMethod]
    public void AAValue_ImplicitUInt8() {
        AAValue x = (byte)42;
        Assert.IsInstanceOfType(x, typeof(AAUInt8Value));
    }

    [TestMethod]
    public void AAValue_ImplicitUInt16() {
        AAValue x = (ushort)42;
        Assert.IsInstanceOfType(x, typeof(AAUInt16Value));
    }

    [TestMethod]
    public void AAValue_ImplicitUInt32() {
        AAValue x = (uint)42;
        Assert.IsInstanceOfType(x, typeof(AAUInt32Value));
    }

    [TestMethod]
    public void AAValue_ImplicitUInt64() {
        AAValue x = (ulong)42;
        Assert.IsInstanceOfType(x, typeof(AAUInt64Value));
    }

    [TestMethod]
    public void AAValue_ImplicitFloat32() {
        AAValue x = (float)42;
        Assert.IsInstanceOfType(x, typeof(AAFloat32Value));
    }

    [TestMethod]
    public void AAValue_ImplicitFloat64() {
        AAValue x = (double)42;
        Assert.IsInstanceOfType(x, typeof(AAFloat64Value));
    }

    [TestMethod]
    public void AAValue_ImplicitString() {
        AAValue x = "A";
        Assert.IsInstanceOfType(x, typeof(AAStringValue));
    }

    [TestMethod]
    public void AAValue_ImplicitBinary1() {
        AAValue x = new byte[] { 0x42 };
        Assert.IsInstanceOfType(x, typeof(AABinaryValue));
    }

    [TestMethod]
    public void AAValue_ImplicitBinary2() {
        AAValue x = new ReadOnlyMemory<Byte>( new byte[] { 0x42 });
        Assert.IsInstanceOfType(x, typeof(AABinaryValue));
    }

    [TestMethod]
    public void AAValue_ImplicitDateTime() {
        AAValue x = new DateTimeOffset(1997, 4, 1, 23, 11, 54, 565, new TimeSpan(0, 0, 0));
        Assert.IsInstanceOfType(x, typeof(AADateTimeValue));
    }

    [TestMethod]
    public void AAValue_ImplicitDateTime2() {
        AAValue x = new DateTime(1997, 4, 1, 23, 11, 54, 565, DateTimeKind.Local);
        Assert.IsInstanceOfType(x, typeof(AADateTimeValue));
    }

    [TestMethod]
    public void AAValue_ImplicitDate() {
        AAValue x = new DateOnly(1997, 4, 1);
        Assert.IsInstanceOfType(x, typeof(AADateValue));
    }

    [TestMethod]
    public void AAValue_ImplicitTime() {
        AAValue x = new TimeOnly(23, 11, 54, 565);
        Assert.IsInstanceOfType(x, typeof(AATimeValue));
    }

    [TestMethod]
    public void AAValue_ImplicitDuration() {
        AAValue x = new TimeSpan(23, 11, 54, 565);
        Assert.IsInstanceOfType(x, typeof(AADurationValue));
    }

    [TestMethod]
    public void AAValue_ImplicitIPAddress() {
        AAValue x = IPAddress.Parse("ff08::152");
        Assert.IsInstanceOfType(x, typeof(AAIPAddressValue));
    }

    #endregion Implicit


    #region AsObject

    [TestMethod]
    public void AAValue_AsObjectBoolean() {
        AAValue x = true;
        Assert.IsNotNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsInt8Object() {
        AAValue x = (sbyte)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNotNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsInt16Object() {
        AAValue x = (short)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNotNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsInt32Object() {
        AAValue x = 32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNotNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsInt64Object() {
        AAValue x = (long)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNotNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsUInt8Object() {
        AAValue x = (byte)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNotNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsUInt16Object() {
        AAValue x = (ushort)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNotNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsUInt32Object() {
        AAValue x = (uint)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNotNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsUInt64Object() {
        AAValue x = (ulong)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNotNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsFloat32Object() {
        AAValue x = (float)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNotNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsFloat64Object() {
        AAValue x = (double)32;
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNotNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsStringObject() {
        AAValue x = "A";
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNotNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsBinaryObject() {
        AAValue x = Array.Empty<byte>();
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNotNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsDateTimeObject() {
        AAValue x = new DateTimeOffset(2005, 2, 1, 11, 43, 33, 787, new TimeSpan(2, 30, 0));
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNotNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsDateTimeObject2() {
        AAValue x = new DateTime(2005, 2, 1, 11, 43, 33, 787, DateTimeKind.Local);
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNotNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsDateObject() {
        AAValue x = new DateOnly(2005, 2, 1);
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNotNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsTimeObject() {
        AAValue x = new TimeOnly(11, 43, 33, 787);
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNotNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsDurationObject() {
        AAValue x = new TimeSpan(11, 43, 33, 787);
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNotNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsIPAddressObject1() {
        AAValue x = IPAddress.Parse("ff04::152");
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNotNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsIPAddressObject2() {
        AAValue x = IPAddress.Parse("239.192.111.17");
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNotNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsIPv6AddressObject() {
        AAValue x = AAIPv6AddressValue.Parse("ff04::152");
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNotNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsIPv4AddressObject() {
        AAValue x = AAIPv4AddressValue.Parse("239.192.111.17");
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNotNull(x.AsIPv4AddressObject());
        Assert.IsNull(x.AsSizeObject());
    }

    [TestMethod]
    public void AAValue_AsSizeObject() {
        AAValue x = new AASizeValue(42);
        Assert.IsNull(x.AsBooleanObject());
        Assert.IsNull(x.AsInt8Object());
        Assert.IsNull(x.AsInt16Object());
        Assert.IsNull(x.AsInt32Object());
        Assert.IsNull(x.AsInt64Object());
        Assert.IsNull(x.AsUInt8Object());
        Assert.IsNull(x.AsUInt16Object());
        Assert.IsNull(x.AsUInt32Object());
        Assert.IsNull(x.AsUInt64Object());
        Assert.IsNull(x.AsFloat32Object());
        Assert.IsNull(x.AsFloat64Object());
        Assert.IsNull(x.AsStringObject());
        Assert.IsNull(x.AsBinaryObject());
        Assert.IsNull(x.AsDateTimeObject());
        Assert.IsNull(x.AsDateObject());
        Assert.IsNull(x.AsTimeObject());
        Assert.IsNull(x.AsDurationObject());
        Assert.IsNull(x.AsIPAddressObject());
        Assert.IsNull(x.AsIPv6AddressObject());
        Assert.IsNull(x.AsIPv4AddressObject());
        Assert.IsNotNull(x.AsSizeObject());
    }

    #endregion AsObject


    #region AsValue

    [TestMethod]
    public void AAValue_AsBoolean() {
        AAValue x = true;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)1, x.AsInt8());
        Assert.AreEqual((short)1, x.AsInt16());
        Assert.AreEqual(1, x.AsInt32());
        Assert.AreEqual(1, x.AsInt64());
        Assert.AreEqual((byte)1, x.AsUInt8());
        Assert.AreEqual((ushort)1, x.AsUInt16());
        Assert.AreEqual((uint)1, x.AsUInt32());
        Assert.AreEqual((ulong)1, x.AsUInt64());
        Assert.AreEqual(1.0f, x.AsFloat32());
        Assert.AreEqual(1.0, x.AsFloat64());
        Assert.AreEqual("True", x.AsString());
        Assert.IsNull(x.AsBinary());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsInt8() {
        AAValue x = (sbyte)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsFloat32());
        Assert.AreEqual(42.0, x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("2A", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsInt16() {
        AAValue x = (short)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsFloat32());
        Assert.AreEqual(42.0, x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("002A", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsInt32() {
        AAValue x = 42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsFloat32());
        Assert.AreEqual(42.0, x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("0000002A", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsInt64() {
        AAValue x = (long)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsFloat32());
        Assert.AreEqual(42.0, x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("00000000 0000002A", x.AsBinary().ToHexString());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.AreEqual(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsUInt8() {
        AAValue x = (byte)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsFloat32());
        Assert.AreEqual(42.0, x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("2A", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsUInt16() {
        AAValue x = (ushort)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsFloat32());
        Assert.AreEqual(42.0, x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("002A", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsUInt32() {
        AAValue x = (uint)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsFloat32());
        Assert.AreEqual(42.0, x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("0000002A", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsUInt64() {
        AAValue x = (ulong)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsFloat32());
        Assert.AreEqual(42.0, x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("00000000 0000002A", x.AsBinary().ToHexString());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.AreEqual(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsFloat32() {
        AAValue x = (float)42.2;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.2f, x.AsFloat32());
        Assert.AreEqual(42.2, x.AsFloat64(0), 5);
        Assert.AreEqual("42.2", x.AsString());
        Assert.AreEqual("4228CCCD", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsFloat64() {
        AAValue x = (double)42.2;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.2f, x.AsFloat32());
        Assert.AreEqual(42.2, x.AsFloat64());
        Assert.AreEqual("42.2", x.AsString());
        Assert.AreEqual("40451999 9999999A", x.AsBinary().ToHexString());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 200, new TimeSpan(00, 00, 00)), x.AsDateTime());
        Assert.AreEqual(new DateOnly(1970, 01, 01), x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsString() {
        AAValue x = "ABC";
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.IsNull(x.AsFloat64());
        Assert.AreEqual("ABC", x.AsString());
        Assert.AreEqual("414243", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsDateTime() {
        AAValue x = new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0));
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(915214364, x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915214364, x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.AreEqual(915214364.469, x.AsFloat64());
        Assert.AreEqual("1999-01-02T04:12:44+10:00", x.AsString());
        Assert.IsNull(x.AsBinary());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0)), x.AsDateTime());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsDateTime2() {
        AAValue x = new DateTime(1999, 1, 2, 4, 12, 44, 469, DateTimeKind.Utc);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(915250364, x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915250364, x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.AreEqual(915250364.469, x.AsFloat64());
        Assert.AreEqual("1999-01-02T04:12:44+00:00", x.AsString());
        Assert.IsNull(x.AsBinary());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(0, 0, 0)), x.AsDateTime());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsDate() {
        AAValue x = new DateOnly(1999, 1, 2);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(915235200, x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915235200, x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.AreEqual(915235200, x.AsFloat64());
        Assert.AreEqual("1999-01-02", x.AsString());
        Assert.IsNull(x.AsBinary());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 0, 0, 0, 0, new TimeSpan(0, 0, 0)), x.AsDateTime());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsTime() {
        AAValue x = new TimeOnly(4, 12, 44, 469);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.IsNull(x.AsFloat64());
        Assert.AreEqual("04:12:44.469", x.AsString());
        Assert.IsNull(x.AsBinary());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsDuration() {
        AAValue x = new TimeSpan(4, 22, 12, 44, 469);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(425564, x.AsInt32());
        Assert.AreEqual(425564, x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.AreEqual(425564u, x.AsUInt32());
        Assert.AreEqual(425564ul, x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.AreEqual(425564.469, x.AsFloat64());
        Assert.AreEqual("4.22:12:44.469", x.AsString());
        Assert.IsNull(x.AsBinary());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.AreEqual(new TimeSpan(4, 22, 12, 44, 469), x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsIPAddress() {
        AAValue x = IPAddress.Parse("ff08::152");
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.IsNull(x.AsFloat64());
        Assert.AreEqual("ff08::152", x.AsString());
        Assert.AreEqual("FF080000 00000000 00000000 00000152", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.AreEqual(IPAddress.Parse("ff08::152"), x.AsIPAddress());
        Assert.AreEqual(IPAddress.Parse("ff08::152"), x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsIPAddress2() {
        AAValue x = IPAddress.Parse("239.192.111.17");
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.IsNull(x.AsFloat64());
        Assert.AreEqual("239.192.111.17", x.AsString());
        Assert.AreEqual("EFC06F11", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.AreEqual(IPAddress.Parse("239.192.111.17"), x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.AreEqual(IPAddress.Parse("239.192.111.17"), x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsIPv6Address() {
        AAValue x = AAIPv6AddressValue.Parse("ff08::152");
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.IsNull(x.AsFloat64());
        Assert.AreEqual("ff08::152", x.AsString());
        Assert.AreEqual("FF080000 00000000 00000000 00000152", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.AreEqual(IPAddress.Parse("ff08::152"), x.AsIPAddress());
        Assert.AreEqual(IPAddress.Parse("ff08::152"), x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsIPv4Address() {
        AAValue x = AAIPv4AddressValue.Parse("239.192.111.17");
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsInt8());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsUInt8());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.IsNull(x.AsFloat64());
        Assert.AreEqual("239.192.111.17", x.AsString());
        Assert.AreEqual("EFC06F11", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.AreEqual(IPAddress.Parse("239.192.111.17"), x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.AreEqual(IPAddress.Parse("239.192.111.17"), x.AsIPv4Address());
        Assert.IsNull(x.AsSize());
    }

    [TestMethod]
    public void AAValue_AsSize() {
        AAValue x = new AASizeValue(42);
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsInt8());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsUInt8());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.IsNull(x.AsFloat32());
        Assert.IsNull(x.AsFloat64());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("00000000 0000002A", x.AsBinary().ToHexString());
        Assert.IsNull(x.AsDateTime());
        Assert.IsNull(x.AsDate());
        Assert.IsNull(x.AsTime());
        Assert.IsNull(x.AsDuration());
        Assert.IsNull(x.AsIPAddress());
        Assert.IsNull(x.AsIPv6Address());
        Assert.IsNull(x.AsIPv4Address());
        Assert.AreEqual(42ul, x.AsSize());
    }

    #endregion AsValue

}
