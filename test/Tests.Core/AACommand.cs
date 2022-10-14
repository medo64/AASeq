using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class AACommand_Tests {

    [TestMethod]
    public void AACommand_Basic() {
        var x = new AACommand("Test");
        Assert.AreEqual("Test", x.Name);
        Assert.AreEqual(0, x.Fields.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AACommand_NameCannotBeNull() {
        var _ = new AACommand(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AACommand_NameCannotBeEmpty() {
        var _ = new AACommand("");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AACommand_ValidationFailed() {
        var _ = new AACommand("3_");
    }

    [TestMethod]
    public void AACommand_Clone() {
        var s = new AACommand("Name");
        s.Fields.Add(new AAField("H1", "V1"));
        s.Fields.Add(new AAField("H2", new AAFieldCollection()));
        s.Fields.Add(new AAField("F1", "V1"));
        s.Fields.Add(new AAField("F2", new AAFieldCollection()));

        var x = s.Clone();

        Assert.AreEqual("Name", x.Name);
        Assert.AreEqual(4, ((AACommand)x).Fields.Count);
        Assert.AreEqual("H1", ((AACommand)x).Fields[0].Name);
        Assert.AreEqual("V1", ((AACommand)x).Fields[0].Value);
        Assert.AreEqual("H2", ((AACommand)x).Fields[1].Name);
        Assert.AreEqual(true, ((AACommand)x).Fields[1].HasSubfields);
        Assert.AreEqual("F1", ((AACommand)x).Fields[2].Name);
        Assert.AreEqual("V1", ((AACommand)x).Fields[2].Value);
        Assert.AreEqual("F2", ((AACommand)x).Fields[3].Name);
        Assert.AreEqual(true, ((AACommand)x).Fields[3].HasSubfields);
    }

}
