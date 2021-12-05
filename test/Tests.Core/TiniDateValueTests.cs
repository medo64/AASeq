using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniDateValueTests {

    [Fact(DisplayName = "TiniDateValue: Basic")]
    public void Basic() {
        var text = "2021-01-14";
        Assert.True(TiniDateValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniDateValue.Parse(text));
    }

    [Fact(DisplayName = "TiniDateValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniDateValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniDateValue.Parse("A");
        });
    }

}
