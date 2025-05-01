namespace DiamHexDecoder;
using System;
using System.Collections.Generic;
using System.IO;
using AASeq;
using AASeqPlugin;

internal static class App {

    internal static void Main(string[] args) {
        Console.WriteLine("Wireshark: Copy ... as a Hex Stream:");
        var hexStream = Console.ReadLine() ?? "";

        using var memory = new MemoryStream();
        for (int i = 0; i < hexStream.Length; i += 2) {
            var hex = hexStream.Substring(i, 2);
            var b = Convert.ToByte(hex, 16);
            memory.WriteByte(b);
        }
        memory.Position = 0;
        var bytes = memory.ToArray();

        var message = DiameterMessage.ReadFrom(bytes);
        if (message is null) {
            Console.WriteLine("Failed to decode message.");
            return;
        }
        var nodes = DiameterEncoder.Decode(message, out var messageName);
        var node = new AASeqNode(messageName, message.HasRequestFlag ? ">Remote" : "<Remote", nodes);

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        node.Save(Console.Out);
        Console.ResetColor();
    }

}
