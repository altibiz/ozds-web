using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class FastImportQueue
{
  public void Push(IEnumerable<ContentItem> items) =>
    Queue
      .Enqueue(items
        .ToArray());

  public IEnumerable<ContentItem> Pull() =>
    Queue
      .TryTake()
      .WhenNull(Enumerable
        .Empty<ContentItem>());

  public FastImportQueue(
      ILogger<FastImportQueue> log)
  {
    Log = log;
  }

  ILogger Log { get; }

  private ConcurrentQueue<ContentItem[]> Queue { get; } = new();
}
