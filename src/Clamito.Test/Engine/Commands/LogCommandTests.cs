using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Clamito.Test {
    [TestClass]
    public class LogCommandTests {

        [TestMethod]
        public void LogCommand_Basic() {
            var cmd = new LogCommand();
            var errors = new List<Failure>(cmd.Execute(new FieldCollection(new Field[] { new Field("Level", "Verbose"), new Field("Text", "Dummy.") })));
            Assert.AreEqual(0, errors.Count, "No errors must returned");
        }

        [TestMethod]
        public void LogCommand_Empty() {
            var cmd = new LogCommand();
            var errors = new List<Failure>(cmd.Execute(new FieldCollection()));
            Assert.AreEqual(1, errors.Count, "No errors must returned");
            Assert.AreEqual(true, errors[0].IsWarning);
        }

    }
}
