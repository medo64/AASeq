using System;
using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core;

[TestClass]
public class AAField_Tests {

    [TestMethod]
    public void AAField_Basic() {
        var field = new AAField("Test", 42);
        Assert.AreEqual("Test", field.Name);
        Assert.IsFalse(field.IsHeader);
        Assert.IsInstanceOfType(field.Value, typeof(AAInt32Value));
        Assert.AreEqual(0, field.Tags.Count);
    }

    [TestMethod]
    public void AAField_Header() {
        var field = new AAField(".Test", "");
        Assert.IsTrue(field.IsHeader);
    }

    [TestMethod]
    public void AAField_InvalidNames() {
        Assert.ThrowsException<ArgumentNullException>(() => new AAField(null, ""));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField("", ""));  // cannot be empty
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField(".", ""));  // cannot have only dot
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AAField("A ", ""));  // no spaces allowed
        Assert.ThrowsException<ArgumentNullException>(() => new AAField("A", default(AAFieldCollection)));
    }

    [TestMethod]
    public void Field_Clone1() {
        var s = new AAField("Name", "Value");
        s.Tags.Add(new AATag("M1", true));
        s.Tags.Add(new AATag("M2", false));

        var x = s.Clone();
        s.Value = "NewValue";
        s.Tags.Clear();

        Assert.AreEqual("Name", x.Name);
        Assert.AreEqual("Value", x.Value);
        Assert.AreEqual("M1", x.Tags[0].Name);
        Assert.AreEqual("M2", x.Tags[1].Name);
        Assert.AreEqual(true, x.Tags[0].State);
        Assert.AreEqual(false, x.Tags[1].State);
    }

    [TestMethod]
    public void AAField_Clone2() {
        var s = new AAField("Name", new AAFieldCollection());
        s.Value.AsFieldCollection().Add(new AAField("F1", "V1"));
        s.Value.AsFieldCollection().Add(new AAField("F2", new AAFieldCollection()));
        s.Value.AsFieldCollection()[1].Subfields.Add(new AAField("F21", "V21"));
        s.Value.AsFieldCollection()[1].Subfields.Add(new AAField("F22",""));
        s.Tags.Add(new AATag("M1", true));
        s.Tags.Add(new AATag("M2", false));

        var x = s.Clone();
        s.Subfields.Clear();
        s.Tags.Clear();
        s.Value = "NewValue";

        Assert.AreEqual("Name", x.Name);
        Assert.AreEqual(2, x.Subfields.Count);
        Assert.AreEqual("F1", x.Subfields[0].Name);
        Assert.AreEqual("V1", x.Subfields[0].Value);
        Assert.AreEqual("F2", x.Subfields[1].Name);
        Assert.AreEqual(2, x.Subfields[1].Subfields.Count);
        Assert.AreEqual("F21", x.Subfields[1].Subfields[0].Name);
        Assert.AreEqual("V21", x.Subfields[1].Subfields[0].Value);
        Assert.AreEqual("F22", x.Subfields[1].Subfields[1].Name);
        Assert.AreEqual("M1", x.Tags[0].Name);
        Assert.AreEqual("M2", x.Tags[1].Name);
        Assert.AreEqual(true, x.Tags[0].State);
        Assert.AreEqual(false, x.Tags[1].State);
    }

}
