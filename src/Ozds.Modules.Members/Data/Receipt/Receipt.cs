using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public class Receipt : ContentPart
{
  public TextField ProjectId { get; set; } = new();
  public ContentPickerField Official { get; set; } = new();
  public ContentPickerField Contract { get; set; } = new();
  public DateField DateFrom { get; set; } = new();
  public DateField DateTo { get; set; } = new();
  public DateField Date { get; set; } = new();
  public DateField DeliveryDate { get; set; } = new();
  public TaxonomyField PaymentCurrency { get; set; } = new();
  public NumericField InTotal { get; set; } = new();
  public NumericField InTotalWithTax { get; set; } = new();
  public TextField Remark { get; set; } = new();
}

public class ReceiptSettings : IFieldEditorSettings
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
