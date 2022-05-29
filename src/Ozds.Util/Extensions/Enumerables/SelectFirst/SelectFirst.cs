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

      if (result.Truthy())
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut SelectFirst<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut SelectFirst<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> SelectFirst<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> SelectFirst<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirst<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirst<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      TOut @default)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirst<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirst<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirst<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> SelectFirst<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirst<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirst<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirst<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> SelectFirst<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

      if (result.Truthy())
      {
        return result;
      }
    }

    return await @default();
  }
}
