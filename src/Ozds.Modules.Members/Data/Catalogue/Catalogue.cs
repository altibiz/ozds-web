using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;
using Newtonsoft.Json;

namespace Ozds.Modules.Members;

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
