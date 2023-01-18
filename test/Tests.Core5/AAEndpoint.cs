using System;
using System.Net;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAEndpoint_Tests {

    [TestMethod]
    public void AAEndpoint_Basic() {
        var x = new AAEndpoint("Test", "Protocol");
        Assert.AreEqual("Test", x.Name);
        Assert.AreEqual("Protocol", x.PluginName);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AAEndpoint_NameCannotBeNull() {
        var _ = new AAEndpoint(null, "Protocol");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAEndpoint_NameCannotBeEmpty() {
        var _ = new AAEndpoint("", "Protocol");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AAEndpoint_PluginNameCannotBeNull() {
        var _ = new AAEndpoint("Test", null);
    }

    [TestMethod]
    public void AAEndpoint_PluginNameCanBeEmpty() {
        var x = new AAEndpoint("Test", "");
        Assert.AreEqual(String.Empty, x.PluginName);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAEndpoint_ValidationFailed1() {
        var _ = new AAEndpoint("3X", "Dummy");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AAEndpoint_ValidationFailed2() {
        var _ = new AAEndpoint("X-", "Dummy");
    }

    [TestMethod]
    public void AAEndpoint_Fields() {
        var x = new AAEndpoint("Test", "Protocol");
        x.Data.Add(new AAField("P1", "V1"));
        x.Data.Add(new AAField("P2", "V2"));
        x.Data.Add(new AAField("P3", "V3"));
        Assert.AreEqual("P1", x.Data[0].Name);
        Assert.AreEqual("V1", x.Data[0].Value);
        Assert.AreEqual("P2", x.Data[1].Name);
        Assert.AreEqual("V2", x.Data[1].Value);
        Assert.AreEqual("P3", x.Data[2].Name);
        Assert.AreEqual("V3", x.Data[2].Value);
    }

    [TestMethod]
    public void AAEndpoint_Clone() {
        var s = new AAEndpoint("Test", "Protocol");
        s.Data.Add(new AAField("P1", "V1"));
        s.Data.Add(new AAField("P2", "V2"));
        s.Data.Add(new AAField("P3", "V3"));

        var x = s.Clone();
        s.Data.Clear();

        Assert.AreEqual("Test", x.Name);
        Assert.AreEqual("Protocol", x.PluginName);
        Assert.AreEqual("P1", x.Data[0].Name);
        Assert.AreEqual("V1", x.Data[0].Value);
        Assert.AreEqual("P2", x.Data[1].Name);
        Assert.AreEqual("V2", x.Data[1].Value);
        Assert.AreEqual("P3", x.Data[2].Name);
        Assert.AreEqual("V3", x.Data[2].Value);
    }

}
