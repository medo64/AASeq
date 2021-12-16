using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueBooleanTests {

    [Fact(DisplayName = "TiniValueBoolean: Basic")]
    public void Basic() {
        var text = "True";
        Assert.True(TiniValueBoolean.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueBoolean.Parse(text));
    }

    [Fact(DisplayName = "TiniValueBoolean True")]
    public void True() {
        Assert.True(TiniValueBoolean.Parse("True"));
        Assert.True(TiniValueBoolean.Parse("T"));
        Assert.True(TiniValueBoolean.Parse("Yes"));
        Assert.True(TiniValueBoolean.Parse("Y"));
        Assert.True(TiniValueBoolean.Parse("+"));
    }

    [Fact(DisplayName = "TiniValueBoolean: False")]
    public void False() {
        Assert.False(TiniValueBoolean.Parse("False"));
        Assert.False(TiniValueBoolean.Parse("F"));
        Assert.False(TiniValueBoolean.Parse("No"));
        Assert.False(TiniValueBoolean.Parse("N"));
        Assert.False(TiniValueBoolean.Parse("-"));
    }

    [Fact(DisplayName = "TiniValueBoolean: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueBoolean.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueBoolean.Parse("A");
        });
    }

}
