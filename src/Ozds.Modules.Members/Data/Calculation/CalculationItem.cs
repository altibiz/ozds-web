using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public class CalculationItem : ContentPart
{
  TaxonomyField Tariff { get; set; } = new();
  NumericField ValueFrom { get; set; } = new();
  NumericField ValueTo { get; set; } = new();
  TaxonomyField Status { get; set; } = new();
  NumericField Constant { get; set; } = new();
  NumericField Consumption { get; set; } = new();
  NumericField UnitPrice { get; set; } = new();
  NumericField Amount { get; set; } = new();
}

public class CalculationItemSettings : IFieldEditorSettings
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
