namespace Tests;
using System;
using AASeqPlugin;

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

}
