namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

internal static partial class App {

    public static void New(FileInfo file) {
        var doc = new AASeqNodes();
        doc.Save(file.FullName);
    }

}
