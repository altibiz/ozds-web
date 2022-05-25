using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TValue> ForEach<TValue>(
      this IEnumerable<TValue> @this,
      Action<TValue> @do)
  {
    foreach (var value in @this)
    {
      @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IEnumerable<TValue> @this,
      Func<TValue, Task> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> ForEachValueTask<TValue>(
      this IEnumerable<TValue> @this,
      Func<TValue, ValueTask> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IAsyncEnumerable<TValue> @this,
      Action<TValue> @do)
  {
    await foreach (var value in @this)
    {
      @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IAsyncEnumerable<TValue> @this,
      Func<TValue, Task> @do)
  {
    await foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> ForEachValueTask<TValue>(
      this IAsyncEnumerable<TValue> @this,
      Func<TValue, ValueTask> @do)
  {
    await foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IEnumerable<TValue>> ForEachAwait<TValue>(
      this IEnumerable<TValue> @this,
      Func<TValue, Task> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
    }

    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IEnumerable<TValue>> ForEachAwaitValueTask<TValue>(
      this IEnumerable<TValue> @this,
      Func<TValue, ValueTask> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
    }

    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IEnumerable<TValue>> ForEachAwait<TValue>(
      this IAsyncEnumerable<TValue> @this,
      Action<TValue> @do)
  {
    await foreach (var value in @this) { @do(value); }

    return await @this.Await();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IEnumerable<TValue>> ForEachAwait<TValue>(
      this IAsyncEnumerable<TValue> @this,
      Func<TValue, Task> @do)
  {
    await foreach (var value in @this) { await @do(value); }

    return await @this.Await();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IEnumerable<TValue>> ForEachAwaitValueTask<TValue>(
      this IAsyncEnumerable<TValue> @this,
      Func<TValue, ValueTask> @do)
  {
    await foreach (var value in @this) { await @do(value); }

    return await @this.Await();
  }
}
