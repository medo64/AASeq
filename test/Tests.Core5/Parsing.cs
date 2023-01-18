namespace Tests.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;
using System;

[TestClass]
public class Parsing_Tests {

    #region Sections

    [DataTestMethod]
    [DataRow("")]
    public void Parsing_Sections_Empty(string text) {
        var lines = Parsing.ParseLines(Helper.GetStreamFromString(text));
        var sections = Parsing.ParseSections(lines);
        Assert.AreEqual(0, sections.Count);
    }

    [DataTestMethod]
    [DataRow("[A]")]
    [DataRow("[A]\n")]
    [DataRow("[A]\n\n")]
    public void Parsing_Sections_One(string text) {
        var lines = Parsing.ParseLines(Helper.GetStreamFromString(text));
        var sections = Parsing.ParseSections(lines);
        Assert.AreEqual(1, sections.Count);
        Assert.AreEqual("[A]", sections[0].HeaderLine);
        Assert.AreEqual(lines.Count - 1, sections[0].ContentLines.Count);
    }

    [DataTestMethod]
    [DataRow("[A]\n[B]", 0, 0)]
    [DataRow("[A]\n\n[B]", 1, 0)]
    [DataRow("[A]\n\n\n[B]\n", 2, 1)]
    [DataRow("\n\n[A]\r\n[B]\n", 0, 1)]
    [DataRow("#!\n\n[A]\r\n[B]\n", 0, 1)]
    [DataRow("#!\n//\n\n[A]\r\n[B]\n", 0, 1)]
    public void Parsing_Sections_Two(string text, int expectedLinesA, int expectedLinesB) {
        var lines = Parsing.ParseLines(Helper.GetStreamFromString(text));
        var sections = Parsing.ParseSections(lines);
        Assert.AreEqual(2, sections.Count);
        Assert.AreEqual("[A]", sections[0].HeaderLine);
        Assert.AreEqual(expectedLinesA, sections[0].ContentLines.Count);
        Assert.AreEqual("[B]", sections[1].HeaderLine);
        Assert.AreEqual(expectedLinesB, sections[1].ContentLines.Count);

    }

    [DataTestMethod]
    [DataRow("#!\nA")]
    [DataRow("#!\n\nA")]
    [DataRow("A")]
    [DataRow("/")]
    [DataRow("\nA")]
    public void Parsing_Sections_Error(string text) {
        var lines = Parsing.ParseLines(Helper.GetStreamFromString(text));
        Assert.ThrowsException<FormatException>(() => {
            var _ = Parsing.ParseSections(lines);
        });
    }

    #endregion Sections


    #region Lines

    [DataTestMethod]
    [DataRow("")]
    [DataRow("A")]
    [DataRow("AB")]
    public void Parsing_Lines_Single(string text) {
        var lines = Parsing.ParseLines(Helper.GetStreamFromString(text));
        Assert.AreEqual(1, lines.Count);
        Assert.AreEqual(text, lines[0].Text);
        Assert.AreEqual(1, lines[0]);
    }

    [DataTestMethod]
    [DataRow("\n")]
    [DataRow("\r")]
    [DataRow("\r\n")]
    public void Parsing_Lines_TwoEmpty(string text) {
        var lines = Parsing.ParseLines(Helper.GetStreamFromString(text));
        Assert.AreEqual(2, lines.Count);
        Assert.AreEqual("", lines[0]);
        Assert.AreEqual("", lines[1]);
        Assert.AreEqual(1, lines[0]);
        Assert.AreEqual(2, lines[1]);
    }

    [DataTestMethod]
    [DataRow("\n\n")]
    [DataRow("\n\r")]
    [DataRow("\n\r\n")]
    [DataRow("\r\r")]
    [DataRow("\r\r\n")]
    [DataRow("\r\n\n")]
    [DataRow("\r\n\r")]
    [DataRow("\r\n\r\n")]
    public void Parsing_Lines_ThreeEmptyMixed(string text) {
        var lines = Parsing.ParseLines(Helper.GetStreamFromString(text));
        Assert.AreEqual(3, lines.Count);
        Assert.AreEqual("", lines[0]);
        Assert.AreEqual("", lines[1]);
        Assert.AreEqual("", lines[2]);
        Assert.AreEqual(1, lines[0]);
        Assert.AreEqual(2, lines[1]);
        Assert.AreEqual(3, lines[2]);
    }

    [DataTestMethod]
    [DataRow("A\nB\nC")]
    [DataRow("A\nB\rC")]
    [DataRow("A\nB\r\nC")]
    [DataRow("A\rB\nC")]
    [DataRow("A\rB\rC")]
    [DataRow("A\rB\r\nC")]
    [DataRow("A\r\nB\nC")]
    [DataRow("A\r\nB\rC")]
    [DataRow("A\r\nB\r\nC")]
    public void Parsing_Lines_ThreeMixed(string text) {
        var lines = Parsing.ParseLines(Helper.GetStreamFromString(text));
        Assert.AreEqual(3, lines.Count);
        Assert.AreEqual("A", lines[0]);
        Assert.AreEqual("B", lines[1]);
        Assert.AreEqual("C", lines[2]);
        Assert.AreEqual(1, lines[0]);
        Assert.AreEqual(2, lines[1]);
        Assert.AreEqual(3, lines[2]);
    }

    [TestMethod]
    public void Parsing_Lines_BOM_Empty() {
        var bytes = new byte[] { 0xEF, 0xBB, 0xBF };
        var lines = Parsing.ParseLines(Helper.GetStreamFromBytes(bytes));
        Assert.AreEqual(1, lines.Count);
        Assert.AreEqual("", lines[0].Text);
        Assert.AreEqual(1, lines[0]);
    }

    [TestMethod]
    public void Parsing_Lines_BOM_SingleLetter() {
        var bytes = new byte[] { 0xEF, 0xBB, 0xBF, 0x41 };
        var lines = Parsing.ParseLines(Helper.GetStreamFromBytes(bytes));
        Assert.AreEqual(1, lines.Count);
        Assert.AreEqual("A", lines[0].Text);
        Assert.AreEqual(1, lines[0]);
    }

    #endregion Lines

}
