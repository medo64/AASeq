using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AASeq.Test;

[TestClass]
public class ResultCollectionTests {

    [TestMethod]
    public void Failure_NewWarning() {
        var result = Failure.NewWarning(1, "Test: {0}", "XXX");

        Assert.AreEqual(true, result.IsWarning);
        Assert.AreEqual(false, result.IsError);
        Assert.AreEqual(1, result.Line);
        Assert.AreEqual("Test: XXX", result.Text);
    }

    [TestMethod]
    public void Failure_NewError() {
        var result = Failure.NewError(1, "Test: {0}", "XXX");

        Assert.AreEqual(false, result.IsWarning);
        Assert.AreEqual(true, result.IsError);
        Assert.AreEqual(1, result.Line);
        Assert.AreEqual("Test: XXX", result.Text);
    }

    [TestMethod]
    public void Failure_Clone() {
        var resultInit = Failure.NewError(1, "Test: {0}", "XXX");
        var result = resultInit.Clone();

        Assert.AreEqual(false, result.IsWarning);
        Assert.AreEqual(true, result.IsError);
        Assert.AreEqual(1, result.Line);
        Assert.AreEqual("Test: XXX", result.Text);
    }

    [TestMethod]
    public void Failure_CloneWithPrefix() {
        var resultInit = Failure.NewWarning(1, "Test: {0}", "XXX");
        var result = resultInit.Clone("P: ");

        Assert.AreEqual(true, result.IsWarning);
        Assert.AreEqual(false, result.IsError);
        Assert.AreEqual(1, result.Line);
        Assert.AreEqual("P: Test: XXX", result.Text);
    }

}
