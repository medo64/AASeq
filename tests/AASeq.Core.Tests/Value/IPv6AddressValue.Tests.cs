using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class IPv6AddressValue_Tests {

    [TestMethod]
    public void IPv6AddressValue_Basic() {
        var text = "ff08::152";
        Assert.IsTrue(IPv6AddressValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
        Assert.AreEqual(result, IPAddress.Parse(text));
    }

    [TestMethod]
    public void IPv6AddressValue_FailedParse() {
        Assert.IsFalse(IPv6AddressValue.TryParse("A", out var _));
    }

    [TestMethod]
    public void IPv6AddressValue_OutOfRange() {
        Assert.IsFalse(IPv6AddressValue.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
        Assert.IsFalse(IPv6AddressValue.TryParse("239.192.111.17", out var _));
    }

}
