using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AASeq;

namespace Tests.Core;

[TestClass]
public class AAFieldCollection_Tests {

    [TestMethod]
    public void AAFieldCollection_Basic() {
        var c = new AAFieldCollection {
            new AAField("Test", "Value")
        };
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.AreEqual("Value", c[0].Value);
    }

    [TestMethod]
    public void AAFieldCollection_BasicWithSubfields() {
        var c = new AAFieldCollection {
            new AAField("Test", new AAFieldCollection(
                new AAField("TestInner1"),
                new AAField("TestInner2")))
        };
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.IsInstanceOfType(c[0].Value, typeof(AAFieldCollection));

        Assert.AreEqual(2, c[0].Value.AsFieldCollection().Count);
        Assert.AreEqual("TestInner1", c[0].Value.AsFieldCollection()[0].Name);
        Assert.AreEqual("TestInner2", c[0].Value.AsFieldCollection()[1].Name);
        Assert.AreEqual(AANullValue.Instance, c[0].Value.AsFieldCollection()[0].Value);
        Assert.AreEqual(AANullValue.Instance, c[0].Value.AsFieldCollection()[1].Value);

        var paths = new List<AAFieldNode>(c.AllPaths);
        Assert.AreEqual(3, paths.Count);
        Assert.AreEqual("Test", paths[0].Path);
        Assert.AreEqual("Test/TestInner1", paths[1].Path);
        Assert.AreEqual("Test/TestInner2", paths[2].Path);
    }


    [TestMethod]
    public void AAFieldCollection_DuplicateName() {
        var c = new AAFieldCollection {
            new AAField("Test", "Value"),
            new AAField("Test", "Value")
        };
        Assert.AreEqual(2, c.Count);
    }

    [TestMethod]
    public void AAFieldCollection_DuplicateCaseInsensitiveName() {
        var c = new AAFieldCollection {
            new AAField("Test", "Value"),
            new AAField("test", "Value")
        };
        Assert.AreEqual(2, c.Count);

        var paths = new List<AAFieldNode>(c.AllPaths);
        Assert.AreEqual(2, paths.Count);
        Assert.AreEqual("Test", paths[0].Path);
        Assert.AreEqual("test", paths[1].Path);
    }


    [TestMethod]
    public void AAFieldCollection_LookupByName() {
        var c = new AAFieldCollection {
            new AAField("Test1", "Dummy"),
            new AAField("Test2", "Dummy")
        };
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test1", c.FindFirst("test1").Name);
        Assert.AreEqual("Test2", c.FindFirst("test2").Name);
        Assert.AreEqual("Test1", c.FindLast("test1").Name);
        Assert.AreEqual("Test2", c.FindLast("test2").Name);

        var list = new List<AAField>(c.FindAll("test1"));
        Assert.AreEqual("Test1", list[0].Name);
    }

    [TestMethod]
    public void AAFieldCollection_LookupNoItem() {
        var c = new AAFieldCollection();
        Assert.AreEqual(null, c.FindFirst("A"));
        Assert.AreEqual(null, c.FindLast("A"));

        var list = new List<AAField>(c.FindAll("test1"));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void AAFieldCollection_LookupNoItemInTree() {
        var c = new AAFieldCollection {
            new AAField("Test")
        };
        c[0].Value = new AAFieldCollection(new AAField("X", "Dummy"));
        Assert.AreEqual(null, c.FindFirst("Test/A"));
        Assert.AreEqual(null, c.FindLast("Test/A"));

        var list = new List<AAField>(c.FindAll("test1"));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void AAFieldCollection_LookupMultipleByName() {
        var c = new AAFieldCollection();
        c.Add(new AAField("Test"));
        c.Insert(0, new AAField("test"));
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("test", c.FindFirst("Test").Name);
        Assert.AreEqual("Test", c.FindLast("Test").Name);

        var list = new List<AAField>(c.FindAll("test"));
        Assert.AreEqual("test", list[0].Name);
        Assert.AreEqual("Test", list[1].Name);
    }

    [TestMethod]
    public void AAFieldCollection_LookupByNameAfterRemove() {
        var c = new AAFieldCollection {
            new AAField("Test"),
            new AAField("test")
        };
        c.RemoveAt(0);
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("test", c.FindFirst("Test").Name);
        Assert.AreEqual("test", c.FindLast("Test").Name);

        var list = new List<AAField>(c.FindAll("test"));
        Assert.AreEqual("test", list[0].Name);
    }

    [TestMethod]
    public void AAFieldCollection_RemoveByNameCaseInsensitive() {
        var c = new AAFieldCollection {
            new AAField("Test"),
            new AAField("test")
        };
        c.Remove("TEST");
        Assert.AreEqual(0, c.Count);
    }

    [TestMethod]
    public void AAFieldCollection_RenameItem() {
        var c = new AAFieldCollection {
            new AAField("Test1", "Dummy"),
            new AAField("Test2", "Dummy")
        };
        c[0].Name = "Test";
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test", c[0].Name);
        Assert.AreEqual("Test2", c[1].Name);
    }

    [TestMethod]
    public void AAFieldCollection_RenameItemNoChange() {
        var c = new AAFieldCollection {
            new AAField("Test1", "Dummy"),
            new AAField("Test2", "Dummy")
        };
        c[0].Name = "Test1";
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test1", c[0].Name);
        Assert.AreEqual("Test2", c[1].Name);
    }

    [TestMethod]
    public void AAFieldCollection_RenameItemDuplicate() {
        var c = new AAFieldCollection {
            new AAField("Test1", "Dummy"),
            new AAField("Test2", "Dummy")
        };
        c[0].Name = "Test2";
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("Test2", c[0].Name);
        Assert.AreEqual("Test2", c[1].Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AAFieldCollection_RenameItemNull() {
        var c = new AAFieldCollection {
            new AAField("Test1", "Dummy"),
            new AAField("Test2", "Dummy")
        };
        c[0].Name = null;
    }

    [TestMethod]
    public void AAFieldCollection_RemoveByName() {
        var c = new AAFieldCollection {
            new AAField("Test", "Value")
        };
        c.Remove("Test");
        Assert.AreEqual(0, c.Count);
    }

    [TestMethod]
    public void AAFieldCollection_RemoveMultipleByName() {
        var c = new AAFieldCollection {
            new AAField("Test"),
            new AAField("test")
        };
        c.Remove("Test");
        Assert.AreEqual(0, c.Count);
    }


    [TestMethod]
    public void AAFieldCollection_Lookup() {
        var c = new AAFieldCollection() {
                new AAField("A", "1"),
                new AAField("B", "2")
            };
        Assert.AreEqual("1", c["A"]);
        Assert.AreEqual("2", c["B"]);
    }

    [TestMethod]
    public void AAFieldCollection_LookupImplicitString() {
        var c = new AAFieldCollection() {
                new AAField("A", "1"),
                new AAField("B", "2")
            };
        Assert.AreEqual("1", c["A"]);
        Assert.AreEqual("2", c["B"]);
        Assert.AreEqual(null, c["C"]);
    }

    [TestMethod]
    public void AAFieldCollection_LookupCaseInsensitive() {
        var c = new AAFieldCollection() {
                new AAField("A", "1"),
                new AAField("B", "2")
            };
        Assert.AreEqual("1", c["a"]);
        Assert.AreEqual("2", c["b"]);
        Assert.AreEqual(null, c["c"]);
    }


    #region Paths

    [TestMethod]
    public void AAFieldCollection_LookupByPath() {
        var c = new AAFieldCollection(new AAField[] { new AAField(".HA", "1h"), new AAField(".HB", new AAFieldCollection()), new AAField("A", "1"), new AAField("B", new AAFieldCollection()) });
        c.FindFirst(".HB").Value.AsFieldCollection().Add(new AAField("HC", "3h"));
        c.FindFirst(".HB").Value.AsFieldCollection().Add(new AAField("HC", "4h"));
        c.FindFirst("B").Value.AsFieldCollection().Add(new AAField("C", "3"));
        c.FindFirst("B").Value.AsFieldCollection().Add(new AAField("C", "4"));

        Assert.AreEqual("1h", c[".HA"]);
        Assert.IsInstanceOfType(c[".HB"], typeof(AAFieldCollection));
        Assert.AreEqual("3h", c[".HB/HC"]);

        Assert.AreEqual("1", c["A"]);
        Assert.IsInstanceOfType(c["B"], typeof(AAFieldCollection));
        Assert.AreEqual("3", c["B/C"]);

        {
            var list = new List<AAFieldNode>(c.PathsWithValue);
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(".HA", list[0].Path);
            Assert.AreEqual("1h", list[0].Field.Value);
            Assert.AreEqual(".HB/HC", list[1].Path);
            Assert.AreEqual("3h", list[1].Field.Value);
            Assert.AreEqual(".HB/HC", list[2].Path);
            Assert.AreEqual("4h", list[2].Field.Value);
            Assert.AreEqual("A", list[3].Path);
            Assert.AreEqual("1", list[3].Field.Value);
            Assert.AreEqual("B/C", list[4].Path);
            Assert.AreEqual("3", list[4].Field.Value);
            Assert.AreEqual("B/C", list[5].Path);
            Assert.AreEqual("4", list[5].Field.Value);
        }

        {
            var list = new List<AAFieldNode>(c.AllPaths);
            Assert.AreEqual(8, list.Count);
            Assert.AreEqual(".HA", list[0].Path);
            Assert.AreEqual("1h", list[0].Field.Value);
            Assert.AreEqual(".HB", list[1].Path);
            Assert.IsInstanceOfType(list[1].Field.Value, typeof(AAFieldCollection));
            Assert.AreEqual(".HB/HC", list[2].Path);
            Assert.AreEqual("3h", list[2].Field.Value);
            Assert.AreEqual(".HB/HC", list[3].Path);
            Assert.AreEqual("4h", list[3].Field.Value);
            Assert.AreEqual("A", list[4].Path);
            Assert.AreEqual("1", list[4].Field.Value);
            Assert.AreEqual("B", list[5].Path);
            Assert.IsInstanceOfType(list[5].Field.Value, typeof(AAFieldCollection));
            Assert.AreEqual("B/C", list[6].Path);
            Assert.AreEqual("3", list[6].Field.Value);
            Assert.AreEqual("B/C", list[7].Path);
            Assert.AreEqual("4", list[7].Field.Value);
        }
    }

    [TestMethod]
    public void AAFieldCollection_SetByPath() {
        var c = new AAFieldCollection();
        c[".HA"] = "1h";
        c[".HB/HC"] = "2h";
        c[".HB\\HC"] = "3h";
        c["A"] = "1";
        c["B/C"] = "2";
        c["B/C"] = "3";

        Assert.AreEqual("1h", c[".HA"]);
        Assert.IsInstanceOfType(c[".HB"], typeof(AAFieldCollection));
        Assert.AreEqual("3h", c[".HB/HC"]);
        Assert.AreEqual(1, c.FindFirst(".HB").Value.AsFieldCollection().Count);

        Assert.AreEqual("1", c["A"]);
        Assert.IsInstanceOfType(c["B"], typeof(AAFieldCollection));
        Assert.AreEqual("3", c["B\\C"]);
        Assert.AreEqual(1, c.FindFirst("B").Value.AsFieldCollection().Count);

        var list = new List<AAFieldNode>(c.PathsWithValue);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(".HA", list[0].Path);
        Assert.AreEqual("1h", list[0].Field.Value);
        Assert.AreEqual(".HB/HC", list[1].Path);
        Assert.AreEqual("3h", list[1].Field.Value);
        Assert.AreEqual("A", list[2].Path);
        Assert.AreEqual("1", list[2].Field.Value);
        Assert.AreEqual("B/C", list[3].Path);
        Assert.AreEqual("3", list[3].Field.Value);
    }

    [TestMethod]
    public void AAFieldCollection_SetByPathMixedType() {
        var c = new AAFieldCollection();
        c[".HA"] = "1h";
        c[".HB"] = 42;
        c[".HB/HC"] = "2h";
        c[".HB\\HC"] = "3h";
        c["A"] = "1";
        c["B/C"] = 42;
        c["B/C"] = "3";

        Assert.AreEqual("1h", c[".HA"]);
        Assert.IsInstanceOfType(c[".HB"], typeof(AAFieldCollection));
        Assert.AreEqual("3h", c[".HB/HC"]);
        Assert.AreEqual(1, c.FindFirst(".HB").Value.AsFieldCollection().Count);

        Assert.AreEqual("1", c["A"]);
        Assert.IsInstanceOfType(c["B"], typeof(AAFieldCollection));
        Assert.AreEqual("3", c["B\\C"]);
        Assert.AreEqual(1, c.FindFirst("B").Value.AsFieldCollection().Count);

        var list = new List<AAFieldNode>(c.PathsWithValue);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(".HA", list[0].Path);
        Assert.AreEqual("1h", list[0].Field.Value);
        Assert.AreEqual(".HB/HC", list[1].Path);
        Assert.AreEqual("3h", list[1].Field.Value);
        Assert.AreEqual("A", list[2].Path);
        Assert.AreEqual("1", list[2].Field.Value);
        Assert.AreEqual("B/C", list[3].Path);
        Assert.AreEqual("3", list[3].Field.Value);
    }

    #endregion

}
