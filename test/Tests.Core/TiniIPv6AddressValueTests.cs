using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniIPv6AddressValueTests {

    [Fact(DisplayName = "TiniIPv6AddressValue: Basic")]
    public void Basic() {
        var text = "ff08::152";
        Assert.True(TiniIPv6AddressValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniIPv6AddressValue.Parse(text));
    }

    [Fact(DisplayName = "TiniIPv6AddressValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniIPv6AddressValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniIPv6AddressValue.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniIPv6AddressValue: Out of range")]
    public void OutOfRange() {
        Assert.False(TiniIPv6AddressValue.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
        Assert.False(TiniIPv6AddressValue.TryParse("239.192.111.17", out var _));
    }

}
