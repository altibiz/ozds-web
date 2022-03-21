using System;

namespace Elasticsearch {
public class Measurement {
  public string deviceId { get; init; } = default!;
  public double deviceType { get; init; } = default!;
  public DateTime deviceDateTime { get; init; } = default!;
  public MeasurementGeoCoordinates geoCoordinates { get; init; } = default!;
  public MeasurementData measurementData { get; init; } = default!;
};

public class MeasurementGeoCoordinates {
  public double longitude { get; init; } = default!;
  public double latitude { get; init; } = default!;
};

public class MeasurementData {
  public string dongleId { get; init; } = default!;
  public string meterIdent { get; init; } = default!;
  public string meterSerial { get; init; } = default!;
  public int tariff { get; init; } = default!;
  public double currentL1 { get; init; } = default!;
  public double currentL2 { get; init; } = default!;
  public double currentL3 { get; init; } = default!;
  public double nergyIn_T1 { get; init; } = default!;
  public double energyIn_T2 { get; init; } = default!;
  public double energyOut_T1 { get; init; } = default!;
  public double energyOut_T2 { get; init; } = default!;
  public uint numLongPwrFailures { get; init; } = default!;
  public uint numPwrFailures { get; init; } = default!;
  public uint numVoltageSagsL1 { get; init; } = default!;
  public uint numVoltageSagsL2 { get; init; } = default!;
  public uint numVoltageSagsL3 { get; init; } = default!;
  public uint numVoltageSwellsL1 { get; init; } = default!;
  public uint numVoltageSwellsL2 { get; init; } = default!;
  public uint numVoltageSwellsL3 { get; init; } = default!;
  public double powerIn { get; init; } = default!;
  public double powerInL1 { get; init; } = default!;
  public double powerInL2 { get; init; } = default!;
  public double powerInL3 { get; init; } = default!;
  public double powerOut { get; init; } = default!;
  public double powerOutL1 { get; init; } = default!;
  public double powerOutL2 { get; init; } = default!;
  public double powerOutL3 { get; init; } = default!;
  public double voltageL1 { get; init; } = default!;
  public double voltageL2 { get; init; } = default!;
  public double voltageL { get; init; } = default!;
};
}
