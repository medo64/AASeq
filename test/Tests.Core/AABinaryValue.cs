using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AABinaryValue_Tests {

    [TestMethod]
    public void AABinaryValue_Basic() {
        var text = "0xFEFF";
        Assert.IsTrue(AABinaryValue.TryParse(text, out var result));
        Assert.AreEqual("FE-FF", BitConverter.ToString(result));
        Assert.AreEqual("0xFEFF", AABinaryValue.Parse(text));
        Assert.AreEqual(result, AABinaryValue.Parse(text));
        Assert.IsTrue(result.Equals(new byte[] { 0xFE, 0xFF }));
    }

    [TestMethod]
    public void AABinaryValue_NoPrefix() {
        var text = "fefe";
        Assert.IsTrue(AABinaryValue.TryParse(text, out var result));
        Assert.AreEqual("FE-FE", BitConverter.ToString(result));
        Assert.AreEqual("0xFEFE", AABinaryValue.Parse(text));
    }


    [TestMethod]
    public void AABinaryValue_AsInt8() {
        var binary1P = new AABinaryValue(new byte[] { 0x30 });
        Assert.AreEqual((sbyte)48, binary1P.AsInt8());
        Assert.AreEqual((short)48, binary1P.AsInt16());
        Assert.AreEqual(48, binary1P.AsInt32());
        Assert.AreEqual(48L, binary1P.AsInt64());
        Assert.AreEqual((byte)48, binary1P.AsUInt8());
        Assert.AreEqual((ushort)48, binary1P.AsUInt16());
        Assert.AreEqual(48U, binary1P.AsUInt32());
        Assert.AreEqual(48UL, binary1P.AsUInt64());

        var binary1N = new AABinaryValue(new byte[] { 0x82 });
        Assert.AreEqual((sbyte)-126, binary1N.AsInt8());
        Assert.AreEqual((short)-126, binary1N.AsInt16());
        Assert.AreEqual(-126, binary1N.AsInt32());
        Assert.AreEqual(-126L, binary1N.AsInt64());
        Assert.AreEqual((byte)130, binary1N.AsUInt8());
        Assert.AreEqual((ushort)130, binary1N.AsUInt16());
        Assert.AreEqual(130U, binary1N.AsUInt32());
        Assert.AreEqual(130UL, binary1N.AsUInt64());
    }

    [TestMethod]
    public void AABinaryValue_AsInt16() {
        var binary1P = new AABinaryValue(new byte[] { 0x00, 0x30 });
        Assert.AreEqual((sbyte)48, binary1P.AsInt8());
        Assert.AreEqual((short)48, binary1P.AsInt16());
        Assert.AreEqual(48, binary1P.AsInt32());
        Assert.AreEqual(48L, binary1P.AsInt64());
        Assert.AreEqual((byte)48, binary1P.AsUInt8());
        Assert.AreEqual((ushort)48, binary1P.AsUInt16());
        Assert.AreEqual(48U, binary1P.AsUInt32());
        Assert.AreEqual(48UL, binary1P.AsUInt64());

        var binary1N = new AABinaryValue(new byte[] { 0x00, 0x82 });
        Assert.IsNull(binary1N.AsInt8());
        Assert.AreEqual((short)130, binary1N.AsInt16());
        Assert.AreEqual(130, binary1N.AsInt32());
        Assert.AreEqual(130L, binary1N.AsInt64());
        Assert.AreEqual((byte)130, binary1N.AsUInt8());
        Assert.AreEqual((ushort)130, binary1N.AsUInt16());
        Assert.AreEqual(130U, binary1N.AsUInt32());
        Assert.AreEqual(130UL, binary1N.AsUInt64());

        var binary2P = new AABinaryValue(new byte[] { 0x01, 0x30 });
        Assert.IsNull(binary2P.AsInt8());
        Assert.AreEqual((short)304, binary2P.AsInt16());
        Assert.AreEqual(304, binary2P.AsInt32());
        Assert.AreEqual(304L, binary2P.AsInt64());
        Assert.IsNull(binary2P.AsUInt8());
        Assert.AreEqual((ushort)304, binary2P.AsUInt16());
        Assert.AreEqual(304U, binary2P.AsUInt32());
        Assert.AreEqual(304UL, binary2P.AsUInt64());

        var binary2N = new AABinaryValue(new byte[] { 0x81, 0x30 });
        Assert.IsNull(binary2N.AsInt8());
        Assert.AreEqual((short)-32464, binary2N.AsInt16());
        Assert.AreEqual(-32464, binary2N.AsInt32());
        Assert.AreEqual(-32464L, binary2N.AsInt64());
        Assert.IsNull(binary2N.AsUInt8());
        Assert.AreEqual((ushort)33072, binary2N.AsUInt16());
        Assert.AreEqual(33072U, binary2N.AsUInt32());
        Assert.AreEqual(33072UL, binary2N.AsUInt64());
    }

    [TestMethod]
    public void AABinaryValue_AsInt32() {
        var binary1P = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x2A });
        Assert.AreEqual((sbyte)42, binary1P.AsInt8());
        Assert.AreEqual((short)42, binary1P.AsInt16());
        Assert.AreEqual(42, binary1P.AsInt32());
        Assert.AreEqual(42L, binary1P.AsInt64());
        Assert.AreEqual((byte)42, binary1P.AsUInt8());
        Assert.AreEqual((ushort)42, binary1P.AsUInt16());
        Assert.AreEqual(42U, binary1P.AsUInt32());
        Assert.AreEqual(42UL, binary1P.AsUInt64());

        var binary1N = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x9A });  // not real negative
        Assert.IsNull(binary1N.AsInt8());
        Assert.AreEqual((short)154, binary1N.AsInt16());
        Assert.AreEqual(154, binary1N.AsInt32());
        Assert.AreEqual(154L, binary1N.AsInt64());
        Assert.AreEqual((byte)154, binary1N.AsUInt8());
        Assert.AreEqual((ushort)154, binary1N.AsUInt16());
        Assert.AreEqual(154U, binary1N.AsUInt32());
        Assert.AreEqual(154UL, binary1N.AsUInt64());

        var binary2P = new AABinaryValue(new byte[] { 0x00, 0x00, 0x04, 0x12 });
        Assert.IsNull(binary2P.AsInt8());
        Assert.AreEqual((short)1042, binary2P.AsInt16());
        Assert.AreEqual(1042, binary2P.AsInt32());
        Assert.AreEqual(1042L, binary2P.AsInt64());
        Assert.IsNull(binary2P.AsUInt8());
        Assert.AreEqual((ushort)1042, binary2P.AsUInt16());
        Assert.AreEqual(1042U, binary2P.AsUInt32());
        Assert.AreEqual(1042UL, binary2P.AsUInt64());

        var binary2N = new AABinaryValue(new byte[] { 0x00, 0x00, 0x94, 0x12 });  // not really a negative number
        Assert.IsNull(binary2N.AsInt8());
        Assert.IsNull(binary2N.AsInt16());
        Assert.AreEqual(37906, binary2N.AsInt32());
        Assert.AreEqual(37906L, binary2N.AsInt64());
        Assert.IsNull(binary2N.AsUInt8());
        Assert.AreEqual((ushort)37906, binary2N.AsUInt16());
        Assert.AreEqual(37906U, binary2N.AsUInt32());
        Assert.AreEqual(37906UL, binary2N.AsUInt64());

        var binary4P = new AABinaryValue(new byte[] { 0x00, 0x01, 0x86, 0xCA });
        Assert.IsNull(binary4P.AsInt8());
        Assert.IsNull(binary4P.AsInt16());
        Assert.AreEqual(100042, binary4P.AsInt32());
        Assert.AreEqual(100042L, binary4P.AsInt64());
        Assert.IsNull(binary4P.AsUInt8());
        Assert.IsNull(binary4P.AsUInt16());
        Assert.AreEqual(100042U, binary4P.AsUInt32());
        Assert.AreEqual(100042UL, binary4P.AsUInt64());

        var binary4N = new AABinaryValue(new byte[] { 0x80, 0x01, 0x86, 0xCA });
        Assert.IsNull(binary4N.AsInt8());
        Assert.IsNull(binary4N.AsInt16());
        Assert.AreEqual(-2147383606, binary4N.AsInt32());
        Assert.AreEqual(-2147383606L, binary4N.AsInt64());
        Assert.IsNull(binary4N.AsUInt8());
        Assert.IsNull(binary4N.AsUInt16());
        Assert.AreEqual(2147583690U, binary4N.AsUInt32());
        Assert.AreEqual(2147583690UL, binary4N.AsUInt64());
    }

    [TestMethod]
    public void AABinaryValue_AsInt64() {
        var binary1P = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2A });
        Assert.AreEqual((sbyte)42, binary1P.AsInt8());
        Assert.AreEqual((short)42, binary1P.AsInt16());
        Assert.AreEqual(42, binary1P.AsInt32());
        Assert.AreEqual(42L, binary1P.AsInt64());
        Assert.AreEqual((byte)42, binary1P.AsUInt8());
        Assert.AreEqual((ushort)42, binary1P.AsUInt16());
        Assert.AreEqual(42U, binary1P.AsUInt32());
        Assert.AreEqual(42UL, binary1P.AsUInt64());

        var binary1N = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA });
        Assert.IsNull(binary1N.AsInt8());
        Assert.AreEqual((short)170, binary1N.AsInt16());
        Assert.AreEqual(170, binary1N.AsInt32());
        Assert.AreEqual(170L, binary1N.AsInt64());
        Assert.AreEqual((byte)170, binary1N.AsUInt8());
        Assert.AreEqual((ushort)170, binary1N.AsUInt16());
        Assert.AreEqual(170U, binary1N.AsUInt32());
        Assert.AreEqual(170UL, binary1N.AsUInt64());

        var binary2P = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x12 });
        Assert.IsNull(binary2P.AsInt8());
        Assert.AreEqual((short)1042, binary2P.AsInt16());
        Assert.AreEqual(1042, binary2P.AsInt32());
        Assert.AreEqual(1042L, binary2P.AsInt64());
        Assert.IsNull(binary2P.AsUInt8());
        Assert.AreEqual((ushort)1042, binary2P.AsUInt16());
        Assert.AreEqual(1042U, binary2P.AsUInt32());
        Assert.AreEqual(1042UL, binary2P.AsUInt64());

        var binary2N = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD4, 0x12 });
        Assert.IsNull(binary2N.AsInt8());
        Assert.IsNull(binary2N.AsInt16());
        Assert.AreEqual(54290, binary2N.AsInt32());
        Assert.AreEqual(54290L, binary2N.AsInt64());
        Assert.IsNull(binary2N.AsUInt8());
        Assert.AreEqual((ushort)54290, binary2N.AsUInt16());
        Assert.AreEqual(54290U, binary2N.AsUInt32());
        Assert.AreEqual(54290UL, binary2N.AsUInt64());

        var binary4P = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x86, 0xCA });
        Assert.IsNull(binary4P.AsInt8());
        Assert.IsNull(binary4P.AsInt16());
        Assert.AreEqual(100042, binary4P.AsInt32());
        Assert.AreEqual(100042L, binary4P.AsInt64());
        Assert.IsNull(binary4P.AsUInt8());
        Assert.IsNull(binary4P.AsUInt16());
        Assert.AreEqual(100042U, binary4P.AsUInt32());
        Assert.AreEqual(100042UL, binary4P.AsUInt64());

        var binary4N = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0xF0, 0x01, 0x86, 0xCA });
        Assert.IsNull(binary4N.AsInt8());
        Assert.IsNull(binary4N.AsInt16());
        Assert.IsNull(binary4N.AsInt32());
        Assert.AreEqual(4026631882L, binary4N.AsInt64());
        Assert.IsNull(binary4N.AsUInt8());
        Assert.IsNull(binary4N.AsUInt16());
        Assert.AreEqual(4026631882U, binary4N.AsUInt32());
        Assert.AreEqual(4026631882UL, binary4N.AsUInt64());

        var binary8P = new AABinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x17, 0x48, 0x76, 0xE8, 0x2A });
        Assert.IsNull(binary8P.AsInt8());
        Assert.IsNull(binary8P.AsInt16());
        Assert.IsNull(binary8P.AsInt32());
        Assert.AreEqual(100000000042L, binary8P.AsInt64());
        Assert.IsNull(binary8P.AsUInt8());
        Assert.IsNull(binary8P.AsUInt16());
        Assert.IsNull(binary8P.AsUInt32());
        Assert.AreEqual(100000000042UL, binary8P.AsUInt64());

        var binary8N = new AABinaryValue(new byte[] { 0xB0, 0x00, 0x00, 0x17, 0x48, 0x76, 0xE8, 0x2A });
        Assert.IsNull(binary8N.AsInt8());
        Assert.IsNull(binary8N.AsInt16());
        Assert.IsNull(binary8N.AsInt32());
        Assert.AreEqual(-5764607423034234838L, binary8N.AsInt64());
        Assert.IsNull(binary8N.AsUInt8());
        Assert.IsNull(binary8N.AsUInt16());
        Assert.IsNull(binary8N.AsUInt32());
        Assert.AreEqual(12682136650675316778UL, binary8N.AsUInt64());
    }


    [TestMethod]
    public void AABinaryValue_FailedParse() {
        Assert.IsFalse(AABooleanValue.TryParse("GG", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AABooleanValue.Parse("GG");
        });

        Assert.IsFalse(AABooleanValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AABooleanValue.Parse("A");
        });
    }

    [TestMethod]
    public void AABinaryValue_ReadOnlyMemory() {
        var buffer = new byte[] { 0x01, 0x02 };
        var x = new AABinaryValue(buffer);
        buffer[1] = 0x2A;
        Assert.AreEqual("0102", x.Value.ToHexString());
    }

}
