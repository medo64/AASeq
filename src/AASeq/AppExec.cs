using AASeq;
using System.IO;

namespace AASeqCli;

internal static class AppExec {

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
