using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;
using Newtonsoft.Json;
using Ozds.Util;

namespace Ozds.Modules.Ozds;

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
          TariffModelTermId = TariffModel.TermContentItemIds.First(),
          Items = ContentItem
            .AsContent<CatalogueType>()
            .WhenNonNullable(catalogue =>
              catalogue.Items.Value.ContentItems
                .Select(item =>
                  item.AsContent<CatalogueItemType>()
                    !.CatalogueItem.Value.Data.Value),
              Enumerable.Empty<CatalogueItemData>())
        });
  }
}

public record struct CatalogueData
(string TariffModelTermId,
 IEnumerable<CatalogueItemData> Items);
