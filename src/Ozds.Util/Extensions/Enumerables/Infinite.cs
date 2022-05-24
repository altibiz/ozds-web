using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Infinite<T>(
      this T generator)
  {
    do
    {
      yield return generator;
    }
    while (true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> InfiniteAsync<T>(
      this T generator)
  {
    do
    {
      yield return await Task.FromResult(generator);
    }
    while (true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Infinite<T>(
      this Func<T> generator)
  {
    do
    {
      yield return generator();
    }
    while (true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Infinite<T>(
      this Func<int, T> generator)
  {
    var index = 0;

    do
    {
      yield return generator(index++);
    }
    while (true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> InfiniteTask<T>(
      this Func<Task<T>> generator)
  {
    do
    {
      yield return await generator();
    }
    while (true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> InfiniteTask<T>(
      this Func<int, Task<T>> generator)
  {
    var index = 0;

    do
    {
      yield return await generator(index++);
    }
    while (true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> InfiniteValueTask<T>(
      this Func<ValueTask<T>> generator)
  {
    do
    {
      yield return await generator();
    }
    while (true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> InfiniteValueTask<T>(
      this Func<int, ValueTask<T>> generator)
  {
    var index = 0;

    do
    {
      yield return await generator(index++);
    }
    while (true);
  }
}
