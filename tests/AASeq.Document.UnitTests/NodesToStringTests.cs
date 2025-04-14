namespace Tests;
using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;

[TestClass]
public sealed class NodesToStringTests {

    [TestMethod]
    public void NodesToString_Bool() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", true));
        doc.Add(new AASeqNode("b", false));

        Assert.AreEqual("a true; b false", doc.ToString());
    }

    [TestMethod]
    public void NodesToString_Int() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", (SByte)8));
        doc.Add(new AASeqNode("b", (Int16)16));
        doc.Add(new AASeqNode("c", (Int32)32));
        doc.Add(new AASeqNode("d", (Int64)64));
        doc.Add(new AASeqNode("e", (Int128)128));

        Assert.AreEqual("a (i8)8; b (i16)16; c 32; d (i64)64; e (i128)128", doc.ToString());
    }

    [TestMethod]
    public void NodesToString_UInt() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", (Byte)8));
        doc.Add(new AASeqNode("b", (UInt16)16));
        doc.Add(new AASeqNode("c", (UInt32)32));
        doc.Add(new AASeqNode("d", (UInt64)64));
        doc.Add(new AASeqNode("e", (UInt128)128));

        Assert.AreEqual("a (u8)8; b (u16)16; c (u32)32; d (u64)64; e (u128)128", doc.ToString());
    }

    [TestMethod]
    public void NodesToString_Float() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", (Half)16));
        doc.Add(new AASeqNode("b", (Single)32));
        doc.Add(new AASeqNode("c", (Double)64));
        doc.Add(new AASeqNode("d", (Decimal)128));

        Assert.AreEqual("a (f16)16.0; b (f32)32.0; c 64.0; d (d128)128.0", doc.ToString());
    }

    [TestMethod]
    public void NodesToString_Float_Special() {
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", Half.NaN));
        doc.Add(new AASeqNode("b", Single.PositiveInfinity));
        doc.Add(new AASeqNode("c", Double.NegativeInfinity));

        Assert.AreEqual("a (f16)NaN; b (f32)+Inf; c -Inf", doc.ToString());
    }

    [TestMethod]
    public void NodesToString_Date() {
        var date = new DateTimeOffset(1972, 12, 14, 22, 54, 37, TimeSpan.Zero);
        var doc = new AASeqNodes();
        doc.Add(new AASeqNode("a", date));
        doc.Add(new AASeqNode("b", DateOnly.FromDateTime(date.DateTime.Date)));
        doc.Add(new AASeqNode("c", TimeOnly.FromTimeSpan(date.DateTime.TimeOfDay)));
        doc.Add(new AASeqNode("d", new TimeSpan(0, 01, 06, 00, 013).Negate()));

        Assert.AreEqual("a (datetime)\"1972-12-14T22:54:37+00:00\"; b (dateonly)\"1972-12-14\"; c (timeonly)\"22:54:37\"; d (duration)\"-1h 6m 0.013s\"", doc.ToString());
    }

}
