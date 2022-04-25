using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members;

public class Location : ContentPart
{
  public TextField Address { get; set; } = new();
  public TextField City { get; set; } = new();
  public TextField PostalCode { get; set; } = new();
  public TaxonomyField County { get; set; } = new();
}

public class LocationSettings : IFieldEditorSettings
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
