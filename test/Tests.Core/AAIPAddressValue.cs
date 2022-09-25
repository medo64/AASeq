using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAIPAddressValue_Tests {

    [TestMethod]
    public void AAIPAddressValue_BasicV4() {
        var text = "239.192.111.17";
        Assert.IsTrue(AAIPAddressValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAIPAddressValue.Parse(text));
    }

    [TestMethod]
    public void AAIPAddressValue_BasicV6() {
        var text = "ff08::152";
        Assert.IsTrue(AAIPAddressValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AAIPAddressValue.Parse(text));
    }

    [TestMethod]
    public void AAIPAddressValue_FailedParse() {
        Assert.IsFalse(AAIPAddressValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AAIPAddressValue.Parse("A");
        });
    }

    [TestMethod]
    public void AAIPAddressValue_OutOfRange() {
        Assert.IsFalse(AAIPAddressValue.TryParse("256.0.0.1", out var _));
        Assert.IsFalse(AAIPAddressValue.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
    }

}
