using AASeq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.Mime.MediaTypeNames;

namespace Tests.Core;

[TestClass]
public class AANullValue_Tests {

    [TestMethod]
    public void AANullValue_Basic() {
        Assert.IsTrue(AANullValue.TryParse(null, out var result));
        Assert.IsInstanceOfType(result, typeof(AANullValue));
        Assert.AreEqual(string.Empty, result);
        Assert.AreEqual(result, new AANullValue());
        Assert.IsTrue(result.Equals(null));
    }

}
