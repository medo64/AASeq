using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueIPv6AddressTests {

    [Fact(DisplayName = "ValueIPv6Address: Basic")]
    public void Basic() {
        var text = "ff08::152";
        Assert.True(ValueIPv6Address.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueIPv6Address.Parse(text));
    }

    [Fact(DisplayName = "ValueIPv6Address: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueIPv6Address.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueIPv6Address.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueIPv6Address: Out of range")]
    public void OutOfRange() {
        Assert.False(ValueIPv6Address.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
        Assert.False(ValueIPv6Address.TryParse("239.192.111.17", out var _));
    }

}
