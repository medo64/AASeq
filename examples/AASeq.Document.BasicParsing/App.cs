namespace BasicParsing;
using System;
using System.IO;
using AASeq;

internal class App {
    public static void Main() {
        var document = AASeqNodes.Load("test.aaseq");

        Console.WriteLine("---");
        document.Save(Console.Out, AASeqOutputOptions.Default with { ExtraEmptyRootNodeLines = true });
        Console.WriteLine("---");

        Console.WriteLine();

        Console.WriteLine("---");
        Console.WriteLine(document.ToString());
        Console.WriteLine("---");

        Console.ReadKey();
    }
}
