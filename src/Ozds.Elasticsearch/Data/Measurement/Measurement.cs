using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch;

[ElasticsearchType(RelationName = "measurement", IdProperty = nameof(Id))]
public class Measurement
{
  public static string MakeId(
      string deviceId,
      DateTime measurementTimestamp) =>
    Strings.CombineIntoStringId(
      "D",
      deviceId,
      "TS",
      measurementTimestamp.ToUtcIsoString());

  public Measurement(
      DateTime timestamp,
      DeviceDataType device,
      MeasurementDataType measurement,
      GeoCoordinate? geo = null)
  {
    Timestamp = timestamp;
    Geo = geo;
    DeviceData = device;
    MeasurementData = measurement ?? new MeasurementDataType { };
    Id = MakeId(DeviceData.DeviceId, Timestamp);
  }

  public string Id { get; init; }

  [Date(Name = "timestamp")]
  public DateTime Timestamp { get; init; }

  [GeoPoint(Name = "geo")]
  public GeoCoordinate? Geo { get; init; } = null;

  [Object(Name = "device")]
  public DeviceDataType DeviceData { get; init; }

  [Object(Name = "measurement")]
  public MeasurementDataType MeasurementData { get; init; } =
    new MeasurementDataType { };

  [ElasticsearchType(RelationName = "measurementDeviceData")]
  public class DeviceDataType
  {
    public DeviceDataType(
      string source,
      string deviceId,
      string @operator,
      string centerId,
      string? centerUserId,
      string ownerId,
      string? ownerUserId)
    {
      Source = source;
      DeviceId = deviceId;
      Operator = @operator;
      CenterId = centerId;
      CenterUserId = CenterUserId;
      OwnerId = ownerId;
      OwnerUserId = ownerUserId;
    }

    [Keyword(Name = "source")]
    public string Source { get; init; }

    [Keyword(Name = "deviceId")]
    public string DeviceId { get; init; }

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

  // TODO: formalize
  [ElasticsearchType(RelationName = "measurementMeasurementData")]
  public class MeasurementDataType
  {
    public decimal energyIn { get; init; } = default!;
    public decimal energyIn_T1 { get; init; } = default!;
    public decimal energyIn_T2 { get; init; } = default!;
    public decimal powerIn { get; init; } = default!;

    public string? dongleId { get; init; } = default!;
    public string? meterIdent { get; init; } = default!;
    public string? meterSerial { get; init; } = default!;
    public string? localTime { get; init; } = default!;
    public string? localDate { get; init; } = default!;
    public int? tariff { get; init; } = default!;
    public int? limiter { get; init; } = default!;
    public int? fuseSupervisionL1 { get; init; } = default!;
    public int? disconnectControl { get; init; } = default!;
    public int? numLongPwrFailures { get; init; } = default!;
    public int? numPwrFailures { get; init; } = default!;
    public int? numVoltageSagsL1 { get; init; } = default!;
    public int? numVoltageSagsL2 { get; init; } = default!;
    public int? numVoltageSagsL3 { get; init; } = default!;
    public int? numVoltageSwellsL1 { get; init; } = default!;
    public int? numVoltageSwellsL2 { get; init; } = default!;
    public int? numVoltageSwellsL3 { get; init; } = default!;
    public decimal? currentL1 { get; init; } = default!;
    public decimal? currentL2 { get; init; } = default!;
    public decimal? currentL3 { get; init; } = default!;
    public decimal? energyOut { get; init; } = default!;
    public decimal? energyOut_T1 { get; init; } = default!;
    public decimal? energyOut_T2 { get; init; } = default!;
    public decimal? powerInL1 { get; init; } = default!;
    public decimal? powerInL2 { get; init; } = default!;
    public decimal? powerInL3 { get; init; } = default!;
    public decimal? powerOut { get; init; } = default!;
    public decimal? powerOutL1 { get; init; } = default!;
    public decimal? powerOutL2 { get; init; } = default!;
    public decimal? powerOutL3 { get; init; } = default!;
    public decimal? voltageL1 { get; init; } = default!;
    public decimal? voltageL2 { get; init; } = default!;
    public decimal? voltageL3 { get; init; } = default!;
  };

  public override bool Equals(object? obj) =>
    Equals(obj as Measurement);

  public bool Equals(Measurement? other) =>
    other is not null &&
    Id == other.Id;

  public override int GetHashCode() =>
    Id.GetHashCode();

  public void Deconstruct(
    out string id,
    out DateTime timestamp,
    out GeoCoordinate? geo,
    out DeviceDataType device,
    out MeasurementDataType measurement)
  {
    id = Id;
    timestamp = Timestamp;
    geo = Geo;
    device = DeviceData;
    measurement = MeasurementData;
  }
};
