namespace Ozds.Elasticsearch;

public interface IDashboardMeasurementProvider
{
  public const decimal MaxPower = 20M;
  public const decimal MaxCurrent = 20M;
  public const decimal MaxVoltage = 300M;

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByDeviceAsync(
      string deviceId,
      Period? period = null);

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByDevice(
      string deviceId,
      Period? period = null);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null);

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByOwner(
      string ownerId,
      Period? period = null);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null);

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null);
}
