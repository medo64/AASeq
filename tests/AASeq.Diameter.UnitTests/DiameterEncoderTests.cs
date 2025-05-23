namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AASeq;
using AASeq.Diameter;

[TestClass]
public class DiameterEncoderTests {

    [TestMethod]
    public void DiameterEncoder_EncodeCER() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("Origin-Host", "me.aaseq.com"),
                new AASeqNode("Origin-Realm", "aaseq.com"),
                new AASeqNode("Product-Name", "AASeq"),
                new AASeqNode("Vendor-Id",  10415),
                new AASeqNode("Auth-Application-Id", 16777264),
                new AASeqNode("Auth-Application-Id", 16777265),
            ]);

        Assert.AreEqual(257U, message.CommandCode);
        Assert.AreEqual(0U, message.ApplicationId);
        Assert.IsTrue(message.HasRequestFlag);
        Assert.IsFalse(message.HasProxiableFlag);
        Assert.IsFalse(message.HasErrorFlag);

        Assert.AreEqual(6, message.Avps.Count);
        Assert.AreEqual(264U, message.Avps[0].Code);  // Origin-Host
        Assert.AreEqual(0x40, message.Avps[0].Flags);
        Assert.IsNull(message.Avps[0].VendorCode);
        Assert.AreEqual("6D-65-2E-61-61-73-65-71-2E-63-6F-6D", BitConverter.ToString(message.Avps[0].GetData()));
        Assert.AreEqual(296U, message.Avps[1].Code);  // Origin-Realm
        Assert.AreEqual(0x40, message.Avps[1].Flags);
        Assert.IsNull(message.Avps[1].VendorCode);
        Assert.AreEqual("61-61-73-65-71-2E-63-6F-6D", BitConverter.ToString(message.Avps[1].GetData()));
        Assert.AreEqual(269U, message.Avps[2].Code);  // Product-Name
        Assert.AreEqual(0x00, message.Avps[2].Flags);
        Assert.IsNull(message.Avps[2].VendorCode);
        Assert.AreEqual("41-41-53-65-71", BitConverter.ToString(message.Avps[2].GetData()));
        Assert.AreEqual(266U, message.Avps[3].Code);  // Vendor-Id
        Assert.AreEqual(0x40, message.Avps[3].Flags);
        Assert.IsNull(message.Avps[3].VendorCode);
        Assert.AreEqual("00-00-28-AF", BitConverter.ToString(message.Avps[3].GetData()));
        Assert.AreEqual(258U, message.Avps[4].Code);  // Auth-Application-Id
        Assert.AreEqual(0x40, message.Avps[4].Flags);
        Assert.IsNull(message.Avps[4].VendorCode);
        Assert.AreEqual("01-00-00-30", BitConverter.ToString(message.Avps[4].GetData()));
        Assert.AreEqual(258U, message.Avps[5].Code);  // Auth-Application-Id
        Assert.AreEqual(0x40, message.Avps[5].Flags);
        Assert.IsNull(message.Avps[5].VendorCode);
        Assert.AreEqual("01-00-00-31", BitConverter.ToString(message.Avps[5].GetData()));
    }

    [TestMethod]
    public void DiameterEncoder_EncodeDWR() {
        var message = DiameterEncoder.Encode(
            "Device-Watchdog-Request", [
                new AASeqNode("Origin-Host", "me.aaseq.com"),
                new AASeqNode("Origin-Realm", "aaseq.com"),
            ]);

        Assert.AreEqual(280U, message.CommandCode);
        Assert.AreEqual(0U, message.ApplicationId);
        Assert.IsTrue(message.HasRequestFlag);
        Assert.IsFalse(message.HasProxiableFlag);
        Assert.IsFalse(message.HasErrorFlag);

        Assert.AreEqual(2, message.Avps.Count);
        Assert.AreEqual(264U, message.Avps[0].Code);  // Origin-Host
        Assert.AreEqual(0x40, message.Avps[0].Flags);
        Assert.IsNull(message.Avps[0].VendorCode);
        Assert.AreEqual("6D-65-2E-61-61-73-65-71-2E-63-6F-6D", BitConverter.ToString(message.Avps[0].GetData()));
        Assert.AreEqual(296U, message.Avps[1].Code);  // Origin-Realm
        Assert.AreEqual(0x40, message.Avps[1].Flags);
        Assert.IsNull(message.Avps[1].VendorCode);
        Assert.AreEqual("61-61-73-65-71-2E-63-6F-6D", BitConverter.ToString(message.Avps[1].GetData()));
    }

    [TestMethod]
    public void DiameterEncoder_Grouped() {
        var message = DiameterEncoder.Encode(
            "Device-Watchdog-Request", [
                new AASeqNode("Vendor-Specific-Application-Id", [
                    new AASeqNode("Vendor-Id", 10415),
                    new AASeqNode("Auth-Application-Id", 16777264),
                ]),
            ]);

        var nodes = DiameterEncoder.Decode(message, out var messageName);
        Assert.IsTrue(nodes.Remove(".HopByHop"));
        Assert.IsTrue(nodes.Remove(".EndToEnd"));
        Assert.IsTrue(nodes.Remove(".Flags"));
        Assert.AreEqual("Vendor-Specific-Application-Id flags=0x40 mandatory=true vendor=false { Vendor-Id (u32)10415 flags=0x40 mandatory=true vendor=false; Auth-Application-Id (u32)16777264 flags=0x40 mandatory=true vendor=false }", nodes.ToString());
    }


    #region TimeZone

    [TestMethod]
    public void DiameterEncoder_TimeZone_4A00() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "-6:00"),
            ]);
        Assert.AreEqual("4A-00", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("-06:00+0", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_4A01() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "-6:00+1"),
            ]);
        Assert.AreEqual("4A-01", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("-06:00+1", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_8A00() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "-07:00"),
            ]);
        Assert.AreEqual("8A-00", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("-07:00+0", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_8A01() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "-07:00+01"),
            ]);
        Assert.AreEqual("8A-01", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("-07:00+1", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_6900() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "-04:00+0"),
            ]);
        Assert.AreEqual("69-00", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("-04:00+0", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_6901() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "-04:00 +1"),
            ]);
        Assert.AreEqual("69-01", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("-04:00+1", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_0000() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "00:00"),
            ]);
        Assert.AreEqual("00-00", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("00:00+0", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_0001() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "00:00+1"),
            ]);
        Assert.AreEqual("00-01", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("00:00+1", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_4000() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "01:00"),
            ]);
        Assert.AreEqual("40-00", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("01:00+0", decoded[^1].Value.AsString());
    }

    [TestMethod]
    public void DiameterEncoder_TimeZone_4001() {
        var message = DiameterEncoder.Encode(
            "Common:Capabilities-Exchange-Request", [
                new AASeqNode("3GPP-MS-TimeZone", "01:00+1"),
            ]);
        Assert.AreEqual("40-01", BitConverter.ToString(message.Avps[0].GetData()));

        var decoded = DiameterEncoder.Decode(message, out var messageName);
        Assert.AreEqual("01:00+1", decoded[^1].Value.AsString());
    }

    #endregion TimeZone

}
