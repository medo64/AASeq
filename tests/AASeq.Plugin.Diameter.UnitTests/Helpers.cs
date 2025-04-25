namespace Tests;
using System;
using System.IO;
using AASeqPlugin;

internal static class Helpers {

    public static byte[] GetRawBytes(DiameterAvp avp) {
        var buffer = new byte[avp.LengthWithPadding];
        avp.WriteTo(new Span<byte>(buffer));
        return buffer;
    }

}
