namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AASeq;

[TestClass]
public sealed class ValidateTests {

    [TestMethod]
    public void Validate_BasicMatch() {
        var nodes = AASeqNodes.Parse("Test1");
        var match = AASeqNodes.Parse("Test1");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_BasicMatchCase() {
        var nodes = AASeqNodes.Parse("Test1");
        var match = AASeqNodes.Parse("test1");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_BasicNonmatch() {
        var nodes = AASeqNodes.Parse("Test1");
        var match = AASeqNodes.Parse("Test2");
        Assert.IsFalse(nodes.TryValidate(match));
    }


    [TestMethod]
    public void Validate_PropertyMatch() {
        var nodes = AASeqNodes.Parse("Test1 k=v");
        var match = AASeqNodes.Parse("Test1 k=v");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_PropertyMatchCase() {
        var nodes = AASeqNodes.Parse("Test1 k=v");
        var match = AASeqNodes.Parse("Test1 K=V");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_PropertyKeyNonmatch() {
        var nodes = AASeqNodes.Parse("Test1 k=v");
        var match = AASeqNodes.Parse("Test1 k2=v");
        Assert.IsFalse(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_PropertyValueNonmatch() {
        var nodes = AASeqNodes.Parse("Test1 k=v");
        var match = AASeqNodes.Parse("Test1 k=v2");
        Assert.IsFalse(nodes.TryValidate(match));
    }


    [TestMethod]
    public void Validate_Multi() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E } }");
        var match = AASeqNodes.Parse("A { B { D } } A { B { E } } A { B { C } }");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_MultiNonmatch() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E } }");
        var match = AASeqNodes.Parse("A { B { D } } A { B { E } } A { B { F } }");
        Assert.IsFalse(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_MultiProp() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E k=v } }");
        var match = AASeqNodes.Parse("A { B { D } } A { B { E } } A { B { C } }");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_MultiPropCase() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E k=v } }");
        var match = AASeqNodes.Parse("a { b { e } } a { b { d } } a { b { c } }");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_MultiPropNonMatch() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E } }");
        var match = AASeqNodes.Parse("A { B { D } } A { B { E } } A { B { C k=v } }");
        Assert.IsFalse(nodes.TryValidate(match));
    }


    [TestMethod]
    public void Validate_Value() {
        var nodes = AASeqNodes.Parse("A aa");
        var match = AASeqNodes.Parse("a aa");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueNonMatch() {
        var nodes = AASeqNodes.Parse("A Aa");
        var match = AASeqNodes.Parse("a aa");
        Assert.IsFalse(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueTextMatch() {
        var nodes = AASeqNodes.Parse("A 12");
        var match = AASeqNodes.Parse("a \"12\"");
        Assert.IsTrue(nodes.TryValidate(match));
    }


    [TestMethod]
    public void Validate_ValueRegexMatch() {
        var nodes = AASeqNodes.Parse("A Test");
        var match = AASeqNodes.Parse("a /op=regex Te..");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueRegexNonMatch() {
        var nodes = AASeqNodes.Parse("A Test");
        var match = AASeqNodes.Parse("a /op=regex TeX.");
        Assert.IsFalse(nodes.TryValidate(match));
    }


    [TestMethod]
    public void Validate_ValueEq() {
        var nodes = AASeqNodes.Parse("A 10.0");
        var match = AASeqNodes.Parse("a /op=eq 10.0");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueEqNonMatch() {
        var nodes = AASeqNodes.Parse("A 10.0");
        var match = AASeqNodes.Parse("a /op=eq 10.1");
        Assert.IsFalse(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueNe() {
        var nodes = AASeqNodes.Parse("A 10");
        var match = AASeqNodes.Parse("a /op=ne 10.1");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueNeNonMatch() {
        var nodes = AASeqNodes.Parse("A 10");
        var match = AASeqNodes.Parse("a /op=ne 10.0");
        Assert.IsFalse(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueLt() {
        var nodes = AASeqNodes.Parse("A \"10\"");
        var match = AASeqNodes.Parse("a /op=lt 11");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueLtNonMatch() {
        var nodes = AASeqNodes.Parse("A 9.0");
        var match = AASeqNodes.Parse("a /op=lt 9");
        Assert.IsFalse(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueLe() {
        var nodes = AASeqNodes.Parse("A \"10.0\"");
        var match = AASeqNodes.Parse("a /op=le 10");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueLeNonMatch() {
        var nodes = AASeqNodes.Parse("A 10");
        var match = AASeqNodes.Parse("a /op=le 9.0");
        Assert.IsFalse(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueGt() {
        var nodes = AASeqNodes.Parse("A (i8)10");
        var match = AASeqNodes.Parse("a /op=gt 9");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueGtNonMatch() {
        var nodes = AASeqNodes.Parse("A 9");
        var match = AASeqNodes.Parse("a /op=gt (i16)9");
        Assert.IsFalse(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueGe() {
        var nodes = AASeqNodes.Parse("A (half)10.0");
        var match = AASeqNodes.Parse("a /op=ge 10");
        Assert.IsTrue(nodes.TryValidate(match));
    }

    [TestMethod]
    public void Validate_ValueGeNonMatch() {
        var nodes = AASeqNodes.Parse("A (decimal)10");
        var match = AASeqNodes.Parse("a /op=ge 11");
        Assert.IsFalse(nodes.TryValidate(match));
    }

}
