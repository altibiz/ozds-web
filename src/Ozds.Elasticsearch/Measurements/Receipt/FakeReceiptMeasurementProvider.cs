using Ozds.Util;

namespace Ozds.Elasticsearch;

public class FakeReceiptMeasurementProvider : IReceiptMeasurementProvider
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(string deviceId, Period period) =>
    Task.FromResult(
      (new EnergyMeasurement
      {
        Energy = Random.Shared.Next(s_energyMinMax),
        HighCostEnergy = Random.Shared.Next(s_energyMinMax),
        LowCostEnergy = Random.Shared.Next(s_energyMinMax),
      },
       new EnergyMeasurement
       {
         Energy = Random.Shared.Next(s_energyMinMax),
         HighCostEnergy = Random.Shared.Next(s_energyMinMax),
         LowCostEnergy = Random.Shared.Next(s_energyMinMax),
       }));

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(string deviceId, Period period) =>
    GetEnergyMeasurementsAsync(deviceId, period).BlockTask();

  public Task<PowerMeasurement> GetPowerMeasurementAsync(
      string deviceId, Period period) =>
    Task.FromResult(
        (new PowerMeasurement
        {
          Power = Random.Shared.Next(s_powerMinMax)
        }));

  public PowerMeasurement GetPowerMeasurement(
      string deviceId, Period period) =>
    GetPowerMeasurementAsync(deviceId, period).BlockTask();

  private static MinMax s_energyMinMax = new(100, 200);
  private static MinMax s_powerMinMax = new(10, 20);
}
