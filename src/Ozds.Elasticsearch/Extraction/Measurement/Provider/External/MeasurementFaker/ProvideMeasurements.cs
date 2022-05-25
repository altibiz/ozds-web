using Ozds.Util;

namespace Ozds.Elasticsearch.MeasurementFaker;

public partial interface IClient : IMeasurementProvider { };

public sealed partial class Client : IClient
{
  public string Source { get => Client.FakeSource; }

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsAwait(
      ProvisionDevice device,
      Period? period = null) =>
    GetMeasurements(device, period).ToAsync();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  GetMeasurementsAsync(
      ProvisionDevice device,
      Period? period = null) =>
    GetMeasurements(device, period).ToTask();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurements(
      ProvisionDevice device,
      Period? period = null) =>
    (device.SourceDeviceId != Client.FakeDeviceId) &&
    (!Client.FakeDeviceIds.Contains(device.SourceDeviceId)) ?
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
              // NOTE: last might fall out of bounds
              .SkipLast(1)
              .Select(period =>
                Convert(Measurement
                  .Generate(
                    device.SourceDeviceId,
                    // NOTE: add a second so were surely within limits
                    period.From + TimeSpan.FromSeconds(1))))));

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
