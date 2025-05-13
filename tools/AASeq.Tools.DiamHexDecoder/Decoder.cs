namespace DiamHexDecoder;
using System;
using AASeq.Diameter;
using AASeq;
using System.IO;
using System.Text.Unicode;
using System.Text;

internal static class Decoder {

    public static string GetDecodedText(string wiresharkStream, bool includeAllFlags, bool includeTypeAnnotations) {
        byte[] bytes;

        try {  // first try hex
            using var memory = new MemoryStream();
            for (int i = 0; i < wiresharkStream.Length; i += 2) {
                var hex = wiresharkStream.Substring(i, 2);
                var b = Convert.ToByte(hex, 16);
                memory.WriteByte(b);
            }
            memory.Position = 0;
            bytes = memory.ToArray();
        } catch (Exception) {
            try {  // then try base64
                bytes = Convert.FromBase64String(wiresharkStream);
            } catch (Exception) {
                throw new InvalidOperationException("Cannot decode wireshark stream.");
            }
        }

        try {
            var message = DiameterMessage.ReadFrom(bytes);
            if (message is null) { throw new InvalidOperationException("Failed to decode message."); }

            var nodes = DiameterEncoder.Decode(message, includeAllFlags, out var messageName);
            var node = new AASeqNode(messageName, message.HasRequestFlag ? ">Remote" : "<Remote", nodes);

            using var outStream = new MemoryStream();
            node.Save(outStream, includeTypeAnnotations ? AASeqOutputOptions.Default : OutputOptionsWithoutType);

            return Utf8.GetString(outStream.ToArray());
        } catch (Exception) {
            throw new InvalidOperationException("Cannot decode diameter message.");
        }
    }


    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    private static readonly AASeqOutputOptions OutputOptionsWithoutType = AASeqOutputOptions.Default with { NoTypeAnnotation = true };

}
