namespace AASeqCli;
using System;
using System.Drawing;
using System.Threading;

internal static class Output {

    private static readonly Lock SyncRoot = new();

    public static void WriteLine(string s) {
        lock (SyncRoot) {
            Console.WriteLine(s);
        }
    }

    public static void ErrorLine(string s) {
        lock (SyncRoot) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(s);
            Console.ResetColor();
        }
    }

}
