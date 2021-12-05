using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniIPv4AddressValueTests {

    [Fact(DisplayName = "TiniIPv4AddressValue: Basic")]
    public void Basic() {
        var text = "239.192.111.17";
        Assert.True(TiniIPv4AddressValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniIPv4AddressValue.Parse(text));
    }

    [Fact(DisplayName = "TiniIPv4AddressValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniIPv4AddressValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniIPv4AddressValue.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniIPv4AddressValue: Out of range")]
    public void OutOfRange() {
        Assert.False(TiniIPv4AddressValue.TryParse("256.0.0.1", out var _));
        Assert.False(TiniIPv4AddressValue.TryParse("ff08::152", out var _));
    }

}
