using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using Etch.OrchardCore.Fields.Dictionary.Fields;
using Newtonsoft.Json;
using Ozds.Elasticsearch;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class Site : ContentPart
{
  public TaxonomyField Source { get; init; } = new();
  public TextField SourceDeviceId { get; init; } = new();
  public DictionaryField SourceData { get; init; } = new();

  public TaxonomyField Phase { get; init; } = new();

  public NumericField MeasurementIntervalInSeconds { get; init; } = new();
  public DateTimeField ExtractionStart { get; init; } = new();
  public NumericField ExtractionOffsetInSeconds { get; init; } = new();
  public NumericField ExtractionTimeoutInSeconds { get; init; } = new();
  public NumericField ExtractionRetries { get; init; } = new();
  public NumericField ValidationIntervalInSeconds { get; init; } = new();

  public TaxonomyField Status { get; init; } = new();

  public SiteData Data { get; set; } = default;

  [JsonIgnore]
  public string DeviceId
  {
    get =>
      Device.MakeId(
        SiteMeasurementSource.GetDeviceMeasurementSource(
          Source.TermContentItemIds.First())
        .ThrowWhenNull(),
        SourceDeviceId.Text);
  }
}

public readonly record struct SiteData
(string OperatorName,
 string OperatorOib,

 string CenterContentItemId,
 string? CenterUserId,
 string CenterOwnerName,
 string CenterOwnerOib,

 string OwnerContentItemId,
 string? OwnerUserId,
 string OwnerName,
 string OwnerOib);
