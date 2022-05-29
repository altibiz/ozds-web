using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsync<T>(
      this IEnumerable<T> @this)
  {
    foreach (var item in @this)
    {
      yield return await Task.FromResult(item);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsync<T>(
      this IEnumerable<Task<T>> @this)
  {
    foreach (var item in @this)
    {
      yield return await item;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsync<T>(
      this IEnumerable<ValueTask<T>> @this)
  {
    foreach (var item in @this)
    {
      yield return await item;
    }
  }
}
