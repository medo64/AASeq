using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueIPv6AddressTests {

    [Fact(DisplayName = "TiniValueIPv6Address: Basic")]
    public void Basic() {
        var text = "ff08::152";
        Assert.True(TiniValueIPv6Address.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueIPv6Address.Parse(text));
    }

    [Fact(DisplayName = "TiniValueIPv6Address: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueIPv6Address.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueIPv6Address.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueIPv6Address: Out of range")]
    public void OutOfRange() {
        Assert.False(TiniValueIPv6Address.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
        Assert.False(TiniValueIPv6Address.TryParse("239.192.111.17", out var _));
    }

}
