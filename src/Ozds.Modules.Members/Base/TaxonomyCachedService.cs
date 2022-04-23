using OrchardCore;
using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class TaxonomyCacheService
{
  public Task<IEnumerable<ContentItem>> GetTerms(TaxonomyField? field) =>
    field
      .WhenNonNullable(field => field.TermContentItemIds
        .SelectFilterTask(term => (id: field.TaxonomyContentItemId, term)
          .WhenFinallyTask(
            key => !Cache.ContainsKey(key),
            key => Helper
              .GetTaxonomyTermAsync(key.id, key.term)
              .Then(item => Cache[key] = item)
            ))
        .Await(),
      Enumerable.Empty<ContentItem>().ToTask());

  public Task<ContentItem?> GetTerm(TaxonomyField? field) =>
    GetTerms(field)
      .Then(terms => terms
          .FirstOrDefault());

  public Task<IEnumerable<T>> GetTerms<T>(
      TaxonomyField? field) where T : ContentPart =>
    GetTerms(field)
      .Then(terms => terms
        .SelectFilter(term => ContentItemExtensions
          .As<T>(term)));

  public Task<T?> GetTerm<T>(
      TaxonomyField? field) where T : ContentPart =>
    GetTerm(field)
      .ThenWhen(term => ContentItemExtensions
        .As<T>(term));

  public TaxonomyCacheService(IOrchardHelper helper)
  {
    Helper = helper;
  }

  private IOrchardHelper Helper { get; }

  private Dictionary<(string, string), ContentItem> Cache { get; } = new();
}
