namespace Tests;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using AASeq.Diameter;

internal static class Helpers {

    public static byte[] GetRawBytes(DiameterAvp avp) {
        var buffer = new byte[avp.LengthWithPadding];
        avp.WriteTo(new Span<byte>(buffer));
        return buffer;
    }

    public static byte[] GetRawBytes(DiameterMessage message) {
        var buffer = new byte[message.Length];
        message.WriteTo(new Span<byte>(buffer));
        return buffer;
    }

    public static Byte[] GetBytesFromHex(string hex) {
        Debug.Assert(hex.Length % 2 == 0);
        var bytes = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2) {
            bytes[i / 2] = (byte)int.Parse(hex.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }
        return bytes;
    }

    public static String GetHexFromBytes(byte[] buffer) {
        var sb = new StringBuilder();
        for (int i = 0; i < buffer.Length; i++) {
            sb.Append(buffer[i].ToString("X2"));
        }
        return sb.ToString();
    }


    public static MemoryStream GetStreamFromHex(string hex) {
        return new MemoryStream(GetBytesFromHex(hex));
    }

    public static String GetHexFromStream(MemoryStream stream) {
        return GetHexFromBytes(stream.ToArray());
    }

}
