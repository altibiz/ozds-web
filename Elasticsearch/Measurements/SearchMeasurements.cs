using System;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public ISearchResponse<Measurement> SearchMeasurements(
      DateTime? from = null, DateTime? to = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsAsync(
      DateTime? from = null, DateTime? to = null);

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      DateTime? from = null, DateTime? to = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsSortedAsync(
      DateTime? from = null, DateTime? to = null);

  public ISearchResponse<Measurement> SearchMeasurements(
      Device device, DateTime? from = null, DateTime? to = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsAsync(
      Device device, DateTime? from = null, DateTime? to = null);

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Device device, DateTime? from = null, DateTime? to = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsSortedAsync(
      Device device, DateTime? from = null, DateTime? to = null);
};

public sealed partial class Client : IClient {
  public ISearchResponse<Measurement> SearchMeasurements(
      DateTime? from = null, DateTime? to = null) =>
      this._client.Search<Measurement>(
          s =>

              s.Query(
                  q => q.DateRange(
                      r => r.Field(f => f.MeasurementTimestamp)
                               .GreaterThanOrEquals(from ?? DateTime.MinValue)
                               .LessThanOrEquals(to ?? DateTime.Now))));

  public async Task<ISearchResponse<Measurement>>
      SearchMeasurementsAsync(DateTime? from = null, DateTime? to = null) => (
          await this._client.SearchAsync<Measurement>(
              s => s.Query(
                  q => q.DateRange(
                      r => r.Field(f => f.MeasurementTimestamp)
                               .GreaterThanOrEquals(from ?? DateTime.MinValue)
                               .LessThanOrEquals(to ?? DateTime.Now)))));

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      DateTime? from = null, DateTime? to = null) =>
      this._client.Search<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                        r => r.Field(f => f.MeasurementTimestamp)
                                 .GreaterThanOrEquals(from ?? DateTime.MinValue)
                                 .LessThanOrEquals(to ?? DateTime.Now)))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp)));

  public async Task<ISearchResponse<Measurement>> SearchMeasurementsSortedAsync(
      DateTime? from = null, DateTime? to = null) =>
      (await this._client.SearchAsync<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                        r => r.Field(f => f.MeasurementTimestamp)
                                 .GreaterThanOrEquals(from ?? DateTime.MinValue)
                                 .LessThanOrEquals(to ?? DateTime.Now)))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp))));

  public ISearchResponse<Measurement> SearchMeasurements(
      Device device, DateTime? from = null, DateTime? to = null) =>
      this._client.Search<Measurement>(
          s => s.Query(
              q => q.DateRange(
                       r => r.Field(f => f.MeasurementTimestamp)
                                .GreaterThanOrEquals(from ?? DateTime.MinValue)
                                .LessThanOrEquals(to ?? DateTime.Now)) &&
                   q.Term(t => t.DeviceId, device.SourceDeviceId)));

  public async Task<ISearchResponse<Measurement>> SearchMeasurementsAsync(
      Device device, DateTime? from = null, DateTime? to = null) =>
      (await this._client.SearchAsync<Measurement>(
          s => s.Query(
              q => q.DateRange(
                       r => r.Field(f => f.MeasurementTimestamp)
                                .GreaterThanOrEquals(from ?? DateTime.MinValue)
                                .LessThanOrEquals(to ?? DateTime.Now)) &&
                   q.Term(t => t.DeviceId, device.SourceDeviceId))));

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Device device, DateTime? from = null, DateTime? to = null) =>
      this._client.Search<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                             r => r.Field(f => f.MeasurementTimestamp)
                                      .GreaterThanOrEquals(
                                          from ?? DateTime.MinValue)
                                      .LessThanOrEquals(to ?? DateTime.Now)) &&
                         q.Term(t => t.DeviceId, device.SourceDeviceId))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp)));

  public async Task<ISearchResponse<Measurement>> SearchMeasurementsSortedAsync(
      Device device, DateTime? from = null, DateTime? to = null) =>
      (await this._client.SearchAsync<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                             r => r.Field(f => f.MeasurementTimestamp)
                                      .GreaterThanOrEquals(
                                          from ?? DateTime.MinValue)
                                      .LessThanOrEquals(to ?? DateTime.Now)) &&
                         q.Term(t => t.DeviceId, device.SourceDeviceId))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp))));
}
}
