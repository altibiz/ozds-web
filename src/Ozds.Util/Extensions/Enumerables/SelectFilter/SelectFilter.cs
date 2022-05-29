using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  // NOTE: https://www.infoq.com/articles/csharp-nullable-reference-case-study/
  // NOTE: https://www.reddit.com/r/csharp/comments/c4aev5/c_generics_t_either_nullablet_or_nullableref_of_t/ ...
  // I really hate this....
  // TODO: nullable versions for these functions?
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector) where TOut : struct
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        yield return result.Value;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

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
  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

      if (result.Truthy())
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      TOut @default)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default)
  {
    await foreach (var value in @this)
    {
      var result = selector(value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, TOut?> selector)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

      if (result.Truthy())
      {
        yield return result;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, TOut?> selector,
      TOut @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

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
  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default)
  {
    foreach (var value in @this)
    {
      var result = selector(await value);

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
