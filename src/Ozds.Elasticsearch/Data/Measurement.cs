using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch;

[ElasticsearchType(RelationName = "measurement", IdProperty = nameof(Id))]
public class Measurement
{
  public static string MakeId(
      string deviceId, DateTime measurementTimestamp)
  {
    return Strings.CombineIntoStringId(
        "D", deviceId, "TS", measurementTimestamp.ToUtcIsoString());
  }

  public Measurement(DateTime measurementTimestamp,
      GeoCoordinate? geoCoordinate, string source, string deviceId,
      KnownData? data = null)
  {
    MeasurementTimestamp = measurementTimestamp;
    GeoCoordinate = geoCoordinate;
    Source = source;
    DeviceId = deviceId;
    Data = data ?? new KnownData { };
    Id = MakeId(DeviceId, MeasurementTimestamp);
  }

  public string Id { get; init; }

  [Date(Name = "measurementTimestamp")]
  public DateTime MeasurementTimestamp { get; init; }

  [GeoPoint(Name = "geo")]
  public GeoCoordinate? GeoCoordinate { get; init; } = null;

  [Keyword(Name = "source")]
  public string Source { get; init; }

  [Keyword(Name = "deviceId")]
  public string DeviceId { get; init; }

  [Object(Name = "data")]
  public KnownData Data { get; init; } = new KnownData { };

  public override bool Equals(object? obj)
  {
    return Equals(obj as Measurement);
  }

  public bool Equals(Measurement? other)
  {
    return other != null && DeviceId == other.DeviceId &&
           MeasurementTimestamp == other.MeasurementTimestamp &&
           Source == other.Source;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(DeviceId, MeasurementTimestamp, Source);
  }

  [ElasticsearchType(RelationName = "measurementData")]
  public class KnownData
  {
    public decimal energyIn { get; init; } = default;
    public decimal energyIn_T1 { get; init; } = default;
    public decimal energyIn_T2 { get; init; } = default;
    public decimal powerIn { get; init; } = default;

    public string? dongleId { get; init; } = default;
    public string? meterIdent { get; init; } = default;
    public string? meterSerial { get; init; } = default;
    public string? localTime { get; init; } = default;
    public string? localDate { get; init; } = default;
    public int? tariff { get; init; } = default;
    public int? limiter { get; init; } = default;
    public int? fuseSupervisionL1 { get; init; } = default;
    public int? disconnectControl { get; init; } = default;
    public int? numLongPwrFailures { get; init; } = default;
    public int? numPwrFailures { get; init; } = default;
    public int? numVoltageSagsL1 { get; init; } = default;
    public int? numVoltageSagsL2 { get; init; } = default;
    public int? numVoltageSagsL3 { get; init; } = default;
    public int? numVoltageSwellsL1 { get; init; } = default;
    public int? numVoltageSwellsL2 { get; init; } = default;
    public int? numVoltageSwellsL3 { get; init; } = default;
    public decimal? currentL1 { get; init; } = default;
    public decimal? currentL2 { get; init; } = default;
    public decimal? currentL3 { get; init; } = default;
    public decimal? energyOut { get; init; } = default;
    public decimal? energyOut_T1 { get; init; } = default;
    public decimal? energyOut_T2 { get; init; } = default;
    public decimal? powerInL1 { get; init; } = default;
    public decimal? powerInL2 { get; init; } = default;
    public decimal? powerInL3 { get; init; } = default;
    public decimal? powerOut { get; init; } = default;
    public decimal? powerOutL1 { get; init; } = default;
    public decimal? powerOutL2 { get; init; } = default;
    public decimal? powerOutL3 { get; init; } = default;
    public decimal? voltageL1 { get; init; } = default;
    public decimal? voltageL2 { get; init; } = default;
    public decimal? voltageL3 { get; init; } = default;
  };
};
