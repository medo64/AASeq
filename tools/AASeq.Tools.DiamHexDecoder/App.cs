namespace DiamHexDecoder;

using System;

internal static partial class App {

    internal static void Main(string[] args) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Please provide Wireshark's Diameter hex stream:");
        Console.ResetColor();

        var wiresharkStream = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();

        try {
            var message = Decoder.GetDecodedText(wiresharkStream, includeAllFlags: false, includeTypeAnnotations: false);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(message);
            Console.ResetColor();
        } catch (Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }

}
