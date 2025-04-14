namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;
using System.IO;
using System.Text;

[TestClass]
public sealed class DocumentOutputTests {

    [TestMethod]
    public void DocumentOutput_Nodes_ExtraNewline() {
        var doc = AASeqNodes.Parse("test1 p1=v1 { test2 p2=v2 { test3a value3a; test3b p3b=v3b; test3c } } test2; test3 p3=v3 { test3a } test4");

        using var newStream = new MemoryStream();
        doc.Save(newStream, new AASeqOutputOptions() { NewLine = "\n", ExtraEmptyRootNodeLines = true, SkipLastNewLine = true });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("test1 p1=v1 {", outputLines[0]);
        Assert.AreEqual("    test2 p2=v2 {", outputLines[1]);
        Assert.AreEqual("        test3a value3a", outputLines[2]);
        Assert.AreEqual("        test3b p3b=v3b", outputLines[3]);
        Assert.AreEqual("        test3c", outputLines[4]);
        Assert.AreEqual("    }", outputLines[5]);
        Assert.AreEqual("}", outputLines[6]);
        Assert.AreEqual("", outputLines[7]);
        Assert.AreEqual("test2", outputLines[8]);
        Assert.AreEqual("", outputLines[9]);
        Assert.AreEqual("test3 p3=v3 {", outputLines[10]);
        Assert.AreEqual("    test3a", outputLines[11]);
        Assert.AreEqual("}", outputLines[12]);
        Assert.AreEqual("", outputLines[13]);
        Assert.AreEqual("test4", outputLines[14]);
        Assert.AreEqual(15, outputLines.Length);
    }

    [TestMethod]
    public void DocumentOutput_Nodes_Header() {
        var doc = AASeqNodes.Parse("test1 p1=v1 { test2 p2=v2 { test3a value3a; test3b p3b=v3b; test3c } } test2; test3 p3=v3 { test3a } test4");

        using var newStream = new MemoryStream();
        doc.Save(newStream, new AASeqOutputOptions() { NewLine = "\n", HeaderExecutable = "test" });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("#!/usr/bin/env test", outputLines[0]);
        Assert.AreEqual("test1 p1=v1 {", outputLines[1]);
        Assert.AreEqual("    test2 p2=v2 {", outputLines[2]);
        Assert.AreEqual("        test3a value3a", outputLines[3]);
        Assert.AreEqual("        test3b p3b=v3b", outputLines[4]);
        Assert.AreEqual("        test3c", outputLines[5]);
        Assert.AreEqual("    }", outputLines[6]);
        Assert.AreEqual("}", outputLines[7]);
        Assert.AreEqual("test2", outputLines[8]);
        Assert.AreEqual("test3 p3=v3 {", outputLines[9]);
        Assert.AreEqual("    test3a", outputLines[10]);
        Assert.AreEqual("}", outputLines[11]);
        Assert.AreEqual("test4", outputLines[12]);
        Assert.AreEqual("", outputLines[13]);
        Assert.AreEqual(14, outputLines.Length);
    }

    [TestMethod]
    public void DocumentOutput_Nodes_HeaderWithExtra() {
        var doc = AASeqNodes.Parse("test1 p1=v1 { test2 p2=v2 { test3a value3a; test3b p3b=v3b; test3c } } test2; test3 p3=v3 { test3a } test4");

        using var newStream = new MemoryStream();
        doc.Save(newStream, new AASeqOutputOptions() { NewLine = "\n", HeaderExecutable = "test", ExtraEmptyRootNodeLines = true });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("#!/usr/bin/env test", outputLines[0]);
        Assert.AreEqual("", outputLines[1]);
        Assert.AreEqual("test1 p1=v1 {", outputLines[2]);
        Assert.AreEqual("    test2 p2=v2 {", outputLines[3]);
        Assert.AreEqual("        test3a value3a", outputLines[4]);
        Assert.AreEqual("        test3b p3b=v3b", outputLines[5]);
        Assert.AreEqual("        test3c", outputLines[6]);
        Assert.AreEqual("    }", outputLines[7]);
        Assert.AreEqual("}", outputLines[8]);
        Assert.AreEqual("", outputLines[9]);
        Assert.AreEqual("test2", outputLines[10]);
        Assert.AreEqual("", outputLines[11]);
        Assert.AreEqual("test3 p3=v3 {", outputLines[12]);
        Assert.AreEqual("    test3a", outputLines[13]);
        Assert.AreEqual("}", outputLines[14]);
        Assert.AreEqual("", outputLines[15]);
        Assert.AreEqual("test4", outputLines[16]);
        Assert.AreEqual("", outputLines[17]);
        Assert.AreEqual(18, outputLines.Length);
    }

    [TestMethod]
    public void DocumentOutput_Nodes_NoTypeAnnotation() {
        var doc = AASeqNodes.Parse("test1 (duration)1 { inner 0.1 }");

        using var newStream = new MemoryStream();
        doc.Save(newStream, new AASeqOutputOptions() { NewLine = "\n", ExtraEmptyRootNodeLines = true, NoTypeAnnotation = true, SkipLastNewLine = true });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("test1 \"1s\" {", outputLines[0]);
        Assert.AreEqual("    inner 0.1", outputLines[1]);
        Assert.AreEqual("}", outputLines[2]);
        Assert.AreEqual(3, outputLines.Length);
    }


    [TestMethod]
    public void DocumentOutput_Node_ExtraNewline() {
        var doc = AASeqNodes.Parse("test1 p1=v1 { test2 p2=v2 { test3a value3a; test3b p3b=v3b; test3c } } test2; test3 p3=v3 { test3a } test4");

        using var newStream = new MemoryStream();
        doc[0].Save(newStream, new AASeqOutputOptions() { NewLine = "\n", ExtraEmptyRootNodeLines = true, SkipLastNewLine = true });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("test1 p1=v1 {", outputLines[0]);
        Assert.AreEqual("    test2 p2=v2 {", outputLines[1]);
        Assert.AreEqual("        test3a value3a", outputLines[2]);
        Assert.AreEqual("        test3b p3b=v3b", outputLines[3]);
        Assert.AreEqual("        test3c", outputLines[4]);
        Assert.AreEqual("    }", outputLines[5]);
        Assert.AreEqual("}", outputLines[6]);
        Assert.AreEqual(7, outputLines.Length);
    }

    [TestMethod]
    public void DocumentOutput_Node_Header() {
        var doc = AASeqNodes.Parse("test1 p1=v1 { test2 p2=v2 { test3a value3a; test3b p3b=v3b; test3c } } test2; test3 p3=v3 { test3a } test4");

        using var newStream = new MemoryStream();
        doc[0].Save(newStream, new AASeqOutputOptions() { NewLine = "\n", HeaderExecutable = "test" });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("#!/usr/bin/env test", outputLines[0]);
        Assert.AreEqual("test1 p1=v1 {", outputLines[1]);
        Assert.AreEqual("    test2 p2=v2 {", outputLines[2]);
        Assert.AreEqual("        test3a value3a", outputLines[3]);
        Assert.AreEqual("        test3b p3b=v3b", outputLines[4]);
        Assert.AreEqual("        test3c", outputLines[5]);
        Assert.AreEqual("    }", outputLines[6]);
        Assert.AreEqual("}", outputLines[7]);
        Assert.AreEqual("", outputLines[8]);
        Assert.AreEqual(9, outputLines.Length);
    }

    [TestMethod]
    public void DocumentOutput_Node_HeaderWithExtra() {
        var doc = AASeqNodes.Parse("test1 p1=v1 { test2 p2=v2 { test3a value3a; test3b p3b=v3b; test3c } } test2; test3 p3=v3 { test3a } test4");

        using var newStream = new MemoryStream();
        doc[0].Save(newStream, new AASeqOutputOptions() { NewLine = "\n", HeaderExecutable = "test", ExtraEmptyRootNodeLines = true });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("#!/usr/bin/env test", outputLines[0]);
        Assert.AreEqual("", outputLines[1]);
        Assert.AreEqual("test1 p1=v1 {", outputLines[2]);
        Assert.AreEqual("    test2 p2=v2 {", outputLines[3]);
        Assert.AreEqual("        test3a value3a", outputLines[4]);
        Assert.AreEqual("        test3b p3b=v3b", outputLines[5]);
        Assert.AreEqual("        test3c", outputLines[6]);
        Assert.AreEqual("    }", outputLines[7]);
        Assert.AreEqual("}", outputLines[8]);
        Assert.AreEqual("", outputLines[9]);
        Assert.AreEqual(10, outputLines.Length);
    }

    [TestMethod]
    public void DocumentOutput_Node_NoTypeAnnotation() {
        var doc = AASeqNodes.Parse("test1 (duration)1 { inner 0.1 }");

        using var newStream = new MemoryStream();
        doc[0].Save(newStream, new AASeqOutputOptions() { NewLine = "\n", ExtraEmptyRootNodeLines = true, NoTypeAnnotation = true, SkipLastNewLine = true });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("test1 \"1s\" {", outputLines[0]);
        Assert.AreEqual("    inner 0.1", outputLines[1]);
        Assert.AreEqual("}", outputLines[2]);
        Assert.AreEqual(3, outputLines.Length);
    }

    [TestMethod]
    public void DocumentOutput_Node_ZeroSecondDuration() {
        var doc = AASeqNodes.Parse("test1 (duration)0");

        using var newStream = new MemoryStream();
        doc[0].Save(newStream, new AASeqOutputOptions() { NewLine = "\n", ExtraEmptyRootNodeLines = true, SkipLastNewLine = true });
        var outputLines = GetLines(newStream);

        Assert.AreEqual("test1 (duration)\"0s\"", outputLines[0]);
        Assert.AreEqual(1, outputLines.Length);
    }


    #region Helpers

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
    private static string[] GetLines(MemoryStream stream) {
        return Utf8.GetString(stream.ToArray()).Split('\n');
    }

    #endregion Helpers

}
