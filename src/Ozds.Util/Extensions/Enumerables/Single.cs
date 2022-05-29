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
  public async static IAsyncEnumerable<T> SingleAsync<T>(
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
  public async static IAsyncEnumerable<T> SingleAsync<T>(
      this Func<ValueTask<T>> generator)
  {
    yield return await generator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> SingleNullable<T>(
      this T? generator)
  {
    if (generator is T nonNullable)
    {
      yield return nonNullable;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> SingleNullableAsync<T>(
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
  public async static IAsyncEnumerable<T> SingleNullableAsync<T>(
      this Func<ValueTask<T?>> generator)
  {
    if (await generator() is T awaited)
    {
      yield return awaited;
    }
  }
}
