using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Single<T>(
      this T generator)
  {
    yield return generator;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> SingleAsync<T>(
      this T generator)
  {
    yield return await Task.FromResult(generator);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> Single<T>(
      this Task<T> generator)
  {
    yield return await generator;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> Single<T>(
      this ValueTask<T> generator)
  {
    yield return await generator;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Single<T>(
      this Func<T> generator)
  {
    yield return generator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> Single<T>(
      this Func<Task<T>> generator)
  {
    yield return await generator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> Single<T>(
      this Func<ValueTask<T>> generator)
  {
    yield return await generator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> SingleNullable<T>(
      this T? generator)
  {
    if (generator is not null)
    {
      yield return generator;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> SingleNullableAsync<T>(
      this T generator)
  {
    if (generator is not null)
    {
      yield return await Task.FromResult(generator);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> SingleNullable<T>(
      this Task<T?> generator)
  {
    if (await generator is T awaited)
    {
      yield return awaited;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> SingleNullable<T>(
      this ValueTask<T?> generator)
  {
    if (await generator is T awaited)
    {
      yield return awaited;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> SingleNullable<T>(
      this Func<T?> generator)
  {
    if (generator() is T awaited)
    {
      yield return awaited;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> SingleNullable<T>(
      this Func<Task<T?>> generator)
  {
    if (await generator() is T awaited)
    {
      yield return awaited;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> SingleNullable<T>(
      this Func<ValueTask<T?>> generator)
  {
    if (await generator() is T awaited)
    {
      yield return awaited;
    }
  }
}
