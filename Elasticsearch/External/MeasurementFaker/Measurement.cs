using System;

namespace Elasticsearch.MeasurementFaker
{
  public class Measurement
  {
    public Measurement(DateTime? timestamp = null, KnownData? data = null)
    {
      Timestamp = timestamp ?? DateTime.UtcNow.AddMinutes(-1);
      Data = data ?? new KnownData { };
    }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow.AddMinutes(-1);
    public string DeviceId { get; init; } = Client.FakeDeviceId;
    public KnownData Data { get; init; } = new KnownData { };

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

    public static Measurement Generate(DateTime timestamp)
    {
      var rand = new Random();

      return new Measurement(timestamp,
            new KnownData
            {
              voltageL1 = rand.Next(s_voltageL1MinMax),
              voltageL2 = 0,
              voltageL3 = 0
            });
    }

    private static MinMax s_voltageL1MinMax = new MinMax(150, 250);
  };
}
