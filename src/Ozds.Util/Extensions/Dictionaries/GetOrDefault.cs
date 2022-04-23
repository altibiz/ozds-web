using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Dictionaries
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? GetOrDefault<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key)
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut GetOrDefault<TIn, TOut>(
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
      return @default;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut GetOrDefault<TIn, TOut>(
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
      return @default();
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> GetOrDefault<TIn, TOut>(
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
      return await @default();
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> GetOrDefault<TIn, TOut>(
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
      return await @default();
    }
  }
}
