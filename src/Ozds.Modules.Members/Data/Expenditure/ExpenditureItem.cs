using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class ExpenditureItem : ContentPart
{
  public TaxonomyField TariffItem { get; set; } = new();
  public NumericField ValueFrom { get; set; } = new();
  public NumericField ValueTo { get; set; } = new();
  public NumericField Consumption { get; set; } = new();
  public NumericField UnitPrice { get; set; } = new();
  public NumericField Amount { get; set; } = new();
}
