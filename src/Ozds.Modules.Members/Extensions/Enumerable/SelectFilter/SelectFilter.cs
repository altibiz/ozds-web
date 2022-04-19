namespace Ozds.Modules.Members;

public static partial class IEnumerableExtensions
{
  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this, Func<TIn, TOut?> selector)
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

  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this, Func<TIn, TOut?> selector, TOut @default)
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

  public static IEnumerable<TOut> SelectFilter<TIn, TOut>(this IEnumerable<TIn> @this,
      Func<TIn, TOut?> selector, Func<TOut> @default)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this, Func<TIn, TOut?> selector,
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<TIn> @this, Func<TIn, TOut?> selector,
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this, Func<TIn, TOut?> selector)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this, Func<TIn, TOut?> selector, TOut @default)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this,
      Func<TIn, TOut?> selector, Func<TOut> @default)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this, Func<TIn, TOut?> selector,
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IAsyncEnumerable<TIn> @this, Func<TIn, TOut?> selector,
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this, Func<TIn, TOut?> selector)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this, Func<TIn, TOut?> selector, TOut @default)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this,
      Func<TIn, TOut?> selector, Func<TOut> @default)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this, Func<TIn, TOut?> selector,
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<Task<TIn>> @this, Func<TIn, TOut?> selector,
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this, Func<TIn, TOut?> selector)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this, Func<TIn, TOut?> selector, TOut @default)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this,
      Func<TIn, TOut?> selector, Func<TOut> @default)
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this, Func<TIn, TOut?> selector,
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

  public static async IAsyncEnumerable<TOut> SelectFilter<TIn, TOut>(
      this IEnumerable<ValueTask<TIn>> @this, Func<TIn, TOut?> selector,
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
