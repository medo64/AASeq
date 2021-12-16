using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueIPv4AddressTests {

    [Fact(DisplayName = "TiniValueIPv4Address: Basic")]
    public void Basic() {
        var text = "239.192.111.17";
        Assert.True(TiniValueIPv4Address.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueIPv4Address.Parse(text));
    }

    [Fact(DisplayName = "TiniValueIPv4Address Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueIPv4Address.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueIPv4Address.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueIPv4Address: Out of range")]
    public void OutOfRange() {
        Assert.False(TiniValueIPv4Address.TryParse("256.0.0.1", out var _));
        Assert.False(TiniValueIPv4Address.TryParse("ff08::152", out var _));
    }

}
