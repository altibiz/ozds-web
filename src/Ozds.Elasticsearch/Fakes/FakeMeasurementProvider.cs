using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public sealed partial class FakeMeasurementProvider : IMeasurementProvider
{
  public const string FakeSource = "FakeSource";
  public const string FakeDeviceId = "fakeDevice";
  public static readonly List<string> FakeDeviceIds =
    new List<string>
    {
      "fakeDevice1",
      "fakeDevice2",
      "fakeDevice3",
    };
  public const int MeasurementIntervalInSeconds = 15;

  public string Source { get => FakeMeasurementProvider.FakeSource; }

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsAsync(
      ProvisionDevice device,
      Period? period = null) =>
    GetMeasurements(device, period).ToAsync();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurements(
      ProvisionDevice device,
      Period? period = null) =>
    (device.SourceDeviceId != FakeMeasurementProvider.FakeDeviceId) &&
    (!FakeMeasurementProvider.FakeDeviceIds.Contains(device.SourceDeviceId)) ?
      Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>()
    : (period ??
        new()
        {
          From = DateTime.UtcNow.AddMinutes(-5),
          To = DateTime.UtcNow
        })
        .SplitAscending(TimeSpan.FromMinutes(5))
        .Select(period =>
          new ExtractionBucket<ExtractionMeasurement>(
            period,
            period
              // NOTE: always consistent
              .SplitAscending(TimeSpan.FromSeconds(
                  MeasurementIntervalInSeconds - 2))
              .Select(period =>
                Convert(Measurement
                  .Generate(
                    device.SourceDeviceId,
                    period.HalfPoint)))));

  public FakeMeasurementProvider(ILogger<FakeMeasurementProvider> logger) { Logger = logger; }

  private ILogger Logger { get; }

  // TODO: just generate
  private class Measurement
  {
    public Measurement(
        string deviceId,
        DateTime? timestamp = null,
        DataType? data = null)
    {
      DeviceId = deviceId;
      Timestamp = timestamp ?? DateTime.UtcNow.AddMinutes(-1);
      Data = data ?? new DataType { };
    }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow.AddMinutes(-1);
    public string DeviceId { get; init; } = FakeMeasurementProvider.FakeDeviceId;
    public DataType Data { get; init; } = new DataType { };

    public class DataType
    {
      public decimal energyIn { get; init; } = default;
      public decimal energyIn_T1 { get; init; } = default;
      public decimal energyIn_T2 { get; init; } = default;
      public decimal powerIn { get; init; } = default;

      public decimal energyOut { get; init; } = default;
      public decimal energyOut_T1 { get; init; } = default;
      public decimal energyOut_T2 { get; init; } = default;
      public decimal powerOut { get; init; } = default;

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
      public decimal? powerInL1 { get; init; } = default;
      public decimal? powerInL2 { get; init; } = default;
      public decimal? powerInL3 { get; init; } = default;
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


    private static MinMax s_energyMinMax = new(15000, 16000);
    private static MinMax s_powerMinMax = new(10, 20);
  };

  private ExtractionMeasurement Convert(
      Measurement measurement) =>
    new ExtractionMeasurement
    {
      Timestamp = measurement.Timestamp,
      Geo = null,
      Source = FakeSource,
      SourceDeviceId = measurement.DeviceId,
      Data = new ExtractionMeasurementData
      {
        energyIn = measurement.Data.energyIn,
        energyIn_T1 = measurement.Data.energyIn_T1,
        energyIn_T2 = measurement.Data.energyIn_T2,
        powerIn = measurement.Data.powerIn,

        dongleId = measurement.Data.dongleId,
        meterIdent = measurement.Data.meterIdent,
        meterSerial = measurement.Data.meterSerial,
        localTime = measurement.Data.localTime,
        localDate = measurement.Data.localDate,
        tariff = measurement.Data.tariff,
        limiter = measurement.Data.limiter,
        fuseSupervisionL1 = measurement.Data.fuseSupervisionL1,
        disconnectControl = measurement.Data.disconnectControl,
        numLongPwrFailures = measurement.Data.numLongPwrFailures,
        numPwrFailures = measurement.Data.numPwrFailures,
        numVoltageSagsL1 = measurement.Data.numVoltageSagsL1,
        numVoltageSagsL2 = measurement.Data.numVoltageSagsL2,
        numVoltageSagsL3 = measurement.Data.numVoltageSagsL3,
        numVoltageSwellsL1 = measurement.Data.numVoltageSwellsL1,
        numVoltageSwellsL2 = measurement.Data.numVoltageSwellsL2,
        numVoltageSwellsL3 = measurement.Data.numVoltageSwellsL3,
        currentL1 = measurement.Data.currentL1,
        currentL2 = measurement.Data.currentL2,
        currentL3 = measurement.Data.currentL3,
        energyOut = measurement.Data.energyOut,
        energyOut_T1 = measurement.Data.energyOut_T1,
        energyOut_T2 = measurement.Data.energyOut_T2,
        powerInL1 = measurement.Data.powerInL1,
        powerInL2 = measurement.Data.powerInL2,
        powerInL3 = measurement.Data.powerInL3,
        powerOut = measurement.Data.powerOut,
        powerOutL1 = measurement.Data.powerOutL1,
        powerOutL2 = measurement.Data.powerOutL2,
        powerOutL3 = measurement.Data.powerOutL3,
        voltageL1 = measurement.Data.voltageL1,
        voltageL2 = measurement.Data.voltageL2,
        voltageL3 = measurement.Data.voltageL3,
      }
    };
}
