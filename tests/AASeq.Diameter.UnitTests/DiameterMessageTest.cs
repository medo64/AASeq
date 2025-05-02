namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AASeq.Diameter;

[TestClass]
public class DiameterMessageTest {

    [TestMethod]
    public void DiameterMessage_Basic() {
        var message = new DiameterMessage(0, 0, 0, 0, 0, []);
        Assert.AreEqual("01-00-00-14-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00", BitConverter.ToString(Helpers.GetRawBytes(message)));
    }

}
