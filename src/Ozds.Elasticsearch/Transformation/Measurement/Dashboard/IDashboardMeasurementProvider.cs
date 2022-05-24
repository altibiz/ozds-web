namespace Ozds.Elasticsearch;

public readonly record struct DashboardMeasurement
(decimal Energy,
 decimal LowCostEnergy,
 decimal HighCostEnergy,
 decimal Power,
 DateTime Date);

public interface IDashboardMeasurementProvider
{
  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(string source, string deviceId);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(string source, string deviceId);
}
