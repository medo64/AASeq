using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueNullTests {

    [Fact(DisplayName = "TiniValueNull: Basic")]
    public void Basic() {
        Assert.True(TiniValueNull.TryParse(null, out var result));
        Assert.IsType<TiniValueNull>(result);
        Assert.Equal(string.Empty, result);
    }

}
