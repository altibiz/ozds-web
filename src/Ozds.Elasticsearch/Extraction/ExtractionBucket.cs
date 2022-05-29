using System.Collections;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

// TODO: covariance with deconstruction
public interface IExtractionBucket<T> : IEnumerable<T>
{
  public Period Period { get; }

  public string? Error { get; }

  public void Deconstruct(
      out Period period,
      out string? error,
      out IEnumerable<T> items)
  {
    period = Period;
    error = Error;
    items = this;
  }
}

public class ExtractionBucket<T> : IExtractionBucket<T>
{
  public ExtractionBucket(
      Period period,
      IEnumerable<T> items)
  {
    Period = period;
    Items = items;
  }

  public ExtractionBucket(
      Period period,
      string error)
  {
    Period = period;
    Error = error;
  }

  public Period Period { get; }

  public string? Error { get; } = default;

  IEnumerator<T> IEnumerable<T>.GetEnumerator() =>
    Items.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() =>
    Items.GetEnumerator();

  private IEnumerable<T> Items { get; } = Enumerables.Empty<T>();
}
