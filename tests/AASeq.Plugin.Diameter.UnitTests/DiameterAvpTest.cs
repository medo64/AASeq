namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AASeqPlugin;

[TestClass]
public class DiameterAvpTest {

    [TestMethod]
    public void DiameterAvp_NoPadding() {
        var avp = new DiameterAvp(0, 0, null, [0, 1, 2, 3]);
        Assert.AreEqual("00-01-02-03", BitConverter.ToString(avp.GetData()));
        Assert.AreEqual(4, avp.DataLength);
        Assert.AreEqual(4, avp.DataLengthWithPadding);
        Assert.AreEqual(12, avp.Length);
        Assert.AreEqual(12, avp.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-0C-00-01-02-03", BitConverter.ToString(Helpers.GetRawBytes(avp)));
    }

    [TestMethod]
    public void DiameterAvp_Empty() {
        var avp = new DiameterAvp(0, 0, null, []);
        Assert.AreEqual(0, avp.DataLength);
        Assert.AreEqual(0, avp.DataLengthWithPadding);
        Assert.AreEqual(8, avp.Length);
        Assert.AreEqual(8, avp.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-08", BitConverter.ToString(Helpers.GetRawBytes(avp)));
    }

    [TestMethod]
    public void DiameterAvp_EmptyWithVendor() {
        var avp = new DiameterAvp(0, 0, 10415, []);
        Assert.AreEqual(0, avp.DataLength);
        Assert.AreEqual(0, avp.DataLengthWithPadding);
        Assert.AreEqual(12, avp.Length);
        Assert.AreEqual(12, avp.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-0C-00-00-28-AF", BitConverter.ToString(Helpers.GetRawBytes(avp)));
    }

    [TestMethod]
    public void DiameterAvp_PaddingNeeded() {
        var avp = new DiameterAvp(0, 0, null, [1, 2, 3]);
        Assert.AreEqual(3, avp.DataLength);
        Assert.AreEqual(4, avp.DataLengthWithPadding);
        Assert.AreEqual(11, avp.Length);
        Assert.AreEqual(12, avp.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-0B-01-02-03-00", BitConverter.ToString(Helpers.GetRawBytes(avp)));
    }

    [TestMethod]
    public void DiameterAvp_PaddingNeededWithVendor() {
        var avp = new DiameterAvp(0, 0, 10415, [1]);
        Assert.AreEqual(1, avp.DataLength);
        Assert.AreEqual(4, avp.DataLengthWithPadding);
        Assert.AreEqual(13, avp.Length);
        Assert.AreEqual(16, avp.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-0D-00-00-28-AF-01-00-00-00", BitConverter.ToString(Helpers.GetRawBytes(avp)));
    }

}
