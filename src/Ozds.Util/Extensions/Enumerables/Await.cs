using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IEnumerable<T>> Await<T>(
      this IAsyncEnumerable<T> @this)
  {
    var result = new List<T>();
    await foreach (var current in @this)
    {
      result.Add(current);
    }
    return result;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IEnumerable<T>> Await<T>(
      this IEnumerable<Task<T>> @this)
  {
    var result = new List<T>();
    foreach (var current in @this)
    {
      result.Add(await current);
    }
    return result;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IEnumerable<T>> Await<T>(
      this IEnumerable<ValueTask<T>> @this)
  {
    var result = new List<T>();
    foreach (var current in @this)
    {
      result.Add(await current);
    }
    return result;
  }
}
