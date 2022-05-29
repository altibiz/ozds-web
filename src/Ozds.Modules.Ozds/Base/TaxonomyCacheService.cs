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
      .SelectFilter(term => term.AsContent<T>());

  public Task<T?> GetTerm<T>(
      TaxonomyField field) where T : ContentTypeBase =>
    GetTerm(field).ThenWhenNonNullable(term => term.AsContent<T>());

  public Task<T?> GetTerm<T>(
      string contentItemId, string termId) where T : ContentTypeBase =>
    GetTerm(contentItemId, termId)
      .ThenWhenNonNullable(term =>
        term.AsContent<T>());

  public IAsyncEnumerable<ContentItem> GetTerms(
      TaxonomyField field) =>
    field.TermContentItemIds
      .SelectFilterTask(termId =>
        GetTerm(field.TaxonomyContentItemId, termId));

  public Task<ContentItem?> GetTerm(
      TaxonomyField field) =>
    field.TermContentItemIds
      .FirstOrDefault()
      .WhenNonNullable(
        termId => GetTerm(field.TaxonomyContentItemId, termId),
        Task.FromResult(null as ContentItem));

  public Task<ContentItem?> GetTerm(
      string contentItemId, string termId) =>
    (id: contentItemId, term: termId)
      .When(
        key => !Cache.ContainsKey(key),
        key => Helper
          .GetTaxonomyTermAsync(key.id, key.term).Nullable()
          .Then(item => item
            .WhenNonNullable(item => Cache[key] = item)),
        () => Task.FromResult(
          Cache.GetOrDefault((contentItemId, termId))));

  public TaxonomyCacheService(IOrchardHelper helper)
  {
    Helper = helper;
  }

  private IOrchardHelper Helper { get; }

  private Dictionary<(string, string), ContentItem> Cache { get; } = new();
}
