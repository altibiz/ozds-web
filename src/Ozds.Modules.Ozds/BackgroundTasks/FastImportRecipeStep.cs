using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Recipes.Models;
using OrchardCore.Recipes.Services;

namespace Ozds.Modules.Ozds;

public class FastImportRecipeStep : IRecipeStepHandler
{
  private record StepModel
  (JArray Data);

  public Task ExecuteAsync(RecipeExecutionContext? context) =>
    Task.Run(() =>
      {
        if (context is null ||
            !string.Equals(
            context.Name, "fastimport",
            StringComparison.OrdinalIgnoreCase))
        {
          return;
        }

        var items = context.Step
          .ToObject<StepModel>()
          ?.Data.ToObject<ContentItem[]>();
        if (items is null)
        {
          throw new InvalidOperationException(
            "'fastimport' step requires a 'data' argument");
        }

        Importer.Enqueue(items);
      });

  public FastImportRecipeStep(
      ILogger<FastImportRecipeStep> log,

      FastImporter importer)
  {
    Log = log;

    Importer = importer;
  }

  ILogger Log { get; }

  FastImporter Importer { get; }
}
