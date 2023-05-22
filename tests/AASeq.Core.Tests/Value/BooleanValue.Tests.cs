using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

namespace Tests;

[TestClass]
public class BooleanValue_Tests {

    [TestMethod]
    public void BooleanValue_Basic() {
        var text = "True";
        Assert.IsTrue(BooleanValue.TryParse(text, out var result));
        Assert.AreEqual(text, result.ToString());
        Assert.AreEqual(text, result);
    }

    [TestMethod]
    public void BooleanValue_True() {
        {
            Assert.IsTrue(BooleanValue.TryParse("True", out var result));
            Assert.IsTrue(result);
        }
        {
            Assert.IsTrue(BooleanValue.TryParse("T", out var result));
            Assert.IsTrue(result);
        }
        {
            Assert.IsTrue(BooleanValue.TryParse("Yes", out var result));
            Assert.IsTrue(result);
        }
        {
            Assert.IsTrue(BooleanValue.TryParse("Y", out var result));
            Assert.IsTrue(result);
        }
        {
            Assert.IsTrue(BooleanValue.TryParse("+", out var result));
            Assert.IsTrue(result);
        }
    }

    [TestMethod]
    public void BooleanValue_False() {
        {
            Assert.IsTrue(BooleanValue.TryParse("False", out var result));
            Assert.IsFalse(result);
        }
        {
            Assert.IsTrue(BooleanValue.TryParse("F", out var result));
            Assert.IsFalse(result);
        }
        {
            Assert.IsTrue(BooleanValue.TryParse("No", out var result));
            Assert.IsFalse(result);
        }
        {
            Assert.IsTrue(BooleanValue.TryParse("N", out var result));
            Assert.IsFalse(result);
        }
        {
            Assert.IsTrue(BooleanValue.TryParse("-", out var result));
            Assert.IsFalse(result);
        }
    }

    [TestMethod]
    public void BooleanValue_FailedParse() {
        Assert.IsFalse(BooleanValue.TryParse("A", out var _));
    }

}
