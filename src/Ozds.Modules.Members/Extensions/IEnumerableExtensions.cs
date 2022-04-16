namespace Ozds.Modules.Members;

public static class IEnumerableExtensions
{
  public static TOut? SelectFirstOrDefault<TIn, TOut>(
      this IEnumerable<TIn> @this, Func<TIn, TOut?> select)
      where TOut : class?
  {
    foreach (var value in @this)
    {
      var result = select(value);

      if (result == default)
      {
        return result;
      }
    }

    return default;
  }

  public static TOut SelectFirstOrDefault<TIn, TOut>(
      this IEnumerable<TIn> @this, Func<TIn, TOut> select, TOut @default)
      where TOut : class?
  {
    foreach (var value in @this)
    {
      var result = select(value);

      if (result == default)
      {
        return result;
      }
    }

    return @default;
  }

  public static TOut SelectFirstOrDefault<TIn, TOut>(
      this IEnumerable<TIn> @this, Func<TIn, TOut> select, Func<TOut> @default)
      where TOut : class?
  {
    foreach (var value in @this)
    {
      var result = select(value);

      if (result == default)
      {
        return result;
      }
    }

    return @default();
  }

  public static IEnumerable<TValue> ForEach<TValue>(
      this IEnumerable<TValue> @this, Action<TValue> @do)
  {
    foreach (var value in @this)
    {
      @do(value);
      yield return value;
    }
  }

  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IAsyncEnumerable<TValue> @this, Action<TValue> @do)
  {
    await foreach (var value in @this)
    {
      @do(value);
      yield return value;
    }
  }
}
