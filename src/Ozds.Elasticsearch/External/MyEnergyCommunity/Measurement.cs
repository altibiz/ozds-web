namespace Ozds.Elasticsearch.MyEnergyCommunity;

public class Measurement
{
  public string deviceId { get; init; } = default!;
  public double deviceType { get; init; } = default!;
  public DateTime deviceDateTime { get; init; } = default!;
  public GeoCoordinates geoCoordinates { get; init; } = default!;
  public Data measurementData { get; init; } = default!;

  // NOTE: deuble because of NEST
  public class GeoCoordinates
  {
    public double longitude { get; init; } = default!;
    public double latitude { get; init; } = default!;
  }

  // NOTE: field names are bound to REST API field names because of JSON
  // deserialization
  public class Data
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
  }

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
};
