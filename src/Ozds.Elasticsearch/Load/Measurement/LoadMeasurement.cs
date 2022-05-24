namespace Ozds.Elasticsearch;

public readonly record struct LoadMeasurement
(DateTime Timestamp,
 LoadMeasurementGeo? Geo,
 string Operator,
 string CenterId,
 string CenterUserId,
 string OwnerId,
 string OwnerUserId,
 string Source,
 string DeviceId,
 LoadMeasurementData Data);

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
