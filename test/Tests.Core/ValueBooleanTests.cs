using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueBooleanTests {

    [Fact(DisplayName = "ValueBoolean: Basic")]
    public void Basic() {
        var text = "True";
        Assert.True(ValueBoolean.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueBoolean.Parse(text));
    }

    [Fact(DisplayName = "ValueBoolean True")]
    public void True() {
        Assert.True(ValueBoolean.Parse("True"));
        Assert.True(ValueBoolean.Parse("T"));
        Assert.True(ValueBoolean.Parse("Yes"));
        Assert.True(ValueBoolean.Parse("Y"));
        Assert.True(ValueBoolean.Parse("+"));
    }

    [Fact(DisplayName = "ValueBoolean: False")]
    public void False() {
        Assert.False(ValueBoolean.Parse("False"));
        Assert.False(ValueBoolean.Parse("F"));
        Assert.False(ValueBoolean.Parse("No"));
        Assert.False(ValueBoolean.Parse("N"));
        Assert.False(ValueBoolean.Parse("-"));
    }

    [Fact(DisplayName = "ValueBoolean: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueBoolean.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueBoolean.Parse("A");
        });
    }

}
