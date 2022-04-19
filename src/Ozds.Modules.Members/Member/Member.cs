using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members.Core
{
  public class Member : ContentPart
  {
    public UserPickerField User { get; set; } = default!;

    public DateField DateOfBirth { get; set; } = default!;

    public TaxonomyField Sex { get; set; } = default!;

    public TextField AdminNotes { get; set; } = default!;
  }

  public class MemberSettings : IFieldEditorSettings
  {
    public DisplayModeResult GetFieldDisplayMode(string propertyName,
        string defaultMode, BuildFieldEditorContext context,
        bool isAdminTheme)
    {
      if (!isAdminTheme && propertyName == nameof(Member.AdminNotes))
        return false;
      return defaultMode;
    }

    public string GetFieldLabel(
        string propertyName, string defaultVale, bool isAdminTheme)
    {
      return defaultVale;
    }
  }

}
