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


    public static void WriteErrorLine(string s) {
        lock (SyncRoot) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(s);
            Console.ResetColor();
        }
    }


    #region Log

    public static void WriteDebug(string s, bool prependEmptyLine = false) {
        lock (SyncRoot) {
            if (prependEmptyLine) { Console.Out.WriteLine(); }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Out.Write("### " + DateTime.Now.ToString("HH:mm:ss.fff") + " ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Out.WriteLine(s);
            Console.ResetColor();
        }
    }

    public static void WriteInfo(string s, bool prependEmptyLine = false) {
        lock (SyncRoot) {
            if (prependEmptyLine) { Console.Out.WriteLine(); }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Out.Write("### " + DateTime.Now.ToString("HH:mm:ss.fff") + " ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Out.WriteLine(s);
            Console.ResetColor();
        }
    }

    public static void WriteWarning(string s, bool prependEmptyLine = false) {
        lock (SyncRoot) {
            if (prependEmptyLine) { Console.Out.WriteLine(); }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Out.Write("### " + DateTime.Now.ToString("HH:mm:ss.fff") + " ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Error.WriteLine(s);
            Console.ResetColor();
        }
    }

    public static void WriteError(string s, bool prependEmptyLine = false) {
        lock (SyncRoot) {
            if (prependEmptyLine) { Console.Out.WriteLine(); }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Out.Write("### " + DateTime.Now.ToString("HH:mm:ss.fff") + " ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Error.WriteLine(s);
            Console.ResetColor();
        }
    }

    #endregion Log


    #region Engine

    private readonly static AASeqOutputOptions NodeOutputOptions = AASeqOutputOptions.Default with { ExtraEmptyRootNodeLines = true, NoTypeAnnotation = true };
    private readonly static AASeqOutputOptions NodeOutputOptionsDocument = NodeOutputOptions with {
        AnsiColorName = 33,
        AnsiColorType = 33,
        AnsiColorValue = 93,
        AnsiColorPropertyName = 33,
        AnsiColorPropertyValue = 33,
        AnsiColorSymbols = 33,
    };
    private readonly static AASeqOutputOptions NodeOutputOptionsCommand = NodeOutputOptions with {
        AnsiColorName = 35,
        AnsiColorType = 35,
        AnsiColorValue = 95,
        AnsiColorPropertyName = 35,
        AnsiColorPropertyValue = 35,
        AnsiColorSymbols = 35,
    };
    private readonly static AASeqOutputOptions NodeOutputOptionsMessageOut = NodeOutputOptions with {
        AnsiColorName = 36,
        AnsiColorType = 36,
        AnsiColorValue = 96,
        AnsiColorPropertyName = 36,
        AnsiColorPropertyValue = 36,
        AnsiColorSymbols = 36,
    };
    private readonly static AASeqOutputOptions NodeOutputOptionsMessageIn = NodeOutputOptions with {
        AnsiColorName = 32,
        AnsiColorType = 32,
        AnsiColorValue = 92,
        AnsiColorPropertyName = 32,
        AnsiColorPropertyValue = 32,
        AnsiColorSymbols = 32,
    };
    private readonly static AASeqOutputOptions NodeOutputOptionsMessageError = NodeOutputOptions with {
        AnsiColorName = 31,
        AnsiColorType = 31,
        AnsiColorValue = 91,
        AnsiColorPropertyName = 31,
        AnsiColorPropertyValue = 31,
        AnsiColorSymbols = 31,
    };

    public static void WriteNote(string s, int level = 1, bool prependEmptyLine = false) {
        lock (SyncRoot) {
            if (prependEmptyLine) { Console.Out.WriteLine(); }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Out.Write(new string('#', level) + new string(' ', 4 - level) + DateTime.Now.ToString("HH:mm:ss.fff") + " ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Out.WriteLine(s);
            Console.ResetColor();
        }
    }

    public static void WriteDocument(AASeqNodes nodes) {
        lock (SyncRoot) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            nodes.Save(Console.Out, NodeOutputOptionsDocument);
            Console.ResetColor();
        }
    }

    public static void WriteActionOk(IFlowAction action, AASeqNode node) {
        lock (SyncRoot) {
            if (action.IsCommand) {
                Console.ForegroundColor = ConsoleColor.Magenta;
                node.Save(Console.Out, NodeOutputOptionsCommand);
            } else if (action.IsMessageOut) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                node.Save(Console.Out, NodeOutputOptionsMessageOut);
            } else if (action.IsMessageIn) {
                node.Save(Console.Out, NodeOutputOptionsMessageIn);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.ResetColor();
        }
    }

    public static void WriteActionNok(IFlowAction _, AASeqNode node) {
        lock (SyncRoot) {
            Console.ForegroundColor = ConsoleColor.Red;
            node.Save(Console.Out, NodeOutputOptionsMessageError);
            Console.ResetColor();
        }
    }

    #endregion Engine

}
