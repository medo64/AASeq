namespace Tests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

[TestClass]
public sealed class File_Tests {

    [DataTestMethod]
    [DynamicData(nameof(BasicAssets))]
    public void File_Basic(string directory, string file) {
        File_Test(directory, file);
    }

    [DataTestMethod]
    [DynamicData(nameof(CanonicalAssets))]
    public void File_Canonical(string directory, string file) {
        File_Test(directory, file);
    }

    [DataTestMethod]
    [DynamicData(nameof(ConversionAssets))]
    public void File_Conversion(string directory, string file) {
        File_Test(directory, file);
    }

    [DataTestMethod]
    [DynamicData(nameof(ErrorAssets))]
    public void File_Error(string directory, string file) {
        File_Test(directory, file);
    }


    private void File_Test(string directory, string file) {
        var inStream = GetInputStream(directory, file)!;
        var outStream = GetOutputStream(directory, file);

        if (outStream is not null) {

            var doc = AASeqNodes.Load(inStream);
            using var newStream = new MemoryStream();
            doc.Save(newStream, new AASeqOutputOptions() { NewLine = "\n" });

            var expectedLines = GetLines(outStream);
            var actualLines = GetLines(newStream);

            for (var i = 0; i < Math.Min(expectedLines.Length, actualLines.Length); i++) {
                Assert.AreEqual(expectedLines[i], actualLines[i], $"{file}: Difference in line content");
            }
            Assert.AreEqual(expectedLines.Length, actualLines.Length, $"{file}: Line count is not the same");

        } else {  // parsing should fail
            Assert.ThrowsException<InvalidOperationException>(
                () => _ = AASeqNodes.Load(inStream)
            );
        }
    }

    private static IEnumerable<(string, string)> BasicAssets {
        get {
            var directory = "Basic";
            foreach (var file in GetDocumentNames(directory)) {
                yield return new(directory, file);
            }
        }
    }

    private static IEnumerable<(string, string)> CanonicalAssets {
        get {
            var directory = "Canonical";
            foreach (var file in GetDocumentNames(directory)) {
                yield return new(directory, file);
            }
        }
    }

    private static IEnumerable<(string, string)> ConversionAssets {
        get {
            var directory = "Conversion";
            foreach (var file in GetDocumentNames(directory)) {
                yield return new(directory, file);
            }
        }
    }

    private static IEnumerable<(string, string)> ErrorAssets {
        get {
            var directory = "Error";
            foreach (var file in GetDocumentNames(directory)) {
                yield return new(directory, file);
            }
        }
    }

    private static IEnumerable<string> GetDocumentNames(string directory) {
        var foundAny = false;
        var resNames = Assembly.GetExecutingAssembly().GetManifestResourceNames()!;
        foreach (var resName in resNames) {
            Debug.Assert(resName.StartsWith("Tests.Assets."));
            Debug.Assert(resName.EndsWith(".raw"));

            var parts = resName.Split('.');
            if (parts.Length != 6) { continue; }

            if (parts[2].Equals(directory, StringComparison.Ordinal)) {
                if (parts[3].Equals("input", StringComparison.Ordinal)) {
                    foundAny = true;
                    yield return parts[4];
                }
            }
        }
        if (!foundAny) { throw new InvalidOperationException($"No documents found"); }
    }

    private static MemoryStream GetInputStream(string directory, string file) {
        var streamName = $"Tests.Assets.{directory}.input.{file}.raw";
        return GetStream(streamName) ?? throw new InvalidOperationException($"No input stream found for {streamName}");
    }

    private static MemoryStream GetOutputStream(string directory, string file) {
        var streamName = $"Tests.Assets.{directory}.output.{file}.raw";
        return GetStream(streamName);
    }

    private static MemoryStream GetStream(string streamName) {
        var assembly = Assembly.GetExecutingAssembly();
        var resNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        foreach (var resName in resNames) {
            if (resName.Equals(streamName, StringComparison.Ordinal)) {
                var resStream = assembly.GetManifestResourceStream(streamName);
                var buffer = new byte[(int)resStream!.Length];
                resStream.Read(buffer, 0, buffer.Length);
                return new MemoryStream(buffer) { Position = 0 };
            }
        }
        return null;
    }

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
    private static string[] GetLines(MemoryStream stream) {
        return Utf8.GetString(stream.ToArray()).Split('\n');
    }

}
