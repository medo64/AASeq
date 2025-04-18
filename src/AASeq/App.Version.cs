namespace AASeqCli;
using System;
using System.Reflection;

internal static partial class App {

    public static void Version(bool isVerbose) {
        var assembly = Assembly.GetEntryAssembly();
        var name = assembly?.GetName().Name ?? string.Empty;
        var version = assembly?.GetName().Version ?? new Version();
        Console.WriteLine($"{name} {version.Major}.{version.Minor} {version.Revision}");
    }

}
