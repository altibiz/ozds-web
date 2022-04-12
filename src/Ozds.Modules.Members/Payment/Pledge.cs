using Ozds.Modules.Members.PartFieldSettings;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members.Payments
{
  public class Pledge : ContentPart
  {
    public TextField PayerName { get; set; }
    public TextField Oib { get; set; }
    public TaxonomyField Variant { get; set; }
    public NumericField Amount { get; set; }
    public TextField ReferenceNr { get; set; }
    public ContentPickerField Person { get; set; }

    public TextField Email { get; set; }

    public TextField Note { get; set; }
  }

  public class PledgeSettings : IFieldEditorSettings
  {
    public DisplayModeResult GetFieldDisplayMode(string propertyName,
        string defaultMode, BuildFieldEditorContext context,
        bool isAdminTheme)
    {
      if (isAdminTheme)
        return defaultMode;
      if (propertyName == nameof(Pledge.Person) ||
          propertyName == nameof(Pledge.ReferenceNr) ||
          propertyName == nameof(Pledge.Amount) ||
          propertyName == nameof(Pledge.Note))
        return false;
      return defaultMode;
    }

    public string GetFieldLabel(
        string propertyName, string defaultVale, bool isAdminTheme)
    {
      return defaultVale;
    }
  }

  public class PledgeVariant : ContentPart
  {
    public NumericField Price { get; set; }
  }
}
