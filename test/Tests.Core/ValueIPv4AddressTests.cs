using System;
using Tipfeler;
using Xunit;

namespace Tests.Core;

public class ValueIPv4AddressTests {

    [Fact(DisplayName = "ValueIPv4Address: Basic")]
    public void Basic() {
        var text = "239.192.111.17";
        Assert.True(ValueIPv4Address.TryParse(text, out var result));
        Assert.Equal(text, result.ToString());
        Assert.Equal(text, ValueIPv4Address.Parse(text));
    }

    [Fact(DisplayName = "ValueIPv4Address Failed Parse")]
    public void FailedParse() {
        Assert.False(ValueIPv4Address.TryParse("A", out var _));
        Assert.Throws<FormatException>(() => {
            ValueIPv4Address.Parse("A");
        });
    }

    [Fact(DisplayName = "ValueIPv4Address: Out of range")]
    public void OutOfRange() {
        Assert.False(ValueIPv4Address.TryParse("256.0.0.1", out var _));
        Assert.False(ValueIPv4Address.TryParse("ff08::152", out var _));
    }

}
