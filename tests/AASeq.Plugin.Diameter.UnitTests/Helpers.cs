namespace Tests;

using System.IO;
using AASeqPlugin;

internal static class Helpers {

    public static byte[] GetRawBytes(DiameterAvp avp) {
        var buffer = new MemoryStream();
        avp.WriteTo(buffer);
        return buffer.ToArray();
    }

}
