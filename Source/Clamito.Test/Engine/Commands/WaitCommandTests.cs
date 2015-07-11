using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Clamito.Test {
    [TestClass]
    public class WaitCommandTests {

        [TestMethod]
        public void WaitCommand_Basic() {
            var cmd = new WaitCommand();
            var sw = Stopwatch.StartNew();
            Assert.IsTrue(cmd.Execute(new FieldCollection(new Field[] { new Field("Interval", "100ms") })));
            var ms = sw.ElapsedMilliseconds;
            Assert.IsTrue((sw.ElapsedMilliseconds >= 100), "Expected delay of 100 ms; took {0} ms.", ms);
        }

        [TestMethod]
        public void WaitCommand_Empty() {
            var cmd = new WaitCommand();
            var sw = Stopwatch.StartNew();
            Assert.IsTrue(cmd.Execute(new FieldCollection()));
            var ms = sw.ElapsedMilliseconds;
            Assert.IsTrue((sw.ElapsedMilliseconds >= 1000), "Expected delay of 1000 ms; took {0} ms.", ms);
        }

    }
}
