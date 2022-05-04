using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class Consumer : ContentPart
{
  public ContentPickerField SecondarySites { get; set; } = new();
}
