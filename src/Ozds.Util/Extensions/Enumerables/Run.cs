using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Run<TValue>(this IEnumerable<TValue> @this)
  {
    foreach (var _ in @this)
    {
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task Run<TValue>(this IAsyncEnumerable<TValue> @this)
  {
    await foreach (var _ in @this)
    {
    }
  }
}
