using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class IPv4AddressValue_Tests {

    [TestMethod]
    public void IPv4AddressValue_Basic() {
        var text = "239.192.111.17";
        Assert.IsTrue(IPv4AddressValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
        Assert.AreEqual(result, IPAddress.Parse(text));
    }

    [TestMethod]
    public void IPv4AddressValue_FailedParse() {
        Assert.IsFalse(IPv4AddressValue.TryParse("A", out var _));
    }

    [TestMethod]
    public void IPv4AddressValue_OutOfRange() {
        Assert.IsFalse(IPv4AddressValue.TryParse("256.0.0.1", out var _));
        Assert.IsFalse(IPv4AddressValue.TryParse("ff08::152", out var _));
    }

}
