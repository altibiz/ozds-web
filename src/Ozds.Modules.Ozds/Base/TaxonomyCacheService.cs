using OrchardCore;
using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class TaxonomyCacheService
{
  public IAsyncEnumerable<T> GetTerms<T>(
      TaxonomyField field) where T : ContentTypeBase =>
    GetTerms(field)
      .SelectFilterAsync(term => term.AsContent<T>());

  public Task<T?> GetTerm<T>(
      TaxonomyField field) where T : ContentTypeBase =>
    GetTerm(field).ThenWhenNonNull(term => term.AsContent<T>());

  public Task<T?> GetTerm<T>(
      string contentItemId, string termId) where T : ContentTypeBase =>
    GetTerm(contentItemId, termId)
      .ThenWhenNonNull(term =>
        term.AsContent<T>());

  public IAsyncEnumerable<ContentItem> GetTerms(
      TaxonomyField field) =>
    field.TermContentItemIds
      .SelectFilterAwaitAsync(async termId =>
          await GetTerm(field.TaxonomyContentItemId, termId));

  public Task<ContentItem?> GetTerm(
      TaxonomyField field) =>
    field.TermContentItemIds
      .FirstOrDefault()
      .WhenNonNull(
        termId => GetTerm(field.TaxonomyContentItemId, termId),
        Task.FromResult(null as ContentItem));

  public async Task<ContentItem?> GetTerm(
      string contentItemId, string termId) =>
    Cache.ContainsKey((contentItemId, termId)) ?
      Cache.GetOrDefault((contentItemId, termId))
    : await Helper
        .GetTaxonomyTermAsync(contentItemId, termId).Nullable()
        .Then(item => item
          .WhenNonNull(item => Cache[(contentItemId, termId)] = item));

  public TaxonomyCacheService(IOrchardHelper helper)
  {
    Helper = helper;
  }

  private IOrchardHelper Helper { get; }

  private IDictionary<(string, string), ContentItem> Cache { get; } =
    new Dictionary<(string, string), ContentItem>();
}
