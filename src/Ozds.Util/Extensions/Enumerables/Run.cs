using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object Run<TValue>(
      this IEnumerable<TValue> @this)
  {
    foreach (var _ in @this)
    {
    }

    return new { };
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<object> Run<TValue>(
      this IAsyncEnumerable<TValue> @this)
  {
    await foreach (var _ in @this)
    {
    }

    return new { };
  }
}