using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class ReceiptItem : ContentPart
{
  public TextField OrdinalNumber { get; set; } = new();
  public TaxonomyField Article { get; set; } = new();
  public NumericField Amount { get; set; } = new();
  public NumericField Price { get; set; } = new();
  public NumericField InTotal { get; set; } = new();
}
