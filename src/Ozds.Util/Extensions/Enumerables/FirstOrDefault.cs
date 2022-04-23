using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> FirstOrDefault<T>(
      this IAsyncEnumerable<T> @this)
  {
    await foreach (var item in @this)
    {
      return item;
    }

    return default;
  }
}
