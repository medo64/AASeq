using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniBinaryValueTests {

    [Fact(DisplayName = "TiniBinaryValue: Basic")]
    public void Basic() {
        var text = "0xFEFE";
        Assert.True(TiniBinaryValue.TryParse(text, out var result));
        Assert.Equal("FE-FE", BitConverter.ToString(result));
        Assert.Equal("0xFEFE", TiniBinaryValue.Parse(text));
    }

    [Fact(DisplayName = "TiniBinaryValue: No prefix")]
    public void NoPrefix() {
        var text = "fefe";
        Assert.True(TiniBinaryValue.TryParse(text, out var result));
        Assert.Equal("FE-FE", BitConverter.ToString(result));
        Assert.Equal("0xFEFE", TiniBinaryValue.Parse(text));
    }


    [Fact(DisplayName = "TiniBinaryValue: Int8")]
    public void Int8() {
        var binary1P = new TiniBinaryValue(new byte[] { 0x30 });
        Assert.Equal((sbyte)48, binary1P.AsInt8());
        Assert.Equal((short)48, binary1P.AsInt16());
        Assert.Equal(48, binary1P.AsInt32());
        Assert.Equal(48L, binary1P.AsInt64());
        Assert.Equal((byte)48, binary1P.AsUInt8());
        Assert.Equal((ushort)48, binary1P.AsUInt16());
        Assert.Equal(48U, binary1P.AsUInt32());
        Assert.Equal(48UL, binary1P.AsUInt64());

        var binary1N = new TiniBinaryValue(new byte[] { 0x82 });
        Assert.Equal((sbyte)-126, binary1N.AsInt8());
        Assert.Equal((short)-126, binary1N.AsInt16());
        Assert.Equal(-126, binary1N.AsInt32());
        Assert.Equal(-126L, binary1N.AsInt64());
        Assert.Equal((byte)130, binary1N.AsUInt8());
        Assert.Equal((ushort)130, binary1N.AsUInt16());
        Assert.Equal(130U, binary1N.AsUInt32());
        Assert.Equal(130UL, binary1N.AsUInt64());
    }

    [Fact(DisplayName = "TiniBinaryValue: Int16")]
    public void Int16() {
        var binary1P = new TiniBinaryValue(new byte[] { 0x00, 0x30 });
        Assert.Equal((sbyte)48, binary1P.AsInt8());
        Assert.Equal((short)48, binary1P.AsInt16());
        Assert.Equal(48, binary1P.AsInt32());
        Assert.Equal(48L, binary1P.AsInt64());
        Assert.Equal((byte)48, binary1P.AsUInt8());
        Assert.Equal((ushort)48, binary1P.AsUInt16());
        Assert.Equal(48U, binary1P.AsUInt32());
        Assert.Equal(48UL, binary1P.AsUInt64());

        var binary1N = new TiniBinaryValue(new byte[] { 0x00, 0x82 });
        Assert.Null(binary1N.AsInt8());
        Assert.Equal((short)130, binary1N.AsInt16());
        Assert.Equal(130, binary1N.AsInt32());
        Assert.Equal(130L, binary1N.AsInt64());
        Assert.Equal((byte)130, binary1N.AsUInt8());
        Assert.Equal((ushort)130, binary1N.AsUInt16());
        Assert.Equal(130U, binary1N.AsUInt32());
        Assert.Equal(130UL, binary1N.AsUInt64());

        var binary2P = new TiniBinaryValue(new byte[] { 0x01, 0x30 });
        Assert.Null(binary2P.AsInt8());
        Assert.Equal((short)304, binary2P.AsInt16());
        Assert.Equal(304, binary2P.AsInt32());
        Assert.Equal(304L, binary2P.AsInt64());
        Assert.Null(binary2P.AsUInt8());
        Assert.Equal((ushort)304, binary2P.AsUInt16());
        Assert.Equal(304U, binary2P.AsUInt32());
        Assert.Equal(304UL, binary2P.AsUInt64());

        var binary2N = new TiniBinaryValue(new byte[] { 0x81, 0x30 });
        Assert.Null(binary2N.AsInt8());
        Assert.Equal((short)-32464, binary2N.AsInt16());
        Assert.Equal(-32464, binary2N.AsInt32());
        Assert.Equal(-32464L, binary2N.AsInt64());
        Assert.Null(binary2N.AsUInt8());
        Assert.Equal((ushort)33072, binary2N.AsUInt16());
        Assert.Equal(33072U, binary2N.AsUInt32());
        Assert.Equal(33072UL, binary2N.AsUInt64());
    }

    [Fact(DisplayName = "TiniBinaryValue: Int32")]
    public void Int32() {
        var binary1P = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x2A });
        Assert.Equal((sbyte)42, binary1P.AsInt8());
        Assert.Equal((short)42, binary1P.AsInt16());
        Assert.Equal(42, binary1P.AsInt32());
        Assert.Equal(42L, binary1P.AsInt64());
        Assert.Equal((byte)42, binary1P.AsUInt8());
        Assert.Equal((ushort)42, binary1P.AsUInt16());
        Assert.Equal(42U, binary1P.AsUInt32());
        Assert.Equal(42UL, binary1P.AsUInt64());

        var binary1N = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x9A });  // not real negative
        Assert.Null(binary1N.AsInt8());
        Assert.Equal((short)154, binary1N.AsInt16());
        Assert.Equal(154, binary1N.AsInt32());
        Assert.Equal(154L, binary1N.AsInt64());
        Assert.Equal((byte)154, binary1N.AsUInt8());
        Assert.Equal((ushort)154, binary1N.AsUInt16());
        Assert.Equal(154U, binary1N.AsUInt32());
        Assert.Equal(154UL, binary1N.AsUInt64());

        var binary2P = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x04, 0x12 });
        Assert.Null(binary2P.AsInt8());
        Assert.Equal((short)1042, binary2P.AsInt16());
        Assert.Equal(1042, binary2P.AsInt32());
        Assert.Equal(1042L, binary2P.AsInt64());
        Assert.Null(binary2P.AsUInt8());
        Assert.Equal((ushort)1042, binary2P.AsUInt16());
        Assert.Equal(1042U, binary2P.AsUInt32());
        Assert.Equal(1042UL, binary2P.AsUInt64());

        var binary2N = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x94, 0x12 });  // not really a negative number
        Assert.Null(binary2N.AsInt8());
        Assert.Null(binary2N.AsInt16());
        Assert.Equal(37906, binary2N.AsInt32());
        Assert.Equal(37906L, binary2N.AsInt64());
        Assert.Null(binary2N.AsUInt8());
        Assert.Equal((ushort)37906, binary2N.AsUInt16());
        Assert.Equal(37906U, binary2N.AsUInt32());
        Assert.Equal(37906UL, binary2N.AsUInt64());

        var binary4P = new TiniBinaryValue(new byte[] { 0x00, 0x01, 0x86, 0xCA });
        Assert.Null(binary4P.AsInt8());
        Assert.Null(binary4P.AsInt16());
        Assert.Equal(100042, binary4P.AsInt32());
        Assert.Equal(100042L, binary4P.AsInt64());
        Assert.Null(binary4P.AsUInt8());
        Assert.Null(binary4P.AsUInt16());
        Assert.Equal(100042U, binary4P.AsUInt32());
        Assert.Equal(100042UL, binary4P.AsUInt64());

        var binary4N = new TiniBinaryValue(new byte[] { 0x80, 0x01, 0x86, 0xCA });
        Assert.Null(binary4N.AsInt8());
        Assert.Null(binary4N.AsInt16());
        Assert.Equal(-2147383606, binary4N.AsInt32());
        Assert.Equal(-2147383606L, binary4N.AsInt64());
        Assert.Null(binary4N.AsUInt8());
        Assert.Null(binary4N.AsUInt16());
        Assert.Equal(2147583690U, binary4N.AsUInt32());
        Assert.Equal(2147583690UL, binary4N.AsUInt64());
    }

    [Fact(DisplayName = "TiniBinaryValue: Int64")]
    public void Int64() {
        var binary1P = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2A });
        Assert.Equal((sbyte)42, binary1P.AsInt8());
        Assert.Equal((short)42, binary1P.AsInt16());
        Assert.Equal(42, binary1P.AsInt32());
        Assert.Equal(42L, binary1P.AsInt64());
        Assert.Equal((byte)42, binary1P.AsUInt8());
        Assert.Equal((ushort)42, binary1P.AsUInt16());
        Assert.Equal(42U, binary1P.AsUInt32());
        Assert.Equal(42UL, binary1P.AsUInt64());

        var binary1N = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA });
        Assert.Null(binary1N.AsInt8());
        Assert.Equal((short)170, binary1N.AsInt16());
        Assert.Equal(170, binary1N.AsInt32());
        Assert.Equal(170L, binary1N.AsInt64());
        Assert.Equal((byte)170, binary1N.AsUInt8());
        Assert.Equal((ushort)170, binary1N.AsUInt16());
        Assert.Equal(170U, binary1N.AsUInt32());
        Assert.Equal(170UL, binary1N.AsUInt64());

        var binary2P = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x12 });
        Assert.Null(binary2P.AsInt8());
        Assert.Equal((short)1042, binary2P.AsInt16());
        Assert.Equal(1042, binary2P.AsInt32());
        Assert.Equal(1042L, binary2P.AsInt64());
        Assert.Null(binary2P.AsUInt8());
        Assert.Equal((ushort)1042, binary2P.AsUInt16());
        Assert.Equal(1042U, binary2P.AsUInt32());
        Assert.Equal(1042UL, binary2P.AsUInt64());

        var binary2N = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD4, 0x12 });
        Assert.Null(binary2N.AsInt8());
        Assert.Null(binary2N.AsInt16());
        Assert.Equal(54290, binary2N.AsInt32());
        Assert.Equal(54290L, binary2N.AsInt64());
        Assert.Null(binary2N.AsUInt8());
        Assert.Equal((ushort)54290, binary2N.AsUInt16());
        Assert.Equal(54290U, binary2N.AsUInt32());
        Assert.Equal(54290UL, binary2N.AsUInt64());

        var binary4P = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x86, 0xCA });
        Assert.Null(binary4P.AsInt8());
        Assert.Null(binary4P.AsInt16());
        Assert.Equal(100042, binary4P.AsInt32());
        Assert.Equal(100042L, binary4P.AsInt64());
        Assert.Null(binary4P.AsUInt8());
        Assert.Null(binary4P.AsUInt16());
        Assert.Equal(100042U, binary4P.AsUInt32());
        Assert.Equal(100042UL, binary4P.AsUInt64());

        var binary4N = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x00, 0xF0, 0x01, 0x86, 0xCA });
        Assert.Null(binary4N.AsInt8());
        Assert.Null(binary4N.AsInt16());
        Assert.Null(binary4N.AsInt32());
        Assert.Equal(4026631882L, binary4N.AsInt64());
        Assert.Null(binary4N.AsUInt8());
        Assert.Null(binary4N.AsUInt16());
        Assert.Equal(4026631882U, binary4N.AsUInt32());
        Assert.Equal(4026631882UL, binary4N.AsUInt64());

        var binary8P = new TiniBinaryValue(new byte[] { 0x00, 0x00, 0x00, 0x17, 0x48, 0x76, 0xE8, 0x2A });
        Assert.Null(binary8P.AsInt8());
        Assert.Null(binary8P.AsInt16());
        Assert.Null(binary8P.AsInt32());
        Assert.Equal(100000000042L, binary8P.AsInt64());
        Assert.Null(binary8P.AsUInt8());
        Assert.Null(binary8P.AsUInt16());
        Assert.Null(binary8P.AsUInt32());
        Assert.Equal(100000000042UL, binary8P.AsUInt64());

        var binary8N = new TiniBinaryValue(new byte[] { 0xB0, 0x00, 0x00, 0x17, 0x48, 0x76, 0xE8, 0x2A });
        Assert.Null(binary8N.AsInt8());
        Assert.Null(binary8N.AsInt16());
        Assert.Null(binary8N.AsInt32());
        Assert.Equal(-5764607423034234838L, binary8N.AsInt64());
        Assert.Null(binary8N.AsUInt8());
        Assert.Null(binary8N.AsUInt16());
        Assert.Null(binary8N.AsUInt32());
        Assert.Equal(12682136650675316778UL, binary8N.AsUInt64());
    }


    [Fact(DisplayName = "TiniBooleanValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniBooleanValue.TryParse("GG", out var _));
        Assert.Throws<FormatException>(() => {
            TiniBooleanValue.Parse("GG");
        });

        Assert.False(TiniBooleanValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniBooleanValue.Parse("A");
        });
    }

}
