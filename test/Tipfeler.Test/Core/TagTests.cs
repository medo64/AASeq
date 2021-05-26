using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tipfeler.Test {
    [TestClass]
    public class TagTests {

        [TestMethod]
        public void Tag_Basic() {
            var x = new Tag("Test");
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual(true, x.State);
        }

        [TestMethod]
        public void Tag_Negative() {
            var x = new Tag("Test", false);
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual(false, x.State);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Tag_NameCannotBeNull() {
            var x = new Tag(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Tag_NameCannotBeEmpty() {
            var x = new Tag("", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Tag_NameCannotBeOnlySpecial() {
            var x = new Tag("@");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Tag_NameCannotBeChangedToNull() {
            var x = new Tag("Test", true);
            x.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Tag_NameCannotBeChangedToEmpty() {
            var x = new Tag("Test");
            x.Name = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Tag_ValidationFailed1() {
            var x = new Tag("-3X");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Tag_ValidationFailed2() {
            var x = new Tag("X_");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Tag_ValidationFailedSpecial() {
            var x = new Tag("@3_");
        }

        [TestMethod]
        public void Tag_Clone() {
            var s = new Tag("Name", false);

            var x = s.Clone();
            s.Name = "NewName";
            s.State = true;

            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual(false, x.State);
        }

        [TestMethod]
        public void Tag_AsReadOnly() {
            var s = new Tag("Name", false);

            var x = s.AsReadOnly();
            s.Name = "NewName";
            s.State = true;

            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual(false, x.State);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Tag_AsReadOnly_Change1() {
            var s = new Tag("Name", false);

            var x = s.AsReadOnly();
            x.Name = "NewName";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Tag_AsReadOnly_Change2() {
            var s = new Tag("Name", false);

            var x = s.AsReadOnly();
            x.State = false;
        }

    }
}
