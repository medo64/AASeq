using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class IPAddressValue_Tests {

    [TestMethod]
    public void IPAddressValue_BasicV4() {
        var text = "239.192.111.17";
        Assert.IsTrue(IPAddressValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
        Assert.AreEqual(result, IPAddress.Parse(text));
    }

    [TestMethod]
    public void IPAddressValue_BasicV6() {
        var text = "ff08::152";
        Assert.IsTrue(IPAddressValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
        Assert.AreEqual(IPAddress.Parse(text), result);
    }

    [TestMethod]
    public void IPAddressValue_FailedParse() {
        Assert.IsFalse(IPAddressValue.TryParse("A", out var _));
    }

    [TestMethod]
    public void IPAddressValue_OutOfRange() {
        Assert.IsFalse(IPAddressValue.TryParse("256.0.0.1", out var _));
        Assert.IsFalse(IPAddressValue.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
    }

}
