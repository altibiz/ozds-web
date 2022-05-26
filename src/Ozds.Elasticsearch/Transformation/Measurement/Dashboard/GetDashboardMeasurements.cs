using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient :
  IDashboardMeasurementProvider
{
}

public sealed partial class Client : IClient
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string source,
      string deviceId,
      Period? period = null) =>
    DateTime.UtcNow
      .WhenNullable(now =>
        SearchMeasurementsByDeviceSortedAsync(
          Device.MakeId(source, deviceId),
          period ??
            new Period
            {
              From = now.AddMinutes(-5),
              To = now
            })
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
      string source,
      string deviceId,
      Period? period = null) =>
    DateTime.UtcNow
      .WhenNullable(now =>
        SearchMeasurementsByDeviceSorted(
          Device.MakeId(source, deviceId),
          period ??
            new Period
            {
              From = now.AddMinutes(-5),
              To = now
            })
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
