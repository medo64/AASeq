using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tipfeler.Test {
    [TestClass]
    public class TagCollectionTests {

        [TestMethod]
        public void TagCollection_Basic() {
            var c = new TagCollection();
            c.Add(new Tag("Test"));
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual(true, c[0].State);
            Assert.AreEqual(true, c.GetState("Test"));
        }

        [TestMethod]
        public void TagCollection_BasicNegative() {
            var c = new TagCollection();
            c.Add(new Tag("Test", false));
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual(false, c[0].State);
            Assert.AreEqual(false, c.GetState("Test"));
        }

        [TestMethod]
        public void TagCollection_BasicStateLookup() {
            var c = new TagCollection();
            c.Add(new Tag("Test", false));
            Assert.AreEqual(null, c.GetState("XXX"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TagCollection_DuplicateName() {
            var c = new TagCollection();
            c.Add(new Tag("Test"));
            c.Add(new Tag("test", false));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TagCollection_DuplicateCaseInsensitiveName() {
            var c = new TagCollection();
            c.Add(new Tag("Test"));
            c.Add(new Tag("test"));
        }


        [TestMethod]
        public void TagCollection_LookupByName() {
            var c = new TagCollection();
            c.Add(new Tag("Test1"));
            c.Add(new Tag("Test2"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c["test1"].Name);
            Assert.AreEqual("Test2", c["test2"].Name);
        }

        [TestMethod]
        public void TagCollection_RenameItem() {
            var c = new TagCollection();
            c.Add(new Tag("Test1"));
            c.Add(new Tag("Test2"));
            c[0].Name = "Test";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        public void TagCollection_RenameItemNoChange() {
            var c = new TagCollection();
            c.Add(new Tag("Test1"));
            c.Add(new Tag("Test2"));
            c[0].Name = "Test1";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TagCollection_RenameItemDuplicate() {
            var c = new TagCollection();
            c.Add(new Tag("Test1"));
            c.Add(new Tag("Test2"));
            c[0].Name = "Test2";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TagCollection_RenameItemNull() {
            var c = new TagCollection();
            c.Add(new Tag("Test1"));
            c.Add(new Tag("Test2"));
            c[0].Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TagCollection_CannotAddSameItemTwice() {
            var c = new TagCollection();
            var item = new Tag("Test");
            c.Add(item);
            c.Add(item);
        }

        [TestMethod]
        public void TagCollection_RemoveByName() {
            var c = new TagCollection();
            c.Add(new Tag("Test"));
            c.Remove("Test");
            Assert.AreEqual(0, c.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TagCollection_CannotAddItemToTwoCollectionsAtSameTime() {
            var c1 = new TagCollection();
            var c2 = new TagCollection();
            var item = new Tag("Test");
            c1.Add(item);
            c2.Add(item);
        }

        [TestMethod]
        public void TagCollection_CanAddItemToTwoCollectionsAtDifferentTimes() {
            var c1 = new TagCollection();
            var c2 = new TagCollection();
            var item = new Tag("Test");
            c1.Add(item);
            c1.Remove(item);
            c2.Add(item);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TagCollection_ItemCannotBeUsedTwice() {
            var c1 = new TagCollection();
            var c2 = new TagCollection();
            var i1 = new Tag("A");
            var i2 = new Tag("B");

            c1.Add(i1);
            c1.Add(i2);

            c2.Add(i1);
        }

        [TestMethod]
        public void TagCollection_ItemsRelease() {
            var c1 = new TagCollection();
            var c2 = new TagCollection();
            var i1 = new Tag("A");
            var i2 = new Tag("B");

            c1.Add(i1);
            c1.Add(i2);
            c1.Remove(i2);

            c2.Add(i2);
        }

        [TestMethod]
        public void TagCollection_ItemsReleaseDueToClear() {
            var c1 = new TagCollection();
            var c2 = new TagCollection();
            var i1 = new Tag("A");
            var i2 = new Tag("B");

            c1.Add(i1);
            c1.Add(i2);
            c1.Clear();

            c2.Add(i1);
            c2.Add(i2);
        }


        [TestMethod]
        public void TagCollection_Clone() {
            var o = new TagCollection();
            o.Add(new Tag("Test"));
            var c = o.Clone();
            o[0].Name = "XXX";
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual(true, c[0].State);
            Assert.AreEqual(true, c.GetState("Test"));
        }

        [TestMethod]
        public void TagCollection_AsReadOnly() {
            var o = new TagCollection();
            o.Add(new Tag("Test"));
            var c = o.AsReadOnly();
            o[0].Name = "XXX";
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual(true, c[0].State);
            Assert.AreEqual(true, c.GetState("Test"));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TagCollection_AsReadOnly_Change1() {
            var o = new TagCollection();
            o.Add(new Tag("Test"));
            var c = o.AsReadOnly();
            c.Add(new Tag("Test2"));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TagCollection_AsReadOnly_Change2() {
            var o = new TagCollection();
            o.Add(new Tag("Test"));
            var c = o.AsReadOnly();
            c.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TagCollection_AsReadOnly_Change3() {
            var o = new TagCollection();
            o.Add(new Tag("Test"));
            var c = o.AsReadOnly();
            c[0].Name = "x";
        }

    }
}
