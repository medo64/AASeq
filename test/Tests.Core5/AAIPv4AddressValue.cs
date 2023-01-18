using System;
using System.Net;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAIPv4AddressValue_Tests {

    [TestMethod]
    public void AAIPv4AddressValue_Basic() {
        var text = "239.192.111.17";
        Assert.IsTrue(AAIPv4AddressValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAIPv4AddressValue.Parse(text));
        Assert.AreEqual(result, AAIPv4AddressValue.Parse(text));
        Assert.AreEqual(result, IPAddress.Parse(text));
    }

    [TestMethod]
    public void AAIPv4AddressValue_FailedParse() {
        Assert.IsFalse(AAIPv4AddressValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAIPv4AddressValue.Parse("A");
        });
    }

    [TestMethod]
    public void AAIPv4AddressValue_OutOfRange() {
        Assert.IsFalse(AAIPv4AddressValue.TryParse("256.0.0.1", out var _));
        Assert.IsFalse(AAIPv4AddressValue.TryParse("ff08::152", out var _));
    }

}
