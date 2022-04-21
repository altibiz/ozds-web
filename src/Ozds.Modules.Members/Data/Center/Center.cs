using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public class Center : ContentPart
{
  public UserPickerField User { get; set; } = new();
}

public class CenterSettings : IFieldEditorSettings
{
  public DisplayModeResult GetFieldDisplayMode(string propertyName,
      string defaultMode, BuildFieldEditorContext context,
      bool isAdminTheme)
  {
    return defaultMode;
  }

  public string GetFieldLabel(
      string propertyName, string defaultVale, bool isAdminTheme)
  {
    return defaultVale;
  }
}
