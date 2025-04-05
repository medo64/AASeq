namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Reflection;

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
        var doc = new AASeqDocument();
        doc.Save(file.FullName);
    }

    public static void Lint(FileInfo file) {
        try {
            var document = AASeqDocument.Load(file.FullName);
        } catch (InvalidOperationException ex) {
            Output.ErrorLine("Error parsing the document: " + ex.Message);
        }
    }

    public static void Run(FileInfo file) {
        try {
            var document = AASeqDocument.Load(file.FullName);
        } catch (InvalidOperationException ex) {
            Output.ErrorLine("Error parsing the document: " + ex.Message);
        }
    }

}
