using System.Globalization;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tests;

internal static class Helpers {

    public static Stream GetResourceStream(string relativePath) {
        var helperType = typeof(Helpers).GetTypeInfo();
        var assembly = helperType.Assembly;
        return assembly.GetManifestResourceStream(helperType.Namespace + "._Resources." + relativePath.Replace('/', '.'));
    }


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
