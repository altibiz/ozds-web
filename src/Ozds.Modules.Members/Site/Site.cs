using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Spatial.Fields;

namespace Ozds.Modules.Members;

public class Site : ContentPart
{
  public TextField Code { get; set; } = new();
  public TextField Type { get; set; } = new();
  public TextField ZDSCode { get; set; } = new();
  public TextField DeviceCode { get; set; } = new();
  public TextField UserCode { get; set; } = new();
  public TextField Coefficient { get; set; } = new();
  public NumericField PhaseNumber { get; set; } = new();
  public GeoPointField Geolocation { get; set; } = new();
  public BooleanField Active { get; set; } = new();
  public TextField UserType { get; set; } = new();
  public BooleanField Primary { get; set; } = new();
}

public class SiteSettings : IFieldEditorSettings
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
