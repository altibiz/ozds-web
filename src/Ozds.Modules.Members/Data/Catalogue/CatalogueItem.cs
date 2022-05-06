using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using Newtonsoft.Json;

namespace Ozds.Modules.Members;

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
          Price = this.Price.Value ?? 0,
          TariffElementTermId = this.TariffElement.TermContentItemIds.First()
        });
  }
}

public record struct CatalogueItemData
(string TariffElementTermId,
 decimal Price);
