using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;

namespace Clamito.Test {
    [TestClass]
    public class WaitCommandTests {

        [TestMethod]
        public void WaitCommand_Basic() {
            var cmd = new WaitCommand();
            var sw = Stopwatch.StartNew();
            var errors = new List<Failure>(cmd.Execute(new FieldCollection(new Field[] { new Field("Interval", "100ms") })));
            Assert.AreEqual(0, errors.Count);
            var ms = sw.ElapsedMilliseconds;
            Assert.IsTrue((sw.ElapsedMilliseconds >= 100), "Expected delay of 100 ms; took {0} ms.", ms);
        }

        [TestMethod]
        public void WaitCommand_Empty() {
            var cmd = new WaitCommand();
            var sw = Stopwatch.StartNew();
            var errors = new List<Failure>(cmd.Execute(new FieldCollection()));
            Assert.AreEqual(0, errors.Count);
            var ms = sw.ElapsedMilliseconds;
            Assert.IsTrue((sw.ElapsedMilliseconds >= 1000), "Expected delay of 1000 ms; took {0} ms.", ms);
        }

    }
}
