using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq;
using System.Collections.Generic;

namespace Tests;

[TestClass]
public class TagCollection_Tests {

    [TestMethod]
    public void TagCollection_Normal() {
        var c = new TagCollection(new Tag[] {
            new Tag("TestE"),
            new Tag("TestD", false)
        });
        Assert.AreEqual(2, c.Count);
        Assert.AreEqual("TestE", c[0].Name);
        Assert.IsTrue(c[0].State);
        Assert.AreEqual("TestD", c[1].Name);
        Assert.IsFalse(c[1].State);

        var normalList = new List<Tag>(c.EnumerateTags());
        Assert.AreEqual(2, normalList.Count);
        Assert.AreEqual("TestE", normalList[0].Name);
        Assert.AreEqual("TestD", normalList[1].Name);

        var systemList = new List<Tag>(c.EnumerateSystemTags());
        Assert.AreEqual(0, systemList.Count);
    }

    [TestMethod]
    public void TagCollection_System() {
        var c = new TagCollection(new Tag[] {
            new Tag("TestE"),
            new Tag("TestD", false),
            new Tag("@SysTestE"),
            new Tag("@SysTestD", false),
        });
        Assert.AreEqual(4, c.Count);
        Assert.AreEqual("TestE", c[0].Name);
        Assert.IsTrue(c[0].State);
        Assert.AreEqual("TestD", c[1].Name);
        Assert.IsFalse(c[1].State);
        Assert.AreEqual("SysTestE", c[2].Name);
        Assert.IsTrue(c[2].State);
        Assert.AreEqual("SysTestD", c[3].Name);
        Assert.IsFalse(c[3].State);

        var normalList = new List<Tag>(c.EnumerateTags());
        Assert.AreEqual(2, normalList.Count);
        Assert.AreEqual("TestE", normalList[0].Name);
        Assert.AreEqual("TestD", normalList[1].Name);

        var systemList = new List<Tag>(c.EnumerateSystemTags());
        Assert.AreEqual(2, systemList.Count);
        Assert.AreEqual("SysTestE", systemList[0].Name);
        Assert.AreEqual("SysTestD", systemList[1].Name);
    }

    [TestMethod]
    public void TagCollection_StateLookup() {
        var c = new TagCollection(new Tag[] {
            new Tag("TestE"),
            new Tag("TestD", false),
            new Tag("@SysTestE"),
            new Tag("@SysTestD", false),
        });

        Assert.IsFalse(c.IsTagEnabled("Test"));
        Assert.IsFalse(c.IsTagDisabled("Test"));
        Assert.IsTrue(c.IsTagEnabled("TestE"));
        Assert.IsFalse(c.IsTagDisabled("TestE"));
        Assert.IsFalse(c.IsTagEnabled("TestD"));
        Assert.IsTrue(c.IsTagDisabled("TestD"));

        Assert.IsFalse(c.IsTagEnabled("SysTest"));
        Assert.IsFalse(c.IsTagDisabled("SysTest"));
        Assert.IsFalse(c.IsTagEnabled("SysTestE"));
        Assert.IsFalse(c.IsTagDisabled("SysTestE"));
        Assert.IsFalse(c.IsTagEnabled("SysTestD"));
        Assert.IsFalse(c.IsTagDisabled("SysTestD"));
    }

    [TestMethod]
    public void TagCollection_SystemStateLookup() {
        var c = new TagCollection(new Tag[] {
            new Tag("TestE"),
            new Tag("TestD", false),
            new Tag("@SysTestE"),
            new Tag("@SysTestD", false),
        });

        Assert.IsFalse(c.IsSystemTagEnabled("SysTest"));
        Assert.IsFalse(c.IsSystemTagDisabled("SysTest"));
        Assert.IsTrue(c.IsSystemTagEnabled("SysTestE"));
        Assert.IsFalse(c.IsSystemTagDisabled("SysTestE"));
        Assert.IsFalse(c.IsSystemTagEnabled("SysTestD"));
        Assert.IsTrue(c.IsSystemTagDisabled("SysTestD"));

        Assert.IsFalse(c.IsSystemTagEnabled("Test"));
        Assert.IsFalse(c.IsSystemTagDisabled("Test"));
        Assert.IsFalse(c.IsSystemTagEnabled("TestE"));
        Assert.IsFalse(c.IsSystemTagDisabled("TestE"));
        Assert.IsFalse(c.IsSystemTagEnabled("TestD"));
        Assert.IsFalse(c.IsSystemTagDisabled("TestD"));
    }

    [TestMethod]
    public void TagCollection_DuplicateName() {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            var c = new TagCollection(new Tag[] {
                new Tag("Test"),
                new Tag("test", false)
            });
        });
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            var c = new TagCollection(new Tag[] {
                new Tag("Test"),
                new Tag("@test")
            });
        });
    }


    [TestMethod]
    public void TagCollection_Equality1() {
        var c1 = new TagCollection(new Tag[] {
            new Tag("Test1"),
            new Tag("Test2")
        });
        var c2 = new TagCollection(new Tag[] {
            new Tag("Test1"),
            new Tag("test2")
        });
        Assert.IsTrue(c1.Equals(c2));
    }

    [TestMethod]
    public void TagCollection_Equality2() {
        var c1 = new TagCollection(new Tag[] {
            new Tag("Test1"),
            new Tag("Test2")
        });
        var c2 = new TagCollection(new Tag[] {
            new Tag("Test1"),
            new Tag("test2a")
        });
        Assert.IsFalse(c1.Equals(c2));
    }

    [TestMethod]
    public void TagCollection_Equality3() {
        var c1 = new TagCollection(new Tag[] {
            new Tag("Test1"),
            new Tag("Test2")
        });
        Assert.IsFalse(c1.Equals(null));
    }

}
