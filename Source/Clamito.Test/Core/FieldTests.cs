using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clamito.Test {
    [TestClass]
    public class FieldTests {

        [TestMethod]
        public void Field_Basic() {
            var x = new Field("Test", "Value");
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual("Value", x.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Field_NameCannotBeNull() {
            var x = new Field(null, "Value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Field_NameCannotBeEmpty() {
            var x = new Field("", "Value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Field_NameCannotBeChangedToNull() {
            var x = new Field("Test", "Something");
            x.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Field_NameCannotBeChangedToEmpty() {
            var x = new Field("Test", "Something");
            x.Name = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Field_ValueCannotBeNull() {
            var x = new Field("Test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Field_ValueCannotBeChangedToNull() {
            var x = new Field("Test", "Something");
            x.Value = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Field_ValidationFailed() {
            var x = new Field("3_", "Dummy");
        }

        [TestMethod]
        public void Field_ValueOverwritesSubfields() {
            var x = new Field("Test");
            x.Subfields.Add(new Field("Test"));
            x.Value = "";
            Assert.AreEqual("", x.Value);
            Assert.AreEqual(0, x.Subfields.Count);
            Assert.AreEqual(true, x.HasValue);
            Assert.AreEqual(false, x.HasSubfields);
        }

        [TestMethod]
        public void Field_SubfieldsOverwriteValue1() {
            var x = new Field("Test", "Value");
            x.Subfields.Clear();
            Assert.AreEqual(null, x.Value);
            Assert.AreEqual(0, x.Subfields.Count);
            Assert.AreEqual(false, x.HasValue);
            Assert.AreEqual(true, x.HasSubfields);
        }

        [TestMethod]
        public void Field_SubfieldsOverwriteValue2() {
            var x = new Field("Test", "Value");
            x.Subfields.Add(new Field("Test"));
            Assert.AreEqual(null, x.Value);
            Assert.AreEqual(1, x.Subfields.Count);
            Assert.AreEqual(false, x.HasValue);
            Assert.AreEqual(true, x.HasSubfields);
        }

        [TestMethod]
        public void Field_Clone1() {
            var s = new Field("Name", "Value");
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.Clone();
            s.Name = "NewName";
            s.Value = "NewValue";
            s.Subfields.Clear();
            s.Tags.Clear();

            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("Value", x.Value);
            Assert.AreEqual(true, x.HasValue);
            Assert.AreEqual(false, x.HasSubfields);
            Assert.AreEqual("M1", x.Tags[0].Name);
            Assert.AreEqual("M2", x.Tags[1].Name);
            Assert.AreEqual(true, x.Tags[0].State);
            Assert.AreEqual(false, x.Tags[1].State);
        }

        [TestMethod]
        public void Field_Clone2() {
            var s = new Field("Name");
            s.Subfields.Add(new Field("F1", "V1"));
            s.Subfields.Add(new Field("F2"));
            s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
            s.Subfields[1].Subfields.Add(new Field("F22"));
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.Clone();
            s.Name = "NewName";
            s.Subfields.Clear();
            s.Tags.Clear();
            s.Value = "NewValue";

            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual(false, x.HasValue);
            Assert.AreEqual(true, x.HasSubfields);
            Assert.AreEqual(2, x.Subfields.Count);
            Assert.AreEqual("F1", x.Subfields[0].Name);
            Assert.AreEqual("V1", x.Subfields[0].Value);
            Assert.AreEqual("F2", x.Subfields[1].Name);
            Assert.AreEqual(false, x.Subfields[1].HasValue);
            Assert.AreEqual(true, x.Subfields[1].HasSubfields);
            Assert.AreEqual(2, x.Subfields[1].Subfields.Count);
            Assert.AreEqual("F21", x.Subfields[1].Subfields[0].Name);
            Assert.AreEqual("V21", x.Subfields[1].Subfields[0].Value);
            Assert.AreEqual("F22", x.Subfields[1].Subfields[1].Name);
            Assert.AreEqual(false, x.Subfields[1].Subfields[1].HasValue);
            Assert.AreEqual(true, x.Subfields[1].Subfields[1].HasSubfields);
            Assert.AreEqual("M1", x.Tags[0].Name);
            Assert.AreEqual("M2", x.Tags[1].Name);
            Assert.AreEqual(true, x.Tags[0].State);
            Assert.AreEqual(false, x.Tags[1].State);
        }


        [TestMethod]
        public void Field_AsReadOnly1() {
            var s = new Field("Name", "Value");
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.AsReadOnly();
            s.Name = "NewName";
            s.Value = "NewValue";
            s.Subfields.Clear();
            s.Tags.Clear();

            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("Value", x.Value);
            Assert.AreEqual(true, x.HasValue);
            Assert.AreEqual(false, x.HasSubfields);
            Assert.AreEqual("M1", x.Tags[0].Name);
            Assert.AreEqual("M2", x.Tags[1].Name);
            Assert.AreEqual(true, x.Tags[0].State);
            Assert.AreEqual(false, x.Tags[1].State);
        }

        [TestMethod]
        public void Field_AsReadOnly2() {
            var s = new Field("Name");
            s.Subfields.Add(new Field("F1", "V1"));
            s.Subfields.Add(new Field("F2"));
            s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
            s.Subfields[1].Subfields.Add(new Field("F22"));
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.AsReadOnly();
            s.Name = "NewName";
            s.Subfields.Clear();
            s.Tags.Clear();
            s.Value = "NewValue";

            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual(false, x.HasValue);
            Assert.AreEqual(true, x.HasSubfields);
            Assert.AreEqual(2, x.Subfields.Count);
            Assert.AreEqual("F1", x.Subfields[0].Name);
            Assert.AreEqual("V1", x.Subfields[0].Value);
            Assert.AreEqual("F2", x.Subfields[1].Name);
            Assert.AreEqual(false, x.Subfields[1].HasValue);
            Assert.AreEqual(true, x.Subfields[1].HasSubfields);
            Assert.AreEqual(2, x.Subfields[1].Subfields.Count);
            Assert.AreEqual("F21", x.Subfields[1].Subfields[0].Name);
            Assert.AreEqual("V21", x.Subfields[1].Subfields[0].Value);
            Assert.AreEqual("F22", x.Subfields[1].Subfields[1].Name);
            Assert.AreEqual(false, x.Subfields[1].Subfields[1].HasValue);
            Assert.AreEqual(true, x.Subfields[1].Subfields[1].HasSubfields);
            Assert.AreEqual("M1", x.Tags[0].Name);
            Assert.AreEqual("M2", x.Tags[1].Name);
            Assert.AreEqual(true, x.Tags[0].State);
            Assert.AreEqual(false, x.Tags[1].State);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Field_AsReadOnly_Change1() {
            var s = new Field("Name");
            s.Subfields.Add(new Field("F1", "V1"));
            s.Subfields.Add(new Field("F2"));
            s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
            s.Subfields[1].Subfields.Add(new Field("F22"));
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.AsReadOnly();
            x.Name = "Test";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Field_AsReadOnly_Change2() {
            var s = new Field("Name");
            s.Subfields.Add(new Field("F1", "V1"));
            s.Subfields.Add(new Field("F2"));
            s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
            s.Subfields[1].Subfields.Add(new Field("F22"));
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.AsReadOnly();
            x.Value = "Test";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Field_AsReadOnly_Change3() {
            var s = new Field("Name");
            s.Subfields.Add(new Field("F1", "V1"));
            s.Subfields.Add(new Field("F2"));
            s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
            s.Subfields[1].Subfields.Add(new Field("F22"));
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.AsReadOnly();
            x.Subfields.Add(new Field("Test"));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Field_AsReadOnly_Change4() {
            var s = new Field("Name");
            s.Subfields.Add(new Field("F1", "V1"));
            s.Subfields.Add(new Field("F2"));
            s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
            s.Subfields[1].Subfields.Add(new Field("F22"));
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.AsReadOnly();
            x.Tags[0].State = true;
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Field_AsReadOnly_Change5() {
            var s = new Field("Name");
            s.Subfields.Add(new Field("F1", "V1"));
            s.Subfields.Add(new Field("F2"));
            s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
            s.Subfields[1].Subfields.Add(new Field("F22"));
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.AsReadOnly();
            x.Subfields[0].Value = "x";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Field_AsReadOnly_Change6() {
            var s = new Field("Name");
            s.Subfields.Add(new Field("F1", "V1"));
            s.Subfields.Add(new Field("F2"));
            s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
            s.Subfields[1].Subfields.Add(new Field("F22"));
            s.Tags.Add(new Tag("M1", true));
            s.Tags.Add(new Tag("M2", false));

            var x = s.AsReadOnly();
            x.Subfields[1].Subfields.Add(new Field("Test"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Field_SubfieldsCannotBeUsedTwice() {
            var f1 = new Field("Test");
            var f2 = new Field("Test");
            var sf1 = new Field("A", "1");
            var sf2 = new Field("B", "2");
            var sf3 = new Field("C", "3");

            f1.Subfields.Add(sf1);
            f1.Subfields.Add(sf2);
            f1.Subfields.Add(sf3);

            f2.Subfields.Add(sf2);
        }

        [TestMethod]
        public void Field_SubfieldsRelease() {
            var f1 = new Field("Test");
            var f2 = new Field("Test");
            var sf1 = new Field("A", "1");
            var sf2 = new Field("B", "2");
            var sf3 = new Field("C", "3");

            f1.Subfields.Add(sf1);
            f1.Subfields.Add(sf2);
            f1.Subfields.Add(sf3);
            f1.Subfields.Remove(sf2);

            f2.Subfields.Add(sf2);
        }

        [TestMethod]
        public void Field_SubieldReleaseDueToClear() {
            var f1 = new Field("Test");
            var f2 = new Field("Test");
            var sf1 = new Field("A", "1");
            var sf2 = new Field("B", "2");
            var sf3 = new Field("C", "3");

            f1.Subfields.Add(sf1);
            f1.Subfields.Add(sf2);
            f1.Subfields.Add(sf3);
            f1.Subfields.Clear();

            f2.Subfields.Add(sf1);
            f2.Subfields.Add(sf2);
            f2.Subfields.Add(sf3);
        }

    }
}
