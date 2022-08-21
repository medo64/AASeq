using System;
using AASeq;
using Xunit;

namespace Tests.Core;

public class ValueIPAddressTests {

    [Fact(DisplayName = "ValueIPAddress: Basic IPv4")]
    public void BasicV4() {
        var text = "239.192.111.17";
        Assert.True(ValueIPAddress.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueIPAddress.Parse(text));
    }

    [Fact(DisplayName = "ValueIPAddress: Basic IPv6")]
    public void BasicV6() {
        var text = "ff08::152";
        Assert.True(ValueIPAddress.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueIPAddress.Parse(text));
    }

    [Fact(DisplayName = "ValueIPAddress: Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueIPAddress.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueIPAddress.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueIPAddress: Out of range")]
    public void OutOfRange() {
        Assert.False(ValueIPAddress.TryParse("256.0.0.1", out var _));
        Assert.False(ValueIPAddress.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
    }

}
