using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result is not null)
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterAsync<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result is not null)
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterAwaitAsync<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result is not null)
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector) where TOut : struct
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result.HasValue)
      {
        yield return result.Value;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterAsync<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector) where TOut : struct
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result.HasValue)
      {
        yield return result.Value;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterAwaitAsync<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector) where TOut : struct
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.HasValue)
      {
        yield return result.Value;
      }
    }
  }
}
