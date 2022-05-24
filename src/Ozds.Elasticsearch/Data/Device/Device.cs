using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch;

public class DeviceState
{
  public const string Added = "added";
  public const string Active = "healthy";
  public const string TemporarilyInactive = "unhealthy";
  public const string Inactive = "inactive";
  public const string Removed = "removed";
}

[ElasticsearchType(RelationName = "device", IdProperty = nameof(Id))]
public class Device
{
  public static string MakeId(
      string source,
      string sourceDeviceId) =>
    Strings.CombineIntoStringId(
        "S",
        source.Substring(0, 5),
        "ID",
        sourceDeviceId);

  public Device(
      string @operator,
      string centerId,
      string centerUserId,
      string ownerId,
      string ownerUserId,
      string source,
      string sourceDeviceId,
      int measurementIntervalInSeconds,
      DateTime extractionStart,
      int extractionOffsetInSeconds,
      int extractionTimeoutInSeconds,
      int extractionRetries,
      int validationIntervalInSeconds,
      KnownSourceDeviceData? sourceDeviceData = null,
      string? state = null)
  {
    Operator = @operator;
    CenterId = centerId;
    CenterUserId = centerUserId;
    OwnerId = ownerId;
    OwnerUserId = ownerUserId;
    Source = source;
    SourceDeviceId = sourceDeviceId;
    MeasurementIntervalInSeconds = measurementIntervalInSeconds;
    ExtractionStart = extractionStart;
    ExtractionOffsetInSeconds = extractionOffsetInSeconds;
    ExtractionTimeoutInSeconds = extractionTimeoutInSeconds;
    ExtractionRetries = extractionRetries;
    ValidationIntervalInSeconds = validationIntervalInSeconds;
    LastValidation = DateTime.UtcNow;
    SourceDeviceData = sourceDeviceData ?? new KnownSourceDeviceData { };
    State = state ?? DeviceState.Added;
    Id = MakeId(Source, SourceDeviceId);
  }

  public string Id { get; init; }

  [Keyword(Name = "operator")]
  public string Operator { get; init; }

  [Keyword(Name = "centerId")]
  public string CenterId { get; init; }

  [Keyword(Name = "centerUserId")]
  public string CenterUserId { get; init; }

  [Keyword(Name = "ownerId")]
  public string OwnerId { get; init; }

  [Keyword(Name = "ownerUserId")]
  public string OwnerUserId { get; init; }

  [Keyword(Name = "source")]
  public string Source { get; init; }

  [Keyword(Name = "sourceDeviceId")]
  public string SourceDeviceId { get; init; }

  [Object(Name = "sourceDeviceData")]
  public KnownSourceDeviceData SourceDeviceData { get; init; } =
    new KnownSourceDeviceData { };

  [Number(NumberType.Integer, Name = "measurementInterval")]
  public int MeasurementIntervalInSeconds { get; init; }

  [Date(Name = "extractionStart")]
  public DateTime ExtractionStart { get; init; }

  [Number(NumberType.Integer, Name = "extractionRetries")]
  public int ExtractionRetries { get; init; }

  [Number(NumberType.Integer, Name = "extractionOffsetInSeconds")]
  public int ExtractionOffsetInSeconds { get; init; }

  [Number(NumberType.Integer, Name = "extractionTimeoutInSeconds")]
  public int ExtractionTimeoutInSeconds { get; init; }

  [Number(NumberType.Integer, Name = "validationIntervalInSeconds")]
  public int ValidationIntervalInSeconds { get; init; }

  [Date(Name = "lastValidation")]
  public DateTime LastValidation { get; init; }

  [Keyword(Name = "state")]
  public string State { get; init; }

  [Date(Name = "dateAdded")]
  public DateTime DateAdded { get; init; } = DateTime.UtcNow;

  [Date(Name = "dateRemoved")]
  public DateTime? DateRemoved { get; init; } = null;

  [ElasticsearchType(RelationName = "deviceSourceData")]
  public class KnownSourceDeviceData
  {
    [Keyword(Name = "ownerId")]
    public string? OwnerId { get; init; } = default;
  }

  public override bool Equals(object? obj) =>
    Equals(obj as Device);

  public bool Equals(Device? other) =>
    other is not null &&
    Id == other.Id;

  public override int GetHashCode() =>
    Id.GetHashCode();
}
