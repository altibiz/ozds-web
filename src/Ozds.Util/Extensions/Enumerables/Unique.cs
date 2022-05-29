using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Unique<T>(
      this IEnumerable<T> @this)
  {
    HashSet<T> uniques = new();

    foreach (var item in @this)
    {
      if (uniques.Add(item))
      {
        yield return item;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> Unique<T>(
      this IAsyncEnumerable<T> @this)
  {
    HashSet<T> uniques = new();

    await foreach (var item in @this)
    {
      if (uniques.Add(item))
      {
        yield return item;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TValue> Unique<TValue, TSelected>(
      this IEnumerable<TValue> @this,
      Func<TValue, TSelected> selector)
  {
    HashSet<TSelected> uniques = new();

    foreach (var item in @this)
    {
      if (uniques.Add(selector(item)))
      {
        yield return item;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> Unique<TValue, TSelected>(
      this IAsyncEnumerable<TValue> @this,
      Func<TValue, TSelected> selector)
  {
    HashSet<TSelected> uniques = new();

    await foreach (var item in @this)
    {
      if (uniques.Add(selector(item)))
      {
        yield return item;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> Unique<TValue, TSelected>(
      this IEnumerable<TValue> @this,
      Func<TValue, ValueTask<TSelected>> selector)
  {
    HashSet<TSelected> uniques = new();

    foreach (var item in @this)
    {
      if (uniques.Add(await selector(item)))
      {
        yield return item;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> Unique<TValue, TSelected>(
      this IAsyncEnumerable<TValue> @this,
      Func<TValue, ValueTask<TSelected>> selector)
  {
    HashSet<TSelected> uniques = new();

    await foreach (var item in @this)
    {
      if (uniques.Add(await selector(item)))
      {
        yield return item;
      }
    }
  }
}
