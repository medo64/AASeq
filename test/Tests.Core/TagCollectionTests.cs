using System;
using AASeq;
using Xunit;

namespace Tests.Core;

public class TagCollectionTests {

    [Fact(DisplayName = "TagCollection: Basic")]
    public void Basic() {
        var c = new TagCollection {
            new Tag("Test"),
            new Tag("Test2", false)
        };
        Assert.Equal(2, c.Count);
        Assert.Equal("Test", c[0].Name);
        Assert.True(c[0].State);
        Assert.True(c.GetState("Test"));
        Assert.Equal("Test2", c[1].Name);
        Assert.False(c[1].State);
        Assert.False(c.GetState("Test2"));
    }

    [Fact(DisplayName = "TagCollection: State lookup")]
    public void StateLookup() {
        var c = new TagCollection {
            new Tag("Test", false)
        };
        Assert.Null(c.GetState("XXX"));
    }

    [Fact(DisplayName = "TagCollection: Duplicate name")]
    public void DuplicateName() {
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            var c = new TagCollection {
                new Tag("Test"),
                new Tag("test", false)
            };
        });
    }

    [Fact(DisplayName = "TagCollection: Lookup by name")]
    public void LookupByName() {
        var c = new TagCollection {
            new Tag("Test1"),
            new Tag("Test2")
        };
        Assert.Equal(2, c.Count);
        Assert.Equal("Test1", c["test1"].Name);
        Assert.Equal("Test2", c["test2"].Name);
    }

    [Fact(DisplayName = "TagCollection: Remove by name")]
    public void RemoveByName() {
        var c = new TagCollection {
            new Tag("Test")
        };
        c.Remove("Test");
        Assert.Empty(c);
    }

}
