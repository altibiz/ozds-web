using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public static class DeviceState
{
  public const string Added = "added";
  public const string Active = "healthy";
  public const string TemporarilyInactive = "unhealthy";
  public const string Inactive = "inactive";
  public const string Removed = "removed";
}

public static class DevicePhase
{
  public const string L1 = "L1";
  public const string L2 = "L2";
  public const string L3 = "L3";
  public const string Triphasic = "triphasic";
}

[ElasticsearchType(RelationName = "device", IdProperty = nameof(Id))]
public class Device : IEquatable<Device>, ICloneable
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
      string phase,
      SourceDeviceDataType? sourceDeviceData,
      OwnerDataType owner,
      MeasurementDataType measurement,
      StateDataType? state = null)
  {
    Source = source;
    SourceDeviceId = sourceDeviceId;
    SourceDeviceData = sourceDeviceData ?? new SourceDeviceDataType { };

    Phase = phase;

    OwnerData = owner;
    MeasurementData = measurement;

    StateData = state ?? new StateDataType(DeviceState.Added);
  }

  [Ignore]
  public string Id
  {
    get =>
      MakeId(
        Source,
        SourceDeviceId);
  }

  [Keyword(Name = "source")]
  public string Source { get; init; }

  [Keyword(Name = "sourceDeviceId")]
  public string SourceDeviceId { get; init; }

  [Object(Name = "sourceDeviceData")]
  public SourceDeviceDataType SourceDeviceData { get; init; } =
    new SourceDeviceDataType { };

  [Keyword(Name = "phase")]
  public string Phase { get; init; }

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
      CenterUserId = centerUserId;
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
        int extractionOffsetInSeconds,
        int extractionRetries,
        int extractionTimeoutInSeconds,
        int validationIntervalInSeconds)
    {
      MeasurementIntervalInSeconds = measurementIntervalInSeconds;
      ExtractionStart = extractionStart;
      ExtractionOffsetInSeconds = extractionOffsetInSeconds;
      ExtractionRetries = extractionRetries;
      ExtractionTimeoutInSeconds = extractionTimeoutInSeconds;
      ValidationIntervalInSeconds = validationIntervalInSeconds;
    }

    [Number(NumberType.Integer, Name = "measurementInterval")]
    public int MeasurementIntervalInSeconds { get; init; }

    [Date(Name = "extractionStart")]
    public DateTime ExtractionStart { get; init; }

    [Number(NumberType.Integer, Name = "extractionOffsetInSeconds")]
    public int ExtractionOffsetInSeconds { get; init; }

    [Number(NumberType.Integer, Name = "extractionRetries")]
    public int ExtractionRetries { get; init; }

    [Number(NumberType.Integer, Name = "extractionTimeoutInSeconds")]
    public int ExtractionTimeoutInSeconds { get; init; }

    [Number(NumberType.Integer, Name = "validationIntervalInSeconds")]
    public int ValidationIntervalInSeconds { get; init; }
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

  public object Clone() => CloneDevice();

  public Device CloneDevice() =>
    new Device(
      source: Source.CloneString(),
      sourceDeviceId: SourceDeviceId.CloneString(),
      sourceDeviceData:
        new SourceDeviceDataType
        {
          OwnerId = SourceDeviceData.OwnerId?.CloneString()
        },
      phase: Phase,
      owner:
        new OwnerDataType(
          @operator: OwnerData.Operator.CloneString(),
          centerId: OwnerData.CenterId.CloneString(),
          centerUserId: OwnerData.CenterUserId?.CloneString(),
          ownerId: OwnerData.OwnerId.CloneString(),
          ownerUserId: OwnerData.OwnerUserId?.CloneString()),
      measurement:
        new MeasurementDataType(
          measurementIntervalInSeconds:
            MeasurementData.MeasurementIntervalInSeconds,
          extractionStart:
            MeasurementData.ExtractionStart,
          extractionOffsetInSeconds:
            MeasurementData.ExtractionOffsetInSeconds,
          extractionRetries:
            MeasurementData.ExtractionRetries,
          extractionTimeoutInSeconds:
            MeasurementData.ExtractionTimeoutInSeconds,
          validationIntervalInSeconds:
            MeasurementData.ValidationIntervalInSeconds),
      state:
        new StateDataType(
          state: StateData.State.CloneString(),
          dateAdded: StateData.DateAdded,
          dateRemoved: StateData.DateRemoved));
}
