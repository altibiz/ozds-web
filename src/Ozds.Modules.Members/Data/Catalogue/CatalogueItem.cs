using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class CatalogueItem : ContentPart
{
  public TaxonomyField Tariff { get; set; } = new();
  public NumericField Price { get; set; } = new();
}
