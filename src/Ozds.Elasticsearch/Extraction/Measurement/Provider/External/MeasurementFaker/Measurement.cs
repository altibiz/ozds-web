using Ozds.Util;

namespace Ozds.Elasticsearch.MeasurementFaker;

public class Measurement
{
  public Measurement(
      string deviceId,
      DateTime? timestamp = null,
      KnownData? data = null)
  {
    DeviceId = deviceId;
    Timestamp = timestamp ?? DateTime.UtcNow.AddMinutes(-1);
    Data = data ?? new KnownData { };
  }

  public DateTime Timestamp { get; init; } = DateTime.UtcNow.AddMinutes(-1);
  public string DeviceId { get; init; } = Client.FakeDeviceId;
  public KnownData Data { get; init; } = new KnownData { };

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

  public static Measurement Generate(
      string deviceId,
      DateTime timestamp) =>
    new(
      deviceId,
      timestamp,
      new()
      {
        energyIn = Random.Shared.Next(s_energyMinMax),
        energyIn_T1 = Random.Shared.Next(s_energyMinMax),
        energyIn_T2 = Random.Shared.Next(s_energyMinMax),
        powerIn = Random.Shared.Next(s_powerMinMax),
      });


  private static MinMax s_energyMinMax = new(100, 200);
  private static MinMax s_powerMinMax = new(10, 20);
};