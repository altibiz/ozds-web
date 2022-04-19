using System.Collections.Concurrent;
namespace Ozds.Modules.Members;

public static class IProducerConsumerCollection
{
  public static T? TryTake<T>(this IProducerConsumerCollection<T> collection)
  {
    if (collection.TryTake(out var result))
    {
      return result;
    }

    return default;
  }
}
