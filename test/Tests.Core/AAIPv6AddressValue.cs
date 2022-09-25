using System;
using System.Net;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAIPv6AddressValue_Tests {

    [TestMethod]
    public void AAIPv6AddressValue_Basic() {
        var text = "ff08::152";
        Assert.IsTrue(AAIPv6AddressValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAIPv6AddressValue.Parse(text));
        Assert.AreEqual(result, AAIPv6AddressValue.Parse(text));
        Assert.AreEqual(result, IPAddress.Parse(text));
    }

    [TestMethod]
    public void AAIPv6AddressValue_FailedParse() {
        Assert.IsFalse(AAIPv6AddressValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAIPv6AddressValue.Parse("A");
        });
    }

    [TestMethod]
    public void AAIPv6AddressValue_OutOfRange() {
        Assert.IsFalse(AAIPv6AddressValue.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
        Assert.IsFalse(AAIPv6AddressValue.TryParse("239.192.111.17", out var _));
    }

}
