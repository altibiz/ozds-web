using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient :
  IDashboardMeasurementProvider
{
}

public sealed partial class Client : IClient
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(string source, string deviceId) =>
    DateTime.UtcNow
      .WhenNullable(now =>
        SearchMeasurementsByDeviceSortedAsync(
          Device.MakeId(source, deviceId),
          new Period
          {
            From = now.AddMinutes(-5),
            To = now
          })
          .Then(response =>
            response.Hits.Select(hit =>
              new DashboardMeasurement
              {
                Date = hit.Source.MeasurementTimestamp,
                Energy = hit.Source.Data.energyIn,
                HighCostEnergy = hit.Source.Data.energyIn_T1,
                LowCostEnergy = hit.Source.Data.energyIn_T2,
                Power = hit.Source.Data.powerIn,
              })));

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(string source, string deviceId) =>
    GetDashboardMeasurementsAsync(source, deviceId).BlockTask();
}
