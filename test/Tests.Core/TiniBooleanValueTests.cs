using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniBooleanValueTests {

    [Fact(DisplayName = "TiniBooleanValue: Basic")]
    public void Basic() {
        var text = "True";
        Assert.True(TiniBooleanValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniBooleanValue.Parse(text));
    }

    [Fact(DisplayName = "TiniBooleanValue: True")]
    public void True() {
        Assert.True(TiniBooleanValue.Parse("True"));
        Assert.True(TiniBooleanValue.Parse("T"));
        Assert.True(TiniBooleanValue.Parse("Yes"));
        Assert.True(TiniBooleanValue.Parse("Y"));
        Assert.True(TiniBooleanValue.Parse("+"));
    }

    [Fact(DisplayName = "TiniBooleanValue: False")]
    public void False() {
        Assert.False(TiniBooleanValue.Parse("False"));
        Assert.False(TiniBooleanValue.Parse("F"));
        Assert.False(TiniBooleanValue.Parse("No"));
        Assert.False(TiniBooleanValue.Parse("N"));
        Assert.False(TiniBooleanValue.Parse("-"));
    }

    [Fact(DisplayName = "TiniBooleanValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniBooleanValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniBooleanValue.Parse("A");
        });
    }

}
