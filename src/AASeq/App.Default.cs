namespace AASeqCli;
using AASeq;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

internal static partial class App {

    public static void Default(FileInfo file, bool debug, bool verbose) {
        App.Run(file, debug, verbose, isInteractive: true);
    }

}
