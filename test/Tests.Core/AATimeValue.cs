using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AATimeValue_Tests {

    [TestMethod]
    public void AATimeValue_Basic() {
        var text = "23:20:59";
        Assert.IsTrue(AATimeValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, AATimeValue.Parse(text));
    }

    [TestMethod]
    public void AATimeValue_FailedParse() {
        Assert.IsFalse(AATimeValue.TryParse("A", out var _));
        Assert.ThrowsException<FormatException>(() => {
            AATimeValue.Parse("A");
        });
    }

    [TestMethod]
    public void AATimeValue_MinutesOnly() {
        Assert.AreEqual("23:20:00", AATimeValue.Parse("23:20"));
    }

    [TestMethod]
    public void AATimeValue_Milliseconds() {
        Assert.AreEqual("23:23:12.564", AATimeValue.Parse("23:23:12.564"));
    }

    [TestMethod]
    public void AATimeValue_Nanos() {
        Assert.AreEqual("23:23:12.5643442", AATimeValue.Parse("23:23:12.5643442"));
    }

    [TestMethod]
    public void AATimeValue_NanosWithZeroes() {
        Assert.AreEqual("23:23:12.56434", AATimeValue.Parse("23:23:12.5643400"));
        Assert.AreEqual("23:23:12", AATimeValue.Parse("23:23:12.0000000"));
    }

}
