using System.IO;
using System.Reflection;

namespace Tests;

internal static class Helpers {

    public static Stream GetResourceStream(string relativePath) {
        var helperType = typeof(Helpers).GetTypeInfo();
        var assembly = helperType.Assembly;
        return assembly.GetManifestResourceStream(helperType.Namespace + "._Resources." + relativePath.Replace('/', '.'));
    }

}
