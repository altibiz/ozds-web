using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class FastImporter
{
  public void Enqueue(IEnumerable<ContentItem> items) =>
    Queue
      .Push(items);

  public Task ImportAsync() =>
    Queue
      .Pull()
      .Split(ImportBatchSize)
      .WithAsync(batches => batches
        .ForEachAwait(batch => ContentManager
          .ImportAsync(batch
            .Unique(item => item.ContentItemVersionId))
          .ToValueTask())
        .Run())
      .ThenWith(batches => Logger
        .LogDebug(
          "Imported: {ImportedCount}",
          batches.Sum(batch => batch.Count())));

  public FastImporter(
      ILogger<DefaultContentManager> logger,

      IContentManager content,

      FastImportQueue queue)
  {
    Logger = logger;

    ContentManager = content;

    Queue = queue;
  }

  private ILogger Logger { get; }

  private IContentManager ContentManager { get; }

  private FastImportQueue Queue { get; }

  private static int ImportBatchSize { get; } = 500;
}
