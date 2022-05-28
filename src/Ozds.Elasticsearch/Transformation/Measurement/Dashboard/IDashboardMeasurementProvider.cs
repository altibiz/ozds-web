namespace Ozds.Elasticsearch;

public interface IDashboardMeasurementProvider
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string deviceId,
      Period? period = null);

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(
      string deviceId,
      Period? period = null);

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null);

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwner(
      string ownerId,
      Period? period = null);

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null);

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null);
}
