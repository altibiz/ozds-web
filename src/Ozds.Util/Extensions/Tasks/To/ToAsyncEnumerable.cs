using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsyncEnumerableNonNullable<T>(
      this Task<T> @this)
  {
    yield return await @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsyncEnumerableNonNullable<T>(
      this ValueTask<T> @this)
  {
    yield return await @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(
      this Task<T?> @this)
  {
    if (await @this is T awaited)
    {
      yield return awaited;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(
      this ValueTask<T?> @this)
  {
    if (await @this is T awaited)
    {
      yield return awaited;
    }
  }
}
