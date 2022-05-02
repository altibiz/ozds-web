using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public class FieldEditorSettings : IFieldEditorSettings
{
  public DisplayModeResult GetFieldDisplayMode(
      string propertyName,
      string displayMode,
      BuildFieldEditorContext context,
      bool isAdminTheme) =>
    displayMode;

  public string GetFieldLabel(
      string propertyName,
      string displayName,
      bool isAdminTheme) =>
    displayName;
}
