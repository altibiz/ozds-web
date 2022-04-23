using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirstValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }
}