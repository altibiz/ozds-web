using Xunit;

namespace Ozds.Elasticsearch.Test.MeasurementFaker;

public partial class PeriodTest
{
  [Fact]
  public void SplitAscending()
  {
    var now = DateTime.UtcNow;
    var period =
      new Period
      {
        From = now.AddMinutes(-5),
        To = now
      };

    var split = period.SplitAscending(TimeSpan.FromSeconds(30));
    Assert.All(
      split,
      inner =>
      {
        Assert.InRange(inner.From, period.From, period.To);
        Assert.InRange(inner.To, period.From, period.To);
      });
  }

  [Fact]
  public void SplitDescending()
  {
    var now = DateTime.UtcNow;
    var period =
      new Period
      {
        From = now.AddMinutes(-5),
        To = now
      };

    var split = period.SplitAscending(TimeSpan.FromSeconds(30));
    Assert.All(
      split,
      inner =>
      {
        Assert.InRange(inner.From, period.From, period.To);
        Assert.InRange(inner.To, period.From, period.To);
      });
  }
}
