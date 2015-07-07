using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

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

            var paths = new List<FieldNode>(c.AllPaths);
            Assert.AreEqual(1, paths.Count);
            Assert.AreEqual("Test", paths[0].Path);
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

            var paths = new List<FieldNode>(c.AllPaths);
            Assert.AreEqual(2, paths.Count);
            Assert.AreEqual("Test", paths[0].Path);
            Assert.AreEqual("test", paths[1].Path);
        }


        [TestMethod]
        public void FieldCollection_LookupByName() {
            var c = new FieldCollection();
            c.Add(new Field("Test1", "Dummy"));
            c.Add(new Field("Test2", "Dummy"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c.FindFirst("test1").Name);
            Assert.AreEqual("Test2", c.FindFirst("test2").Name);
            Assert.AreEqual("Test1", c.FindLast("test1").Name);
            Assert.AreEqual("Test2", c.FindLast("test2").Name);

            var list = new List<Field>(c.FindAll("test1"));
            Assert.AreEqual("Test1", list[0].Name); 
        }

        [TestMethod]
        public void FieldCollection_LookupNoItem() {
            var c = new FieldCollection();
            Assert.AreEqual(null, c.FindFirst("A"));
            Assert.AreEqual(null, c.FindLast("A"));

            var list = new List<Field>(c.FindAll("test1"));
            Assert.AreEqual(0,list.Count);
        }

        [TestMethod]
        public void FieldCollection_LookupNoItemInTree() {
            var c = new FieldCollection();
            c.Add(new Field("Test"));
            c[0].Subfields.Add("X", "Dummy");
            Assert.AreEqual(null, c.FindFirst("Test/A"));
            Assert.AreEqual(null, c.FindLast("Test/A"));

            var list = new List<Field>(c.FindAll("test1"));
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void FieldCollection_LookupMultipleByName() {
            var c = new FieldCollection();
            c.Add(new Field("Test"));
            c.Insert(0, new Field("test"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("test", c.FindFirst("Test").Name);
            Assert.AreEqual("Test", c.FindLast("Test").Name);

            var list = new List<Field>(c.FindAll("test"));
            Assert.AreEqual("test", list[0].Name);
            Assert.AreEqual("Test", list[1].Name);
        }

        [TestMethod]
        public void FieldCollection_LookupByNameAfterRemove() {
            var c = new FieldCollection();
            c.Add(new Field("Test"));
            c.Add(new Field("test"));
            c.RemoveAt(0);
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("test", c.FindFirst("Test").Name);
            Assert.AreEqual("test", c.FindLast("Test").Name);

            var list = new List<Field>(c.FindAll("test"));
            Assert.AreEqual("test", list[0].Name);
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
            Assert.AreEqual("1", c["A"]);
            Assert.AreEqual("2", c["B"]);
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
            o.FindFirst("B").Subfields.Add(new Field("C", "3"));
            o.FindFirst("B").Subfields.Add(new Field("C", "4"));

            var c = o.Clone();
            o[0].Value = "1'";
            o[1].Subfields[0].Value = "3'";

            Assert.AreEqual("1'", o["A"]);
            Assert.AreEqual("3'", o["B/C"]);

            Assert.AreEqual("1", c["A"]);
            Assert.AreEqual(null, c["B"]);
            Assert.AreEqual("3", c["B/C"]);

            var paths = new List<FieldNode>(c.AllPaths);
            Assert.AreEqual(4, paths.Count);
            Assert.AreEqual("A", paths[0].Path);
            Assert.AreEqual("B", paths[1].Path);
            Assert.AreEqual("B/C", paths[2].Path);
            Assert.AreEqual("B/C", paths[3].Path);
        }

        [TestMethod]
        public void FieldCollection_AsReadOnly() {
            var o = new FieldCollection() {
                new Field("A", "1"),
                new Field("B")
            };
            o.FindFirst("B").Subfields.Add(new Field("C", "3"));
            o.FindFirst("B").Subfields.Add(new Field("C", "4"));

            var c = o.AsReadOnly();
            o[0].Value = "1'";
            o[1].Subfields[0].Value = "3'";

            Assert.AreEqual("1'", o["A"]);
            Assert.AreEqual("3'", o["B/C"]);

            Assert.AreEqual("1", c["A"]);
            Assert.AreEqual(null, c["B"]);
            Assert.AreEqual("3", c["B/C"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void FieldCollection_AsReadOnly_Change1() {
            var o = new FieldCollection() {
                new Field("A", "1"),
                new Field("B")
            };
            o.FindFirst("B").Subfields.Add(new Field("C", "3"));
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
            o.FindFirst("B").Subfields.Add(new Field("C", "3"));
            o.FindFirst("B").Subfields.Add(new Field("C", "4"));

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
            o.FindFirst("B").Subfields.Add(new Field("C", "3"));
            o.FindFirst("B").Subfields.Add(new Field("C", "4"));

            var c = o.AsReadOnly();
            c.FindFirst("B").Subfields.Clear();
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


        #region Paths

        [TestMethod]
        public void FieldCollection_LookupByPath() {
            var c = new FieldCollection(new Field[] { new Field(".HA", "1h"), new Field(".HB"), new Field("A", "1"), new Field("B") });
            c.FindFirst(".HB").Subfields.Add(new Field("HC", "3h"));
            c.FindFirst(".HB").Subfields.Add(new Field("HC", "4h"));
            c.FindFirst("B").Subfields.Add(new Field("C", "3"));
            c.FindFirst("B").Subfields.Add(new Field("C", "4"));

            Assert.AreEqual("1h", c[".HA"]);
            Assert.AreEqual(null, c[".HB"]);
            Assert.AreEqual("3h", c[".HB/HC"]);

            Assert.AreEqual("1", c["A"]);
            Assert.AreEqual(null, c["B"]);
            Assert.AreEqual("3", c["B/C"]);

            Assert.AreEqual(".HA: 1h\r\n.HB:\r\n    HC: 3h\r\n    HC: 4h\r\nA: 1\r\nB:\r\n    C: 3\r\n    C: 4\r\n", c.ToString());

            {
                var list = new List<FieldNode>(c.PathsWithValue);
                Assert.AreEqual(6, list.Count);
                Assert.AreEqual(".HA", list[0].Path);
                Assert.AreEqual("1h", list[0].Field.Value);
                Assert.AreEqual(".HB/HC", list[1].Path);
                Assert.AreEqual("3h", list[1].Field.Value);
                Assert.AreEqual(".HB/HC", list[2].Path);
                Assert.AreEqual("4h", list[2].Field.Value);
                Assert.AreEqual("A", list[3].Path);
                Assert.AreEqual("1", list[3].Field.Value);
                Assert.AreEqual("B/C", list[4].Path);
                Assert.AreEqual("3", list[4].Field.Value);
                Assert.AreEqual("B/C", list[5].Path);
                Assert.AreEqual("4", list[5].Field.Value);
            }

            {
                var list = new List<FieldNode>(c.AllPaths);
                Assert.AreEqual(8, list.Count);
                Assert.AreEqual(".HA", list[0].Path);
                Assert.AreEqual("1h", list[0].Field.Value);
                Assert.AreEqual(".HB", list[1].Path);
                Assert.AreEqual(null, list[1].Field.Value);
                Assert.AreEqual(".HB/HC", list[2].Path);
                Assert.AreEqual("3h", list[2].Field.Value);
                Assert.AreEqual(".HB/HC", list[3].Path);
                Assert.AreEqual("4h", list[3].Field.Value);
                Assert.AreEqual("A", list[4].Path);
                Assert.AreEqual("1", list[4].Field.Value);
                Assert.AreEqual("B", list[5].Path);
                Assert.AreEqual(null, list[5].Field.Value);
                Assert.AreEqual("B/C", list[6].Path);
                Assert.AreEqual("3", list[6].Field.Value);
                Assert.AreEqual("B/C", list[7].Path);
                Assert.AreEqual("4", list[7].Field.Value);
            }
        }

        [TestMethod]
        public void FieldCollection_SetByPath() {
            var c = new FieldCollection();
            c[".HA"] = "1h";
            c[".HB\\HC"] = "2h";
            c[".HB/HC"] = "3h";
            c["A"] = "1";
            c["B/C"] = "2";
            c["B/C"] = "3";

            Assert.AreEqual("1h", c[".HA"]);
            Assert.AreEqual(null, c[".HB"]);
            Assert.AreEqual("3h", c[".HB/HC"]);
            Assert.AreEqual(1, c.FindFirst(".HB").Subfields.Count);

            Assert.AreEqual("1", c["A"]);
            Assert.AreEqual(null, c["B"]);
            Assert.AreEqual("3", c["B\\C"]);
            Assert.AreEqual(1, c.FindFirst("B").Subfields.Count);

            Assert.AreEqual(".HA: 1h\r\n.HB:\r\n    HC: 3h\r\nA: 1\r\nB:\r\n    C: 3\r\n", c.ToString());

            var list = new List<FieldNode>(c.PathsWithValue);
            Assert.AreEqual(".HA", list[0].Path);
            Assert.AreEqual("1h", list[0].Field.Value);
            Assert.AreEqual(".HB/HC", list[1].Path);
            Assert.AreEqual("3h", list[1].Field.Value);
            Assert.AreEqual("A", list[2].Path);
            Assert.AreEqual("1", list[2].Field.Value);
            Assert.AreEqual("B/C", list[3].Path);
            Assert.AreEqual("3", list[3].Field.Value);
        }

        #endregion

    }
}
