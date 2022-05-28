using Ozds.Util;

namespace Ozds.Elasticsearch;

public sealed partial class FakeDashboardMeasurementProvider :
  IDashboardMeasurementProvider
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string deviceId,
      Period? period = null) =>
    GenerateDashboardMeasurements(period).ToTask();

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(
      string deviceId,
      Period? period = null) =>
    GenerateDashboardMeasurements(period);

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null) =>
    GenerateDashboardMeasurements(period)
      .ToMultiDashboardMeasurements()
      .ToTask();

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwner(
      string ownerId,
      Period? period = null) =>
    GenerateDashboardMeasurements(period)
      .ToMultiDashboardMeasurements();

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null) =>
    GenerateDashboardMeasurements(period)
      .ToMultiDashboardMeasurements()
      .ToTask();

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null) =>
    GenerateDashboardMeasurements(period)
      .ToMultiDashboardMeasurements();

  private IEnumerable<DashboardMeasurement>
  GenerateDashboardMeasurements(Period? period = null) =>
    period
      .OnlyWhenNullable(() =>
        new Period
        {
          From = DateTime.UtcNow.AddMinutes(-5),
          To = DateTime.UtcNow
        })
      .WhenNullable(period => period
        .SplitDescending(TimeSpan.FromSeconds(10))
        .Select((period, index) =>
          {
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
                Timestamp = period.From,
                Data =
                  new DashboardMeasurementData
                  {
                    Energy = energy,
                    LowCostEnergy = energy,
                    HighCostEnergy = energy,
                    Power = power,
                  },
              };
          }));

  private static readonly MinMax s_energyMinMaxStart = new(15000, 16000);
  private static readonly MinMax s_powerMinMax = new(10, 20);
}
