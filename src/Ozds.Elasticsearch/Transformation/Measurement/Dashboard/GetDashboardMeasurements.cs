using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IDashboardMeasurementProvider { }

public sealed partial class Client : IClient
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string deviceId,
      Period? period = null) =>
    DateTime.UtcNow
      .WhenNullable(now =>
        SearchMeasurementsByDeviceSortedAsync(
          deviceId,
          period)
          .Then(response =>
            response.Hits.Select(hit =>
              new DashboardMeasurement
              {
                Timestamp = hit.Source.Timestamp,
                Energy = hit.Source.MeasurementData.energyIn,
                HighCostEnergy = hit.Source.MeasurementData.energyIn_T1,
                LowCostEnergy = hit.Source.MeasurementData.energyIn_T2,
                Power = hit.Source.MeasurementData.powerIn,
              })));

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(
      string deviceId,
      Period? period = null) =>
    DateTime.UtcNow
      .WhenNullable(now =>
        SearchMeasurementsByDeviceSorted(
          deviceId,
          period)
        .Hits.Select(hit =>
          new DashboardMeasurement
          {
            Timestamp = hit.Source.Timestamp,
            Energy = hit.Source.MeasurementData.energyIn,
            HighCostEnergy = hit.Source.MeasurementData.energyIn_T1,
            LowCostEnergy = hit.Source.MeasurementData.energyIn_T2,
            Power = hit.Source.MeasurementData.powerIn,
          }));
}
