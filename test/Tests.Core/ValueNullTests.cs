using AASeq;
using Xunit;

namespace Tests.Core;

public class ValueNullTests {

    [Fact(DisplayName = "ValueNull: Basic")]
    public void Basic() {
        Assert.True(ValueNull.TryParse(null, out var result));
        Assert.IsType<ValueNull>(result);
        Assert.Equal(string.Empty, result);
    }

}
