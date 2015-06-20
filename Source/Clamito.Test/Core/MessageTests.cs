using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;

namespace Clamito.Test {
    [TestClass]
    public class MessageTests {

        [TestMethod]
        public void Message_Basic() {
            var x = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual("S", x.Source.Name);
            Assert.AreEqual("D", x.Destination.Name);
            Assert.AreEqual(0, x.Content.Header.Count);
            Assert.AreEqual(0, x.Content.Data.Count);

            Assert.AreEqual(InteractionKind.Message, x.Kind);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Message_NameCannotBeNull() {
            var x = new Message(null, new Endpoint("S"), new Endpoint("D"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Message_NameCannotBeEmpty() {
            var x = new Message("", new Endpoint("S"), new Endpoint("D"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Message_NameCannotBeChangedToNull() {
            var x = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            x.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Message_NameCannotBeChangedToEmpty() {
            var x = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            x.Name = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Message_SourceCannotBeNull() {
            var x = new Message("Test", null, new Endpoint("D"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Message_DestinationCannotBeNull() {
            var x = new Message("Test", new Endpoint("S"), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Message_SourceCannotBeChangedToNull() {
            var x = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            x.SetEndpoints(null, x.Destination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Message_DestinationCannotBeChangedToNull() {
            var x = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            x.SetEndpoints(x.Source, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Message_SourceAndDestinationCannotBeTheSame() {
            var ep = new Endpoint("E");
            var x = new Message("Test", ep, ep);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Message_SourceAndDestinationCannotBeTheSameName() {
            var ep1 = new Endpoint("E");
            var ep2 = new Endpoint("E");
            var x = new Message("Test", ep1, ep2);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Message_ValidationFailed() {
            var x = new Message("3_", new Endpoint("S"), new Endpoint("D"));
        }

        [TestMethod]
        public void Message_WithHeaderFields() {
            var e1 = new Endpoint("Test1");
            var e2 = new Endpoint("Test2");
            var x = new Message("Test", e1, e2);
            x.Content.Header.Add(new Field("test"));
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual(e1, x.Source);
            Assert.AreEqual(e2, x.Destination);
            Assert.AreEqual(1, x.Content.Header.Count);
            Assert.AreEqual(0, x.Content.Data.Count);
        }

        [TestMethod]
        public void Message_WithFields() {
            var e1 = new Endpoint("Test1");
            var e2 = new Endpoint("Test2");
            var x = new Message("Test", e1, e2);
            x.Content.Data.Add(new Field("test"));
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual(e1, x.Source);
            Assert.AreEqual(e2, x.Destination);
            Assert.AreEqual(0, x.Content.Header.Count);
            Assert.AreEqual(1, x.Content.Data.Count);
        }

        [TestMethod]
        public void Message_Clone() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = s.Clone();
            s.Name = "NewName";
            s.Description = "NewNote";
            s.Content.Header.Clear();
            s.Content.Data.Clear();

            Assert.AreEqual(true, x.IsMessage);
            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("Note", x.Description);
            Assert.AreEqual(2, ((Message)x).Content.Header.Count);
            Assert.AreEqual("H1", ((Message)x).Content.Header[0].Name);
            Assert.AreEqual("V1", ((Message)x).Content.Header[0].Value);
            Assert.AreEqual("H2", ((Message)x).Content.Header[1].Name);
            Assert.AreEqual(false, ((Message)x).Content.Header[1].HasValue);
            Assert.AreEqual(true, ((Message)x).Content.Header[1].HasSubfields);
            Assert.AreEqual(2, ((Message)x).Content.Data.Count);
            Assert.AreEqual("F1", ((Message)x).Content.Data[0].Name);
            Assert.AreEqual("V1", ((Message)x).Content.Data[0].Value);
            Assert.AreEqual("F2", ((Message)x).Content.Data[1].Name);
            Assert.AreEqual(false, ((Message)x).Content.Data[1].HasValue);
            Assert.AreEqual(true, ((Message)x).Content.Data[1].HasSubfields);
        }

        [TestMethod]
        public void Message_AsReadOnly() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = s.AsReadOnly();
            s.Name = "NewName";
            s.Description = "NewNote";
            s.Content.Header.Clear();
            s.Content.Data.Clear();

            Assert.AreEqual(true, x.IsMessage);
            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("Note", x.Description);
            Assert.AreEqual(2, ((Message)x).Content.Header.Count);
            Assert.AreEqual("H1", ((Message)x).Content.Header[0].Name);
            Assert.AreEqual("V1", ((Message)x).Content.Header[0].Value);
            Assert.AreEqual("H2", ((Message)x).Content.Header[1].Name);
            Assert.AreEqual(false, ((Message)x).Content.Header[1].HasValue);
            Assert.AreEqual(true, ((Message)x).Content.Header[1].HasSubfields);
            Assert.AreEqual(2, ((Message)x).Content.Data.Count);
            Assert.AreEqual("F1", ((Message)x).Content.Data[0].Name);
            Assert.AreEqual("V1", ((Message)x).Content.Data[0].Value);
            Assert.AreEqual("F2", ((Message)x).Content.Data[1].Name);
            Assert.AreEqual(false, ((Message)x).Content.Data[1].HasValue);
            Assert.AreEqual(true, ((Message)x).Content.Data[1].HasSubfields);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change1() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Name = "NewName";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change2() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Description = "Note";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change3() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Source.Description = "XXX";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change4() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Destination.Description = "XXX";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change5() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Content.Header.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change6() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Content.Data.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change7() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Content.Header.Add(new Field("Test"));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change8() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Description = "Note" };
            s.Content.Header.Add(new Field("H1", "V1"));
            s.Content.Header.Add(new Field("H2"));
            s.Content.Data.Add(new Field("F1", "V1"));
            s.Content.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Content.Data.Add(new Field("Test"));
        }


        #region MultilineContent

        [TestMethod]
        public void Message_ToMultilineContent_Simple() {
            var e1 = new Endpoint("Test1");
            var e2 = new Endpoint("Test2");
            var message = new Message("Test", e1, e2);
            message.Content.Header.Add(new Field("H1", "V1"));
            message.Content.Data.Add(new Field("F1", "V1"));
            message.Content.Data.Add(new Field("F2", "V2"));

            Assert.AreEqual(GetFragment("Simple.output"), Content.ToString(message));
        }

        [TestMethod]
        public void Message_FromMultilineContent_Simple() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Simple.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            Assert.AreEqual(1, headers.Count);
            Assert.AreEqual("H1", headers[0].Name);
            Assert.AreEqual("V1", headers[0].Value);

            Assert.AreEqual(2, data.Count);
            Assert.AreEqual("F1", data[0].Name);
            Assert.AreEqual("V1", data[0].Value);
            Assert.AreEqual("F2", data[1].Name);
            Assert.AreEqual("V2", data[1].Value);
        }


        [TestMethod]
        public void Message_MultilineContent_Tags() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Tags.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            Assert.AreEqual(1, headers.Count);
            Assert.AreEqual("H1", headers[0].Name);
            Assert.AreEqual("V1", headers[0].Value);
            Assert.AreEqual(1, headers[0].Tags.Count);
            Assert.AreEqual("Mandatory", headers[0].Tags[0].Name);
            Assert.AreEqual(false, headers[0].Tags[0].State);

            Assert.AreEqual(2, data.Count);
            Assert.AreEqual("F1", data[0].Name);
            Assert.AreEqual("V1", data[0].Value);
            Assert.AreEqual(0, data[0].Tags.Count);
            Assert.AreEqual("F2", data[1].Name);
            Assert.AreEqual("V2", data[1].Value);
            Assert.AreEqual(3, data[1].Tags.Count);
            Assert.AreEqual("Test", data[1].Tags[0].Name);
            Assert.AreEqual("Test1", data[1].Tags[1].Name);
            Assert.AreEqual("Test2", data[1].Tags[2].Name);

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Tags.output"), Content.ToString(messageOut));
        }


        [TestMethod]
        public void Message_MultilineContent_Escaping() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Escaping.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            Assert.AreEqual(0, headers.Count);

            Assert.AreEqual(3, data.Count);
            Assert.AreEqual("F1", data[0].Name);
            Assert.AreEqual("V1\n", data[0].Value);
            Assert.AreEqual("F2", data[1].Name);
            Assert.AreEqual("V\r\n2", data[1].Value);
            Assert.AreEqual("F3", data[2].Name);
            Assert.AreEqual(" V 3 ", data[2].Value);

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Escaping.output"), Content.ToString(messageOut));
        }


        [TestMethod]
        public void Message_MultilineContent_Nested1() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Nested1.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Nested1.output"), Content.ToString(messageOut));
        }


        [TestMethod]
        public void Message_MultilineContent_Fixable1() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Fixable1.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable1.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable2() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Fixable2.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable2.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable3() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Fixable3.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable3.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable4() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Fixable4.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable4.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable5() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Fixable5.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable5.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable6() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Fixable6.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable6.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable7() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.Content.SetFields(GetFragment("Fixable7.input"));

            var headers = message.Content.Header;
            var data = message.Content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable7.output"), Content.ToString(messageOut));
        }


        #region Private

        private string GetFragment(string fragmentName) {
            var resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Clamito.Test.Core.Resources.MultilineContent." + fragmentName);
            var buffer = new byte[(int)resStream.Length];
            resStream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        #endregion

        #endregion


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Message_HeadersCannotBeUsedTwice() {
            var m1 = new Message("Test", new Endpoint("Test"), new Endpoint("Test"));
            var m2 = new Message("Test", new Endpoint("Test"), new Endpoint("Test"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Content.Header.Add(f1);
            m1.Content.Header.Add(f2);
            m1.Content.Header.Add(f3);

            m2.Content.Header.Add(f2);
        }

        [TestMethod]
        public void Message_HeadersRelease() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Content.Header.Add(f1);
            m1.Content.Header.Add(f2);
            m1.Content.Header.Add(f3);
            m1.Content.Header.Remove(f2);

            m2.Content.Header.Add(f2);
        }

        [TestMethod]
        public void Message_HeadersReleaseDueToClear() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Content.Header.Add(f1);
            m1.Content.Header.Add(f2);
            m1.Content.Header.Add(f3);
            m1.Content.Header.Clear();

            m2.Content.Header.Add(f1);
            m2.Content.Header.Add(f2);
            m2.Content.Header.Add(f3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Message_FieldsCannotBeUsedTwice() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Content.Data.Add(f1);
            m1.Content.Data.Add(f2);
            m1.Content.Data.Add(f3);

            m2.Content.Data.Add(f2);
        }

        [TestMethod]
        public void Message_FieldsRelease() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Content.Data.Add(f1);
            m1.Content.Data.Add(f2);
            m1.Content.Data.Add(f3);
            m1.Content.Data.Remove(f2);

            m2.Content.Data.Add(f2);
        }

        [TestMethod]
        public void Message_FieldsReleaseDueToClear() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Content.Data.Add(f1);
            m1.Content.Data.Add(f2);
            m1.Content.Data.Add(f3);
            m1.Content.Data.Clear();

            m2.Content.Data.Add(f1);
            m2.Content.Data.Add(f2);
            m2.Content.Data.Add(f3);
        }

    }
}
