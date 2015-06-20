using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;

namespace Clamito.Test {
    [TestClass]
    public class CommandTests {

        [TestMethod]
        public void Command_Basic() {
            var x = new Command("Test", "Dummy");
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual("Dummy", x.Parameters);

            Assert.AreEqual(InteractionKind.Command, x.Kind);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Command_NameCannotBeNull() {
            var x = new Command(null, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Command_NameCannotBeEmpty() {
            var x = new Command("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Command_NameCannotBeChangedToNull() {
            var x = new Command("Test");
            x.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Command_NameCannotBeChangedToEmpty() {
            var x = new Command("Test");
            x.Name = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Command_ValidationFailed() {
            var x = new Command("3_");
        }

        [TestMethod]
        public void Command_Clone() {
            var s = new Command("Name", "XXX") { Description = "Note" };

            var x = s.Clone();
            s.Name = "NewName";
            s.Parameters = "YYY";
            s.Description = "NewNote";

            Assert.AreEqual(InteractionKind.Command, x.Kind);
            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("XXX", ((Command)x).Parameters);
            Assert.AreEqual("Note", x.Description);
        }

        [TestMethod]
        public void Command_AsReadOnly() {
            var s = new Command("Name", "XXX") { Description = "Note" };

            var x = s.AsReadOnly();
            s.Name = "NewName";
            s.Parameters = "YYY";
            s.Description = "NewNote";

            Assert.AreEqual(InteractionKind.Command, x.Kind);
            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("XXX", ((Command)x).Parameters);
            Assert.AreEqual("Note", x.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Command_AsReadOnly_Change1() {
            var s = new Command("Name", "XXX") { Description = "Note" };

            var x = (Command)(s.AsReadOnly());
            x.Name = "NewName";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Command_AsReadOnly_Change2() {
            var s = new Command("Name", "XXX") { Description = "Note" };

            var x = (Command)(s.AsReadOnly());
            x.Description = "Note";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Command_AsReadOnly_Change3() {
            var s = new Command("Name", "XXX") { Description = "Note" };

            var x = (Command)(s.AsReadOnly());
            x.Parameters = "XXX";
        }

    }
}
