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
            Assert.AreEqual(0, x.Data.Count);

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
            x.ReplaceEndpoints(null, x.Destination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Message_DestinationCannotBeChangedToNull() {
            var x = new Message("Test", new Endpoint("S"), new Endpoint("D"));
            x.ReplaceEndpoints(x.Source, null);
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
            x.Data.Add(new Field(".test"));
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual(e1, x.Source);
            Assert.AreEqual(e2, x.Destination);
            Assert.AreEqual(1, x.Data.Count);
        }

        [TestMethod]
        public void Message_WithFields() {
            var e1 = new Endpoint("Test1");
            var e2 = new Endpoint("Test2");
            var x = new Message("Test", e1, e2);
            x.Data.Add(new Field("test"));
            Assert.AreEqual("Test", x.Name);
            Assert.AreEqual(e1, x.Source);
            Assert.AreEqual(e2, x.Destination);
            Assert.AreEqual(1, x.Data.Count);
        }

        [TestMethod]
        public void Message_Clone() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = s.Clone();
            s.Name = "NewName";
            s.Caption = "NewNote";
            s.Data.Clear();

            Assert.AreEqual(true, x.IsMessage);
            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("Note", x.Caption);
            Assert.AreEqual(4, ((Message)x).Data.Count);
            Assert.AreEqual("H1", ((Message)x).Data[0].Name);
            Assert.AreEqual("V1", ((Message)x).Data[0].Value);
            Assert.AreEqual("H2", ((Message)x).Data[1].Name);
            Assert.AreEqual(false, ((Message)x).Data[1].HasValue);
            Assert.AreEqual(true, ((Message)x).Data[1].HasSubfields);
            Assert.AreEqual("F1", ((Message)x).Data[2].Name);
            Assert.AreEqual("V1", ((Message)x).Data[2].Value);
            Assert.AreEqual("F2", ((Message)x).Data[3].Name);
            Assert.AreEqual(false, ((Message)x).Data[3].HasValue);
            Assert.AreEqual(true, ((Message)x).Data[3].HasSubfields);
        }

        [TestMethod]
        public void Message_AsReadOnly() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = s.AsReadOnly();
            s.Name = "NewName";
            s.Caption = "NewNote";
            s.Data.Clear();

            Assert.AreEqual(true, x.IsMessage);
            Assert.AreEqual("Name", x.Name);
            Assert.AreEqual("Note", x.Caption);
            Assert.AreEqual(4, ((Message)x).Data.Count);
            Assert.AreEqual("H1", ((Message)x).Data[0].Name);
            Assert.AreEqual("V1", ((Message)x).Data[0].Value);
            Assert.AreEqual("H2", ((Message)x).Data[1].Name);
            Assert.AreEqual(false, ((Message)x).Data[1].HasValue);
            Assert.AreEqual(true, ((Message)x).Data[1].HasSubfields);
            Assert.AreEqual("F1", ((Message)x).Data[2].Name);
            Assert.AreEqual("V1", ((Message)x).Data[2].Value);
            Assert.AreEqual("F2", ((Message)x).Data[3].Name);
            Assert.AreEqual(false, ((Message)x).Data[3].HasValue);
            Assert.AreEqual(true, ((Message)x).Data[3].HasSubfields);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change1() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Name = "NewName";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change2() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Caption = "Note";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change3() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Source.Caption = "XXX";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change4() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Destination.Caption = "XXX";
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change5() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Data.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change6() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Data.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change7() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Data.Add(new Field("Test"));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Message_AsReadOnly_Change8() {
            var s = new Message("Name", new Endpoint("S"), new Endpoint("D")) { Caption = "Note" };
            s.Data.Add(new Field("H1", "V1"));
            s.Data.Add(new Field("H2"));
            s.Data.Add(new Field("F1", "V1"));
            s.Data.Add(new Field("F2"));

            var x = (Message)(s.AsReadOnly());
            x.Data.Add(new Field("Test"));
        }


        #region MultilineContent

        [TestMethod]
        public void Message_ToMultilineContent_Simple() {
            var e1 = new Endpoint("Test1");
            var e2 = new Endpoint("Test2");
            var message = new Message("Test", e1, e2);
            message.Data.Add(new Field(".H1", "V1"));
            message.Data.Add(new Field("F1", "V1"));
            message.Data.Add(new Field("F2", "V2"));

            Assert.AreEqual(GetFragment("Simple.output"), message.Data.ToString());
        }

        [TestMethod]
        public void Message_FromMultilineContent_Simple() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Simple.input")));

            var fields = message.Data;

            Assert.AreEqual(3, fields.Count);
            Assert.AreEqual(".H1", fields[0].Name);
            Assert.AreEqual("V1", fields[0].Value);
            Assert.AreEqual("F1", fields[1].Name);
            Assert.AreEqual("V1", fields[1].Value);
            Assert.AreEqual("F2", fields[2].Name);
            Assert.AreEqual("V2", fields[2].Value);
        }


        [TestMethod]
        public void Message_MultilineContent_Tags() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Tags.input")));

            var fields = message.Data;

            Assert.AreEqual(3, fields.Count);
            Assert.AreEqual(".H1", fields[0].Name);
            Assert.AreEqual("V1", fields[0].Value);
            Assert.AreEqual("Mandatory", fields[0].Tags[0].Name);
            Assert.AreEqual(false, fields[0].Tags[0].State);
            Assert.AreEqual("F1", fields[1].Name);
            Assert.AreEqual("V1", fields[1].Value);
            Assert.AreEqual(0, fields[1].Tags.Count);
            Assert.AreEqual("F2", fields[2].Name);
            Assert.AreEqual("V2", fields[2].Value);
            Assert.AreEqual(5, fields[2].Tags.Count);
            Assert.AreEqual(".Fixed", fields[2].Tags[0].Name);
            Assert.AreEqual(".RegEx", fields[2].Tags[1].Name);
            Assert.AreEqual("Test", fields[2].Tags[2].Name);
            Assert.AreEqual("Test1", fields[2].Tags[3].Name);
            Assert.AreEqual("Test2", fields[2].Tags[4].Name);

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(fields.Clone());

            Assert.AreEqual(GetFragment("Tags.output"), messageOut.Data.ToString());
        }


        [TestMethod]
        public void Message_MultilineContent_Escaping() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Escaping.input")));

            var fields = message.Data;

            Assert.AreEqual(3, fields.Count);
            Assert.AreEqual("F1", fields[0].Name);
            Assert.AreEqual("V1\n", fields[0].Value);
            Assert.AreEqual("F2", fields[1].Name);
            Assert.AreEqual("V\r\n2", fields[1].Value);
            Assert.AreEqual("F3", fields[2].Name);
            Assert.AreEqual(" V 3 ", fields[2].Value);

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(fields.Clone());

            Assert.AreEqual(GetFragment("Escaping.output"), messageOut.Data.ToString());
        }


        [TestMethod]
        public void Message_MultilineContent_Nested1() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Nested1.input")));

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(message.Data.Clone());

            Assert.AreEqual(GetFragment("Nested1.output"), messageOut.Data.ToString());
        }


        [TestMethod]
        public void Message_MultilineContent_Fixable1() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Fixable1.input")));

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(message.Data.Clone());

            Assert.AreEqual(GetFragment("Fixable1.output"), messageOut.Data.ToString());
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable2() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Fixable2.input")));

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(message.Data.Clone());

            Assert.AreEqual(GetFragment("Fixable2.output"), messageOut.Data.ToString());
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable3() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Fixable3.input")));

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(message.Data.Clone());

            Assert.AreEqual(GetFragment("Fixable3.output"), messageOut.Data.ToString());
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable4() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Fixable4.input")));

            var headers = message.Data;
            var data = message.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(message.Data.Clone());

            Assert.AreEqual(GetFragment("Fixable4.output"), messageOut.Data.ToString());
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable5() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Fixable5.input")));

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(message.Data.Clone());

            Assert.AreEqual(GetFragment("Fixable5.output"), messageOut.Data.ToString());
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable6() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Fixable6.input")));

            var headers = message.Data;
            var data = message.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(message.Data.Clone());

            Assert.AreEqual(GetFragment("Fixable6.output"), messageOut.Data.ToString());
        }

        [TestMethod]
        public void Message_MultilineContent_Fixable7() {
            var message = new Message("Name", new Endpoint("A"), new Endpoint("B"));
            message.ReplaceData(FieldCollection.Parse(GetFragment("Fixable7.input")));

            var headers = message.Data;
            var data = message.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.ReplaceData(message.Data.Clone());

            Assert.AreEqual(GetFragment("Fixable7.output"), messageOut.Data.ToString());
        }


        #region Private

        private static string GetFragment(string fragmentName) {
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

            m1.Data.Add(f1);
            m1.Data.Add(f2);
            m1.Data.Add(f3);

            m2.Data.Add(f2);
        }

        [TestMethod]
        public void Message_HeadersRelease() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Data.Add(f1);
            m1.Data.Add(f2);
            m1.Data.Add(f3);
            m1.Data.Remove(f2);

            m2.Data.Add(f2);
        }

        [TestMethod]
        public void Message_HeadersReleaseDueToClear() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Data.Add(f1);
            m1.Data.Add(f2);
            m1.Data.Add(f3);
            m1.Data.Clear();

            m2.Data.Add(f1);
            m2.Data.Add(f2);
            m2.Data.Add(f3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Message_FieldsCannotBeUsedTwice() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Data.Add(f1);
            m1.Data.Add(f2);
            m1.Data.Add(f3);

            m2.Data.Add(f2);
        }

        [TestMethod]
        public void Message_FieldsRelease() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Data.Add(f1);
            m1.Data.Add(f2);
            m1.Data.Add(f3);
            m1.Data.Remove(f2);

            m2.Data.Add(f2);
        }

        [TestMethod]
        public void Message_FieldsReleaseDueToClear() {
            var m1 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var m2 = new Message("Test", new Endpoint("TestA"), new Endpoint("TestB"));
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            m1.Data.Add(f1);
            m1.Data.Add(f2);
            m1.Data.Add(f3);
            m1.Data.Clear();

            m2.Data.Add(f1);
            m2.Data.Add(f2);
            m2.Data.Add(f3);
        }


        //#region SubFields

        //[TestMethod]
        //public void Content_LookupSubfields() {
        //    var c = new Content(
        //        new FieldCollection() { new Field("HA", "1h"), new Field("HB") },
        //        new FieldCollection() { new Field("A", "1"), new Field("B") }
        //    );
        //    c.Header["HB"].Subfields.Add(new Field("HC", "3h"));
        //    c.Header["HB"].Subfields.Add(new Field("HC", "4h"));
        //    c.Data["B"].Subfields.Add(new Field("C", "3"));
        //    c.Data["B"].Subfields.Add(new Field("C", "4"));

        //    Assert.AreEqual("1h", c["@HA"]);
        //    Assert.AreEqual(null, c["@HB"]);
        //    Assert.AreEqual("3h", c["@HB/HC"]);

        //    Assert.AreEqual("1", c["A"]);
        //    Assert.AreEqual(null, c["B"]);
        //    Assert.AreEqual("3", c["B/C"]);

        //    Assert.AreEqual("@HA: 1h\r\n@HB:\r\n    HC: 3h\r\n    HC: 4h\r\nA: 1\r\nB:\r\n    C: 3\r\n    C: 4\r\n", c.ToString());

        //    var list = new List<KeyValuePair<string, string>>(c);
        //    Assert.AreEqual("@HA", list[0].Key);
        //    Assert.AreEqual("1h", list[0].Value);
        //    Assert.AreEqual("@HB/HC", list[1].Key);
        //    Assert.AreEqual("3h", list[1].Value);
        //    Assert.AreEqual("@HB/HC", list[2].Key);
        //    Assert.AreEqual("4h", list[2].Value);
        //    Assert.AreEqual("A", list[3].Key);
        //    Assert.AreEqual("1", list[3].Value);
        //    Assert.AreEqual("B/C", list[4].Key);
        //    Assert.AreEqual("3", list[4].Value);
        //    Assert.AreEqual("B/C", list[5].Key);
        //    Assert.AreEqual("4", list[5].Value);
        //}

        //[TestMethod]
        //public void Content_SetSubfields() {
        //    var c = new FieldCollection();
        //    c["@HA"] = "1h";
        //    c["@HB\\HC"] = "2h";
        //    c["@HB/HC"] = "3h";
        //    c["A"] = "1";
        //    c["B/C"] = "2";
        //    c["B/C"] = "3";

        //    Assert.AreEqual("1h", c["@HA"]);
        //    Assert.AreEqual(null, c["@HB"]);
        //    Assert.AreEqual("3h", c["@HB/HC"]);
        //    Assert.AreEqual(1, c["HB"].Subfields.Count);

        //    Assert.AreEqual("1", c["A"]);
        //    Assert.AreEqual(null, c["B"]);
        //    Assert.AreEqual("3", c["B\\C"]);
        //    Assert.AreEqual(1, c["B"].Subfields.Count);

        //    Assert.AreEqual("@HA: 1h\r\n@HB:\r\n    HC: 3h\r\nA: 1\r\nB:\r\n    C: 3\r\n", c.ToString());

        //    var list = new List<KeyValuePair<string, string>>(c.GetTreeEnumerator());
        //    Assert.AreEqual("@HA", list[0].Key);
        //    Assert.AreEqual("1h", list[0].Value);
        //    Assert.AreEqual("@HB/HC", list[1].Key);
        //    Assert.AreEqual("3h", list[1].Value);
        //    Assert.AreEqual("A", list[2].Key);
        //    Assert.AreEqual("1", list[2].Value);
        //    Assert.AreEqual("B/C", list[3].Key);
        //    Assert.AreEqual("3", list[3].Value);
        //}

        //#endregion

    }
}
