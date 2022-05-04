using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class Expenditure : ContentPart
{
  public NumericField InTotal { get; set; } = new();
}
