namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

internal static partial class App {

    public static void Default(bool showVersion, bool useVerbose) {
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

}
