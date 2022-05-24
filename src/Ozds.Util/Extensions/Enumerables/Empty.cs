using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Empty<T>() =>
    Enumerable.Empty<T>();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IAsyncEnumerable<T> EmptyAsync<T>() =>
    Empty<T>().ToAsync();
}
