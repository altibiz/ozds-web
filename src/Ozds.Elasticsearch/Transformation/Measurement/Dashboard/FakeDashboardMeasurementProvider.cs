using Ozds.Util;

namespace Ozds.Elasticsearch;

public sealed partial class FakeDashboardMeasurementProvider :
  IDashboardMeasurementProvider
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string source,
      string deviceId,
      Period? period = null) =>
    DateTime.UtcNow
      .WhenNullable(now =>
        Enumerables
          .Infinite(index =>
            {
              var date = now.AddSeconds(index * 10);
              var energy = Random.Shared.Next(
                  new MinMax
                  {
                    Min = s_energyMinMaxStart.Min + 50 * index,
                    Max = s_energyMinMaxStart.Max + 50 * index
                  });
              var power = Random.Shared.Next(s_powerMinMax);
              return
                new DashboardMeasurement
                {
                  Date = date,
                  Energy = energy,
                  LowCostEnergy = energy,
                  HighCostEnergy = energy,
                  Power = power,
                };
            })
          .Take(20)
          .ToTask());

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(
      string source,
      string deviceId,
      Period? period = null) =>
    GetDashboardMeasurementsAsync(source, deviceId, period)
      .BlockTask();

  private static readonly MinMax s_energyMinMaxStart = new(100, 150);
  private static readonly MinMax s_powerMinMax = new(10, 20);
}
