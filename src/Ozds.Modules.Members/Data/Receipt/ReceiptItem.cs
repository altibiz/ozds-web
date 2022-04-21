using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public class ReceiptItem : ContentPart
{
  public TextField OrdinalNumber { get; set; } = new();
  public TaxonomyField Article { get; set; } = new();
  public TaxonomyField Unit { get; set; } = new();
  public NumericField Amount { get; set; } = new();
  public NumericField Price { get; set; } = new();
  public NumericField InTotal { get; set; } = new();
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
