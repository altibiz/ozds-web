using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public readonly record struct LoadMeasurement
(DateTime Timestamp,
 LoadMeasurementDevice Device,
 LoadMeasurementData Data,
 LoadMeasurementGeo? Geo = null);

public readonly record struct LoadMeasurementDevice
(string Source,
 string DeviceId,
 string Operator,
 string CenterId,
 string? CenterUserId,
 string OwnerId,
 string? OwnerUserId);

public readonly record struct LoadMeasurementGeo
(decimal Longitude,
 decimal Latitude);

public readonly record struct LoadMeasurementData
(decimal energyIn,
 decimal energyIn_T1,
 decimal energyIn_T2,
 decimal powerIn,

 string? dongleId,
 string? meterIdent,
 string? meterSerial,
 string? localTime,
 string? localDate,
 int? tariff,
 int? limiter,
 int? fuseSupervisionL1,
 int? disconnectControl,
 int? numLongPwrFailures,
 int? numPwrFailures,
 int? numVoltageSagsL1,
 int? numVoltageSagsL2,
 int? numVoltageSagsL3,
 int? numVoltageSwellsL1,
 int? numVoltageSwellsL2,
 int? numVoltageSwellsL3,
 decimal? currentL1,
 decimal? currentL2,
 decimal? currentL3,
 decimal? energyOut,
 decimal? energyOut_T1,
 decimal? energyOut_T2,
 decimal? powerInL1,
 decimal? powerInL2,
 decimal? powerInL3,
 decimal? powerOut,
 decimal? powerOutL1,
 decimal? powerOutL2,
 decimal? powerOutL3,
 decimal? voltageL1,
 decimal? voltageL2,
 decimal? voltageL3);

public static class LoadMeasurementExtensions
{
  public static Measurement ToMeasurement(
      this LoadMeasurement measurement) =>
    new Measurement(
      measurement.Timestamp,
      new Measurement.DeviceDataType(
        measurement.Device.Source,
        measurement.Device.DeviceId,
        measurement.Device.Operator,
        measurement.Device.CenterId,
        measurement.Device.CenterUserId,
        measurement.Device.OwnerId,
        measurement.Device.OwnerUserId),
      new Measurement.MeasurementDataType
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
      },
      measurement.Geo.WhenNonNullable(geo =>
        new Nest.GeoCoordinate(
          (double)geo.Latitude,
          (double)geo.Longitude)));

  public static LoadMeasurement ToLoadMeasurement(
      this ExtractionMeasurement measurement,
      string @operator,
      string centerId,
      string? centerUserId,
      string ownerId,
      string? ownerUserId) =>
    new LoadMeasurement
    {
      Timestamp = measurement.Timestamp,
      Device =
        new LoadMeasurementDevice
        {
          Operator = @operator,
          CenterId = centerId,
          CenterUserId = centerUserId,
          OwnerId = ownerId,
          OwnerUserId = ownerUserId,
          Source = measurement.Source,
          DeviceId =
            Device.MakeId(
              measurement.Source,
              measurement.SourceDeviceId),
        },
      Data =
        new LoadMeasurementData
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
        },
      Geo = measurement.Geo.WhenNonNullable(geo =>
        new LoadMeasurementGeo
        {
          Latitude = geo.Latitude,
          Longitude = geo.Longitude,
        }),
    };

  public static LoadMeasurement ToLoadMeasurement(
      this ExtractionMeasurement measurement) =>
    measurement.ToLoadMeasurement(
      default!,
      default!,
      default!,
      default!,
      default!);
}
