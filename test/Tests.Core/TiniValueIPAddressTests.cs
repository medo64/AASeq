using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueIPAddressTests {

    [Fact(DisplayName = "TiniValueIPAddress: Basic IPv4")]
    public void BasicV4() {
        var text = "239.192.111.17";
        Assert.True(TiniValueIPAddress.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueIPAddress.Parse(text));
    }

    [Fact(DisplayName = "TiniValueIPAddress: Basic IPv6")]
    public void BasicV6() {
        var text = "ff08::152";
        Assert.True(TiniValueIPAddress.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniValueIPAddress.Parse(text));
    }

    [Fact(DisplayName = "TiniValueIPAddress: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniValueIPAddress.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniValueIPAddress.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniValueIPAddress: Out of range")]
    public void OutOfRange() {
        Assert.False(TiniValueIPAddress.TryParse("256.0.0.1", out var _));
        Assert.False(TiniValueIPAddress.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
    }

}
