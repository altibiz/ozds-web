namespace Ozds.Elasticsearch;

public readonly record struct EnergyMeasurement
(DateTime Timestamp,
 decimal Energy,
 decimal LowCostEnergy,
 decimal HighCostEnergy);

public readonly record struct PowerMeasurement
(decimal Power);

public interface IReceiptMeasurementProvider
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(
      string source,
      string deviceId,
      Period period);

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(
      string source,
      string deviceId,
      Period period);

  public Task<PowerMeasurement>
  GetPowerMeasurementAsync(
      string source,
      string deviceId,
      Period period);

  public PowerMeasurement
  GetPowerMeasurement(
      string source,
      string deviceId,
      Period period);
}
