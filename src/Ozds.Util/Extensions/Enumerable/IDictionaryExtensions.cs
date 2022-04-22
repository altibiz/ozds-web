namespace Ozds.Util;

public static class IDictionaryExtensions
{
  public static TOut? GetOrDefault<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key)
      where TOut : class?
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      return default;
    }
  }

  public static TOut GetOrDefault<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key,
      TOut @default)
      where TOut : class?
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      return @default;
    }
  }

  public static TOut GetOrDefault<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key,
      Func<TOut> @default)
      where TOut : class?
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      return @default();
    }
  }

  public static async ValueTask<TOut> GetOrDefault<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key,
      Func<Task<TOut>> @default)
      where TOut : class?
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      return await @default();
    }
  }

  public static async ValueTask<TOut> GetOrDefault<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key,
      Func<ValueTask<TOut>> @default)
      where TOut : class?
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      return await @default();
    }
  }
}
