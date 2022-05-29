using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return @default;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return await @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return await @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return @default;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return await @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default)
  {
    await foreach (var value in @this)
    {
      var result = await selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return await @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return @default;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return await @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return await @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return @default;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return await @default();
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilterValueTask<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = await selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
      else
      {
        yield return await @default();
      }
    }
  }
}
