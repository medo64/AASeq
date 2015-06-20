using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Clamito.Test {
    [TestClass]
    public class LogTests {

        [TestMethod]
        public void Log_Verbose() {
            var proxy = new PrivateObject(Log.GetLog());
            var lines = new List<string>((IEnumerable<string>)proxy.Invoke("GetTraceLines", "Verbose", 1, new Guid("6EFC72BF-2D45-4712-9D31-CA433C3320EE"), "Test={0}", 1));

            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual("Clamito.Test         Verbose  #1     {6efc72bf-2d45-4712-9d31-ca433c3320ee} Test=1", lines[0].Substring(24));
        }

        [TestMethod]
        public void Log_Verbose_NoOptions() {
            var proxy = new PrivateObject(Log.GetLog());
            var lines = new List<string>((IEnumerable<string>)proxy.Invoke("GetTraceLines", "Verbose", 0, Guid.Empty, "Test={0}", 1));

            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual("Clamito.Test         Verbose                                                Test=1", lines[0].Substring(24));
        }

        [TestMethod]
        public void Log_Verbose_Multiline() {
            var proxy = new PrivateObject(Log.GetLog());
            var lines = new List<string>((IEnumerable<string>)proxy.Invoke("GetTraceLines", "Verbose", 1, new Guid("6EFC72BF-2D45-4712-9D31-CA433C3320EE"), "TestA={0}\nTestB={1}\r\nTestC={2}", 1, 2, 3));

            Assert.AreEqual(3, lines.Count);
            Assert.AreEqual("Clamito.Test         Verbose  #1     {6efc72bf-2d45-4712-9d31-ca433c3320ee} TestA=1", lines[0].Substring(24));
            Assert.AreEqual("                                                                            TestB=2", lines[1].Substring(24));
            Assert.AreEqual("                                                                            TestC=3", lines[2].Substring(24));
        }

    }
}
