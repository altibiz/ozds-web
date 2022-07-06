namespace Ozds.Elasticsearch;

public readonly record struct DashboardMeasurement
(DateTime Timestamp,
 string DeviceId,
 DashboardMeasurementData Data);

public readonly record struct DashboardMeasurementData
(decimal Energy,
 decimal LowCostEnergy,
 decimal HighCostEnergy,
 decimal Power,
 decimal PowerL1,
 decimal PowerL2,
 decimal PowerL3,
 decimal CurrentL1,
 decimal CurrentL2,
 decimal CurrentL3,
 decimal VoltageL1,
 decimal VoltageL2,
 decimal VoltageL3);

public static class DashboardMeasurementExtensions
{
  public static DashboardMeasurement
  ToDashboardMeasurement(this Measurement @this) =>
    new DashboardMeasurement
    {
      Timestamp = @this.Timestamp,
      DeviceId = @this.DeviceData.DeviceId,
      Data = @this.ToDashboardMeasurementData(),
    };

  public static DashboardMeasurementData
  ToDashboardMeasurementData(this Measurement @this) =>
    new DashboardMeasurementData
    {
      Energy = @this.MeasurementData.energyIn,
      LowCostEnergy = @this.MeasurementData.energyIn_T2,
      HighCostEnergy = @this.MeasurementData.energyIn_T1,
      Power = @this.MeasurementData.powerIn,
      PowerL1 = @this.MeasurementData.powerInL1 ?? 0M,
      PowerL2 = @this.MeasurementData.powerInL2 ?? 0M,
      PowerL3 = @this.MeasurementData.powerInL3 ?? 0M,
      CurrentL1 = @this.MeasurementData.currentL1 ?? 0M,
      CurrentL2 = @this.MeasurementData.currentL2 ?? 0M,
      CurrentL3 = @this.MeasurementData.currentL3 ?? 0M,
      VoltageL1 = @this.MeasurementData.voltageL1 ?? 0M,
      VoltageL2 = @this.MeasurementData.voltageL2 ?? 0M,
      VoltageL3 = @this.MeasurementData.voltageL3 ?? 0M,
    };

  public static DashboardMeasurement
  ToDashboardMeasurement(this ExtractionMeasurement @this) =>
    new DashboardMeasurement
    {
      Timestamp = @this.Timestamp,
      DeviceId = Device.MakeId(@this.Source, @this.SourceDeviceId),
      Data = @this.ToDashboardMeasurementData(),
    };

  public static DashboardMeasurementData
  ToDashboardMeasurementData(this ExtractionMeasurement @this) =>
    new DashboardMeasurementData
    {
      Energy = @this.Data.energyIn,
      LowCostEnergy = @this.Data.energyIn_T2,
      HighCostEnergy = @this.Data.energyIn_T1,
      Power = @this.Data.powerIn,
      PowerL1 = @this.Data.powerInL1 ?? 0M,
      PowerL2 = @this.Data.powerInL2 ?? 0M,
      PowerL3 = @this.Data.powerInL3 ?? 0M,
      CurrentL1 = @this.Data.currentL1 ?? 0M,
      CurrentL2 = @this.Data.currentL2 ?? 0M,
      CurrentL3 = @this.Data.currentL3 ?? 0M,
      VoltageL1 = @this.Data.voltageL1 ?? 0M,
      VoltageL2 = @this.Data.voltageL2 ?? 0M,
      VoltageL3 = @this.Data.voltageL3 ?? 0M,
    };
}
