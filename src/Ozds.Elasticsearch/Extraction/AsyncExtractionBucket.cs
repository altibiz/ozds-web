namespace Ozds.Elasticsearch;

// TODO: covariance with deconstruction
public interface IAsyncExtractionBucket<T> : IAsyncEnumerable<T>
{
  public Period Period { get; }

  public string? Error { get; }

  public void Deconstruct(
      out Period period,
      out string? error,
      out IAsyncEnumerable<T> items)
  {
    period = Period;
    error = Error;
    items = this;
  }
}

public class AsyncExtractionBucket<T> : IAsyncExtractionBucket<T>
{
  public AsyncExtractionBucket(
      Period period,
      IAsyncEnumerable<T> items)
  {
    Period = period;
    Items = items;
  }

  public AsyncExtractionBucket(
      Period period,
      string error)
  {
    Period = period;
    Error = error;
  }

  public Period Period { get; }

  public string? Error { get; } = default;

  IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(
      CancellationToken cancellationToken) =>
    Items.GetAsyncEnumerator(cancellationToken);

  private IAsyncEnumerable<T> Items { get; } = AsyncEnumerable.Empty<T>();
}
