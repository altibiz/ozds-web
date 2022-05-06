using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;
using Newtonsoft.Json;

namespace Ozds.Modules.Members;

public class CatalogueType : ContentTypeBase
{
  public Lazy<Catalogue> Catalogue { get; init; } = default!;
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<BagPart> Items { get; init; } = default!;

  private CatalogueType(ContentItem contentItem) : base(contentItem) { }
}

public class Catalogue : ContentPart
{
  public TaxonomyField TariffModel { get; set; } = default!;

  [JsonIgnore]
  public Lazy<CatalogueData> Data { get; }

  public Catalogue()
  {
    Data = new Lazy<CatalogueData>(
      () =>
        new CatalogueData
        {
          TariffModelTermId = this.TariffModel.TermContentItemIds.First(),
          Items = this.ContentItem
            .FromBag<CatalogueItem>()!
            .Select(item => item.Data.Value)
        });
  }
}

public record struct CatalogueData
(string TariffModelTermId,
 IEnumerable<CatalogueItemData> Items);
