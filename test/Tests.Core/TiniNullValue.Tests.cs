using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniNullValueTests {

    [Fact(DisplayName = "TiniNullValue: Basic")]
    public void Basic() {
        Assert.True(TiniNullValue.TryParse(null, out var result));
        Assert.IsType<TiniNullValue>(result);
        Assert.Equal(string.Empty, result);
    }

}
