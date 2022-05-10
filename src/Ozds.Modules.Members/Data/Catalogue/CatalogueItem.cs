using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using Newtonsoft.Json;

namespace Ozds.Modules.Members;

public class CatalogueItemType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; set; } = default!;
  public Lazy<CatalogueItem> CatalogueItem { get; set; } = default!;

  private CatalogueItemType(ContentItem item) : base(item) { }
}

public class CatalogueItem : ContentPart
{
  public TaxonomyField TariffElement { get; set; } = new();
  public NumericField Price { get; set; } = new();

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
