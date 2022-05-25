using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Dictionaries
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut GetOrAdd<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key,
      TOut @default)
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      @this.Add(key, @default);
      return @default;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut GetOrAdd<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key,
      Func<TOut> @default)
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      @this.Add(key, @default());
      return @default();
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> GetOrAdd<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key,
      Func<Task<TOut>> @default)
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      @this.Add(key, await @default());
      return await @default();
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> GetOrAdd<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key,
      Func<ValueTask<TOut>> @default)
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      @this.Add(key, await @default());
      return await @default();
    }
  }
}
