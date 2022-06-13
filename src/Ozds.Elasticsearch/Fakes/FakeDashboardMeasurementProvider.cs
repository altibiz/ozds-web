using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public sealed partial class FakeDashboardMeasurementProvider :
  IDashboardMeasurementProvider
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string deviceId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        deviceId,
        period)
      .ToTask();

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(
      string deviceId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        deviceId,
        period);

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        FakeMeasurementProvider.FakeDeviceId,
        period)
      .ToMultiDashboardMeasurements()
      .ToTask();

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwner(
      string ownerId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        FakeMeasurementProvider.FakeDeviceId,
        period)
      .ToMultiDashboardMeasurements();

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        FakeMeasurementProvider.FakeDeviceId,
        period)
      .ToMultiDashboardMeasurements()
      .ToTask();

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        FakeMeasurementProvider.FakeDeviceId,
        period)
      .ToMultiDashboardMeasurements();

  private IEnumerable<DashboardMeasurement>
  GenerateDashboardMeasurements(
      string deviceId,
      Period? period = null) =>
    period
      .WhenNull(() =>
        new Period
        {
          From = DateTime.UtcNow.AddMinutes(-5),
          To = DateTime.UtcNow
        })
      .To(period => period
        .SplitAscending(100)
        .Select((period, index) =>
          {
            var energy = Random.Shared.NextDecimal(
                new MinMax
                {
                  Min = s_energyMinMaxStart.Min + 5 * index,
                  Max = s_energyMinMaxStart.Max + 5 * index
                });
            return
              new DashboardMeasurement
              {
                DeviceId =
                  Device.MakeId(
                    FakeMeasurementProvider.FakeSource,
                    deviceId),
                Timestamp = period.From,
                Data =
                  new DashboardMeasurementData
                  {
                    Energy = energy,
                    LowCostEnergy = energy,
                    HighCostEnergy = energy,
                    Power = Random.Shared.NextDecimal(s_powerMinMax),
                    PowerL1 = Random.Shared.NextDecimal(s_powerMinMax),
                    PowerL2 = Random.Shared.NextDecimal(s_powerMinMax),
                    PowerL3 = Random.Shared.NextDecimal(s_powerMinMax),
                    CurrentL1 = Random.Shared.NextDecimal(s_currentMinMax),
                    CurrentL2 = Random.Shared.NextDecimal(s_currentMinMax),
                    CurrentL3 = Random.Shared.NextDecimal(s_currentMinMax),
                    VoltageL1 = Random.Shared.NextDecimal(s_voltageMinMax),
                    VoltageL2 = Random.Shared.NextDecimal(s_voltageMinMax),
                    VoltageL3 = Random.Shared.NextDecimal(s_voltageMinMax),
                  },
              };
          }));

  private static readonly MinMax s_energyMinMaxStart = new(15995, 16000);
  private static readonly MinMax s_powerMinMax = new(10, 20);
  private static readonly MinMax s_currentMinMax = new(5, 15);
  private static readonly MinMax s_voltageMinMax = new(239, 241);
}
