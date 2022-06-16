using System.Collections.Concurrent;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public sealed partial class FakeDashboardMeasurementProvider :
  IDashboardMeasurementProvider
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByDeviceAsync(
      string deviceId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        deviceId,
        period)
      .ToTask();

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByDevice(
      string deviceId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        deviceId,
        period);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        FakeMeasurementProvider.FakeDeviceId,
        period)
      .ToTask();

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByOwner(
      string ownerId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        FakeMeasurementProvider.FakeDeviceId,
        period);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        FakeMeasurementProvider.FakeDeviceId,
        period)
      .ToTask();

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null) =>
    GenerateDashboardMeasurements(
        FakeMeasurementProvider.FakeDeviceId,
        period);

  // TODO: better faking for energy
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
        .SplitAscending(Objects
          .Max(TimeSpan
            .FromSeconds(FakeMeasurementProvider
              .MeasurementIntervalInSeconds),
            period.Span / 400))
        .Select((period, index) =>
          {
            index = DeviceCurrentEnergyIndex
              .AddOrUpdate(
                deviceId,
                _ => index,
                (_, index) => index + 1);
            var energy = Random.Shared.NextDecimal(
                new MinMax
                {
                  Min = s_energyMinMaxStart.Min + 5 * index,
                  Max = s_energyMinMaxStart.Max + 5 * index,
                });

            return
              new DashboardMeasurement
              {
                DeviceId =
                  Device.MakeId(
                    FakeMeasurementProvider.FakeSource,
                    deviceId),
                Timestamp = period.HalfPoint,
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

  private static ConcurrentDictionary<string, int> DeviceCurrentEnergyIndex =
    new ConcurrentDictionary<string, int>();

  private static readonly MinMax s_energyMinMaxStart = new(15995, 16000);
  private static readonly MinMax s_powerMinMax = new(10, 20);
  private static readonly MinMax s_currentMinMax = new(5, 15);
  private static readonly MinMax s_voltageMinMax = new(239, 241);
}
