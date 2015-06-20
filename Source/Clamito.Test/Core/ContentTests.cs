using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace Clamito.Test {
    [TestClass]
    public class ContentTests {

        [TestMethod]
        public void Content_Clone() {
            var a = new Content();
            a.Header.Add(new Field("H1", "V1"));
            a.Header.Add(new Field("H2"));
            a.Data.Add(new Field("F1", "V1"));
            a.Data.Add(new Field("F2"));

            var c = a.Clone();
            a.Header.Clear();
            a.Data.Clear();

            Assert.AreEqual(2, c.Header.Count);
            Assert.AreEqual("H1", c.Header[0].Name);
            Assert.AreEqual("V1", c.Header[0].Value);
            Assert.AreEqual("H2", c.Header[1].Name);
            Assert.AreEqual(false, c.Header[1].HasValue);
            Assert.AreEqual(true, c.Header[1].HasSubfields);
            Assert.AreEqual(2, c.Data.Count);
            Assert.AreEqual("F1", c.Data[0].Name);
            Assert.AreEqual("V1", c.Data[0].Value);
            Assert.AreEqual("F2", c.Data[1].Name);
            Assert.AreEqual(false, c.Data[1].HasValue);
            Assert.AreEqual(true, c.Data[1].HasSubfields);
        }

        [TestMethod]
        public void Content_AsReadOnly() {
            var a = new Content();
            a.Header.Add(new Field("H1", "V1"));
            a.Header.Add(new Field("H2"));
            a.Data.Add(new Field("F1", "V1"));
            a.Data.Add(new Field("F2"));

            var x = a.AsReadOnly();
            a.Header.Clear();
            a.Data.Clear();

            Assert.AreEqual(2, x.Header.Count);
            Assert.AreEqual("H1", x.Header[0].Name);
            Assert.AreEqual("V1", x.Header[0].Value);
            Assert.AreEqual("H2", x.Header[1].Name);
            Assert.AreEqual(false, x.Header[1].HasValue);
            Assert.AreEqual(true, x.Header[1].HasSubfields);
            Assert.AreEqual(2, x.Data.Count);
            Assert.AreEqual("F1", x.Data[0].Name);
            Assert.AreEqual("V1", x.Data[0].Value);
            Assert.AreEqual("F2", x.Data[1].Name);
            Assert.AreEqual(false, x.Data[1].HasValue);
            Assert.AreEqual(true, x.Data[1].HasSubfields);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Content_AsReadOnly_Change1() {
            var a = new Content();
            a.Header.Add(new Field("H1", "V1"));
            a.Header.Add(new Field("H2"));
            a.Data.Add(new Field("F1", "V1"));
            a.Data.Add(new Field("F2"));

            var c = a.AsReadOnly();
            c.Header.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Content_AsReadOnly_Change2() {
            var a = new Content();
            a.Header.Add(new Field("H1", "V1"));
            a.Header.Add(new Field("H2"));
            a.Data.Add(new Field("F1", "V1"));
            a.Data.Add(new Field("F2"));

            var c = a.AsReadOnly();
            c.Data.Clear();
        }


        #region MultilineContent

        [TestMethod]
        public void Content_ToMultilineContent_Simple() {
            var content = new Content();
            content.Header.Add(new Field("H1", "V1"));
            content.Data.Add(new Field("F1", "V1"));
            content.Data.Add(new Field("F2", "V2"));

            Assert.AreEqual(GetFragment("Simple.output"), content.ToString());
        }

        [TestMethod]
        public void Content_FromMultilineContent_Simple() {
            var content = new Content();
            content.SetFields(GetFragment("Simple.input"));

            var headers = content.Header;
            var data = content.Data;

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
        public void Content_MultilineContent_Tags() {
            var content = new Content();
            content.SetFields(GetFragment("Tags.input"));

            var headers = content.Header;
            var data = content.Data;

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
        public void Content_MultilineContent_Escaping() {
            var content = Content.Parse(GetFragment("Escaping.input"));

            var headers = content.Header;
            var data = content.Data;

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
        public void Content_MultilineContent_Nested1() {
            var content = Content.Parse(GetFragment("Nested1.input"));

            var headers = content.Header;
            var data = content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Nested1.output"), Content.ToString(messageOut));
        }


        [TestMethod]
        public void Content_MultilineContent_Fixable1() {
            var content = Content.Parse(GetFragment("Fixable1.input"));

            var headers = content.Header;
            var data = content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable1.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable2() {
            var content = Content.Parse(GetFragment("Fixable2.input"));

            var headers = content.Header;
            var data = content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable2.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable3() {
            var content = Content.Parse(GetFragment("Fixable3.input"));

            var headers = content.Header;
            var data = content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable3.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable4() {
            var content = Content.Parse(GetFragment("Fixable4.input"));

            var headers = content.Header;
            var data = content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable4.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable5() {
            var content = Content.Parse(GetFragment("Fixable5.input"));

            var headers = content.Header;
            var data = content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable5.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable6() {
            var content = Content.Parse(GetFragment("Fixable6.input"));

            var headers = content.Header;
            var data = content.Data;

            var messageOut = new Message("Test", new Endpoint("Test1"), new Endpoint("Test2"));
            messageOut.Content.SetFields(headers.Clone(), data.Clone());

            Assert.AreEqual(GetFragment("Fixable6.output"), Content.ToString(messageOut));
        }

        [TestMethod]
        public void Content_MultilineContent_Fixable7() {
            var content = Content.Parse(GetFragment("Fixable7.input"));

            var headers = content.Header;
            var data = content.Data;

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
        public void Content_HeadersCannotBeUsedTwice() {
            var c1 = new Content();
            var c2 = new Content();
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            c1.Header.Add(f1);
            c1.Header.Add(f2);
            c1.Header.Add(f3);

            c2.Header.Add(f2);
        }

        [TestMethod]
        public void Content_HeadersRelease() {
            var c1 = new Content();
            var c2 = new Content();
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            c1.Header.Add(f1);
            c1.Header.Add(f2);
            c1.Header.Add(f3);
            c1.Header.Remove(f2);

            c2.Header.Add(f2);
        }

        [TestMethod]
        public void Content_HeadersReleaseDueToClear() {
            var c1 = new Content();
            var c2 = new Content();
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            c1.Header.Add(f1);
            c1.Header.Add(f2);
            c1.Header.Add(f3);
            c1.Header.Clear();

            c2.Header.Add(f1);
            c2.Header.Add(f2);
            c2.Header.Add(f3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Content_FieldsCannotBeUsedTwice() {
            var c1 = new Content();
            var c2 = new Content();
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            c1.Data.Add(f1);
            c1.Data.Add(f2);
            c1.Data.Add(f3);

            c2.Data.Add(f2);
        }

        [TestMethod]
        public void Content_FieldsRelease() {
            var c1 = new Content();
            var c2 = new Content();
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            c1.Data.Add(f1);
            c1.Data.Add(f2);
            c1.Data.Add(f3);
            c1.Data.Remove(f2);

            c2.Data.Add(f2);
        }

        [TestMethod]
        public void Content_FieldsReleaseDueToClear() {
            var c1 = new Content();
            var c2 = new Content();
            var f1 = new Field("A", "1");
            var f2 = new Field("B", "2");
            var f3 = new Field("C", "3");

            c1.Data.Add(f1);
            c1.Data.Add(f2);
            c1.Data.Add(f3);
            c1.Data.Clear();

            c2.Data.Add(f1);
            c2.Data.Add(f2);
            c2.Data.Add(f3);
        }


        [TestMethod]
        public void Content_LookupSubfields() {
            var c = new Content(
                new FieldCollection() { new Field("HA", "1h"), new Field("HB") },
                new FieldCollection() { new Field("A", "1"), new Field("B") }
            );
            c.Header["HB"].Subfields.Add(new Field("HC", "3h"));
            c.Header["HB"].Subfields.Add(new Field("HC", "4h"));
            c.Data["B"].Subfields.Add(new Field("C", "3"));
            c.Data["B"].Subfields.Add(new Field("C", "4"));

            Assert.AreEqual("1h", c["@HA"]);
            Assert.AreEqual(null, c["@HB"]);
            Assert.AreEqual("3h", c["@HB/HC"]);

            Assert.AreEqual("1", c["A"]);
            Assert.AreEqual(null, c["B"]);
            Assert.AreEqual("3", c["B/C"]);

            Assert.AreEqual("@HA: 1h\r\n@HB:\r\n    HC: 3h\r\n    HC: 4h\r\nA: 1\r\nB:\r\n    C: 3\r\n    C: 4\r\n", c.ToString());

            var list = new List<KeyValuePair<string, string>>(c);
            Assert.AreEqual("@HA", list[0].Key);
            Assert.AreEqual("1h", list[0].Value);
            Assert.AreEqual("@HB/HC", list[1].Key);
            Assert.AreEqual("3h", list[1].Value);
            Assert.AreEqual("@HB/HC", list[2].Key);
            Assert.AreEqual("4h", list[2].Value);
            Assert.AreEqual("A", list[3].Key);
            Assert.AreEqual("1", list[3].Value);
            Assert.AreEqual("B/C", list[4].Key);
            Assert.AreEqual("3", list[4].Value);
            Assert.AreEqual("B/C", list[5].Key);
            Assert.AreEqual("4", list[5].Value);
        }

        [TestMethod]
        public void Content_SetSubfields() {
            var c = new Content();
            c["@HA"] = "1h";
            c["@HB\\HC"] = "2h";
            c["@HB/HC"] = "3h";
            c["A"] = "1";
            c["B/C"] = "2";
            c["B/C"] = "3";

            Assert.AreEqual("1h", c["@HA"]);
            Assert.AreEqual(null, c["@HB"]);
            Assert.AreEqual("3h", c["@HB/HC"]);
            Assert.AreEqual(1, c.Header["HB"].Subfields.Count);

            Assert.AreEqual("1", c["A"]);
            Assert.AreEqual(null, c["B"]);
            Assert.AreEqual("3", c["B\\C"]);
            Assert.AreEqual(1, c.Data["B"].Subfields.Count);

            Assert.AreEqual("@HA: 1h\r\n@HB:\r\n    HC: 3h\r\nA: 1\r\nB:\r\n    C: 3\r\n", c.ToString());

            var list = new List<KeyValuePair<string, string>>(c);
            Assert.AreEqual("@HA", list[0].Key);
            Assert.AreEqual("1h", list[0].Value);
            Assert.AreEqual("@HB/HC", list[1].Key);
            Assert.AreEqual("3h", list[1].Value);
            Assert.AreEqual("A", list[2].Key);
            Assert.AreEqual("1", list[2].Value);
            Assert.AreEqual("B/C", list[3].Key);
            Assert.AreEqual("3", list[3].Value);
        }

    }
}
