using System;
using System.Diagnostics;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
[DebuggerDisplay("{ToString()}")]
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


    #region AsValue

    [TestMethod]
    public void AAValue_Boolean() {
        AAValue x = true;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)1, x.AsSByte());
        Assert.AreEqual((short)1, x.AsInt16());
        Assert.AreEqual(1, x.AsInt32());
        Assert.AreEqual(1, x.AsInt64());
        Assert.AreEqual((byte)1, x.AsByte());
        Assert.AreEqual((ushort)1, x.AsUInt16());
        Assert.AreEqual((uint)1, x.AsUInt32());
        Assert.AreEqual((ulong)1, x.AsUInt64());
        Assert.AreEqual(1.0f, x.AsSingle());
        Assert.AreEqual(1.0, x.AsDouble());
        Assert.AreEqual("True", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_SByte() {
        AAValue x = (sbyte)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("2A", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_Int16() {
        AAValue x = (short)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("002A", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_Int32() {
        AAValue x = 42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("0000002A", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_Int64() {
        AAValue x = (long)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("00000000 0000002A", x.AsReadOnlyMemory().ToHexString());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1970, 01, 01), x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_Byte() {
        AAValue x = (byte)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("2A", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_UInt16() {
        AAValue x = (ushort)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("002A", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_UInt32() {
        AAValue x = (uint)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("0000002A", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_UInt64() {
        AAValue x = (ulong)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("00000000 0000002A", x.AsReadOnlyMemory().ToHexString());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1970, 01, 01), x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_Single() {
        AAValue x = (float)42.2;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.2f, x.AsSingle());
        Assert.AreEqual(42.2, x.AsDouble(0), 5);
        Assert.AreEqual("42.2", x.AsString());
        Assert.AreEqual("4228CCCD", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_Double() {
        AAValue x = (double)42.2;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual(42.2f, x.AsSingle());
        Assert.AreEqual(42.2, x.AsDouble());
        Assert.AreEqual("42.2", x.AsString());
        Assert.AreEqual("40451999 9999999A", x.AsReadOnlyMemory().ToHexString());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 200, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1970, 01, 01), x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_String() {
        AAValue x = "ABC";
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual("ABC", x.AsString());
        Assert.AreEqual("414243", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_DateTime() {
        AAValue x = new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0));
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(915214364, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915214364, x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.AreEqual(915214364.469, x.AsDouble());
        Assert.AreEqual("1999-01-02T04:12:44+10:00", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_DateTime2() {
        AAValue x = new DateTime(1999, 1, 2, 4, 12, 44, 469, DateTimeKind.Utc);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(915250364, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915250364, x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.AreEqual(915250364.469, x.AsDouble());
        Assert.AreEqual("1999-01-02T04:12:44+00:00", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(0, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_DateOnly() {
        AAValue x = new DateOnly(1999, 1, 2);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(915235200, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915235200, x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.AreEqual(915235200, x.AsDouble());
        Assert.AreEqual("1999-01-02", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 0, 0, 0, 0, new TimeSpan(0, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_TimeOnly() {
        AAValue x = new TimeOnly(4, 12, 44, 469);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual("04:12:44.469", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_Duration() {
        AAValue x = new TimeSpan(4, 22, 12, 44, 469);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(425564, x.AsInt32());
        Assert.AreEqual(425564, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.AreEqual(425564u, x.AsUInt32());
        Assert.AreEqual(425564ul, x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.AreEqual(425564.469, x.AsDouble());
        Assert.AreEqual("4.22:12:44.469", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.AreEqual(new TimeSpan(4, 22, 12, 44, 469), x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_IPAddress() {
        AAValue x = IPAddress.Parse("ff08::152");
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual("ff08::152", x.AsString());
        Assert.AreEqual("FF080000 00000000 00000000 00000152", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual(IPAddress.Parse("ff08::152"), x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_IPAddress2() {
        AAValue x = IPAddress.Parse("239.192.111.17");
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual("239.192.111.17", x.AsString());
        Assert.AreEqual("EFC06F11", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual(IPAddress.Parse("239.192.111.17"), x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_AsIPv6Address() {
        AAValue x = AAIPv6AddressValue.Parse("ff08::152");
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual("ff08::152", x.AsString());
        Assert.AreEqual("FF080000 00000000 00000000 00000152", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual(IPAddress.Parse("ff08::152"), x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_IPv4Address() {
        AAValue x = AAIPv4AddressValue.Parse("239.192.111.17");
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual("239.192.111.17", x.AsString());
        Assert.AreEqual("EFC06F11", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual(IPAddress.Parse("239.192.111.17"), x.AsIPAddress());
    }

    [TestMethod]
    public void AAValue_Size() {
        AAValue x = new AASizeValue(42);
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("00000000 0000002A", x.AsReadOnlyMemory().ToHexString());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.IsNull(x.AsIPAddress());
    }

    #endregion AsValue

}
