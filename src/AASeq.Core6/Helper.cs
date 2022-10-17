namespace AASeq;

using System.IO;
using System.Text;

internal static class Helper {

    public static Stream GetStreamFromString(string text) {
        var memStream = new MemoryStream(Encoding.UTF8.GetBytes(text)) {
            Position = 0
        };
        return memStream;
    }

    public static Stream GetStreamFromBytes(byte[] bytes) {
        var memStream = new MemoryStream(bytes) {
            Position = 0
        };
        return memStream;
    }

}
