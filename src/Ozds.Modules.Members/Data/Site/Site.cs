using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class Site : ContentPart
{
  public TaxonomyField Source { get; set; } = new();
  public TextField DeviceId { get; set; } = new();
  public TaxonomyField Status { get; set; } = new();
}
