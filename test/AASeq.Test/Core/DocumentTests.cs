using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Text;

namespace AASeq.Test {
    [TestClass]
    public class DocumentTests {

        [TestMethod]
        public void Document_New() {
            var d = new Document();
            Assert.AreEqual(0, d.Endpoints.Count);
            Assert.AreEqual(0, d.Interactions.Count);
            Assert.AreEqual(false, d.HasAnyEndpoints);
            Assert.AreEqual(false, d.HasAnyInteractions);
        }

        [TestMethod]
        public void Document_LoadAndSave_Empty() {
            var input = GetDocumentStream("Empty.aaseq");
            var output = new MemoryStream();

            var doc = Document.Load(input);
            doc.Save(output);

            Assert.AreEqual(UTF8Encoding.UTF8.GetString(input.ToArray()), UTF8Encoding.UTF8.GetString(output.ToArray()));
        }

        [TestMethod]
        public void Document_LoadAndSave_Basic() {
            var input = GetDocumentStream("Basic.aaseq");
            var output = new MemoryStream();

            var doc = Document.Load(input);
            doc.Save(output);

            Assert.AreEqual(UTF8Encoding.UTF8.GetString(input.ToArray()), UTF8Encoding.UTF8.GetString(output.ToArray()));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Document_LoadAndSave_InvalidVersion() {
            var input = GetDocumentStream("InvalidVersion.aaseq");
            var doc = Document.Load(input);
        }


        #region Private

        private static MemoryStream GetDocumentStream(string fileName) {
            var resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AASeq.Test.Core.Resources.Documents." + fileName);
            var buffer = new byte[(int)resStream.Length];
            resStream.Read(buffer, 0, buffer.Length);
            return new MemoryStream(buffer) { Position = 0 };
        }

        #endregion

    }
}
