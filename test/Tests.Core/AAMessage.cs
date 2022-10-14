using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class AAMessage_Tests {

    [TestMethod]
    public void AAMessage_Basic() {
        var x = new AAMessage("Test", new AAEndpoint("S"), new AAEndpoint("D"));
        Assert.AreEqual("Test", x.Name);
        Assert.AreEqual("S", x.Source.Name);
        Assert.AreEqual("D", x.Destination.Name);
        Assert.AreEqual(0, x.Fields.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AAMessage_NameCannotBeNull() {
        var _ = new AAMessage(null, new AAEndpoint("S"), new AAEndpoint("D"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAMessage_NameCannotBeEmpty() {
        var _ = new AAMessage("", new AAEndpoint("S"), new AAEndpoint("D"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AAMessage_SourceCannotBeNull() {
        var _ = new AAMessage("Test", null, new AAEndpoint("D"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AAMessage_DestinationCannotBeNull() {
        var _ = new AAMessage("Test", new AAEndpoint("S"), null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAMessage_SourceAndDestinationCannotBeTheSame() {
        var ep = new AAEndpoint("E");
        var _ = new AAMessage("Test", ep, ep);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAMessage_SourceAndDestinationCannotBeTheSameName() {
        var ep1 = new AAEndpoint("E");
        var ep2 = new AAEndpoint("E");
        var _ = new AAMessage("Test", ep1, ep2);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAMessage_ValidationFailed() {
        var _ = new AAMessage("3_", new AAEndpoint("S"), new AAEndpoint("D"));
    }

    [TestMethod]
    public void AAMessage_WithHeaderFields() {
        var e1 = new AAEndpoint("Test1");
        var e2 = new AAEndpoint("Test2");
        var x = new AAMessage("Test", e1, e2);
        x.Fields.Add(new AAField(".test", new AAFieldCollection()));
        Assert.AreEqual("Test", x.Name);
        Assert.AreEqual(e1, x.Source);
        Assert.AreEqual(e2, x.Destination);
        Assert.AreEqual(1, x.Fields.Count);
    }

    [TestMethod]
    public void AAMessage_WithFields() {
        var e1 = new AAEndpoint("Test1");
        var e2 = new AAEndpoint("Test2");
        var x = new AAMessage("Test", e1, e2);
        x.Fields.Add(new AAField("test", new AAFieldCollection()));
        Assert.AreEqual("Test", x.Name);
        Assert.AreEqual(e1, x.Source);
        Assert.AreEqual(e2, x.Destination);
        Assert.AreEqual(1, x.Fields.Count);
    }

    [TestMethod]
    public void Message_Clone() {
        var s = new AAMessage("Name", new AAEndpoint("S"), new AAEndpoint("D"));
        s.Fields.Add(new AAField("H1", "V1"));
        s.Fields.Add(new AAField("H2", new AAFieldCollection()));
        s.Fields.Add(new AAField("F1", "V1"));
        s.Fields.Add(new AAField("F2", new AAFieldCollection()));

        var x = s.Clone();

        Assert.AreEqual(true, x.IsMessage);
        Assert.AreEqual("Name", x.Name);
        Assert.AreEqual(4, ((AAMessage)x).Fields.Count);
        Assert.AreEqual("H1", ((AAMessage)x).Fields[0].Name);
        Assert.AreEqual("V1", ((AAMessage)x).Fields[0].Value);
        Assert.AreEqual("H2", ((AAMessage)x).Fields[1].Name);
        Assert.AreEqual(true, ((AAMessage)x).Fields[1].HasSubfields);
        Assert.AreEqual("F1", ((AAMessage)x).Fields[2].Name);
        Assert.AreEqual("V1", ((AAMessage)x).Fields[2].Value);
        Assert.AreEqual("F2", ((AAMessage)x).Fields[3].Name);
        Assert.AreEqual(true, ((AAMessage)x).Fields[3].HasSubfields);
    }

}
