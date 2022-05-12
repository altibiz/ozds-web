using Ozds.Util;

namespace Ozds.Elasticsearch;

public class FakeReceiptMeasurementProvider : IReceiptMeasurementProvider
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(string deviceId, Period period) =>
    Task.FromResult((
      new EnergyMeasurement
      {
        Energy = Random.Shared.Next(s_energyBeginMinMax),
        HighCostEnergy = Random.Shared.Next(s_energyBeginMinMax),
        LowCostEnergy = Random.Shared.Next(s_energyBeginMinMax),
      },
      new EnergyMeasurement
      {
        Energy = Random.Shared.Next(s_energyEndMinMax),
        HighCostEnergy = Random.Shared.Next(s_energyEndMinMax),
        LowCostEnergy = Random.Shared.Next(s_energyEndMinMax),
      }));

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(string deviceId, Period period) =>
    GetEnergyMeasurementsAsync(deviceId, period).BlockTask();

  public Task<PowerMeasurement> GetPowerMeasurementAsync(
      string deviceId, Period period) =>
    Task.FromResult(
        new PowerMeasurement
        {
          Power = Random.Shared.Next(s_powerMinMax)
        });

  public PowerMeasurement GetPowerMeasurement(
      string deviceId, Period period) =>
    GetPowerMeasurementAsync(deviceId, period).BlockTask();

  private static readonly MinMax s_energyBeginMinMax = new(100, 200);
  private static readonly MinMax s_energyEndMinMax = new(200, 300);
  private static readonly MinMax s_powerMinMax = new(10, 20);
}
