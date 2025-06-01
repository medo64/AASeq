namespace Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AASeq;
using AASeq.Plugins.Standard;

[TestClass]
public sealed class CalcCommandTests {

    [TestMethod]
    public void CalcCommand_Basic() {
        Assert.AreEqual("8", Calc.Evaluate("3 + 5"));
    }

    [TestMethod]
    public void CalcCommand_Prio() {
        Assert.AreEqual("8", Calc.Evaluate("2 + 2 * 3"));
    }

    [TestMethod]
    public void CalcCommand_Braces() {
        Assert.AreEqual("12", Calc.Evaluate("(2 + 2) * 3"));
    }

    [TestMethod]
    public void CalcCommand_Power() {
        Assert.AreEqual("18", Calc.Evaluate("(2 + 2^2) * 3"));
    }

}
