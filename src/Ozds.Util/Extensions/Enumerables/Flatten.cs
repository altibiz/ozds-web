using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Flatten<T>(
      this IEnumerable<IEnumerable<T>> @this)
  {
    foreach (var outer in @this)
    {
      foreach (var inner in outer)
      {
        yield return inner;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> Flatten<T>(
      this IEnumerable<IAsyncEnumerable<T>> @this)
  {
    foreach (var outer in @this)
    {
      await foreach (var inner in outer)
      {
        yield return inner;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> Flatten<T>(
      this IAsyncEnumerable<IEnumerable<T>> @this)
  {
    await foreach (var outer in @this)
    {
      foreach (var inner in outer)
      {
        yield return inner;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> Flatten<T>(
      this IAsyncEnumerable<IAsyncEnumerable<T>> @this)
  {
    await foreach (var outer in @this)
    {
      await foreach (var inner in outer)
      {
        yield return inner;
      }
    }
  }
}
