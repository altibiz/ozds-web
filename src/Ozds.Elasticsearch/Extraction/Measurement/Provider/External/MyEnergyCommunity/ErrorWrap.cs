namespace Ozds.Elasticsearch.MyEnergyCommunity;

// TODO: use a proper union type
public class ErrorWrap<T>
{
  public ErrorWrap(T result)
  {
    Result = result;
  }

  public ErrorWrap(string error)
  {
    Error = error;
  }

  public static implicit operator ErrorWrap<T>(T result) =>
    new ErrorWrap<T>(result);

  public static implicit operator ErrorWrap<T>(string error) =>
    new ErrorWrap<T>(error);

  public T? Result { get; } = default;

  public string? Error { get; } = default;

  public void Deconstruct(out T? result, out string? error)
  {
    result = Result;
    error = Error;
  }
}
