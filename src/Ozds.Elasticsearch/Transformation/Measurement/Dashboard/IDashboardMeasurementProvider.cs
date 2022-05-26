namespace Ozds.Elasticsearch;

public readonly record struct DashboardMeasurement
(DateTime Timestamp,
 decimal Energy,
 decimal LowCostEnergy,
 decimal HighCostEnergy,
 decimal Power);

public interface IDashboardMeasurementProvider
{
  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(
      string source,
      string deviceId,
      Period? period = null);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string source,
      string deviceId,
      Period? period = null);
}
