using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests.Core;

[TestClass]
public class DateValue_Tests {

    [TestMethod]
    public void DateValue_Basic() {
        var text = "2021-01-14";
        Assert.IsTrue(DateValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
    }

    [TestMethod]
    public void DateValue_FailedParse() {
        Assert.IsFalse(DateValue.TryParse("A", out var _));
    }

}
