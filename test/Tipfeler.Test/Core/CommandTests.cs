using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;

namespace Tipfeler.Test {
    [TestClass]
    public class CommandTests {

        [TestMethod]
        public void Command_Basic() {
            var x = new Command("Test");
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual(0, x.Data.Count);

            Assert.AreEqual(InteractionKind.Command, x.Kind);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Command_NameCannotBeNull() {
            var x = new Command(null);
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
            var s = new Command("Name") { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = s.Clone();
            s.Name = "NewName";
            s.Caption = "NewNote";
            s.Data.Clear();

            Assert.AreEqual(InteractionKind.Command, x.Kind);
            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("Note", x.Caption);
            Assert.AreEqual(4, ((Command)x).Data.Count);
            Assert.AreEqual("H1", ((Command)x).Data[0].Name);
            Assert.AreEqual("V1", ((Command)x).Data[0].Value);
            Assert.AreEqual("H2", ((Command)x).Data[1].Name);
            Assert.AreEqual(false, ((Command)x).Data[1].HasValue);
            Assert.AreEqual(true, ((Command)x).Data[1].HasSubfields);
            Assert.AreEqual("F1", ((Command)x).Data[2].Name);
            Assert.AreEqual("V1", ((Command)x).Data[2].Value);
            Assert.AreEqual("F2", ((Command)x).Data[3].Name);
            Assert.AreEqual(false, ((Command)x).Data[3].HasValue);
            Assert.AreEqual(true, ((Command)x).Data[3].HasSubfields);
        }

        [TestMethod]
        public void Command_AsReadOnly() {
            var s = new Command("Name") { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = s.AsReadOnly();
            s.Name = "NewName";
            s.Caption = "NewNote";
            s.Data.Clear();

            Assert.AreEqual(InteractionKind.Command, x.Kind);
            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("Note", x.Caption);
            Assert.AreEqual(4, ((Command)x).Data.Count);
            Assert.AreEqual("H1", ((Command)x).Data[0].Name);
            Assert.AreEqual("V1", ((Command)x).Data[0].Value);
            Assert.AreEqual("H2", ((Command)x).Data[1].Name);
            Assert.AreEqual(false, ((Command)x).Data[1].HasValue);
            Assert.AreEqual(true, ((Command)x).Data[1].HasSubfields);
            Assert.AreEqual("F1", ((Command)x).Data[2].Name);
            Assert.AreEqual("V1", ((Command)x).Data[2].Value);
            Assert.AreEqual("F2", ((Command)x).Data[3].Name);
            Assert.AreEqual(false, ((Command)x).Data[3].HasValue);
            Assert.AreEqual(true, ((Command)x).Data[3].HasSubfields);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Command_AsReadOnly_Change1() {
            var s = new Command("Name") { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Command)(s.AsReadOnly());
            x.Name = "NewName";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Command_AsReadOnly_Change2() {
            var s = new Command("Name") { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Command)(s.AsReadOnly());
            x.Caption = "Note";
        }

    }
}
