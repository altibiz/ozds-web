using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class Receipt : ContentPart
{
  public DateField Date { get; set; } = new();
  public NumericField InTotal { get; set; } = new();
  public NumericField Tax { get; set; } = new();
  public NumericField InTotalWithTax { get; set; } = new();
}
