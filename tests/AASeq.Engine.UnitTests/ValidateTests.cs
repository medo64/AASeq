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
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_BasicMatchCase() {
        var nodes = AASeqNodes.Parse("Test1");
        var match = AASeqNodes.Parse("test1");
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_BasicNonmatch() {
        var nodes = AASeqNodes.Parse("Test1");
        var match = AASeqNodes.Parse("Test2");
        Assert.ThrowsException<InvalidOperationException>(() => Engine.Validate(nodes, match));
    }


    [TestMethod]
    public void Validate_PropertyMatch() {
        var nodes = AASeqNodes.Parse("Test1 k=v");
        var match = AASeqNodes.Parse("Test1 k=v");
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_PropertyMatchCase() {
        var nodes = AASeqNodes.Parse("Test1 k=v");
        var match = AASeqNodes.Parse("Test1 K=V");
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_PropertyKeyNonmatch() {
        var nodes = AASeqNodes.Parse("Test1 k=v");
        var match = AASeqNodes.Parse("Test1 k2=v");
        Assert.ThrowsException<InvalidOperationException>(() => Engine.Validate(nodes, match));
    }

    [TestMethod]
    public void Validate_PropertyValueNonmatch() {
        var nodes = AASeqNodes.Parse("Test1 k=v");
        var match = AASeqNodes.Parse("Test1 k=v2");
        Assert.ThrowsException<InvalidOperationException>(() => Engine.Validate(nodes, match));
    }


    [TestMethod]
    public void Validate_Multi() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E } }");
        var match = AASeqNodes.Parse("A { B { D } } A { B { E } } A { B { C } }");
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_MultiNonmatch() {
        var doc1 = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E } }");
        var doc2 = AASeqNodes.Parse("A { B { D } } A { B { E } } A { B { F } }");
        Assert.ThrowsException<InvalidOperationException>(() => Engine.Validate(doc1, doc2));
    }

    [TestMethod]
    public void Validate_MultiProp() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E k=v } }");
        var match = AASeqNodes.Parse("A { B { D } } A { B { E } } A { B { C } }");
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_MultiPropCase() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E k=v } }");
        var match = AASeqNodes.Parse("a { b { e } } a { b { d } } a { b { c } }");
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_MultiPropNonMatch() {
        var nodes = AASeqNodes.Parse("A { B { C } } A { B { D } } A { B { E } }");
        var match = AASeqNodes.Parse("A { B { D } } A { B { E } } A { B { C k=v } }");
        Assert.ThrowsException<InvalidOperationException>(() => Engine.Validate(nodes, match));
    }


    [TestMethod]
    public void Validate_Value() {
        var nodes = AASeqNodes.Parse("A aa");
        var match = AASeqNodes.Parse("a aa");
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_ValueNonMatch() {
        var nodes = AASeqNodes.Parse("A Aa");
        var match = AASeqNodes.Parse("a aa");
        Assert.ThrowsException<InvalidOperationException>(() => Engine.Validate(nodes, match));
    }

    [TestMethod]
    public void Validate_ValueTextMatch() {
        var nodes = AASeqNodes.Parse("A 12");
        var match = AASeqNodes.Parse("a \"12\"");
        Engine.Validate(nodes, match);
    }


    [TestMethod]
    public void Validate_ValueRegexMatch() {
        var nodes = AASeqNodes.Parse("A Test");
        var match = AASeqNodes.Parse("a /op=regex Te..");
        Engine.Validate(nodes, match);
    }

    [TestMethod]
    public void Validate_ValueRegexNonMatch() {
        var nodes = AASeqNodes.Parse("A Test");
        var match = AASeqNodes.Parse("a /op=regex TeX.");
        Assert.ThrowsException<InvalidOperationException>(() => Engine.Validate(nodes, match));
    }

}
