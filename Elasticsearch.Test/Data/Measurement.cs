using System;

namespace Elasticsearch.Test {
  public partial class Data {
    public static readonly Measurement TestMeasurement =
        new Measurement(DateTime.Now, null, "test", "M9EQCU59",
            new Measurement.KnownData {
              dongleId = "M9EQCU59",
              meterIdent = "ISK5/2M550T-2006",
              meterSerial = "83793906",
              localTime = "194938",
              localDate = "220322",
              energyIn = 2803.013,
              energyIn_T1 = 719.16,
              energyIn_T2 = 2083.848,
              energyOut = 0.785,
              energyOut_T1 = 0.121,
              energyOut_T2 = 0.664,
              powerIn = 0,
              powerOut = 0,
              powerInL1 = 0,
              powerInL2 = 0,
              powerInL3 = 0,
              powerOutL1 = 0,
              powerOutL2 = 0,
              powerOutL3 = 0,
              voltageL1 = 238.7,
              voltageL2 = 240.2,
              voltageL3 = 240.9,
              currentL1 = 0,
              currentL2 = 0,
              currentL3 = 0,
              numLongPwrFailures = 3,
              numVoltageSagsL1 = 0,
              numVoltageSagsL2 = 0,
              numVoltageSagsL3 = 0,
              numVoltageSwellsL1 = 0,
              numVoltageSwellsL2 = 0,
              numVoltageSwellsL3 = 0,
              limiter = -1,
              fuseSupervisionL1 = 0,
              disconnectControl = 1,
            });
  };
}
