using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members;

public class Catalogue : ContentPart
{
  public TaxonomyField TariffModel { get; set; } = default!;
}
