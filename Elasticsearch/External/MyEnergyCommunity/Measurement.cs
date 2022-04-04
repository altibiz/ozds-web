using System;

// NOTE: field names are bound to REST API field names because of JSON
// deserialization
// TODO: find a way to make field names more conventional

namespace Elasticsearch.MyEnergyCommunity
{
  public class Measurement
  {
    public string deviceId { get; init; } = default!;
    public double deviceType { get; init; } = default!;
    public DateTime deviceDateTime { get; init; } = default!;
    public GeoCoordinates geoCoordinates { get; init; } = default!;
    public Data measurementData { get; init; } = default!;

    public override bool Equals(object? obj)
    {
      return Equals(obj as Measurement);
    }

    public bool Equals(Measurement? other)
    {
      return other != null && deviceId == other.deviceId &&
             deviceDateTime == other.deviceDateTime;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(deviceId, deviceDateTime);
    }

    public class GeoCoordinates
    {
      public double longitude { get; init; } = default!;
      public double latitude { get; init; } = default!;
    }

    public class Data
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
    }
  };
}
