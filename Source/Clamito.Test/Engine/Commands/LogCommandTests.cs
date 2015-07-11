using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clamito.Test {
    [TestClass]
    public class LogCommandTests {

        [TestMethod]
        public void LogCommand_Basic() {
            var cmd = new LogCommand();
            Assert.IsTrue(cmd.Execute(new FieldCollection(new Field[] { new Field("Level", "Verbose"), new Field("Text", "Dummy.") })), "No errors must returned");
        }

        [TestMethod]
        public void LogCommand_Empty() {
            var cmd = new LogCommand();
            Assert.IsTrue(cmd.Execute(new FieldCollection()));
        }

    }
}
