using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OrchardCore.BackgroundTasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Recipes.Models;
using OrchardCore.Recipes.Services;
using System.Collections.Concurrent;
using YesSql;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class FastImport : IRecipeStepHandler
{
  private record StepModel(JArray Data);

  public Task ExecuteAsync(RecipeExecutionContext? context) =>
    context
      .When(context => string.Equals(
        context.Name, "fastimport", StringComparison.OrdinalIgnoreCase),
        context =>
          context.Step
            .ToObject<StepModel>()?.Data
            .ToObject<ContentItem[]>()
            .With(contentItems => FastImportBackgroundTask.PendingImports
              .Enqueue(contentItems)))
      .Return(Task.CompletedTask);
}

public class Importer
{
  public Task ImportAsync(IEnumerable<ContentItem> contentItems) =>
    (new HashSet<string>())
      .WithTask(
        importedIds =>
          contentItems
            .Split(ImportBatchSize)
            .ForEachAsync(
              batchedItems =>
                batchedItems
                  .ForEachAwait(async item =>
                    {
                      if (importedIds.Contains(item.ContentItemVersionId))
                      {
                        return;
                      }

                      importedIds.Add(item.ContentItemVersionId);
                      await Handlers
                        .InvokeAsync(
                          (handler, context) =>
                            handler.ImportingAsync(context),
                          new ImportContentContext(item),
                          Logger)
                        .After(() => Session.Save(item));
                    })
                  .Await()
                  .After(() =>
                    {
                      Logger.LogDebug("Imported: " + importedIds.Count);
                      return Session.SaveChangesAsync();
                    })));

  public Importer(ISession session, ILogger<DefaultContentManager> logger,
      IEnumerable<IContentHandler> handlers)
  {
    Logger = logger;

    Session = session;

    Handlers = handlers.ToList();
    ReversedHandlers = handlers.Reverse().ToList();
  }

  private ILogger<DefaultContentManager> Logger { get; }

  private ISession Session { get; }

  private IEnumerable<IContentHandler> Handlers { get; }
  private IEnumerable<IContentHandler> ReversedHandlers { get; }

  private static int ImportBatchSize { get; } = 500;
}

[BackgroundTask(
    Schedule = "*/1 * * * *",
    Description = "Fast import background task.")]
public class FastImportBackgroundTask : IBackgroundTask
{
  public Task DoWorkAsync(
      IServiceProvider services,
      CancellationToken token) =>
    PendingImports
      .TryTake()
      .WithTask(imports => services
          .GetRequiredService<Importer>()
          .ImportAsync(imports));

  public static ConcurrentQueue<ContentItem[]> PendingImports { get; } = new();
}
