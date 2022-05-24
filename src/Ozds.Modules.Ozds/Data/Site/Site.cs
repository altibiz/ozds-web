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
  public NumericField MeasurementIntervalInSeconds { get; set; } = new();
  public DateTimeField ExtractionStart { get; set; } = new();
  public NumericField ExtractionOffsetInSeconds { get; set; } = new();
  public NumericField ExtractionTimeoutInSeconds { get; set; } = new();
  public NumericField ExtractionRetries { get; set; } = new();
  public NumericField ValidationIntervalInSeconds { get; set; } = new();
  public TaxonomyField Status { get; set; } = new();
}
