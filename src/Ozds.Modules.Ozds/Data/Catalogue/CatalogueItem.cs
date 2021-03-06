using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using Newtonsoft.Json;

namespace Ozds.Modules.Ozds;

public class CatalogueItemType : ContentTypeBase
{
  public Lazy<TitlePart> TitlePart { get; set; } = default!;
  public Lazy<CatalogueItem> CatalogueItem { get; set; } = default!;

  private CatalogueItemType(ContentItem item) : base(item) { }
}

public class CatalogueItem : ContentPart
{
  public TaxonomyField TariffElement { get; init; } = new();
  public NumericField Price { get; init; } = new();

  [JsonIgnore]
  public Lazy<CatalogueItemData> Data { get; }


  public CatalogueItem()
  {
    Data = new Lazy<CatalogueItemData>(
      () =>
        new CatalogueItemData
        {
          Price = Price.Value ?? 0,
          TariffElementTermId = TariffElement.TermContentItemIds.First()
        });
  }
}

public record struct CatalogueItemData
(string TariffElementTermId,
 decimal Price);
