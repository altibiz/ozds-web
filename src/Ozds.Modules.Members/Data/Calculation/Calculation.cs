using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public class Calculation : ContentPart
{
  TextField DeviceId { get; set; } = new();
  DateField DateFrom { get; set; } = new();
  DateField DateTo { get; set; } = new();
  NumericField MeasurementServiceFee { get; set; } = new();
  NumericField InTotal { get; set; } = new();
}

public class CalculationSettings : IFieldEditorSettings
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
