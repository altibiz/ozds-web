using Ozds.Util;

namespace Ozds.Elasticsearch;

public class FakeReceiptMeasurementProvider : IReceiptMeasurementProvider
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(
      string source,
      string deviceId,
      Period period) =>
    Task.FromResult((
      new EnergyMeasurement
      {
        Energy = Random.Shared.Next(s_energyBeginMinMax),
        HighCostEnergy = Random.Shared.Next(s_energyBeginMinMax),
        LowCostEnergy = Random.Shared.Next(s_energyBeginMinMax),
        Date = period.From,
      },
      new EnergyMeasurement
      {
        Energy = Random.Shared.Next(s_energyEndMinMax),
        HighCostEnergy = Random.Shared.Next(s_energyEndMinMax),
        LowCostEnergy = Random.Shared.Next(s_energyEndMinMax),
        Date = period.To,
      }));

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(
      string source,
      string deviceId,
      Period period) =>
    GetEnergyMeasurementsAsync(
        source,
        deviceId,
        period)
      .BlockTask();

  public Task<PowerMeasurement>
  GetPowerMeasurementAsync(
      string source,
      string deviceId,
      Period period) =>
    Task.FromResult(
        new PowerMeasurement
        {
          Power = Random.Shared.Next(s_powerMinMax)
        });

  public PowerMeasurement
  GetPowerMeasurement(
      string source,
      string deviceId,
      Period period) =>
    GetPowerMeasurementAsync(
        source,
        deviceId,
        period)
      .BlockTask();

  private static readonly MinMax s_energyBeginMinMax = new(100, 200);
  private static readonly MinMax s_energyEndMinMax = new(200, 300);
  private static readonly MinMax s_powerMinMax = new(10, 20);
}
