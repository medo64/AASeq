using AASeq;
using System;
using System.IO;
using System.Reflection;

namespace AASeqCli;

internal static class AppExec {

    public static void Base(bool showVersion, bool useVerbose) {
        if (showVersion) {
            var assembly = Assembly.GetEntryAssembly();
            var name = assembly?.GetName().Name ?? string.Empty;
            var version = assembly?.GetName().Version ?? new Version();
            Console.WriteLine($"{name} {version.Major}.{version.Minor} {version.Revision}");
            if (useVerbose) {
            }
            return;
        }
    }


    public static void New(FileInfo file) {
        using var stream = file.OpenRead();
        var document = new Document(stream);
    }

    public static void Lint(FileInfo file) {
        using var stream = file.OpenRead();
        var document = new Document(stream);
    }

    public static void Run(FileInfo file) {
        using var stream = file.OpenRead();
        var document = new Document(stream);
    }

}
