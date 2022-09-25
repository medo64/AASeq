using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AADateValue_Tests {

    [TestMethod]
    public void AADateValue_Basic() {
        var text = "2021-01-14";
        Assert.IsTrue(AADateValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AADateValue.Parse(text));
    }

    [TestMethod]
    public void AADateValue_FailedParse() {
        Assert.IsFalse(AADateValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AADateValue.Parse("A");
        });
    }

}
