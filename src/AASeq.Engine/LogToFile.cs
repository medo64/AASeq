namespace AASeq;
using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;

internal class LogToFile {

    /// <summary>
    /// Creates a new instance.
    /// Instance is not activated until SetDestination is called.
    /// </summary>
    public LogToFile() {
    }

    /// <summary>
    /// Gets if the file log is enabled.
    /// </summary>
    public bool IsEnabled { get { return Path is not null; } }

    /// <summary>
    /// Gets the destination path for the log file.
    /// </summary>
    public string? Path { get; private set; }

    /// <summary>
    /// Sets the destination path for the log file.
    /// </summary>
    /// <param name="path"></param>
    public void SetDestination(string path) {
        lock (SyncRoot) {
            ArgumentNullException.ThrowIfNull(path);
            var file = new FileInfo(path);
            if (file.Directory is null) { throw new InvalidOperationException("Cannot determine log directory."); ; }
            if (!file.Directory.Exists) {
                Directory.CreateDirectory(file.Directory.FullName);  // also creates intermediate directories
            }
            Stream = File.OpenWrite(file.FullName);
            Stream.SetLength(0);
            Path = path;
        }
    }


    private FileStream? Stream;
    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    private static readonly byte[] NewLineBytes = Utf8.GetBytes("\n");  // use LF on Windows too
    private static readonly AASeqOutputOptions NodeOutputOptions = AASeqOutputOptions.Default with { ExtraEmptyRootNodeLines = true, NoTypeAnnotation = true, NewLine = "\n" };
    private static readonly Lock SyncRoot = new();

    private void WriteLine(string s, int level = 1, bool prependEmptyLine = false) {
        lock (SyncRoot) {
            if (Stream is null) { return; }
            if (prependEmptyLine) { Stream?.Write(NewLineBytes); }
            Stream?.Write(Utf8.GetBytes(new string('#', level) + new string(' ', 4 - level) + DateTime.Now.ToString("HH:mm:ss.fff") + " "));
            Stream?.Write(Utf8.GetBytes(s));
            Stream?.Write(NewLineBytes);
            Stream?.Flush();
        }
    }

    public void WriteLog(string text) {
        WriteLine(text, level: 3, prependEmptyLine: false);
    }

    public void WriteFlowBegin(int flowIndex) {
        WriteLine($"Flow: {flowIndex}", level: 1, prependEmptyLine: true);
    }

    public void WriteActionStart(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        WriteLine($"Action: {flowIndex}:{actionIndex}", level: 2, prependEmptyLine: true);
    }

    public void WriteActionDone(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        lock (SyncRoot) {
            WriteLine("[OK]", level: 3);
            if (Stream is null) { return; }
            node.Save(Stream, NodeOutputOptions);
            Stream.Flush();
        }
    }

    public void WriteActionError(int flowIndex, int actionIndex, IFlowAction action, AASeqNode node) {
        lock (SyncRoot) {
            WriteLine("[NOK]", level: 3);
            if (Stream is null) { return; }
            node.Save(Stream, NodeOutputOptions);
            Stream.Flush();
        }
    }

    public void WriteFlowDone(int flowIndex) {
    }

}
