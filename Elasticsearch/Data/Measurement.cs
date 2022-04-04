using System;
using Nest;

namespace Elasticsearch
{
  [ElasticsearchType(RelationName = "measurement", IdProperty = nameof(Id))]
  public class Measurement
  {
    public static string MakeId(string deviceId, DateTime measurementTimestamp)
    {
      return StringExtensions.CombineIntoStringId(
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
      public double? currentL1 { get; init; } = default;
      public double? currentL2 { get; init; } = default;
      public double? currentL3 { get; init; } = default;
      public double? energyIn { get; init; } = default;
      public double? energyIn_T1 { get; init; } = default;
      public double? energyIn_T2 { get; init; } = default;
      public double? energyOut { get; init; } = default;
      public double? energyOut_T1 { get; init; } = default;
      public double? energyOut_T2 { get; init; } = default;
      public double? powerIn { get; init; } = default;
      public double? powerInL1 { get; init; } = default;
      public double? powerInL2 { get; init; } = default;
      public double? powerInL3 { get; init; } = default;
      public double? powerOut { get; init; } = default;
      public double? powerOutL1 { get; init; } = default;
      public double? powerOutL2 { get; init; } = default;
      public double? powerOutL3 { get; init; } = default;
      public double? voltageL1 { get; init; } = default;
      public double? voltageL2 { get; init; } = default;
      public double? voltageL3 { get; init; } = default;
    };
  };
}
