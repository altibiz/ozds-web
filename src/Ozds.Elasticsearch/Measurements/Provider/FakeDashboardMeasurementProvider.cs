using Ozds.Util;

namespace Ozds.Elasticsearch;

public sealed partial class FakeDashboardMeasurementProvider :
  IDashboardMeasurementProvider
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(string source, string deviceId) =>
  DateTime.UtcNow
    .WhenNullable(now =>
      Enumerables
        .Infinite((index) =>
          new DashboardMeasurement
          {
            Date = now.AddSeconds(index * 10),
            Energy = Random.Shared.Next(s_energyMinMax),
            LowCostEnergy = Random.Shared.Next(s_energyMinMax),
            HighCostEnergy = Random.Shared.Next(s_energyMinMax),
            Power = Random.Shared.Next(s_powerMinMax),
          })
        .Take(20)
        .ToTask());

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(string source, string deviceId) =>
    GetDashboardMeasurementsAsync(source, deviceId).BlockTask();

  private static readonly MinMax s_energyMinMax = new(100, 300);
  private static readonly MinMax s_powerMinMax = new(10, 20);
}
