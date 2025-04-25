namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AASeqPlugin;

[TestClass]
public class DiameterAvpTest {

    [TestMethod]
    public void DiameterAvp_Value() {
        var attr = new DiameterAvp(0, 0, null, [0, 1, 2, 3]);
        Assert.AreEqual("00-01-02-03", BitConverter.ToString(attr.GetData()));
        Assert.AreEqual(4, attr.DataLength);
        Assert.AreEqual(4, attr.PaddedDataLength);
        Assert.AreEqual(12, attr.Length);
    }

    [TestMethod]
    public void DiameterAvp_Value_Empty() {
        var attr = new DiameterAvp(0, 0, null, []);
        Assert.AreEqual(0, attr.DataLength);
        Assert.AreEqual(0, attr.PaddedDataLength);
        Assert.AreEqual(8, attr.Length);
    }

    [TestMethod]
    public void DiameterAvp_Value_EmptyWithVendor() {
        var attr = new DiameterAvp(0, 0, 10415, []);
        Assert.AreEqual(0, attr.DataLength);
        Assert.AreEqual(0, attr.PaddedDataLength);
        Assert.AreEqual(12, attr.Length);
    }

    [TestMethod]
    public void DiameterAvp_Value_PaddingNeeded() {
        var attr = new DiameterAvp(0, 0, null, [1, 2, 3]);
        Assert.AreEqual(3, attr.DataLength);
        Assert.AreEqual(4, attr.PaddedDataLength);
        Assert.AreEqual(11, attr.Length);
        Assert.AreEqual(12, attr.PaddedLength);
    }

    [TestMethod]
    public void DiameterAvp_Value_PaddingNeededWithVendor() {
        var attr = new DiameterAvp(0, 0, 10415, [1, 2, 3]);
        Assert.AreEqual(3, attr.DataLength);
        Assert.AreEqual(4, attr.PaddedDataLength);
        Assert.AreEqual(15, attr.Length);
        Assert.AreEqual(16, attr.PaddedLength);
    }

}
