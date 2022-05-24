namespace Ozds.Elasticsearch;

public readonly record struct ExtractionMeasurement
(DateTime Timestamp,
 ExtractionMeasurementGeo? Geo,
 ExtractionMeasurementData Data);

public readonly record struct ExtractionMeasurementGeo
(decimal Longitude,
 decimal Latitude);

public readonly record struct ExtractionMeasurementData
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

public static class ExtractionMeasurementExtensions
{
  public static bool Validate(this ExtractionMeasurement measurement) =>
    measurement.Data.energyIn != null as decimal? &&
    measurement.Data.energyIn_T1 != null as decimal? &&
    measurement.Data.energyIn_T2 != null as decimal? &&
    measurement.Data.powerIn != null as decimal?;
}
