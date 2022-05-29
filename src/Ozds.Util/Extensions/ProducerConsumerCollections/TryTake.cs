using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

namespace Ozds.Extensions;

public static class ProducerConsumerCollections
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? TryTake<T>(
      this IProducerConsumerCollection<T> collection)
  {
    if (collection.TryTake(out var result))
    {
      return result;
    }

    return default;
  }
}
