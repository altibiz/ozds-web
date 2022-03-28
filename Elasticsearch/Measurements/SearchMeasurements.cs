using System;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public ISearchResponse<Measurement> SearchMeasurements(Period? period = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsAsync(
      Period? period = null);

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Period? period = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsSortedAsync(
      Period? period = null);

  public ISearchResponse<Measurement> SearchMeasurements(
      Device device, Period? period = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsAsync(
      Device device, Period? period = null);

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Device device, Period? period = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsSortedAsync(
      Device device, Period? period = null);
};

public sealed partial class Client : IClient {
  public ISearchResponse<Measurement> SearchMeasurements(
      Period? period = null) =>
      this._client.Search<Measurement>(
          s =>

              s.Query(
                  q => q.DateRange(
                      r => r.Field(f => f.MeasurementTimestamp)
                               .GreaterThanOrEquals(
                                   period?.From ?? DateTime.MinValue)
                               .LessThanOrEquals(period?.To ?? DateTime.Now))));

  public async Task<ISearchResponse<Measurement>>
      SearchMeasurementsAsync(Period? period = null) => (
          await this._client.SearchAsync<Measurement>(
              s => s.Query(q => q.DateRange(
                               r => r.Field(f => f.MeasurementTimestamp)
                                        .GreaterThanOrEquals(
                                            period?.From ?? DateTime.MinValue)
                                        .LessThanOrEquals(
                                            period?.To ?? DateTime.Now)))));

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Period? period = null) =>
      this._client.Search<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                        r => r.Field(f => f.MeasurementTimestamp)
                                 .GreaterThanOrEquals(
                                     period?.From ?? DateTime.MinValue)
                                 .LessThanOrEquals(period?.To ?? DateTime.Now)))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp)));

  public async Task<ISearchResponse<Measurement>>
      SearchMeasurementsSortedAsync(Period? period = null) => (
          await this._client.SearchAsync<Measurement>(
              s => s.Query(q => q.DateRange(
                               r => r.Field(f => f.MeasurementTimestamp)
                                        .GreaterThanOrEquals(
                                            period?.From ?? DateTime.MinValue)
                                        .LessThanOrEquals(
                                            period?.To ?? DateTime.Now)))
                       .Sort(s => s.Descending(h => h.MeasurementTimestamp))));

  public ISearchResponse<Measurement> SearchMeasurements(
      Device device, Period? period = null) =>
      this._client.Search<Measurement>(
          s => s.Query(
              q => q.DateRange(r => r.Field(f => f.MeasurementTimestamp)
                                        .GreaterThanOrEquals(
                                            period?.From ?? DateTime.MinValue)
                                        .LessThanOrEquals(
                                            period?.To ?? DateTime.Now)) &&
                   q.Term(t => t.DeviceId, device.SourceDeviceId)));

  public async Task<ISearchResponse<Measurement>>
  SearchMeasurementsAsync(Device device, Period? period = null) => (
      await this._client.SearchAsync<Measurement>(
          s => s.Query(
              q => q.DateRange(r => r.Field(f => f.MeasurementTimestamp)
                                        .GreaterThanOrEquals(
                                            period?.From ?? DateTime.MinValue)
                                        .LessThanOrEquals(
                                            period?.To ?? DateTime.Now)) &&
                   q.Term(t => t.DeviceId, device.SourceDeviceId))));

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Device device, Period? period = null) =>
      this._client.Search<Measurement>(
          s => s.Query(q => q.DateRange(
                                r => r.Field(f => f.MeasurementTimestamp)
                                         .GreaterThanOrEquals(
                                             period?.From ?? DateTime.MinValue)
                                         .LessThanOrEquals(
                                             period?.To ?? DateTime.Now)) &&
                            q.Term(t => t.DeviceId, device.SourceDeviceId))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp)));

  public async Task<ISearchResponse<Measurement>>
  SearchMeasurementsSortedAsync(Device device, Period? period = null) => (
      await this._client.SearchAsync<Measurement>(
          s => s.Query(q => q.DateRange(
                                r => r.Field(f => f.MeasurementTimestamp)
                                         .GreaterThanOrEquals(
                                             period?.From ?? DateTime.MinValue)
                                         .LessThanOrEquals(
                                             period?.To ?? DateTime.Now)) &&
                            q.Term(t => t.DeviceId, device.SourceDeviceId))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp))));
}
}
