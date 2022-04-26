using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members;

public class Member : ContentPart
{
  public UserPickerField User { get; set; } = new();
  public TaxonomyField Activity { get; set; } = new();
  public ContentPickerField ConnectionContract { get; set; } = new();
  public ContentPickerField UsageContract { get; set; } = new();
  public TextField Note { get; set; } = new();
  public ContentPickerField SecondarySites { get; set; } = new();
}

public class MemberSettings : IFieldEditorSettings
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
