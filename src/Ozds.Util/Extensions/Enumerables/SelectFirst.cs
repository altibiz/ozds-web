using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? SelectFirst<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result is not null)
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstAsync<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result is not null)
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstAwait<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result is not null)
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstAwaitAsync<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result is not null)
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? SelectFirst<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector) where TOut : struct
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result is not null)
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstAsync<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector) where TOut : struct
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result is not null)
      {
        return result.Value;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstAwait<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector) where TOut : struct
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result is not null)
      {
        return result.Value;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstAwaitAsync<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector) where TOut : struct
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result is not null)
      {
        return result.Value;
      }
    }

    return default;
  }
}
