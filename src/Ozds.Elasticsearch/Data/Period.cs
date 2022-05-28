using Nest;

namespace Ozds.Elasticsearch;

[ElasticsearchType(RelationName = "period")]
public class Period :
  IEquatable<Period>,
  IComparable<Period>,
  ICloneable
{
  [Date(Name = "from")]
  public DateTime From { get; init; } = DateTime.MinValue.ToUniversalTime();

  [Date(Name = "to")]
  public DateTime To { get; init; } = DateTime.UtcNow;

  [Ignore]
  public TimeSpan Span { get => To - From; }

  [Ignore]
  public DateTime HalfPoint { get => From + (Span / 2); }

  public static Period UntilNow(DateTime from) =>
    new Period
    {
      From = from,
      To = DateTime.UtcNow
    };

  public static Period Encompassing(IEnumerable<DateTime> events) =>
    new Period
    {
      From = events.Min(),
      To = events.Max()
    };

  public static Period LooselyEncompassing(IEnumerable<DateTime> events) =>
    new Period
    {
      From = events.Min().AddMinutes(-1),
      To = events.Max().AddMinutes(1)
    };

  public decimal Interpolate(decimal begin, decimal end, DateTime at)
  {
    var ticks = (To - From).Ticks;
    var beginTicks = (at - From).Ticks;
    var endTicks = (To - at).Ticks;

    return
      ((begin * beginTicks) + (end * endTicks)) /
      (decimal)ticks;
  }

  public Period LimitFrom(TimeSpan span) =>
    Span > span ?
      new()
      {
        From = To - span,
        To = To
      }
    : ClonePeriod();

  public Period LimitTo(TimeSpan span) =>
    Span > span ?
      new()
      {
        From = From,
        To = From + span
      }
    : ClonePeriod();

  public IEnumerable<Period> SplitAscending(int times) =>
    SplitAscending(Span / times);

  public IEnumerable<Period> SplitAscending(TimeSpan span)
  {
    var currentFrom = From;
    var fromTreshold = To - span;

    while (currentFrom < fromTreshold)
    {
      var from = currentFrom;
      var to = currentFrom += span;
      yield return
        new Period
        {
          From = from,
          To = to,
        };
    }

    yield return
      new Period
      {
        From = currentFrom,
        To = To,
      };
  }

  public IEnumerable<Period> SplitDescending(int times) =>
    SplitAscending(Span / times);

  public IEnumerable<Period> SplitDescending(TimeSpan span)
  {
    var currentTo = To;
    var toThreshold = From + span;

    while (currentTo > toThreshold)
    {
      var to = currentTo;
      var from = currentTo -= span;
      yield return
        new Period
        {
          From = from,
          To = to,
        };
    }

    yield return
      new Period
      {
        From = From,
        To = currentTo
      };
  }

  public override bool Equals(object? obj) =>
    Equals(obj as Period);

  public bool Equals(Period? other) =>
    other != null &&
    From == other.From &&
    To == other.To;

  public override int GetHashCode() =>
    HashCode.Combine(From, To);

  public override string ToString() =>
    $"{From.ToUtcIsoString()} - {To.ToUtcIsoString()}";

  public int CompareTo(Period? other) =>
    other switch
    {
      (_, DateTime otherTo) => To.CompareTo(otherTo),
      null => From.CompareTo(null)
    };

  public void Deconstruct(out DateTime from, out DateTime to)
  {
    from = From;
    to = To;
  }

  public object Clone() => ClonePeriod();

  public Period ClonePeriod() =>
    new Period
    {
      From = From,
      To = To,
    };
};

public static class PeriodExtensions
{
  public static Period UntilNow(this DateTime @this) =>
    Period.UntilNow(@this);
}
