namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AASeq.Diameter;

[TestClass]
public class DiameterStreamTests {

    [TestMethod]
    public void DiameterStream_ReadAndWrite() {
        var hex = "01000084800001010000000018D38B30ED0476D100000108400000166C62756F726C30312E6C6F63616C0000000001284000000E66352E636F6D00000000010A4000000C00000D2F0000010D0000002B4635204249474950204469616D65746572204865616C7468204D6F6E69746F72696E6700000001014000000E0001AC1F80020000";
        var stream = Helpers.GetStreamFromHex(hex);
        var diameter = new DiameterStream(stream) { ReadBlockSize = 8, WriteBlockSize = 8 };

        var message = diameter.ReadMessage();
        Assert.AreEqual(hex.Length / 2, message.Length);

        Assert.IsTrue(message.HasRequestFlag);
        Assert.IsFalse(message.HasProxiableFlag);
        Assert.IsFalse(message.HasErrorFlag);
        Assert.IsFalse(message.HasRetransmittedFlag);
        Assert.AreEqual(257U, message.CommandCode);
        Assert.AreEqual(0U, message.ApplicationId);
        Assert.AreEqual(unchecked((uint)0x18D38B30), message.HopByHopIdentifier);
        Assert.AreEqual(unchecked((uint)0xED0476D1), message.EndToEndIdentifier);

        Assert.AreEqual(5, message.Avps.Count);

        Assert.AreEqual(264U, message.Avps[0].Code); //Origin-Host: lbuorl01.local
        Assert.IsFalse(message.Avps[0].HasVendorFlag);
        Assert.IsTrue(message.Avps[0].HasMandatoryFlag);
        Assert.IsFalse(message.Avps[0].HasProtectedFlag);
        Assert.AreEqual(22, message.Avps[0].Length);
        Assert.AreEqual(24, message.Avps[0].LengthWithPadding);
        Assert.AreEqual("6C62756F726C30312E6C6F63616C", Helpers.GetHexFromBytes(message.Avps[0].GetData()));

        Assert.AreEqual(296U, message.Avps[1].Code); //Origin-Realm: f5.com
        Assert.IsFalse(message.Avps[1].HasVendorFlag);
        Assert.IsTrue(message.Avps[1].HasMandatoryFlag);
        Assert.IsFalse(message.Avps[1].HasProtectedFlag);
        Assert.AreEqual(14, message.Avps[1].Length);
        Assert.AreEqual(16, message.Avps[1].LengthWithPadding);
        Assert.AreEqual("66352E636F6D", Helpers.GetHexFromBytes(message.Avps[1].GetData()));

        Assert.AreEqual(266U, message.Avps[2].Code); //Vendor-Id: 3375 (F5 Networks)
        Assert.IsFalse(message.Avps[2].HasVendorFlag);
        Assert.IsTrue(message.Avps[2].HasMandatoryFlag);
        Assert.IsFalse(message.Avps[2].HasProtectedFlag);
        Assert.AreEqual(12, message.Avps[2].Length);
        Assert.AreEqual(12, message.Avps[2].LengthWithPadding);
        Assert.AreEqual("00000D2F", Helpers.GetHexFromBytes(message.Avps[2].GetData()));

        Assert.AreEqual(269U, message.Avps[3].Code); //Product-Name: F5 BIGIP Diameter Health Monitoring
        Assert.IsFalse(message.Avps[3].HasVendorFlag);
        Assert.IsFalse(message.Avps[3].HasMandatoryFlag);
        Assert.IsFalse(message.Avps[3].HasProtectedFlag);
        Assert.AreEqual(43, message.Avps[3].Length);
        Assert.AreEqual(44, message.Avps[3].LengthWithPadding);
        Assert.AreEqual("4635204249474950204469616D65746572204865616C7468204D6F6E69746F72696E67", Helpers.GetHexFromBytes(message.Avps[3].GetData()));

        Assert.AreEqual(257U, message.Avps[4].Code); //Host-IP-Address: 172.31.128.02
        Assert.IsFalse(message.Avps[4].HasVendorFlag);
        Assert.IsTrue(message.Avps[4].HasMandatoryFlag);
        Assert.IsFalse(message.Avps[4].HasProtectedFlag);
        Assert.AreEqual(14, message.Avps[4].Length);
        Assert.AreEqual(16, message.Avps[4].LengthWithPadding);
        Assert.AreEqual("0001AC1F8002", Helpers.GetHexFromBytes(message.Avps[4].GetData()));

        stream.SetLength(0);
        diameter.WriteMessage(message);
        Assert.AreEqual(hex, Helpers.GetHexFromStream(stream));
    }

    [TestMethod]
    public void DiameterStream_ReadWithErrors() {
        var hex = "01000084800001010000000018D38B30ED0476D100000108400000166C62756F726C30312E6C6F63616C0000000001284000000E66352E636F6D00000000010A4000000C00000D2F0000010D0000002B4635204249474950204469616D65746572204865616C7468204D6F6E69746F72696E6700000001014000000E0001AC1F80020000";
        var stream = Helpers.GetStreamFromHex("0000000000000000" + hex);
        var diameter = new DiameterStream(stream) { ReadBlockSize = 12, WriteBlockSize = 12 };

        try {
            var messageP = diameter.ReadMessage();
            Assert.Fail("Missing first format exception.");
        } catch (FormatException) { }

        try {
            var messageP = diameter.ReadMessage();
            Assert.Fail("Missing second format exception.");
        } catch (FormatException) { }

        var message = diameter.ReadMessage();
        Assert.IsTrue(message.HasRequestFlag);
        Assert.IsFalse(message.HasProxiableFlag);
        Assert.IsFalse(message.HasErrorFlag);
        Assert.IsFalse(message.HasRetransmittedFlag);
        Assert.AreEqual(257U, message.CommandCode);
        Assert.AreEqual(0U, message.ApplicationId);
        Assert.AreEqual(unchecked((uint)0x18D38B30), message.HopByHopIdentifier);
        Assert.AreEqual(unchecked((uint)0xED0476D1), message.EndToEndIdentifier);

        stream.SetLength(0);
        diameter.WriteMessage(message);
        Assert.AreEqual(hex, Helpers.GetHexFromStream(stream));
    }

}
