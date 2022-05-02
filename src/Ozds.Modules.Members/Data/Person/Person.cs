using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public class Person : ContentPart
{
  public TextField Oib { get; set; } = new();
  public TextField Name { get; set; } = new();
  public TextField Address { get; set; } = new();
  public TextField City { get; set; } = new();
  public TextField PostalCode { get; set; } = new();
  public TextField Contact { get; set; } = new();
}

public class PersonSettings : IFieldEditorSettings
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
