using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Text;

namespace Clamito.Test {
    [TestClass]
    public class FieldCollectionTests {

        [TestMethod]
        public void FieldCollection_Basic() {
            var c = new FieldCollection();
            c.Add(new Field("Test", "Value"));
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("Value", c[0].Value);
        }

        [TestMethod]
        public void FieldCollection_BasicWithSubfields() {
            var c = new FieldCollection();
            c.Add(new Field("Test"));
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual(null, c[0].Value);
            Assert.AreEqual(false, c[0].HasValue);
            Assert.AreEqual(true, c[0].HasSubfields);
        }


        [TestMethod]
        public void FieldCollection_DuplicateName() {
            var c = new FieldCollection();
            c.Add(new Field("Test", "Value"));
            c.Add(new Field("Test", "Value"));
            Assert.AreEqual(2, c.Count);
        }

        [TestMethod]
        public void FieldCollection_DuplicateCaseInsensitiveName() {
            var c = new FieldCollection();
            c.Add(new Field("Test", "Value"));
            c.Add(new Field("test", "Value"));
            Assert.AreEqual(2, c.Count);
        }


        [TestMethod]
        public void FieldCollection_LookupByName() {
            var c = new FieldCollection();
            c.Add(new Field("Test1", "Dummy"));
            c.Add(new Field("Test2", "Dummy"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c["test1"].Name);
            Assert.AreEqual("Test2", c["test2"].Name);
        }

        [TestMethod]
        public void FieldCollection_LookupMultipleByName() {
            var c = new FieldCollection();
            c.Add(new Field("Test"));
            c.Insert(0, new Field("test"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("test", c["Test"].Name);
        }

        [TestMethod]
        public void FieldCollection_LookupByNameAfterRemove() {
            var c = new FieldCollection();
            c.Add(new Field("Test"));
            c.Add(new Field("test"));
            c.RemoveAt(0);
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("test", c["Test"].Name);
        }

        [TestMethod]
        public void FieldCollection_LookupByNameAfterRemoveAll() {
            var c = new FieldCollection();
            c.Add(new Field("Test"));
            c.Add(new Field("test"));
            c.Remove("TEST");
            Assert.AreEqual(0, c.Count);
        }

        [TestMethod]
        public void FieldCollection_RenameItem() {
            var c = new FieldCollection();
            c.Add(new Field("Test1", "Dummy"));
            c.Add(new Field("Test2", "Dummy"));
            c[0].Name = "Test";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        public void FieldCollection_RenameItemNoChange() {
            var c = new FieldCollection();
            c.Add(new Field("Test1", "Dummy"));
            c.Add(new Field("Test2", "Dummy"));
            c[0].Name = "Test1";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        public void FieldCollection_RenameItemDuplicate() {
            var c = new FieldCollection();
            c.Add(new Field("Test1", "Dummy"));
            c.Add(new Field("Test2", "Dummy"));
            c[0].Name = "Test2";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test2", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FieldCollection_RenameItemNull() {
            var c = new FieldCollection();
            c.Add(new Field("Test1", "Dummy"));
            c.Add(new Field("Test2", "Dummy"));
            c[0].Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FieldCollection_CannotAddSameItemTwice() {
            var c = new FieldCollection();
            var item = new Field("Test", "Dummy");
            c.Add(item);
            c.Add(item);
        }

        [TestMethod]
        public void FieldCollection_RemoveByName() {
            var c = new FieldCollection();
            c.Add(new Field("Test", "Value"));
            c.Remove("Test");
            Assert.AreEqual(0, c.Count);
        }

        [TestMethod]
        public void FieldCollection_RemoveMultipleByName() {
            var c = new FieldCollection();
            c.Add(new Field("Test"));
            c.Add(new Field("test"));
            c.Remove("Test");
            Assert.AreEqual(0, c.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FieldCollection_CannotAddItemToTwoCollectionsAtSameTime() {
            var c1 = new FieldCollection();
            var c2 = new FieldCollection();
            var item = new Field("Test", "Dummy");
            c1.Add(item);
            c2.Add(item);
        }

        [TestMethod]
        public void FieldCollection_CanAddItemToTwoCollectionsAtDifferentTimes() {
            var c1 = new FieldCollection();
            var c2 = new FieldCollection();
            var item = new Field("Test", "Dummy");
            c1.Add(item);
            c1.Remove(item);
            c2.Add(item);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FieldCollection_ItemCannotBeUsedTwice() {
            var c1 = new FieldCollection();
            var c2 = new FieldCollection();
            var i1 = new Field("A", "1");
            var i2 = new Field("B", "2");

            c1.Add(i1);
            c1.Add(i2);

            c2.Add(i1);
        }

        [TestMethod]
        public void FieldCollection_ItemsRelease() {
            var c1 = new FieldCollection();
            var c2 = new FieldCollection();
            var i1 = new Field("A", "1");
            var i2 = new Field("B", "2");

            c1.Add(i1);
            c1.Add(i2);
            c1.Remove(i2);

            c2.Add(i2);
        }

        [TestMethod]
        public void FieldCollection_ItemsReleaseDueToClear() {
            var c1 = new FieldCollection();
            var c2 = new FieldCollection();
            var i1 = new Field("A", "1");
            var i2 = new Field("B", "2");

            c1.Add(i1);
            c1.Add(i2);
            c1.Clear();

            c2.Add(i1);
            c2.Add(i2);
        }


        [TestMethod]
        public void FieldCollection_Lookup() {
            var c = new FieldCollection() {
                new Field("A", "1"),
                new Field("B", "2")
            };
            Assert.AreEqual("1", c["A"].Value);
            Assert.AreEqual("2", c["B"].Value);
        }

        [TestMethod]
        public void FieldCollection_LookupImplicitString() {
            var c = new FieldCollection() {
                new Field("A", "1"),
                new Field("B", "2")
            };
            Assert.AreEqual("1", c["A"]);
            Assert.AreEqual("2", c["B"]);
            Assert.AreEqual(null, c["C"]);
        }

        [TestMethod]
        public void FieldCollection_LookupCaseInsensitive() {
            var c = new FieldCollection() {
                new Field("A", "1"),
                new Field("B", "2")
            };
            Assert.AreEqual("1", c["a"]);
            Assert.AreEqual("2", c["b"]);
            Assert.AreEqual(null, c["c"]);
        }


        [TestMethod]
        public void FieldCollection_Clone() {
            var o = new FieldCollection() {
                new Field("A", "1"),
                new Field("B")
            };
            o["B"].Subfields.Add(new Field("C", "3"));
            o["B"].Subfields.Add(new Field("C", "4"));

            var c = o.Clone();
            o[0].Value = "1'";
            o[1].Subfields[0].Value = "3'";

            Assert.AreEqual("1'", o["A"].Value);
            Assert.AreEqual("3'", o["B"].Subfields["C"].Value);

            Assert.AreEqual("1", c["A"].Value);
            Assert.AreEqual(null, c["B"].Value);
            Assert.AreEqual("3", c["B"].Subfields["C"].Value);
        }

        [TestMethod]
        public void FieldCollection_AsReadOnly() {
            var o = new FieldCollection() {
                new Field("A", "1"),
                new Field("B")
            };
            o["B"].Subfields.Add(new Field("C", "3"));
            o["B"].Subfields.Add(new Field("C", "4"));

            var c = o.AsReadOnly();
            o[0].Value = "1'";
            o[1].Subfields[0].Value = "3'";

            Assert.AreEqual("1'", o["A"].Value);
            Assert.AreEqual("3'", o["B"].Subfields["C"].Value);

            Assert.AreEqual("1", c["A"].Value);
            Assert.AreEqual(null, c["B"].Value);
            Assert.AreEqual("3", c["B/C"].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void FieldCollection_AsReadOnly_Change1() {
            var o = new FieldCollection() {
                new Field("A", "1"),
                new Field("B")
            };
            o["B"].Subfields.Add(new Field("C", "3"));
            o.Add(@"B\C", "4");

            var c = o.AsReadOnly();
            c.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void FieldCollection_AsReadOnly_Change2() {
            var o = new FieldCollection() {
                new Field("A", "1"),
                new Field("B")
            };
            o["B"].Subfields.Add(new Field("C", "3"));
            o["B"].Subfields.Add(new Field("C", "4"));

            var c = o.AsReadOnly();
            c[0].Value = "1'";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void FieldCollection_AsReadOnly_Change3() {
            var o = new FieldCollection() {
                new Field("A", "1"),
                new Field("B")
            };
            o["B"].Subfields.Add(new Field("C", "3"));
            o["B"].Subfields.Add(new Field("C", "4"));

            var c = o.AsReadOnly();
            c["B"].Subfields.Clear();
        }


        #region MultilineContent

        [TestMethod]
        public void Content_MultilineContent_Escaping() {
            var data = FieldCollection.Parse(GetFragment("Escaping.input"));

            Assert.AreEqual(3, data.Count);
            Assert.AreEqual("F1", data[0].Name);
            Assert.AreEqual("V1\n", data[0].Value);
            Assert.AreEqual("F2", data[1].Name);
            Assert.AreEqual("V\r\n2", data[1].Value);
            Assert.AreEqual("F3", data[2].Name);
            Assert.AreEqual(" V 3 ", data[2].Value);

            Assert.AreEqual(GetFragment("Escaping.output"), data.ToString());
        }



        [TestMethod]
        public void Content_MultilineContent_Fixable1() {
            var data = FieldCollection.Parse(GetFragment("Fixable1.input"));

            Assert.AreEqual(GetFragment("Fixable1.output"), data.ToString());
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable2() {
            var data = FieldCollection.Parse(GetFragment("Fixable2.input"));

            Assert.AreEqual(GetFragment("Fixable2.output"), data.ToString());
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable3() {
            var data = FieldCollection.Parse(GetFragment("Fixable3.input"));
            Assert.AreEqual(GetFragment("Fixable3.output"), data.ToString());
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable4() {
            var data = FieldCollection.Parse(GetFragment("Fixable4.input"));

            Assert.AreEqual(GetFragment("Fixable4.output"), data.ToString());
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable5() {
            var data = FieldCollection.Parse(GetFragment("Fixable5.input"));

            Assert.AreEqual(GetFragment("Fixable5.output"), data.ToString());
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable6() {
            var data = FieldCollection.Parse(GetFragment("Fixable6.input"));

            Assert.AreEqual(GetFragment("Fixable6.output"), data.ToString());
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable7() {
            var data = FieldCollection.Parse(GetFragment("Fixable7.input"));

            Assert.AreEqual(GetFragment("Fixable7.output"), data.ToString());
        }


        #region Private

        private string GetFragment(string fragmentName) {
            var resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Clamito.Test.Core.Resources.MultilineContent." + fragmentName);
            var buffer = new byte[(int)resStream.Length];
            resStream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        #endregion

        #endregion

    }
}
