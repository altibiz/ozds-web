using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TValue> Run<TValue>(
      this IEnumerable<TValue> @this)
  {
    foreach (var _ in @this)
    {
    }

    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<IAsyncEnumerable<TValue>> Run<TValue>(
      this IAsyncEnumerable<TValue> @this)
  {
    await foreach (var _ in @this)
    {
    }

    return @this;
  }
}
