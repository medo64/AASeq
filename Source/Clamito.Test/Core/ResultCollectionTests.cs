using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clamito.Test {
    [TestClass]
    public class ResultCollectionTests {

        [TestMethod]
        public void ResultCollection_Basic() {
            var result = new ResultCollection(new ErrorResult[] { });
            Assert.AreEqual(true, result.IsSuccess);
            Assert.AreEqual(false, result.HasWarnings);
            Assert.AreEqual(false, result.HasErrors);

            Assert.AreEqual(0, result.Count);

            foreach (var item in result) { }
        }

        [TestMethod]
        public void ResultCollection_Warnings() {
            var result = new ResultCollection(new ErrorResult[] {
                ErrorResult.NewWarning("Test ({0}).", 1),
                ErrorResult.NewWarning("Test ({0}).", 2),
                ErrorResult.NewWarning("Test ({0}).", 3),
            });
            Assert.AreEqual(true, result.IsSuccess);
            Assert.AreEqual(true, result.HasWarnings);
            Assert.AreEqual(false, result.HasErrors);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Test (1).", result[0].Text);
            Assert.AreEqual(true, result[0].IsWarning);
            Assert.AreEqual(false, result[0].IsError);
            Assert.AreEqual("Test (2).", result[1].Text);
            Assert.AreEqual(true, result[1].IsWarning);
            Assert.AreEqual(false, result[1].IsError);
            Assert.AreEqual("Test (3).", result[2].Text);
            Assert.AreEqual(true, result[2].IsWarning);
            Assert.AreEqual(false, result[2].IsError);

            foreach (var item in result) { }
        }

        [TestMethod]
        public void ResultCollection_Errors() {
            var result = new ResultCollection(new ErrorResult[] {
                ErrorResult.NewWarning("Test ({0}).", 1),
                ErrorResult.NewWarning("Test ({0}).", 2),
                ErrorResult.NewWarning("Test ({0}).", 3),
                ErrorResult.NewError("Test ({0}).", 9),
            });
            Assert.AreEqual(false, result.IsSuccess);
            Assert.AreEqual(true, result.HasWarnings);
            Assert.AreEqual(true, result.HasErrors);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("Test (1).", result[0].Text);
            Assert.AreEqual(true, result[0].IsWarning);
            Assert.AreEqual(false, result[0].IsError);
            Assert.AreEqual("Test (2).", result[1].Text);
            Assert.AreEqual(true, result[1].IsWarning);
            Assert.AreEqual(false, result[1].IsError);
            Assert.AreEqual("Test (3).", result[2].Text);
            Assert.AreEqual(true, result[2].IsWarning);
            Assert.AreEqual(false, result[2].IsError);
            Assert.AreEqual("Test (9).", result[3].Text);
            Assert.AreEqual(false, result[3].IsWarning);
            Assert.AreEqual(true, result[3].IsError);

            foreach (var item in result) { }
        }


        [TestMethod]

        public void ResultCollection_Clone() {
            var resultInit = new ResultCollection(new ErrorResult[] {
                ErrorResult.NewWarning("Test ({0}).", 1),
                ErrorResult.NewWarning("Test ({0}).", 2),
                ErrorResult.NewWarning("Test ({0}).", 3),
                ErrorResult.NewError("Test ({0}).", 9),
            });
            var result = resultInit.Clone();

            Assert.AreEqual(false, result.IsSuccess);
            Assert.AreEqual(true, result.HasWarnings);
            Assert.AreEqual(true, result.HasErrors);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("Test (1).", result[0].Text);
            Assert.AreEqual(true, result[0].IsWarning);
            Assert.AreEqual(false, result[0].IsError);
            Assert.AreEqual("Test (2).", result[1].Text);
            Assert.AreEqual(true, result[1].IsWarning);
            Assert.AreEqual(false, result[1].IsError);
            Assert.AreEqual("Test (3).", result[2].Text);
            Assert.AreEqual(true, result[2].IsWarning);
            Assert.AreEqual(false, result[2].IsError);
            Assert.AreEqual("Test (9).", result[3].Text);
            Assert.AreEqual(false, result[3].IsWarning);
            Assert.AreEqual(true, result[3].IsError);

            foreach (var item in result) { }
        }

        [TestMethod]
        public void ResultCollection_CloneWithPrefix() {
            var resultInit = new ResultCollection(new ErrorResult[] {
                ErrorResult.NewWarning("Test ({0}).", 1),
                ErrorResult.NewWarning("Test ({0}).", 2),
                ErrorResult.NewWarning("Test ({0}).", 3),
                ErrorResult.NewError("Test ({0}).", 9),
            });
            var result = resultInit.Clone("P: ");

            Assert.AreEqual(false, result.IsSuccess);
            Assert.AreEqual(true, result.HasWarnings);
            Assert.AreEqual(true, result.HasErrors);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("P: Test (1).", result[0].Text);
            Assert.AreEqual(true, result[0].IsWarning);
            Assert.AreEqual(false, result[0].IsError);
            Assert.AreEqual("P: Test (2).", result[1].Text);
            Assert.AreEqual(true, result[1].IsWarning);
            Assert.AreEqual(false, result[1].IsError);
            Assert.AreEqual("P: Test (3).", result[2].Text);
            Assert.AreEqual(true, result[2].IsWarning);
            Assert.AreEqual(false, result[2].IsError);
            Assert.AreEqual("P: Test (9).", result[3].Text);
            Assert.AreEqual(false, result[3].IsWarning);
            Assert.AreEqual(true, result[3].IsError);

            foreach (var item in result) { }
        }

    }
}
