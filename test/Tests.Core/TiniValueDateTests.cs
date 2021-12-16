using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueDateTests {

    [Fact(DisplayName = "TiniValueDate: Basic")]
    public void Basic() {
        var text = "2021-01-14";
        Assert.True(TiniValueDate.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueDate.Parse(text));
    }

    [Fact(DisplayName = "TiniValueDate: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueDate.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueDate.Parse("A");
        });
    }

}
