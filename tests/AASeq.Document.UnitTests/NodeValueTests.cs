namespace Tests;
using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;
using System.Text.RegularExpressions;

[TestClass]
public sealed class NodeValueTests {

    [TestMethod]
    public void NodeValue_Null() {
        var data = new AASeqNode("node");
        Assert.IsNull(data.Value.RawValue);

        Assert.IsNull(data.Value.AsString());
        Assert.AreEqual("Default", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_I8() {
        var data = new AASeqNode("node", (sbyte)42);
        Assert.AreEqual((sbyte)42, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.0, data.Value.AsHalf());
        Assert.AreEqual((Half)42.0, data.Value.AsHalf(-28));
        Assert.AreEqual(42.0f, data.Value.AsSingle());
        Assert.AreEqual(42.0f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.0, data.Value.AsDouble());
        Assert.AreEqual(42.0, data.Value.AsDouble(-28));
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal());
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_U8() {
        var data = new AASeqNode("node", (byte)42);
        Assert.AreEqual((byte)42, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_I16() {
        var data = new AASeqNode("node", (short)42);
        Assert.AreEqual((short)42, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.0, data.Value.AsHalf());
        Assert.AreEqual((Half)42.0, data.Value.AsHalf(-28));
        Assert.AreEqual(42.0f, data.Value.AsSingle());
        Assert.AreEqual(42.0f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.0, data.Value.AsDouble());
        Assert.AreEqual(42.0, data.Value.AsDouble(-28));
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal());
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_U16() {
        var data = new AASeqNode("node", (ushort)42);
        Assert.AreEqual((ushort)42, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_I32() {
        var data = new AASeqNode("node", (int)42);
        Assert.AreEqual(42, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual((sbyte)42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.0, data.Value.AsHalf());
        Assert.AreEqual((Half)42.0, data.Value.AsHalf(-28));
        Assert.AreEqual(42.0f, data.Value.AsSingle());
        Assert.AreEqual(42.0f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.0, data.Value.AsDouble());
        Assert.AreEqual(42.0, data.Value.AsDouble(-28));
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal());
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal(28));

        Assert.AreEqual(new DateTimeOffset(1970, 1, 1, 0, 0, 42, TimeSpan.Zero), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1970, 1, 1, 0, 0, 42, TimeSpan.Zero), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_U32() {
        var data = new AASeqNode("node", (uint)42);
        Assert.AreEqual(42U, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual(new DateTimeOffset(1970, 1, 1, 0, 0, 42, TimeSpan.Zero), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1970, 1, 1, 0, 0, 42, TimeSpan.Zero), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_I64() {
        var data = new AASeqNode("node", (long)42);
        Assert.AreEqual(42L, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual((sbyte)42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.0, data.Value.AsHalf());
        Assert.AreEqual((Half)42.0, data.Value.AsHalf(-28));
        Assert.AreEqual(42.0f, data.Value.AsSingle());
        Assert.AreEqual(42.0f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.0, data.Value.AsDouble());
        Assert.AreEqual(42.0, data.Value.AsDouble(-28));
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal());
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal(28));

        Assert.AreEqual(new DateTimeOffset(1970, 1, 1, 0, 0, 42, TimeSpan.Zero), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1970, 1, 1, 0, 0, 42, TimeSpan.Zero), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_U64() {
        var data = new AASeqNode("node", (ulong)42);
        Assert.AreEqual(42UL, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual(new DateTimeOffset(1970, 1, 1, 0, 0, 42, TimeSpan.Zero), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1970, 1, 1, 0, 0, 42, TimeSpan.Zero), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_I128() {
        var data = new AASeqNode("node", (Int128)42);
        Assert.AreEqual((Int128)42, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.0, data.Value.AsHalf());
        Assert.AreEqual((Half)42.0, data.Value.AsHalf(-28));
        Assert.AreEqual(42.0f, data.Value.AsSingle());
        Assert.AreEqual(42.0f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.0, data.Value.AsDouble());
        Assert.AreEqual(42.0, data.Value.AsDouble(-28));
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal());
        Assert.AreEqual((Decimal)42, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_U128() {
        var data = new AASeqNode("node", (UInt128)42);
        Assert.AreEqual((UInt128)42, data.Value.RawValue);

        Assert.AreEqual("42", data.Value.AsString());
        Assert.AreEqual("42", data.Value.AsString("Default"));

        Assert.IsTrue(data.Value.AsBoolean());
        Assert.IsTrue(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_F16() {
        var data = new AASeqNode("node", (Half)42.4);
        Assert.AreEqual((Half)42.4, data.Value.RawValue);

        Assert.AreEqual("42.4", data.Value.AsString());
        Assert.AreEqual("42.4", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.4, data.Value.AsHalf());
        Assert.AreEqual((Half)42.4, data.Value.AsHalf(-28));
        Assert.AreEqual(42.40625f, data.Value.AsSingle());
        Assert.AreEqual(42.40625f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.40625, data.Value.AsDouble());
        Assert.AreEqual(42.40625, data.Value.AsDouble(-28));
        Assert.AreEqual(42.40625M, data.Value.AsDecimal());
        Assert.AreEqual(42.40625M, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42.40625), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42.40625), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_F32() {
        var data = new AASeqNode("node", (float)42.42);
        Assert.AreEqual(42.42f, data.Value.RawValue);

        Assert.AreEqual("42.42", data.Value.AsString());
        Assert.AreEqual("42.42", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.4, data.Value.AsHalf());
        Assert.AreEqual((Half)42.4, data.Value.AsHalf(-28));
        Assert.AreEqual(42.42f, data.Value.AsSingle());
        Assert.AreEqual(42.42f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.41999816894531, data.Value.AsDouble());
        Assert.AreEqual(42.41999816894531, data.Value.AsDouble(-28));
        Assert.AreEqual(42.42M, data.Value.AsDecimal());
        Assert.AreEqual(42.42M, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42.4199981), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42.4199981), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_F64() {
        var data = new AASeqNode("node", (double)42.4242);
        Assert.AreEqual(42.4242, data.Value.RawValue);

        Assert.AreEqual("42.4242", data.Value.AsString());
        Assert.AreEqual("42.4242", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.44, data.Value.AsHalf());
        Assert.AreEqual((Half)42.44, data.Value.AsHalf(-28));
        Assert.AreEqual(42.4242f, data.Value.AsSingle());
        Assert.AreEqual(42.4242f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.4242, data.Value.AsDouble());
        Assert.AreEqual(42.4242, data.Value.AsDouble(-28));
        Assert.AreEqual(42.4242M, data.Value.AsDecimal());
        Assert.AreEqual(42.4242M, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42.4242), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42.4242), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_Decimal128() {
        var data = new AASeqNode("node", (decimal)42.4242);
        Assert.AreEqual(42.4242M, data.Value.RawValue);

        Assert.AreEqual("42.4242", data.Value.AsString());
        Assert.AreEqual("42.4242", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.AreEqual((sbyte)42, data.Value.AsSByte());
        Assert.AreEqual(42, data.Value.AsSByte(-28));
        Assert.AreEqual((byte)42, data.Value.AsByte());
        Assert.AreEqual(42, data.Value.AsByte(28));
        Assert.AreEqual((short)42, data.Value.AsInt16());
        Assert.AreEqual(42, data.Value.AsInt16(-28));
        Assert.AreEqual((ushort)42, data.Value.AsUInt16());
        Assert.AreEqual(42, data.Value.AsUInt16(28));
        Assert.AreEqual(42, data.Value.AsInt32());
        Assert.AreEqual(42, data.Value.AsInt32(-28));
        Assert.AreEqual(42U, data.Value.AsUInt32());
        Assert.AreEqual(42U, data.Value.AsUInt32(28));
        Assert.AreEqual(42L, data.Value.AsInt64());
        Assert.AreEqual(42L, data.Value.AsInt64(-28));
        Assert.AreEqual(42UL, data.Value.AsUInt64());
        Assert.AreEqual(42UL, data.Value.AsUInt64(28));
        Assert.AreEqual((Int128)42, data.Value.AsInt128());
        Assert.AreEqual((Int128)42, data.Value.AsInt128(-28));
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128());
        Assert.AreEqual((UInt128)42, data.Value.AsUInt128(28));

        Assert.AreEqual((Half)42.44, data.Value.AsHalf());
        Assert.AreEqual((Half)42.44, data.Value.AsHalf(-28));
        Assert.AreEqual(42.4242f, data.Value.AsSingle());
        Assert.AreEqual(42.4242f, data.Value.AsSingle(-28));
        Assert.AreEqual(42.4242, data.Value.AsDouble());
        Assert.AreEqual(42.4242, data.Value.AsDouble(-28));
        Assert.AreEqual(42.4242M, data.Value.AsDecimal());
        Assert.AreEqual(42.4242M, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(TimeSpan.FromSeconds(42.4242), data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(42.4242), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_DateTimeOffset() {
        var value = new DateTimeOffset(1969, 07, 20, 20, 17, 00, new TimeSpan(0, 0, 0));
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("1969-07-20T20:17:00+00:00", data.Value.AsString());
        Assert.AreEqual("1969-07-20T20:17:00+00:00", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.AreEqual(-14182980, data.Value.AsInt32());
        Assert.AreEqual(-14182980, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.AreEqual(-14182980L, data.Value.AsInt64());
        Assert.AreEqual(-14182980L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(-14182980.0, data.Value.AsDouble());
        Assert.AreEqual(-14182980.0, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 20, 17, 00, TimeSpan.Zero), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 20, 17, 00, TimeSpan.Zero), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly());
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(20, 17, 00), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(20, 17, 00), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_DateTimeOffset_MS() {
        var value = new DateTimeOffset(1969, 07, 20, 20, 17, 00, new TimeSpan(0, 0, 0)).AddMilliseconds(1);
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("1969-07-20T20:17:00.001+00:00", data.Value.AsString());
        Assert.AreEqual("1969-07-20T20:17:00.001+00:00", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.AreEqual(-14182980, data.Value.AsInt32());
        Assert.AreEqual(-14182980, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.AreEqual(-14182980L, data.Value.AsInt64());
        Assert.AreEqual(-14182980L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(-14182980.0, data.Value.AsDouble());
        Assert.AreEqual(-14182980.0, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 20, 17, 00, 001, TimeSpan.Zero), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 20, 17, 00, 001, TimeSpan.Zero), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly());
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(20, 17, 00, 001), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(20, 17, 00, 001), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_DateTimeOffset_US() {
        var value = new DateTimeOffset(1969, 07, 20, 20, 17, 00, new TimeSpan(0, 0, 0)).AddMicroseconds(1);
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("1969-07-20T20:17:00.000001+00:00", data.Value.AsString());
        Assert.AreEqual("1969-07-20T20:17:00.000001+00:00", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.AreEqual(-14182980, data.Value.AsInt32());
        Assert.AreEqual(-14182980, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.AreEqual(-14182980L, data.Value.AsInt64());
        Assert.AreEqual(-14182980L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(-14182980.0, data.Value.AsDouble());
        Assert.AreEqual(-14182980.0, data.Value.AsDouble(-28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 20, 17, 00, 000, 001, TimeSpan.Zero), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 20, 17, 00, 000, 001, TimeSpan.Zero), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly());
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(20, 17, 00, 000, 001), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(20, 17, 00, 000, 001), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_DateTimeOffset_NS() {
        var value = new DateTimeOffset(1969, 07, 20, 20, 17, 00, new TimeSpan(0, 0, 0)).AddTicks(1);
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("1969-07-20T20:17:00.0000001+00:00", data.Value.AsString());
        Assert.AreEqual("1969-07-20T20:17:00.0000001+00:00", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.AreEqual(-14182980, data.Value.AsInt32());
        Assert.AreEqual(-14182980, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.AreEqual(-14182980L, data.Value.AsInt64());
        Assert.AreEqual(-14182980L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(-14182980.0, data.Value.AsDouble());
        Assert.AreEqual(-14182980.0, data.Value.AsDouble(-28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 20, 17, 00, TimeSpan.Zero).AddTicks(1), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 20, 17, 00, TimeSpan.Zero).AddTicks(1), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly());
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(new TimeOnly(20, 17, 00).Ticks + 1), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(new TimeOnly(20, 17, 00).Ticks + 1), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_DateTimeOffset_TZ() {
        var value = new DateTimeOffset(1969, 07, 20, 14, 17, 00, new TimeSpan(-6, 0, 0));
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("1969-07-20T14:17:00-06:00", data.Value.AsString());
        Assert.AreEqual("1969-07-20T14:17:00-06:00", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.AreEqual(-14182980, data.Value.AsInt32());
        Assert.AreEqual(-14182980, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.AreEqual(-14182980L, data.Value.AsInt64());
        Assert.AreEqual(-14182980L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(-14182980L, data.Value.AsDouble());
        Assert.AreEqual(-14182980L, data.Value.AsDouble(-28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 14, 17, 00, new TimeSpan(-6, 0, 0)), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 14, 17, 00, new TimeSpan(-6, 0, 0)), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly());
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(14, 17, 00), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(14, 17, 00), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_DateOnly() {
        var value = new DateOnly(1969, 07, 20);
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("1969-07-20", data.Value.AsString());
        Assert.AreEqual("1969-07-20", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 0, 0, 0, TimeSpan.Zero), data.Value.AsDateTimeOffset());
        Assert.AreEqual(new DateTimeOffset(1969, 07, 20, 0, 0, 0, TimeSpan.Zero), data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly());
        Assert.AreEqual(new DateOnly(1969, 07, 20), data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_TimeOnly() {
        var value = new TimeOnly(20, 17, 00);
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("20:17:00", data.Value.AsString());
        Assert.AreEqual("20:17:00", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(20, 17, 00), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(20, 17, 00), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_TimeOnly_MS() {
        var value = new TimeOnly(20, 17, 00).Add(new TimeSpan(0, 0, 0, 0, 1));
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("20:17:00.001", data.Value.AsString());
        Assert.AreEqual("20:17:00.001", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(20, 17, 00, 001), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(20, 17, 00, 001), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_TimeOnly_US() {
        var value = new TimeOnly(20, 17, 00).Add(new TimeSpan(0, 0, 0, 0, 0, 1));
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("20:17:00.000001", data.Value.AsString());
        Assert.AreEqual("20:17:00.000001", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(20, 17, 00, 000, 001), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(20, 17, 00, 000, 001), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_TimeOnly_NS() {
        var value = new TimeOnly(20, 17, 00).Add(new TimeSpan(1));
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("20:17:00.0000001", data.Value.AsString());
        Assert.AreEqual("20:17:00.0000001", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.AreEqual(new TimeOnly(new TimeOnly(20, 17, 00).Ticks + 1), data.Value.AsTimeOnly());
        Assert.AreEqual(new TimeOnly(new TimeOnly(20, 17, 00).Ticks + 1), data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_Duration() {
        var value = new TimeSpan(21, 36, 0);
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("21h 36m", data.Value.AsString());
        Assert.AreEqual("21h 36m", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(77760.0, data.Value.AsDouble());
        Assert.AreEqual(77760.0, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(new TimeSpan(21, 36, 00), data.Value.AsTimeSpan());
        Assert.AreEqual(new TimeSpan(21, 36, 00), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_Duration_MS() {
        var value = new TimeSpan(21, 36, 0).Add(new TimeSpan(0, 00, 00, 00, 001));
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("21h 36m 0.001s", data.Value.AsString());
        Assert.AreEqual("21h 36m 0.001s", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(77760.001, data.Value.AsDouble());
        Assert.AreEqual(77760.001, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(new TimeSpan(0, 21, 36, 00, 001), data.Value.AsTimeSpan());
        Assert.AreEqual(new TimeSpan(0, 21, 36, 00, 001), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_Duration_US() {
        var value = new TimeSpan(21, 36, 0).Add(new TimeSpan(0, 00, 00, 00, 000, 001));
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("21h 36m 0.000001s", data.Value.AsString());
        Assert.AreEqual("21h 36m 0.000001s", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(77760.000001, data.Value.AsDouble());
        Assert.AreEqual(77760.000001, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(new TimeSpan(0, 21, 36, 00, 000, 001), data.Value.AsTimeSpan());
        Assert.AreEqual(new TimeSpan(0, 21, 36, 00, 000, 001), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_Duration_NS() {
        var value = new TimeSpan(21, 36, 0).Add(new TimeSpan(1));
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("21h 36m 0.0000001s", data.Value.AsString());
        Assert.AreEqual("21h 36m 0.0000001s", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.AreEqual(77760.0000001, data.Value.AsDouble());
        Assert.AreEqual(77760.0000001, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.AreEqual(new TimeSpan(new TimeSpan(21, 36, 00).Ticks + 1), data.Value.AsTimeSpan());
        Assert.AreEqual(new TimeSpan(new TimeSpan(21, 36, 00).Ticks + 1), data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_IPv4() {
        var value = IPAddress.Parse("1.2.3.4");
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("1.2.3.4", data.Value.AsString());
        Assert.AreEqual("1.2.3.4", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.AreEqual(IPAddress.Parse("1.2.3.4"), data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Parse("1.2.3.4"), data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }

    [TestMethod]
    public void NodeValue_IPv6() {
        var value = IPAddress.Parse("1:2:3:4:5678::9");
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("1:2:3:4:5678::9", data.Value.AsString());
        Assert.AreEqual("1:2:3:4:5678::9", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.AreEqual(IPAddress.Parse("1:2:3:4:5678::9"), data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Parse("1:2:3:4:5678::9"), data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_Url() {
        var value = new Uri("https://aaseq.com");
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("https://aaseq.com/", data.Value.AsString());
        Assert.AreEqual("https://aaseq.com/", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://example.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_Uuid() {
        var value = Guid.Parse("3CF8DFD4-83D9-4D47-AF93-1C3FE190E3C8");
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("3cf8dfd4-83d9-4d47-af93-1c3fe190e3c8", data.Value.AsString());
        Assert.AreEqual("3cf8dfd4-83d9-4d47-af93-1c3fe190e3c8", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.AreEqual(Guid.Parse("3CF8DFD4-83D9-4D47-AF93-1C3FE190E3C8"), data.Value.AsGuid());
        Assert.AreEqual(Guid.Parse("3CF8DFD4-83D9-4D47-AF93-1C3FE190E3C8"), data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_Base64() {
        var value = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        var data = new AASeqNode("node", value);
        Assert.AreEqual(value, data.Value.RawValue);

        Assert.AreEqual("0102030405060708090a0b0c0d0e0f1011", data.Value.AsString());
        Assert.AreEqual("0102030405060708090a0b0c0d0e0f1011", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_String() {
        var data = new AASeqNode("node", "Test");
        Assert.AreEqual("Test", data.Value.RawValue);

        Assert.AreEqual("Test", data.Value.AsString());
        Assert.AreEqual("Test", data.Value.AsString("Default"));

        Assert.IsNull(data.Value.AsBoolean());
        Assert.IsFalse(data.Value.AsBoolean(false));
        Assert.IsNull(data.Value.AsSByte());
        Assert.AreEqual(-28, data.Value.AsSByte(-28));
        Assert.IsNull(data.Value.AsByte());
        Assert.AreEqual(28, data.Value.AsByte(28));
        Assert.IsNull(data.Value.AsInt16());
        Assert.AreEqual(-28, data.Value.AsInt16(-28));
        Assert.IsNull(data.Value.AsUInt16());
        Assert.AreEqual(28, data.Value.AsUInt16(28));
        Assert.IsNull(data.Value.AsInt32());
        Assert.AreEqual(-28, data.Value.AsInt32(-28));
        Assert.IsNull(data.Value.AsUInt32());
        Assert.AreEqual(28U, data.Value.AsUInt32(28));
        Assert.IsNull(data.Value.AsInt64());
        Assert.AreEqual(-28L, data.Value.AsInt64(-28));
        Assert.IsNull(data.Value.AsUInt64());
        Assert.AreEqual(28UL, data.Value.AsUInt64(28));
        Assert.IsNull(data.Value.AsInt128());
        Assert.AreEqual((Int128)(-28), data.Value.AsInt128(-28));
        Assert.IsNull(data.Value.AsUInt128());
        Assert.AreEqual((UInt128)28, data.Value.AsUInt128(28));

        Assert.IsNull(data.Value.AsHalf());
        Assert.AreEqual((Half)28, data.Value.AsHalf((Half)28));
        Assert.IsNull(data.Value.AsSingle());
        Assert.AreEqual((Single)28, data.Value.AsSingle(28));
        Assert.IsNull(data.Value.AsDouble());
        Assert.AreEqual(28, data.Value.AsDouble(28));
        Assert.IsNull(data.Value.AsDecimal());
        Assert.AreEqual((Decimal)28, data.Value.AsDecimal(28));

        Assert.IsNull(data.Value.AsDateTimeOffset());
        Assert.AreEqual(DateTimeOffset.UnixEpoch, data.Value.AsDateTimeOffset(DateTimeOffset.UnixEpoch));
        Assert.IsNull(data.Value.AsDateOnly());
        Assert.AreEqual(DateOnly.MinValue, data.Value.AsDateOnly(DateOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeOnly());
        Assert.AreEqual(TimeOnly.MinValue, data.Value.AsTimeOnly(TimeOnly.MinValue));
        Assert.IsNull(data.Value.AsTimeSpan());
        Assert.AreEqual(TimeSpan.MinValue, data.Value.AsTimeSpan(TimeSpan.MinValue));

        Assert.IsNull(data.Value.AsIPAddress());
        Assert.AreEqual(IPAddress.Loopback, data.Value.AsIPAddress(IPAddress.Loopback));

        Assert.IsNull(data.Value.AsUri());
        Assert.AreEqual(new Uri("https://aaseq.com"), data.Value.AsUri(new Uri("https://aaseq.com")));

        Assert.IsNull(data.Value.AsGuid());
        Assert.AreEqual(Guid.Empty, data.Value.AsGuid(Guid.Empty));
    }


    [TestMethod]
    public void NodeValue_Bytes_SByte() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsSByte());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual((sbyte)42, data1.Value.AsSByte());
        var data2n = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.IsNull(data2n.Value.AsSByte());
    }

    [TestMethod]
    public void NodeValue_Bytes_Byte() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsByte());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual((byte)42, data1.Value.AsByte());
        var data2 = new AASeqNode("node", new byte[] { 0, 42 });
        Assert.AreEqual((byte)42, data2.Value.AsByte());
        var data2n = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.IsNull(data2n.Value.AsByte());
    }

    [TestMethod]
    public void NodeValue_Bytes_Int16() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsInt16());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual((short)42, data1.Value.AsInt16());
        var data2 = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.AreEqual((short)298, data2.Value.AsInt16());
        var data3 = new AASeqNode("node", new byte[] { 0, 1, 42 });
        Assert.AreEqual((short)298, data3.Value.AsInt16());
        var data3n = new AASeqNode("node", new byte[] { 1, 1, 42 });
        Assert.IsNull(data3n.Value.AsInt16());
    }

    [TestMethod]
    public void NodeValue_Bytes_UInt16() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsUInt16());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual((ushort)42, data1.Value.AsUInt16());
        var data2 = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.AreEqual((ushort)298, data2.Value.AsUInt16());
        var data3 = new AASeqNode("node", new byte[] { 0, 1, 42 });
        Assert.AreEqual((ushort)298, data3.Value.AsUInt16());
        var data3n = new AASeqNode("node", new byte[] { 1, 1, 42 });
        Assert.IsNull(data3n.Value.AsUInt16());
    }

    [TestMethod]
    public void NodeValue_Bytes_Int32() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsInt32());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual(42, data1.Value.AsInt32());
        var data2 = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.AreEqual(298, data2.Value.AsInt32());
        var data3 = new AASeqNode("node", new byte[] { 1, 1, 42 });
        Assert.AreEqual(65834, data3.Value.AsInt32());
        var data4 = new AASeqNode("node", new byte[] { 1, 1, 1, 42 });
        Assert.AreEqual(16843050, data4.Value.AsInt32());
        var data5 = new AASeqNode("node", new byte[] { 0, 1, 1, 1, 42 });
        Assert.AreEqual(16843050, data5.Value.AsInt32());
        var data5n = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 42 });
        Assert.IsNull(data5n.Value.AsInt32());
    }

    [TestMethod]
    public void NodeValue_Bytes_UInt32() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsUInt32());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual(42U, data1.Value.AsUInt32());
        var data2 = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.AreEqual(298U, data2.Value.AsUInt32());
        var data3 = new AASeqNode("node", new byte[] { 1, 1, 42 });
        Assert.AreEqual(65834U, data3.Value.AsUInt32());
        var data4 = new AASeqNode("node", new byte[] { 1, 1, 1, 42 });
        Assert.AreEqual(16843050U, data4.Value.AsUInt32());
        var data5 = new AASeqNode("node", new byte[] { 0, 1, 1, 1, 42 });
        Assert.AreEqual(16843050U, data5.Value.AsUInt32());
        var data5n = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 42 });
        Assert.IsNull(data5n.Value.AsUInt32());
    }

    [TestMethod]
    public void NodeValue_Bytes_Int64() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsInt64());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual(42, data1.Value.AsInt64());
        var data2 = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.AreEqual(298, data2.Value.AsInt64());
        var data3 = new AASeqNode("node", new byte[] { 1, 1, 42 });
        Assert.AreEqual(65834, data3.Value.AsInt64());
        var data4 = new AASeqNode("node", new byte[] { 1, 1, 1, 42 });
        Assert.AreEqual(16843050, data4.Value.AsInt64());
        var data5 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 42 });
        Assert.AreEqual(4311810346, data5.Value.AsInt64());
        var data6 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(1103823438122, data6.Value.AsInt64());
        var data7 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(282578800148778, data7.Value.AsInt64());
        var data8 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(72340172838076714, data8.Value.AsInt64());
        var data9 = new AASeqNode("node", new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(72340172838076714, data9.Value.AsInt64());
        var data9n = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.IsNull(data9n.Value.AsInt64());
    }

    [TestMethod]
    public void NodeValue_Bytes_UInt64() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsUInt64());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual(42U, data1.Value.AsUInt64());
        var data2 = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.AreEqual(298U, data2.Value.AsUInt64());
        var data3 = new AASeqNode("node", new byte[] { 1, 1, 42 });
        Assert.AreEqual(65834U, data3.Value.AsUInt64());
        var data4 = new AASeqNode("node", new byte[] { 1, 1, 1, 42 });
        Assert.AreEqual(16843050U, data4.Value.AsUInt64());
        var data5 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 42 });
        Assert.AreEqual(4311810346U, data5.Value.AsUInt64());
        var data6 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(1103823438122U, data6.Value.AsUInt64());
        var data7 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(282578800148778U, data7.Value.AsUInt64());
        var data8 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(72340172838076714U, data8.Value.AsUInt64());
        var data9 = new AASeqNode("node", new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(72340172838076714U, data9.Value.AsUInt64());
        var data9n = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.IsNull(data9n.Value.AsUInt64());
    }

    [TestMethod]
    public void NodeValue_Bytes_Int128() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsInt128());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual(42, data1.Value.AsInt128());
        var data2 = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.AreEqual(298, data2.Value.AsInt128());
        var data3 = new AASeqNode("node", new byte[] { 1, 1, 42 });
        Assert.AreEqual(65834, data3.Value.AsInt128());
        var data4 = new AASeqNode("node", new byte[] { 1, 1, 1, 42 });
        Assert.AreEqual(16843050, data4.Value.AsInt128());
        var data5 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 42 });
        Assert.AreEqual(4311810346, data5.Value.AsInt128());
        var data6 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(1103823438122, data6.Value.AsInt128());
        var data7 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(282578800148778, data7.Value.AsInt128());
        var data8 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(72340172838076714, data8.Value.AsInt128());
        var data9 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new Int128(0x01, 72340172838076714), data9.Value.AsInt128());
        var data10 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new Int128(0x0101, 72340172838076714), data10.Value.AsInt128());
        var data11 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new Int128(0x010101, 72340172838076714), data11.Value.AsInt128());
        var data12 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new Int128(0x01010101, 72340172838076714), data12.Value.AsInt128());
        var data13 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new Int128(0x0101010101, 72340172838076714), data13.Value.AsInt128());
        var data14 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new Int128(0x010101010101, 72340172838076714), data14.Value.AsInt128());
        var data15 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new Int128(0x01010101010101, 72340172838076714), data15.Value.AsInt128());
        var data16 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new Int128(0x0101010101010101, 72340172838076714), data16.Value.AsInt128());
        var data17n = new AASeqNode("node", new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.IsNull(data17n.Value.AsInt128());
    }

    [TestMethod]
    public void NodeValue_Bytes_UInt128() {
        var data0n = new AASeqNode("node", new byte[] { });
        Assert.IsNull(data0n.Value.AsUInt128());
        var data1 = new AASeqNode("node", new byte[] { 42 });
        Assert.AreEqual(42U, data1.Value.AsUInt128());
        var data2 = new AASeqNode("node", new byte[] { 1, 42 });
        Assert.AreEqual(298U, data2.Value.AsUInt128());
        var data3 = new AASeqNode("node", new byte[] { 1, 1, 42 });
        Assert.AreEqual(65834U, data3.Value.AsUInt128());
        var data4 = new AASeqNode("node", new byte[] { 1, 1, 1, 42 });
        Assert.AreEqual(16843050U, data4.Value.AsUInt128());
        var data5 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 42 });
        Assert.AreEqual(4311810346U, data5.Value.AsUInt128());
        var data6 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(1103823438122U, data6.Value.AsUInt128());
        var data7 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(282578800148778U, data7.Value.AsUInt128());
        var data8 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(72340172838076714U, data8.Value.AsUInt128());
        var data9 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new UInt128(0x01, 72340172838076714), data9.Value.AsUInt128());
        var data10 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new UInt128(0x0101, 72340172838076714), data10.Value.AsUInt128());
        var data11 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new UInt128(0x010101, 72340172838076714), data11.Value.AsUInt128());
        var data12 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new UInt128(0x01010101, 72340172838076714), data12.Value.AsUInt128());
        var data13 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new UInt128(0x0101010101, 72340172838076714), data13.Value.AsUInt128());
        var data14 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new UInt128(0x010101010101, 72340172838076714), data14.Value.AsUInt128());
        var data15 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new UInt128(0x01010101010101, 72340172838076714), data15.Value.AsUInt128());
        var data16 = new AASeqNode("node", new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.AreEqual(new UInt128(0x0101010101010101, 72340172838076714), data16.Value.AsUInt128());
        var data17n = new AASeqNode("node", new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 42 });
        Assert.IsNull(data17n.Value.AsUInt128());
    }

    [TestMethod]
    public void NodeValue_NotSupported() {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            var data = new AASeqValue(new NotSupportedException());
        });
    }

}
