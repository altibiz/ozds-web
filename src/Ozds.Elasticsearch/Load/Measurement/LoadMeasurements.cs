using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementLoader { }

public partial class Client : IClient
{
  public Task LoadMeasurementsAwait(
      IAsyncEnumerable<IExtractionBucket<LoadMeasurement>> measurements) =>
    measurements
      .ForEach(bucket => IndexMeasurementsAsync(bucket.Select(Convert)))
      .Run();

  public Task LoadMeasurementsAsync(
      IEnumerable<IExtractionBucket<LoadMeasurement>> measurements) =>
    measurements
      .ForEach(bucket => IndexMeasurementsAsync(bucket.Select(Convert)))
      .Run();

  public void LoadMeasurements(
      IEnumerable<IExtractionBucket<LoadMeasurement>> measurements) =>
    measurements
      .ForEach(bucket => IndexMeasurements(bucket.Select(Convert)))
      .Run();

  private Measurement Convert(LoadMeasurement measurement) =>
    new Measurement(
      measurement.Timestamp,
      measurement.Geo.WhenNonNullable(geo =>
        new Nest.GeoCoordinate(
          (double)geo.Latitude,
          (double)geo.Longitude)),
      measurement.Operator,
      measurement.CenterId,
      measurement.CenterUserId,
      measurement.OwnerId,
      measurement.OwnerUserId,
      measurement.Source,
      measurement.DeviceId,
      new Measurement.KnownData
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
      });
}
