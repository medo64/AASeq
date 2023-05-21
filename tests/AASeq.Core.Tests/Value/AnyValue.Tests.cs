using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
[DebuggerDisplay("{ToString()}")]
public class AnyValue_Tests {

    #region Implicit

    [TestMethod]
    public void AnyValue_ImplicitBoolean() {
        AnyValue x = true;
        Assert.IsInstanceOfType(x, typeof(BooleanValue));
    }

    [TestMethod]
    public void AnyValue_ImplicitInt8() {
        AnyValue x = (sbyte)42;
        Assert.IsInstanceOfType(x, typeof(Int8Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitInt16() {
        AnyValue x = (short)42;
        Assert.IsInstanceOfType(x, typeof(Int16Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitInt32() {
        AnyValue x = 42;
        Assert.IsInstanceOfType(x, typeof(Int32Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitInt64() {
        AnyValue x = (long)42;
        Assert.IsInstanceOfType(x, typeof(Int64Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitUInt8() {
        AnyValue x = (byte)42;
        Assert.IsInstanceOfType(x, typeof(UInt8Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitUInt16() {
        AnyValue x = (ushort)42;
        Assert.IsInstanceOfType(x, typeof(UInt16Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitUInt32() {
        AnyValue x = (uint)42;
        Assert.IsInstanceOfType(x, typeof(UInt32Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitUInt64() {
        AnyValue x = (ulong)42;
        Assert.IsInstanceOfType(x, typeof(UInt64Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitFloat16() {
        AnyValue x = (Half)42;
        Assert.IsInstanceOfType(x, typeof(Float16Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitFloat32() {
        AnyValue x = (float)42;
        Assert.IsInstanceOfType(x, typeof(Float32Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitFloat64() {
        AnyValue x = (double)42;
        Assert.IsInstanceOfType(x, typeof(Float64Value));
    }

    [TestMethod]
    public void AnyValue_ImplicitDateTime() {
        AnyValue x = new DateTimeOffset(1997, 4, 1, 23, 11, 54, 565, new TimeSpan(0, 0, 0));
        Assert.IsInstanceOfType(x, typeof(DateTimeValue));
    }

    [TestMethod]
    public void AnyValue_ImplicitDateTime2() {
        AnyValue x = new DateTime(1997, 4, 1, 23, 11, 54, 565, DateTimeKind.Local);
        Assert.IsInstanceOfType(x, typeof(DateTimeValue));
    }

    [TestMethod]
    public void AnyValue_ImplicitDate() {
        AnyValue x = new DateOnly(1997, 4, 1);
        Assert.IsInstanceOfType(x, typeof(DateValue));
    }

    [TestMethod]
    public void AnyValue_ImplicitTime() {
        AnyValue x = new TimeOnly(23, 11, 54, 565);
        Assert.IsInstanceOfType(x, typeof(TimeValue));
    }

    [TestMethod]
    public void AnyValue_ImplicitDuration() {
        AnyValue x = new TimeSpan(23, 11, 54, 565);
        Assert.IsInstanceOfType(x, typeof(DurationValue));
    }

    [TestMethod]
    public void AnyValue_ImplicitString() {
        AnyValue x = "A";
        Assert.IsInstanceOfType(x, typeof(StringValue));
    }

    [TestMethod]
    public void AnyValue_ImplicitBinary1() {
        AnyValue x = "B"u8.ToArray();
        Assert.IsInstanceOfType(x, typeof(BinaryValue));
    }

    [TestMethod]
    public void AnyValue_ImplicitBinary2() {
        AnyValue x = new ReadOnlyMemory<Byte>("B"u8.ToArray());
        Assert.IsInstanceOfType(x, typeof(BinaryValue));
    }

    #endregion Implicit


    #region AsValue

    [TestMethod]
    public void AnyValue_Boolean_False() {
        AnyValue x = false;
        Assert.AreEqual(false, x.AsBoolean());
        Assert.AreEqual((sbyte)0, x.AsSByte());
        Assert.AreEqual((short)0, x.AsInt16());
        Assert.AreEqual(0, x.AsInt32());
        Assert.AreEqual(0, x.AsInt64());
        Assert.AreEqual((byte)0, x.AsByte());
        Assert.AreEqual((ushort)0, x.AsUInt16());
        Assert.AreEqual((uint)0, x.AsUInt32());
        Assert.AreEqual((ulong)0, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("False", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_Boolean_True() {
        AnyValue x = true;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)1, x.AsSByte());
        Assert.AreEqual((short)1, x.AsInt16());
        Assert.AreEqual(1, x.AsInt32());
        Assert.AreEqual(1, x.AsInt64());
        Assert.AreEqual((byte)1, x.AsByte());
        Assert.AreEqual((ushort)1, x.AsUInt16());
        Assert.AreEqual((uint)1, x.AsUInt32());
        Assert.AreEqual((ulong)1, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("True", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }


    [TestMethod]
    public void AnyValue_SByte() {
        AnyValue x = (sbyte)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42, x.AsHalf());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("2A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_SByte_Min() {
        AnyValue x = SByte.MinValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)-128, x.AsSByte());
        Assert.AreEqual((short)-128, x.AsInt16());
        Assert.AreEqual(-128, x.AsInt32());
        Assert.AreEqual(-128, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.AreEqual((Half)(-128), x.AsHalf());
        Assert.AreEqual(-128.0f, x.AsSingle());
        Assert.AreEqual(-128.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("-128", x.AsString());
        Assert.AreEqual("80", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_SByte_Max() {
        AnyValue x = SByte.MaxValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)127, x.AsSByte());
        Assert.AreEqual((short)127, x.AsInt16());
        Assert.AreEqual(127, x.AsInt32());
        Assert.AreEqual(127, x.AsInt64());
        Assert.AreEqual((byte)127, x.AsByte());
        Assert.AreEqual((ushort)127, x.AsUInt16());
        Assert.AreEqual((uint)127, x.AsUInt32());
        Assert.AreEqual((ulong)127, x.AsUInt64());
        Assert.AreEqual((Half)127, x.AsHalf());
        Assert.AreEqual(127.0f, x.AsSingle());
        Assert.AreEqual(127.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("127", x.AsString());
        Assert.AreEqual("7F", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_Int16() {
        AnyValue x = (short)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42.0, x.AsHalf());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("002A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Int16_Min() {
        AnyValue x = Int16.MinValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.AreEqual((short)(-32768), x.AsInt16());
        Assert.AreEqual(-32768, x.AsInt32());
        Assert.AreEqual(-32768, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.AreEqual((Half)(-32768), x.AsHalf());
        Assert.AreEqual(-32768f, x.AsSingle());
        Assert.AreEqual(-32768, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("-32768", x.AsString());
        Assert.AreEqual("8000", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Int16_Max() {
        AnyValue x = Int16.MaxValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.AreEqual((short)32767, x.AsInt16());
        Assert.AreEqual(32767, x.AsInt32());
        Assert.AreEqual(32767, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.AreEqual((ushort)32767, x.AsUInt16());
        Assert.AreEqual((uint)32767, x.AsUInt32());
        Assert.AreEqual((ulong)32767, x.AsUInt64());
        Assert.AreEqual((Half)32767, x.AsHalf());
        Assert.AreEqual(32767f, x.AsSingle());
        Assert.AreEqual(32767, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("32767", x.AsString());
        Assert.AreEqual("7FFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_Int32() {
        AnyValue x = 42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42.0, x.AsHalf());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("0000002A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Int32_Min() {
        AnyValue x = Int32.MinValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(-2147483648, x.AsInt32());
        Assert.AreEqual(-2147483648, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(-2147483648f, x.AsSingle());
        Assert.AreEqual(-2147483648, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1901, 12, 13, 20, 45, 52, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("-2147483648", x.AsString());
        Assert.AreEqual("80000000", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Int32_Max() {
        AnyValue x = int.MaxValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(2147483647, x.AsInt32());
        Assert.AreEqual(2147483647, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.AreEqual(2147483647U, x.AsUInt32());
        Assert.AreEqual((ulong)2147483647, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(2147483647f, x.AsSingle());
        Assert.AreEqual(2147483647, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(2038, 01, 19, 03, 14, 07, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("2147483647", x.AsString());
        Assert.AreEqual("7FFFFFFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_Int64() {
        AnyValue x = (long)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42, x.AsHalf());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("00000000 0000002A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Int64_Min() {
        AnyValue x = Int64.MinValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(-9223372036854775808, x.AsInt64());
        Assert.IsNull( x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(-9223372036854775808f, x.AsSingle());
        Assert.AreEqual(-9223372036854775808, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("-9223372036854775808", x.AsString());
        Assert.AreEqual("80000000 00000000", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Int64_Max() {
        AnyValue x = Int64.MaxValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(9223372036854775807, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual(9223372036854775807U, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(9223372036854775807.0f, x.AsSingle());
        Assert.AreEqual(9223372036854775807.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("9223372036854775807", x.AsString());
        Assert.AreEqual("7FFFFFFF FFFFFFFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_Byte() {
        AnyValue x = (byte)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42, x.AsHalf());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("2A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Byte_Min() {
        AnyValue x = Byte.MinValue;
        Assert.AreEqual(false, x.AsBoolean());
        Assert.AreEqual((sbyte)0, x.AsSByte());
        Assert.AreEqual((short)0, x.AsInt16());
        Assert.AreEqual(0, x.AsInt32());
        Assert.AreEqual(0, x.AsInt64());
        Assert.AreEqual((byte)0, x.AsByte());
        Assert.AreEqual((ushort)0, x.AsUInt16());
        Assert.AreEqual((uint)0, x.AsUInt32());
        Assert.AreEqual((ulong)0, x.AsUInt64());
        Assert.AreEqual((Half)0, x.AsHalf());
        Assert.AreEqual(0.0f, x.AsSingle());
        Assert.AreEqual(0.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("0", x.AsString());
        Assert.AreEqual("00", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Byte_Max() {
        AnyValue x = Byte.MaxValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull( x.AsSByte());
        Assert.AreEqual((short)255, x.AsInt16());
        Assert.AreEqual(255, x.AsInt32());
        Assert.AreEqual(255, x.AsInt64());
        Assert.AreEqual((byte)255, x.AsByte());
        Assert.AreEqual((ushort)255, x.AsUInt16());
        Assert.AreEqual((uint)255, x.AsUInt32());
        Assert.AreEqual((ulong)255, x.AsUInt64());
        Assert.AreEqual((Half)255, x.AsHalf());
        Assert.AreEqual(255.0f, x.AsSingle());
        Assert.AreEqual(255.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("255", x.AsString());
        Assert.AreEqual("FF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_UInt16() {
        AnyValue x = (ushort)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42, x.AsHalf());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("002A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_UInt16_Min() {
        AnyValue x = UInt16.MinValue;
        Assert.AreEqual(false, x.AsBoolean());
        Assert.AreEqual((sbyte)0, x.AsSByte());
        Assert.AreEqual((short)0, x.AsInt16());
        Assert.AreEqual(0, x.AsInt32());
        Assert.AreEqual(0, x.AsInt64());
        Assert.AreEqual((byte)0, x.AsByte());
        Assert.AreEqual((ushort)0, x.AsUInt16());
        Assert.AreEqual((uint)0, x.AsUInt32());
        Assert.AreEqual((ulong)0, x.AsUInt64());
        Assert.AreEqual((Half)0, x.AsHalf());
        Assert.AreEqual(0.0f, x.AsSingle());
        Assert.AreEqual(0.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("0", x.AsString());
        Assert.AreEqual("0000", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_UInt16_Max() {
        AnyValue x = UInt16.MaxValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(65535, x.AsInt32());
        Assert.AreEqual(65535, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.AreEqual((ushort)65535, x.AsUInt16());
        Assert.AreEqual((uint)65535, x.AsUInt32());
        Assert.AreEqual((ulong)65535, x.AsUInt64());
        Assert.AreEqual((Half)65535, x.AsHalf());
        Assert.AreEqual(65535.0f, x.AsSingle());
        Assert.AreEqual(65535.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("65535", x.AsString());
        Assert.AreEqual("FFFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_UInt32() {
        AnyValue x = (uint)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42, x.AsHalf());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("0000002A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_UInt32_Min() {
        AnyValue x = UInt32.MinValue;
        Assert.AreEqual(false, x.AsBoolean());
        Assert.AreEqual((sbyte)0, x.AsSByte());
        Assert.AreEqual((short)0, x.AsInt16());
        Assert.AreEqual(0, x.AsInt32());
        Assert.AreEqual(0, x.AsInt64());
        Assert.AreEqual((byte)0, x.AsByte());
        Assert.AreEqual((ushort)0, x.AsUInt16());
        Assert.AreEqual((uint)0, x.AsUInt32());
        Assert.AreEqual((ulong)0, x.AsUInt64());
        Assert.AreEqual((Half)0, x.AsHalf());
        Assert.AreEqual(0.0f, x.AsSingle());
        Assert.AreEqual(0.0, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 00, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("0", x.AsString());
        Assert.AreEqual("00000000", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_UInt32_Max() {
        AnyValue x = UInt32.MaxValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(4294967295, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.AreEqual(4294967295, x.AsUInt32());
        Assert.AreEqual(4294967295, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(4294967295.0f, x.AsSingle());
        Assert.AreEqual(4294967295.0, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(2106, 02, 07, 06, 28, 15, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("4294967295", x.AsString());
        Assert.AreEqual("FFFFFFFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_UInt64() {
        AnyValue x = (ulong)42;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42, x.AsHalf());
        Assert.AreEqual(42.0f, x.AsSingle());
        Assert.AreEqual(42.0, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 42, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42", x.AsString());
        Assert.AreEqual("00000000 0000002A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_UInt64_Min() {
        AnyValue x = UInt64.MinValue;
        Assert.AreEqual(false, x.AsBoolean());
        Assert.AreEqual((sbyte)0, x.AsSByte());
        Assert.AreEqual((short)0, x.AsInt16());
        Assert.AreEqual(0, x.AsInt32());
        Assert.AreEqual(0, x.AsInt64());
        Assert.AreEqual((byte)0, x.AsByte());
        Assert.AreEqual((ushort)0, x.AsUInt16());
        Assert.AreEqual((uint)0, x.AsUInt32());
        Assert.AreEqual((ulong)0, x.AsUInt64());
        Assert.AreEqual((Half)0, x.AsHalf());
        Assert.AreEqual(0.0f, x.AsSingle());
        Assert.AreEqual(0.0, x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1970, 01, 01, 00, 00, 00, 000, new TimeSpan(00, 00, 00)), x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("0", x.AsString());
        Assert.AreEqual("00000000 00000000", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_UInt64_Max() {
        AnyValue x = UInt64.MaxValue;
        Assert.AreEqual(true, x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual(18446744073709551615, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(18446744073709551615.0f, x.AsSingle());
        Assert.AreEqual(18446744073709551615.0, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("18446744073709551615", x.AsString());
        Assert.AreEqual("FFFFFFFF FFFFFFFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_Half() {
        AnyValue x = (Half)42.2;
        Assert.IsNull(x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42.1875, x.AsHalf());
        Assert.AreEqual(42.1875f, x.AsSingle());
        Assert.AreEqual(42.1875, x.AsDouble(0), 5);
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42.2", x.AsString());
        Assert.AreEqual("5146", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Half_Min() {
        AnyValue x = Half.MinValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(-65504, x.AsInt32());
        Assert.AreEqual(-65504, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull( x.AsUInt64());
        Assert.AreEqual((Half)(-65504), x.AsHalf());
        Assert.AreEqual(-65504f, x.AsSingle());
        Assert.AreEqual(-65504, x.AsDouble(0), 5);
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("-65500", x.AsString());
        Assert.AreEqual("FBFF", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Half_Max() {
        AnyValue x = Half.MaxValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(65504, x.AsInt32());
        Assert.AreEqual(65504, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.AreEqual((ushort)65504, x.AsUInt16());
        Assert.AreEqual((uint)65504, x.AsUInt32());
        Assert.AreEqual((ulong)65504, x.AsUInt64());
        Assert.AreEqual((Half)65504, x.AsHalf());
        Assert.AreEqual(65504f, x.AsSingle());
        Assert.AreEqual(65504, x.AsDouble(0), 5);
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("65500", x.AsString());
        Assert.AreEqual("7BFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_Single() {
        AnyValue x = (float)42.2;
        Assert.IsNull(x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42.2, x.AsHalf());
        Assert.AreEqual(42.2f, x.AsSingle());
        Assert.AreEqual(42.2, x.AsDouble(0), 5);
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42.2", x.AsString());
        Assert.AreEqual("4228CCCD", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Single_Min() {
        AnyValue x = Single.MinValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(-3.4028235E+38f, x.AsSingle());
        Assert.AreEqual(-3.4028234663852886E+38, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("-3.4028235E+38", x.AsString());
        Assert.AreEqual("FF7FFFFF", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Single_Max() {
        AnyValue x = Single.MaxValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(3.4028235E+38f, x.AsSingle());
        Assert.AreEqual(3.4028234663852886E+38, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("3.4028235E+38", x.AsString());
        Assert.AreEqual("7F7FFFFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_Double() {
        AnyValue x = (double)42.2;
        Assert.IsNull(x.AsBoolean());
        Assert.AreEqual((sbyte)42, x.AsSByte());
        Assert.AreEqual((short)42, x.AsInt16());
        Assert.AreEqual(42, x.AsInt32());
        Assert.AreEqual(42, x.AsInt64());
        Assert.AreEqual((byte)42, x.AsByte());
        Assert.AreEqual((ushort)42, x.AsUInt16());
        Assert.AreEqual((uint)42, x.AsUInt32());
        Assert.AreEqual((ulong)42, x.AsUInt64());
        Assert.AreEqual((Half)42.2, x.AsHalf());
        Assert.AreEqual(42.2f, x.AsSingle());
        Assert.AreEqual(42.2, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("42.2", x.AsString());
        Assert.AreEqual("40451999 9999999A", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Double_Min() {
        AnyValue x = Double.MinValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.AreEqual(-1.7976931348623157E+308, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("-1.7976931348623157E+308", x.AsString());
        Assert.AreEqual("FFEFFFFF FFFFFFFF", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_Double_Max() {
        AnyValue x = Double.MaxValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.AreEqual(1.7976931348623157E+308, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("1.7976931348623157E+308", x.AsString());
        Assert.AreEqual("7FEFFFFF FFFFFFFF", x.AsReadOnlyMemory().ToHexString());
    }


    [TestMethod]
    public void AnyValue_DateTime() {
        AnyValue x = new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0));
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(915214364, x.AsInt32());
        Assert.AreEqual(915214364, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915214364, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(10, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("1999-01-02T04:12:44+10:00", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_DateTime_Min() {
        AnyValue x = DateTimeOffset.MinValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(-62135596800, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(0001, 01, 01, 00, 00, 00, 000, new TimeSpan(0, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(0001, 01, 01), x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(00, 00, 00, 000), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("0001-01-01T00:00:00+00:00", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_DateTime_Max() {
        AnyValue x = DateTimeOffset.MaxValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(253402300799, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)253402300799, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull( x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(9999, 12, 31, 23, 59, 59, 999, new TimeSpan(0, 0, 0)).AddTicks(9999), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(9999, 12, 31), x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(23, 59, 59, 999), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("9999-12-31T23:59:59+00:00", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_DateTime2() {
        AnyValue x = new DateTime(1999, 1, 2, 4, 12, 44, 469, DateTimeKind.Utc);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(915250364, x.AsInt32());
        Assert.AreEqual(915250364, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915250364, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 4, 12, 44, 469, new TimeSpan(0, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("1999-01-02T04:12:44+00:00", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }


    [TestMethod]
    public void AnyValue_DateOnly() {
        AnyValue x = new DateOnly(1999, 1, 2);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(915235200, x.AsInt32());
        Assert.AreEqual(915235200, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)915235200, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(1999, 1, 2, 0, 0, 0, 0, new TimeSpan(0, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(1999, 1, 2), x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("1999-01-02", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_DateOnly_Min() {
        AnyValue x = DateOnly.MinValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(-62135596800, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(0001, 01, 01, 00, 00, 00, 000, new TimeSpan(0, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(0001, 01, 01), x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("0001-01-01", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_DateOnly_Max() {
        AnyValue x = DateOnly.MaxValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(253402214400, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual((ulong)253402214400, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.AreEqual(new DateTimeOffset(9999, 12, 31, 00, 00, 00, 000, new TimeSpan(0, 0, 0)), x.AsDateTimeOffset());
        Assert.AreEqual(new DateOnly(9999, 12, 31), x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("9999-12-31", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }


    [TestMethod]
    public void AnyValue_TimeOnly() {
        AnyValue x = new TimeOnly(4, 12, 44, 469);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(15164, x.AsInt32());
        Assert.AreEqual(15164, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(4, 12, 44, 469), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("04:12:44.469", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_TimeOnly_Min() {
        AnyValue x = TimeOnly.MinValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(0, x.AsInt32());
        Assert.AreEqual(0, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(00, 00, 00, 0000), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("00:00:00", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_TimeOnly_Max() {
        AnyValue x = TimeOnly.MaxValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(86399, x.AsInt32());
        Assert.AreEqual(86399, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.AreEqual(new TimeOnly(23, 59, 59, 999).Add(TimeSpan.FromTicks(9999)), x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("23:59:59.9999999", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }


    [TestMethod]
    public void AnyValue_Duration() {
        AnyValue x = new TimeSpan(4, 22, 12, 44, 469);
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.AreEqual(425564, x.AsInt32());
        Assert.AreEqual(425564, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.AreEqual(425564u, x.AsUInt32());
        Assert.AreEqual(425564ul, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(425564.469f, x.AsSingle());
        Assert.AreEqual(425564.469, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.AreEqual(new TimeSpan(4, 22, 12, 44, 469), x.AsTimeSpan());
        Assert.AreEqual("4.22:12:44.469", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }

    [TestMethod]
    public void AnyValue_Duration_Min() {
        AnyValue x = TimeSpan.MinValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(-922337203685, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(-922337203685.0f, x.AsSingle());
        Assert.AreEqual(-922337203685.4775, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.AreEqual(TimeSpan.MinValue, x.AsTimeSpan());
        Assert.AreEqual("-02:-48:-05.-4775808", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }


    [TestMethod]
    public void AnyValue_Duration_Max() {
        AnyValue x = TimeSpan.MaxValue;
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.AreEqual(922337203685, x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.AreEqual(922337203685ul, x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.AreEqual(922337203685.0f, x.AsSingle());
        Assert.AreEqual(922337203685.4775, x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.AreEqual(TimeSpan.MaxValue, x.AsTimeSpan());
        Assert.AreEqual("10675199.02:48:05.4775807", x.AsString());
        Assert.IsNull(x.AsReadOnlyMemory());
    }


    [TestMethod]
    public void AnyValue_String() {
        AnyValue x = "ABC";
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("ABC", x.AsString());
        Assert.AreEqual("414243", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_ReadOnlyMemory() {
        AnyValue x = new ReadOnlyMemory<Byte>("ABC"u8.ToArray());
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("ABC", x.AsString());
        Assert.AreEqual("414243", x.AsReadOnlyMemory().ToHexString());
    }

    [TestMethod]
    public void AnyValue_ByteArray() {
        AnyValue x = "ABC"u8.ToArray();
        Assert.IsNull(x.AsBoolean());
        Assert.IsNull(x.AsSByte());
        Assert.IsNull(x.AsInt16());
        Assert.IsNull(x.AsInt32());
        Assert.IsNull(x.AsInt64());
        Assert.IsNull(x.AsByte());
        Assert.IsNull(x.AsUInt16());
        Assert.IsNull(x.AsUInt32());
        Assert.IsNull(x.AsUInt64());
        Assert.IsNull(x.AsHalf());
        Assert.IsNull(x.AsSingle());
        Assert.IsNull(x.AsDouble());
        Assert.IsNull(x.AsDateTimeOffset());
        Assert.IsNull(x.AsDateOnly());
        Assert.IsNull(x.AsTimeOnly());
        Assert.IsNull(x.AsTimeSpan());
        Assert.AreEqual("ABC", x.AsString());
        Assert.AreEqual("414243", x.AsReadOnlyMemory().ToHexString());
    }

    #endregion AsValue

}
