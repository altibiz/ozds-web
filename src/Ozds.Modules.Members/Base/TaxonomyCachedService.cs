using OrchardCore;
using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members.Base
{
  public class TaxonomyCachedService
  {
    public Task<IEnumerable<ContentItem>> GetTaxonomyTerms(TaxonomyField? field) =>
      field
        .When(field => field.TermContentItemIds
          .SelectFilter(term =>
            (id: field.TaxonomyContentItemId, term: term)
              .Named(key => key
                .FinallyWhen(
                  key => !Cache.ContainsKey(key),
                  key => Helper
                    .GetTaxonomyTermAsync(key.id, key.term)
                    .Then(item => Cache[key] = item)
                  )))
          .Await(),
        new List<ContentItem> { }.AsEnumerable().ToTask());

    public Task<ContentItem?> GetFirstTerm(TaxonomyField? field) =>
      GetTaxonomyTerms(field).Then(items => items.FirstOrDefault());

    public TaxonomyCachedService(IOrchardHelper helper)
    {
      Helper = helper;
    }

    private IOrchardHelper Helper { get; }

    private Dictionary<(string, string), ContentItem> Cache { get; } = new();
  }
}
