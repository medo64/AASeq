using System;
using AASeq;
using Xunit;

namespace Tests.Core;

public class TagTests {

    [Fact(DisplayName = "Tag: Basic")]
    public void Basic() {
        var tag = new Tag("Test");
        Assert.Equal("Test", tag.Name);
        Assert.True(tag.State);
    }

    [Fact(DisplayName = "Tag: Equality")]
    public void Equality() {
        Assert.Equal(new Tag("Test"), new Tag("Test"));
        Assert.Equal(new Tag("Test", true), new Tag("Test", true));
        Assert.Equal(new Tag("Test", false), new Tag("Test", false));
        Assert.NotEqual(new Tag("Test", true), new Tag("Test", false));
        Assert.NotEqual(new Tag("A"), new Tag("B"));
        Assert.NotEqual(new Tag("@A"), new Tag("A"));
    }

    [Fact(DisplayName = "Tag: Changed")]
    public void Changed() {
        var tag = new Tag("Test");
        Assert.Raises<EventArgs>(
            handler => tag.Changed += handler,
            handler => tag.Changed -= handler,
            () => tag.State = false
        );
    }


    [Fact(DisplayName = "Tag: Invalid names")]
    public void InvalidNames() {
        Assert.Throws<ArgumentNullException>(() => new Tag(null));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tag(""));  // cannot be empty
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tag("@"));  // cannot have only at sign
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tag("A "));  // no spaces allowed
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tag("1A"));  // cannot start with number
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tag("@1A"));  // cannot start with a number
    }

}
