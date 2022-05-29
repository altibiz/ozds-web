using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IClient : IDashboardMeasurementProvider { }

public sealed partial class Client : IClient
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string deviceId,
      Period? period = null) =>
    SearchMeasurementsByDeviceSortedAsync(deviceId, period)
      .Then(response => response
        .Hits.Select(hit => hit.Source
          .ToDashboardMeasurement()));

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(
      string deviceId,
      Period? period = null) =>
    SearchMeasurementsByDeviceSorted(deviceId, period)
      .Hits.Select(hit => hit.Source
        .ToDashboardMeasurement());

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null) =>
    SearchMeasurementsByOwnerSortedAsync(ownerId, period)
      .Then(response => response
        .Sources()
        .ToMultiDashboardMeasurements());

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwner(
      string ownerId,
      Period? period = null) =>
    SearchMeasurementsByOwnerSorted(ownerId, period)
      .Sources()
      .ToMultiDashboardMeasurements();

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null) =>
    SearchMeasurementsByOwnerUserSortedAsync(ownerUserId, period)
      .Then(response => response
        .Sources()
        .ToMultiDashboardMeasurements());

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null) =>
    SearchMeasurementsByOwnerUserSorted(ownerUserId, period)
      .Sources()
      .ToMultiDashboardMeasurements();
}
