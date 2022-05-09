namespace Ozds.Elasticsearch;

public readonly record struct EnergyMeasurement
(decimal Energy, decimal LowCostEnergy, decimal HighCostEnergy);

public readonly record struct PowerMeasurement
(decimal Power);

public interface IReceiptMeasurementProvider
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(string deviceId, Period period);

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(string deviceId, Period period);

  public Task<PowerMeasurement> GetPowerMeasurementAsync(
      string deviceId, Period period);

  public PowerMeasurement GetPowerMeasurement(
      string deviceId, Period period);
}
