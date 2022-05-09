using Ozds.Util;

namespace Ozds.Elasticsearch;

public class FakeReceiptMeasurementProvider : IReceiptMeasurementProvider
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(string deviceId, Period period) =>
    Task.FromResult(
      (new EnergyMeasurement
      {
        Energy = default,
        HighCostEnergy = Random.Shared.Next(100, 200),
        LowCostEnergy = Random.Shared.Next(100, 200),
      },
       new EnergyMeasurement
       {
         Energy = default,
         HighCostEnergy = Random.Shared.Next(100, 200),
         LowCostEnergy = Random.Shared.Next(100, 200),
       }));

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(string deviceId, Period period) =>
    GetEnergyMeasurementsAsync(deviceId, period).BlockTask();

  public Task<PowerMeasurement> GetPowerMeasurementAsync(
      string deviceId, Period period) =>
    Task.FromResult(
        (new PowerMeasurement
        {
          Power = Random.Shared.Next(10, 20)
        }));

  public PowerMeasurement GetPowerMeasurement(
      string deviceId, Period period) =>
    GetPowerMeasurementAsync(deviceId, period).BlockTask();
}
