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
        Assert.AreEqual(4, attr.DataLengthWithPadding);
        Assert.AreEqual(12, attr.Length);
        Assert.AreEqual(12, attr.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-0C-00-01-02-03", BitConverter.ToString(Helpers.GetRawBytes(attr)));
    }

    [TestMethod]
    public void DiameterAvp_Value_Empty() {
        var attr = new DiameterAvp(0, 0, null, []);
        Assert.AreEqual(0, attr.DataLength);
        Assert.AreEqual(0, attr.DataLengthWithPadding);
        Assert.AreEqual(8, attr.Length);
        Assert.AreEqual(8, attr.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-08", BitConverter.ToString(Helpers.GetRawBytes(attr)));
    }

    [TestMethod]
    public void DiameterAvp_Value_EmptyWithVendor() {
        var attr = new DiameterAvp(0, 0, 10415, []);
        Assert.AreEqual(0, attr.DataLength);
        Assert.AreEqual(0, attr.DataLengthWithPadding);
        Assert.AreEqual(12, attr.Length);
        Assert.AreEqual(12, attr.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-0C-00-00-28-AF", BitConverter.ToString(Helpers.GetRawBytes(attr)));
    }

    [TestMethod]
    public void DiameterAvp_Value_PaddingNeeded() {
        var attr = new DiameterAvp(0, 0, null, [1, 2, 3]);
        Assert.AreEqual(3, attr.DataLength);
        Assert.AreEqual(4, attr.DataLengthWithPadding);
        Assert.AreEqual(11, attr.Length);
        Assert.AreEqual(12, attr.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-0B-01-02-03-00", BitConverter.ToString(Helpers.GetRawBytes(attr)));
    }

    [TestMethod]
    public void DiameterAvp_Value_PaddingNeededWithVendor() {
        var attr = new DiameterAvp(0, 0, 10415, [1]);
        Assert.AreEqual(1, attr.DataLength);
        Assert.AreEqual(4, attr.DataLengthWithPadding);
        Assert.AreEqual(13, attr.Length);
        Assert.AreEqual(16, attr.LengthWithPadding);
        Assert.AreEqual("00-00-00-00-00-00-00-0D-00-00-28-AF-01-00-00-00", BitConverter.ToString(Helpers.GetRawBytes(attr)));
    }

}
