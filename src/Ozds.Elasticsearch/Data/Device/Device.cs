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
      string source,
      string sourceDeviceId,
      SourceDeviceDataType? sourceDeviceData,
      OwnerDataType owner,
      MeasurementDataType measurement,
      StateDataType? state = null)
  {
    Source = source;
    SourceDeviceId = sourceDeviceId;
    SourceDeviceData = sourceDeviceData ?? new SourceDeviceDataType { };

    OwnerData = owner;
    MeasurementData = measurement;

    StateData = state ?? new StateDataType(DeviceState.Added);

    Id = MakeId(Source, SourceDeviceId);
  }

  public string Id { get; init; }

  [Keyword(Name = "source")]
  public string Source { get; init; }

  [Keyword(Name = "sourceDeviceId")]
  public string SourceDeviceId { get; init; }

  [Object(Name = "sourceDeviceData")]
  public SourceDeviceDataType SourceDeviceData { get; init; } =
    new SourceDeviceDataType { };

  [Object(Name = "owner")]
  public OwnerDataType OwnerData { get; init; }

  [Object(Name = "measurement")]
  public MeasurementDataType MeasurementData { get; init; }

  [Object(Name = "state")]
  public StateDataType StateData { get; init; }

  [ElasticsearchType(RelationName = "deviceSourceDeviceData")]
  public class SourceDeviceDataType
  {
    [Keyword(Name = "ownerId")]
    public string? OwnerId { get; init; } = default;
  }

  [ElasticsearchType(RelationName = "deviceOwnerData")]
  public class OwnerDataType
  {
    public OwnerDataType(
      string @operator,
      string centerId,
      string? centerUserId,
      string ownerId,
      string? ownerUserId)
    {
      Operator = @operator;
      CenterId = centerId;
      CenterUserId = CenterUserId;
      OwnerId = ownerId;
      OwnerUserId = ownerUserId;
    }

    [Keyword(Name = "operator")]
    public string Operator { get; init; }

    [Keyword(Name = "centerId")]
    public string CenterId { get; init; }

    [Keyword(Name = "centerUserId")]
    public string? CenterUserId { get; init; }

    [Keyword(Name = "ownerId")]
    public string OwnerId { get; init; }

    [Keyword(Name = "ownerUserId")]
    public string? OwnerUserId { get; init; }
  }

  [ElasticsearchType(RelationName = "deviceMeasurementData")]
  public class MeasurementDataType
  {
    public MeasurementDataType(
        int measurementIntervalInSeconds,
        DateTime extractionStart,
        int extractionRetries,
        int extractionOffsetInSeconds,
        int extractionTimeoutInSeconds,
        int validationIntervalInSeconds,
        DateTime? lastValidation = null)
    {
      MeasurementIntervalInSeconds = measurementIntervalInSeconds;
      ExtractionStart = extractionStart;
      ExtractionRetries = extractionRetries;
      ExtractionOffsetInSeconds = extractionOffsetInSeconds;
      ExtractionTimeoutInSeconds = extractionTimeoutInSeconds;
      ValidationIntervalInSeconds = validationIntervalInSeconds;
      LastValidation = lastValidation ?? DateTime.UtcNow;
    }

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
  }

  [ElasticsearchType(RelationName = "deviceStateData")]
  public class StateDataType
  {
    public StateDataType(
        string? state = null,
        DateTime? dateAdded = null,
        DateTime? dateRemoved = null)
    {
      State = state ?? DeviceState.Added;
      DateAdded = dateAdded ?? DateTime.UtcNow;
      DateRemoved = dateRemoved;
    }

    [Keyword(Name = "state")]
    public string State { get; init; }

    [Date(Name = "dateAdded")]
    public DateTime DateAdded { get; init; } = DateTime.UtcNow;

    [Date(Name = "dateRemoved")]
    public DateTime? DateRemoved { get; init; } = null;
  }

  public override bool Equals(object? obj) =>
    Equals(obj as Device);

  public bool Equals(Device? other) =>
    other is not null &&
    Id == other.Id;

  public override int GetHashCode() =>
    Id.GetHashCode();
}
