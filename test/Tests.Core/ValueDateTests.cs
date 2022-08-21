using System;
using AASeq;
using Xunit;

namespace Tests.Core;

public class ValueDateTests {

    [Fact(DisplayName = "ValueDate: Basic")]
    public void Basic() {
        var text = "2021-01-14";
        Assert.True(ValueDate.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueDate.Parse(text));
    }

    [Fact(DisplayName = "ValueDate: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueDate.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueDate.Parse("A");
        });
    }

}
