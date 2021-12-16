using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniTagTests {

    [Fact(DisplayName = "TiniTag: Basic")]
    public void Basic() {
        var tag = new TiniTag("Test");
        Assert.Equal("Test", tag.Name);
        Assert.True(tag.State);
    }

    [Fact(DisplayName = "TiniTag: Equality")]
    public void Equality() {
        Assert.Equal(new TiniTag("Test"), new TiniTag("Test"));
        Assert.Equal(new TiniTag("Test", true), new TiniTag("Test", true));
        Assert.Equal(new TiniTag("Test", false), new TiniTag("Test", false));
        Assert.NotEqual(new TiniTag("Test", true), new TiniTag("Test", false));
        Assert.NotEqual(new TiniTag("A"), new TiniTag("B"));
        Assert.NotEqual(new TiniTag("@A"), new TiniTag("A"));
    }

    [Fact(DisplayName = "TiniTag: Changed")]
    public void Changed() {
        var tag = new TiniTag("Test");
        Assert.Raises<EventArgs>(
            handler => tag.Changed += handler,
            handler => tag.Changed -= handler,
            () => tag.State = false
        );
    }


    [Fact(DisplayName = "TiniTag: Invalid names")]
    public void InvalidNames() {
        Assert.Throws<ArgumentNullException>(() => new TiniTag(null));
        Assert.Throws<ArgumentOutOfRangeException>(() => new TiniTag(""));  // cannot be empty
        Assert.Throws<ArgumentOutOfRangeException>(() => new TiniTag("@"));  // cannot have only at sign
        Assert.Throws<ArgumentOutOfRangeException>(() => new TiniTag("A "));  // no spaces allowed
        Assert.Throws<ArgumentOutOfRangeException>(() => new TiniTag("1A"));  // cannot start with number
        Assert.Throws<ArgumentOutOfRangeException>(() => new TiniTag("@1A"));  // cannot start with a number
    }

}
