using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public class ReceiptItem : ContentPart
{
}

public class ReceiptItemSettings : IFieldEditorSettings
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
