using System;
using System.Globalization;
using System.Text;

namespace Tests.Core;

public static class HelperExtensions {

    public static string ToHexString(this ReadOnlyMemory<byte>? bytes) {
        if (bytes == null) { return null; }
        return ToHexString(bytes.Value);
    }

    public static string ToHexString(this ReadOnlyMemory<byte> bytes) {
        var sb = new StringBuilder();
        var i = 0;
        foreach (var b in bytes.Span) {
            if (i > 0 && (i % 4 == 0)) { sb.Append(' '); }
            i++;
            sb.Append(b.ToString("X2", CultureInfo.InvariantCulture));
        }
        return sb.ToString();
    }

}
