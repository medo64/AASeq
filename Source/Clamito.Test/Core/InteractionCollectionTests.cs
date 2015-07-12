using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clamito.Test {
    [TestClass]
    public class InteractionCollectionTests {

        [TestMethod]
        public void InteractionCollection_Basic() {
            var c = new InteractionCollection();
            c.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("S", ((Message)(c[0])).Source.Name);
            Assert.AreEqual("D", ((Message)(c[0])).Destination.Name);
        }

        [TestMethod]
        public void InteractionCollection_DuplicateName() {
            var c = new InteractionCollection();
            c.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));
            c.Add(new Command("Test"));
            Assert.AreEqual(2, c.Count);
        }

        [TestMethod]
        public void InteractionCollection_DuplicateCaseInsensitiveName() {
            var c = new InteractionCollection();
            c.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));
            c.Add(new Message("test", new Endpoint("S"), new Endpoint("D")));
            Assert.AreEqual(2, c.Count);
        }


        [TestMethod]
        public void InteractionCollection_LookupByName() {
            var c = new InteractionCollection();
            c.Add(new Message("Test1", new Endpoint("S"), new Endpoint("D")));
            c.Add(new Command("Test2"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c["test1"].Name);
            Assert.AreEqual("Test2", c["test2"].Name);
        }

        [TestMethod]
        public void InteractionCollection_LookupMultipleByName() {
            var c = new InteractionCollection();
            c.Add(new Command("Test"));
            c.Insert(0, new Command("test"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("test", c["Test"].Name);
        }

        [TestMethod]
        public void InteractionCollection_LookupByNameAfterRemove() {
            var c = new InteractionCollection();
            c.Add(new Command("Test"));
            c.Add(new Command("test"));
            c.RemoveAt(0);
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("test", c["Test"].Name);
        }

        [TestMethod]
        public void InteractionCollection_LookupByNameAfterRemoveAll() {
            var c = new InteractionCollection();
            c.Add(new Command("Test"));
            c.Add(new Message("test", new Endpoint("S"), new Endpoint("D")));
            c.Remove("TEST");
            Assert.AreEqual(0, c.Count);
        }

        [TestMethod]
        public void InteractionCollection_RenameItem() {
            var c = new InteractionCollection();
            c.Add(new Message("Test1", new Endpoint("S"), new Endpoint("D")));
            c.Add(new Command("Test2"));
            c[0].Name = "Test";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        public void InteractionCollection_RenameItemNoChange() {
            var c = new InteractionCollection();
            c.Add(new Message("Test1", new Endpoint("S"), new Endpoint("D")));
            c.Add(new Message("Test2", new Endpoint("S"), new Endpoint("D")));
            c[0].Name = "Test1";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        public void InteractionCollection_RenameItemDuplicate() {
            var c = new InteractionCollection();
            c.Add(new Command("Test1"));
            c.Add(new Command("Test2"));
            c[0].Name = "Test2";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test2", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InteractionCollection_RenameItemNull() {
            var c = new InteractionCollection();
            c.Add(new Message("Test1", new Endpoint("S"), new Endpoint("D")));
            c.Add(new Message("Test2", new Endpoint("S"), new Endpoint("D")));
            c[0].Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InteractionCollection_CannotAddSameMessageTwice() {
            var c = new InteractionCollection();
            var item = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            c.Add(item);
            c.Add(item);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InteractionCollection_CannotAddSameCommandTwice() {
            var c = new InteractionCollection();
            var item = new Command("Test");
            c.Add(item);
            c.Add(item);
        }

        [TestMethod]
        public void InteractionCollection_RemoveByName() {
            var c = new InteractionCollection();
            c.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));
            c.Remove("Test");
            Assert.AreEqual(0, c.Count);
        }

        [TestMethod]
        public void InteractionCollection_RemoveMultipleByName() {
            var c = new InteractionCollection();
            c.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));
            c.Add(new Command("test"));
            c.Remove("Test");
            Assert.AreEqual(0, c.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InteractionCollection_CannotAddItemToTwoCollectionsAtSameTime() {
            var c1 = new InteractionCollection();
            var c2 = new InteractionCollection();
            var item = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            c1.Add(item);
            c2.Add(item);
        }

        [TestMethod]
        public void InteractionCollection_CanAddItemToTwoCollectionsAtDifferentTimes() {
            var c1 = new InteractionCollection();
            var c2 = new InteractionCollection();
            var item = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            c1.Add(item);
            c1.Remove(item);
            c2.Add(item);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InteractionCollection_ItemCannotBeUsedTwice() {
            var c1 = new InteractionCollection();
            var c2 = new InteractionCollection();
            var i1 = new Message("A", new Endpoint("Test"), new Endpoint("Test"));
            var i2 = new Command("B");

            c1.Add(i1);
            c1.Add(i2);

            c2.Add(i1);
        }

        [TestMethod]
        public void InteractionCollection_ItemsRelease() {
            var c1 = new InteractionCollection();
            var c2 = new InteractionCollection();
            var i1 = new Message("A", new Endpoint("TestA"), new Endpoint("TestB"));
            var i2 = new Message("B", new Endpoint("TestA"), new Endpoint("TestB"));

            c1.Add(i1);
            c1.Add(i2);
            c1.Remove(i2);

            c2.Add(i2);
        }

        [TestMethod]
        public void InteractionCollection_ItemsReleaseDueToClear() {
            var c1 = new InteractionCollection();
            var c2 = new InteractionCollection();
            var i1 = new Message("A", new Endpoint("TestA"), new Endpoint("TestB"));
            var i2 = new Command("B");

            c1.Add(i1);
            c1.Add(i2);
            c1.Clear();

            c2.Add(i1);
            c2.Add(i2);
        }


        [TestMethod]
        public void InteractionCollection_Clone() {
            var o = new InteractionCollection();
            o.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));

            var c = o.Clone();
            o[0].Name = "TestOld";

            Assert.AreEqual(1, o.Count);
            Assert.AreEqual("TestOld", o[0].Name);
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("S", ((Message)(c[0])).Source.Name);
            Assert.AreEqual("D", ((Message)(c[0])).Destination.Name);
        }

        [TestMethod]
        public void InteractionCollection_AsReadOnly() {
            var o = new InteractionCollection();
            o.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));

            var c = o.AsReadOnly();
            o[0].Name = "TestOld";

            Assert.AreEqual(1, o.Count);
            Assert.AreEqual("TestOld", o[0].Name);
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("S", ((Message)(c[0])).Source.Name);
            Assert.AreEqual("D", ((Message)(c[0])).Destination.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InteractionCollection_AsReadOnly_Change1() {
            var o = new InteractionCollection();
            o.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));

            var c = o.AsReadOnly();
            c.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InteractionCollection_AsReadOnly_Change2() {
            var o = new InteractionCollection();
            o.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));

            var c = o.AsReadOnly();
            c.Add(new Command("Test"));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InteractionCollection_AsReadOnly_Change3() {
            var o = new InteractionCollection();
            o.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));

            var c = o.AsReadOnly();
            c[0].Name = "Test";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InteractionCollection_AsReadOnly_Change4() {
            var o = new InteractionCollection();
            o.Add(new Message("Test", new Endpoint("S"), new Endpoint("D")));

            var c = o.AsReadOnly();
            c[0].Caption = "Test";
        }

    }
}
