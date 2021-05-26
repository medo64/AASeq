using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tipfeler.Test {
    [TestClass]
    public class EndpointCollectionTests {

        [TestMethod]
        public void EndpointCollection_Basic() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test", "Protocol"));
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("Protocol", c[0].ProtocolName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndpointCollection_DuplicateName() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test", "Protocol"));
            c.Add(new Endpoint("Test", "Protocol"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndpointCollection_DuplicateCaseInsensitiveName() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test", "Protocol"));
            c.Add(new Endpoint("test", "Protocol"));
        }


        [TestMethod]
        public void EndpointCollection_LookupByName() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test1", "Dummy"));
            c.Add(new Endpoint("Test2", "Dummy"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c["test1"].Name);
            Assert.AreEqual("Test2", c["test2"].Name);
        }

        [TestMethod]
        public void EndpointCollection_RenameItem() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test1", "Dummy"));
            c.Add(new Endpoint("Test2", "Dummy"));
            c[0].Name = "Test";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        public void EndpointCollection_RenameItemNoChange() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test1", "Dummy"));
            c.Add(new Endpoint("Test2", "Dummy"));
            c[0].Name = "Test1";
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test1", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndpointCollection_RenameItemDuplicate() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test1", "Dummy"));
            c.Add(new Endpoint("Test2", "Dummy"));
            c[0].Name = "Test2";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EndpointCollection_RenameItemNull() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test1", "Dummy"));
            c.Add(new Endpoint("Test2", "Dummy"));
            c[0].Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndpointCollection_CannotAddSameItemTwice() {
            var c = new EndpointCollection();
            var item = new Endpoint("Test", "Dummy");
            c.Add(item);
            c.Add(item);
        }

        [TestMethod]
        public void EndpointCollection_RemoveByName() {
            var c = new EndpointCollection();
            c.Add(new Endpoint("Test", "Protocol"));
            c.Remove("Test");
            Assert.AreEqual(0, c.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndpointCollection_CannotAddItemToTwoCollectionsAtSameTime() {
            var c1 = new EndpointCollection();
            var c2 = new EndpointCollection();
            var item = new Endpoint("Test", "Dummy");
            c1.Add(item);
            c2.Add(item);
        }

        [TestMethod]
        public void EndpointCollection_CanAddItemToTwoCollectionsAtDifferentTimes() {
            var c1 = new EndpointCollection();
            var c2 = new EndpointCollection();
            var item = new Endpoint("Test", "Dummy");
            c1.Add(item);
            c1.Remove(item);
            c2.Add(item);
        }


        [TestMethod]
        public void EndpointCollection_GetNewName() {
            var c = new EndpointCollection();
            c.Add(new Endpoint(c.GetUniqueName("Test"), "test"));
            c.Add(new Endpoint(c.GetUniqueName("Test"), "test"));
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual("Test", c[0].Name);
            Assert.AreEqual("Test2", c[1].Name);
        }

        [TestMethod]
        public void EndpointCollection_GetNewNameInOne() {
            var c = new EndpointCollection();
            c.Add(new Endpoint(c.GetUniqueName("Test"), "Dummy"));
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("Test", c[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndpointCollection_ItemCannotBeUsedTwice() {
            var c1 = new EndpointCollection();
            var c2 = new EndpointCollection();
            var i1 = new Endpoint("A", "1");
            var i2 = new Endpoint("B", "2");

            c1.Add(i1);
            c1.Add(i2);

            c2.Add(i1);
        }

        [TestMethod]
        public void EndpointCollection_ItemsRelease() {
            var c1 = new EndpointCollection();
            var c2 = new EndpointCollection();
            var i1 = new Endpoint("A", "1");
            var i2 = new Endpoint("B", "2");

            c1.Add(i1);
            c1.Add(i2);
            c1.Remove(i2);

            c2.Add(i2);
        }

        [TestMethod]
        public void EndpointCollection_ItemsReleaseDueToClear() {
            var c1 = new EndpointCollection();
            var c2 = new EndpointCollection();
            var i1 = new Endpoint("A", "1");
            var i2 = new Endpoint("B", "2");

            c1.Add(i1);
            c1.Add(i2);
            c1.Clear();

            c2.Add(i1);
            c2.Add(i2);
        }

        [TestMethod]
        public void EndpointCollection_Clone() {
            var o = new EndpointCollection() { 
                new Endpoint("Test") 
            };
            var c = o.Clone();
            o[0].Name = "XXX";
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("XXX", o[0].Name);
            Assert.AreEqual("Test", c[0].Name);
        }

        [TestMethod]
        public void EndpointCollection_AsReadOnly() {
            var o = new EndpointCollection() { 
                new Endpoint("Test") 
            };
            var c = o.AsReadOnly();
            o[0].Name = "XXX";
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("XXX", o[0].Name);
            Assert.AreEqual("Test", c[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void EndpointCollection_AsReadOnly_Change1() {
            var o = new EndpointCollection() { 
                new Endpoint("Test") 
            };
            var c = o.AsReadOnly();
            c.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void EndpointCollection_AsReadOnly_Change2() {
            var o = new EndpointCollection() { 
                new Endpoint("Test") 
            };
            var c = o.AsReadOnly();
            c.Add(new Endpoint("Test2"));
        }

    }
}
