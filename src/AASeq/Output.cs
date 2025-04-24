namespace AASeqCli;
using System;
using System.Threading;
using AASeq;

internal static class Output {

    private static readonly Lock SyncRoot = new();

    public static void WriteLine(string s) {
        lock (SyncRoot) {
            Console.WriteLine(s);
        }
    }

    public static void WriteError(string s, bool prependEmptyLine = false) {
        lock (SyncRoot) {
            if (prependEmptyLine) { Console.Out.WriteLine(); }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(s);
            Console.ResetColor();
        }
    }

    public static void WriteNote(string s, bool prependEmptyLine = false) {
        lock (SyncRoot) {
            if (prependEmptyLine) { Console.Out.WriteLine(); }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Out.WriteLine(s);
            Console.ResetColor();
        }
    }


    private readonly static AASeqOutputOptions NodeOutputOptions = AASeqOutputOptions.Default with { ExtraEmptyRootNodeLines = true, NoTypeAnnotation = true };

    public static void WriteDocument(AASeqNodes nodes) {
        lock (SyncRoot) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            nodes.Save(Console.Out, NodeOutputOptions);
            Console.ResetColor();
        }
    }

    public static void WriteActionOk(IFlowAction action, AASeqNode node) {
        lock (SyncRoot) {
            if (action.IsCommand) {
                Console.ForegroundColor = ConsoleColor.Magenta;
            } else if (action.IsMessageOut) {
                Console.ForegroundColor = ConsoleColor.Cyan;
            } else if (action.IsMessageIn) {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            node.Save(Console.Out, NodeOutputOptions);
            Console.ResetColor();
        }
    }

    public static void WriteActionNok(IFlowAction _, AASeqNode node) {
        lock (SyncRoot) {
            Console.ForegroundColor = ConsoleColor.Red;
            node.Save(Console.Out, NodeOutputOptions);
            Console.ResetColor();
        }
    }

}
