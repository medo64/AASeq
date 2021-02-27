using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clamito.Test {
    [TestClass]
    public class EndpointTests {

        [TestMethod]
        public void Endpoint_Basic() {
            var x = new Endpoint("Test", "Protocol") { Caption = "D" };
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual("Protocol", x.ProtocolName);
            Assert.AreEqual("D", x.Caption);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Endpoint_NameCannotBeNull() {
            var x = new Endpoint(null, "Protocol");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Endpoint_NameCannotBeEmpty() {
            var x = new Endpoint("", "Protocol");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Endpoint_NameCannotBeChangedToNull() {
            var x = new Endpoint("Test", "Something");
            x.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Endpoint_NameCannotBeChangedToEmpty() {
            var x = new Endpoint("Test", "Something");
            x.Name = "";
        }

        [TestMethod]
        public void Endpoint_ProtocolNameCanBeNull() {
            var x = new Endpoint("Test", null);
            Assert.AreEqual(null, x.ProtocolName);
        }

        [TestMethod]
        public void Endpoint_ProtocolNameCanBeChangedToNull() {
            var x = new Endpoint("Test", "Something");
            x.ProtocolName = null;
            Assert.AreEqual(null, x.ProtocolName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Endpoint_ValidationFailed1() {
            var x = new Endpoint("3X", "Dummy");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Endpoint_ValidationFailed2() {
            var x = new Endpoint("X-", "Dummy");
        }

        [TestMethod]
        public void Endpoint_Fields() {
            var x = new Endpoint("Test", "Protocol");
            x.Data.Add(new Field("P1", "V1"));
            x.Data.Add(new Field("P2", "V2"));
            x.Data.Add(new Field("P3", "V3"));
            Assert.AreEqual("P1", x.Data[0].Name);
            Assert.AreEqual("V1", x.Data[0].Value);
            Assert.AreEqual("P2", x.Data[1].Name);
            Assert.AreEqual("V2", x.Data[1].Value);
            Assert.AreEqual("P3", x.Data[2].Name);
            Assert.AreEqual("V3", x.Data[2].Value);
        }

        [TestMethod]
        public void Endpoint_Clone() {
            var s = new Endpoint("Test", "Protocol") { Caption = "Note" };
            s.Data.Add(new Field("P1", "V1"));
            s.Data.Add(new Field("P2", "V2"));
            s.Data.Add(new Field("P3", "V3"));

            var x = s.Clone();
            s.Data.Clear();
            s.Name = "NewTest";
            s.ProtocolName = "NewProtocol";
            s.Caption = "NewNote";

            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual("Protocol", x.ProtocolName);
            Assert.AreEqual("Note", x.Caption);
            Assert.AreEqual("P1", x.Data[0].Name);
            Assert.AreEqual("V1", x.Data[0].Value);
            Assert.AreEqual("P2", x.Data[1].Name);
            Assert.AreEqual("V2", x.Data[1].Value);
            Assert.AreEqual("P3", x.Data[2].Name);
            Assert.AreEqual("V3", x.Data[2].Value);
        }

        [TestMethod]
        public void Endpoint_AsReadOnly() {
            var s = new Endpoint("Test", "Protocol") { Caption = "Note" };
            s.Data.Add(new Field("P1", "V1"));
            s.Data.Add(new Field("P2", "V2"));
            s.Data.Add(new Field("P3", "V3"));

            var x = s.AsReadOnly();
            s.Data.Clear();
            s.Name = "NewTest";
            s.ProtocolName = "NewProtocol";
            s.Caption = "NewNote";

            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual("Protocol", x.ProtocolName);
            Assert.AreEqual("Note", x.Caption);
            Assert.AreEqual("P1", x.Data[0].Name);
            Assert.AreEqual("V1", x.Data[0].Value);
            Assert.AreEqual("P2", x.Data[1].Name);
            Assert.AreEqual("V2", x.Data[1].Value);
            Assert.AreEqual("P3", x.Data[2].Name);
            Assert.AreEqual("V3", x.Data[2].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Endpoint_AsReadOnly_Change1() {
            var s = new Endpoint("Test", "Protocol") { Caption = "Note" };
            s.Data.Add(new Field("P1", "V1"));
            s.Data.Add(new Field("P2", "V2"));
            s.Data.Add(new Field("P3", "V3"));

            var x = s.AsReadOnly();
            x.Data.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Endpoint_AsReadOnly_Change2() {
            var s = new Endpoint("Test", "Protocol") { Caption = "Note" };
            s.Data.Add(new Field("P1", "V1"));
            s.Data.Add(new Field("P2", "V2"));
            s.Data.Add(new Field("P3", "V3"));

            var x = s.AsReadOnly();
            x.Caption = "Note";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Endpoint_AsReadOnly_Change3() {
            var s = new Endpoint("Test", "Protocol") { Caption = "Note" };
            s.Data.Add(new Field("P1", "V1"));
            s.Data.Add(new Field("P2", "V2"));
            s.Data.Add(new Field("P3", "V3"));

            var x = s.AsReadOnly();
            x.Name = "Test";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Endpoint_AsReadOnly_Change4() {
            var s = new Endpoint("Test", "Protocol") { Caption = "Note" };
            s.Data.Add(new Field("P1", "V1"));
            s.Data.Add(new Field("P2", "V2"));
            s.Data.Add(new Field("P3", "V3"));

            var x = s.AsReadOnly();
            x.ProtocolName = "Test";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Endpoint_AsReadOnly_Change5() {
            var s = new Endpoint("Test", "Protocol") { Caption = "Note" };
            s.Data.Add(new Field("P1", "V1"));
            s.Data.Add(new Field("P2", "V2"));
            s.Data.Add(new Field("P3", "V3"));

            var x = s.AsReadOnly();
            x.Caption = "Test";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Endpoint_VariablesCannotBeUsedTwice() {
            var e1 = new Endpoint("Test");
            var e2 = new Endpoint("Test");
            var v1 = new Field("P1", "V1");
            var v2 = new Field("P2", "V2");
            var v3 = new Field("P3", "V3");

            e1.Data.Add(v1);
            e1.Data.Add(v2);
            e1.Data.Add(v3);

            e2.Data.Add(v2);
        }

        [TestMethod]
        public void Endpoint_VariablesRelease() {
            var e1 = new Endpoint("Test");
            var e2 = new Endpoint("Test");
            var v1 = new Field("P1", "V1");
            var v2 = new Field("P2", "V2");
            var v3 = new Field("P3", "V3");

            e1.Data.Add(v1);
            e1.Data.Add(v2);
            e1.Data.Add(v3);
            e1.Data.Remove(v2);

            e2.Data.Add(v2);
        }

        [TestMethod]
        public void Endpoint_VariablesReleaseDueToClear() {
            var e1 = new Endpoint("Test");
            var e2 = new Endpoint("Test");
            var v1 = new Field("P1", "V1");
            var v2 = new Field("P2", "V2");
            var v3 = new Field("P3", "V3");

            e1.Data.Add(v1);
            e1.Data.Add(v2);
            e1.Data.Add(v3);
            e1.Data.Clear();

            e2.Data.Add(v1);
            e2.Data.Add(v2);
            e2.Data.Add(v3);
        }

    }
}
