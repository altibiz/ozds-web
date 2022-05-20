using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using Etch.OrchardCore.Fields.Dictionary.Fields;

namespace Ozds.Modules.Ozds;

public class Site : ContentPart
{
  public TaxonomyField Source { get; set; } = new();
  public TextField DeviceId { get; set; } = new();
  public DictionaryField SourceData { get; set; } = new();
  public NumericField MeasurementFrequency { get; set; } = new();
  public TaxonomyField Status { get; set; } = new();
}
