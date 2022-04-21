using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Spatial.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class Site : ContentPart
{
  public TextField DeviceId { get; set; } = new();
  public TaxonomyField Type { get; set; } = new();
  public NumericField Coefficient { get; set; } = new();
  public TaxonomyField Phase { get; set; } = new();
  public GeoPointField Geolocation { get; set; } = new();
  public BooleanField Active { get; set; } = new();

  public bool Primary
  {
    get => SiteType.PrimaryId.In(Type.TermContentItemIds);
  }
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
