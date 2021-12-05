using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniIPAddressValueTests {

    [Fact(DisplayName = "TiniIPAddressValue: Basic IPv4")]
    public void BasicV4() {
        var text = "239.192.111.17";
        Assert.True(TiniIPAddressValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniIPAddressValue.Parse(text));
    }

    [Fact(DisplayName = "TiniIPAddressValue: Basic IPv6")]
    public void BasicV6() {
        var text = "ff08::152";
        Assert.True(TiniIPAddressValue.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, TiniIPAddressValue.Parse(text));
    }

    [Fact(DisplayName = "TiniIPAddressValue: Failed Parse")]
    public void FailedParse() {
        Assert.False(TiniIPAddressValue.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            TiniIPAddressValue.Parse("A");
        });
    }

    [Fact(DisplayName = "TiniIPAddressValue: Out of range")]
    public void OutOfRange() {
        Assert.False(TiniIPAddressValue.TryParse("256.0.0.1", out var _));
        Assert.False(TiniIPAddressValue.TryParse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff:0", out var _));
    }

}
