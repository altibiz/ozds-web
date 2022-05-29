using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

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
  public async static IAsyncEnumerable<T> InfiniteAsync<T>(
      this Func<ValueTask<T>> generator)
  {
    do
    {
      yield return await generator();
    }
    while (true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public async static IAsyncEnumerable<T> InfiniteAsync<T>(
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
