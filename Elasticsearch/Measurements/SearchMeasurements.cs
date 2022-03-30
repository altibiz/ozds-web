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
      this.Elasticsearch.Search<Measurement>(
          s => s.Query(q => q.DateRange(
                           r => r.Field(f => f.MeasurementTimestamp)
                                    .GreaterThanOrEquals(
                                        period?.From ??
                                        DateTime.MinValue.ToUniversalTime())
                                    .LessThanOrEquals(
                                        period?.To ?? DateTime.UtcNow)))
                   .Index(MeasurementIndexName));

  public async Task<ISearchResponse<Measurement>>
      SearchMeasurementsAsync(Period? period = null) => (
          await this.Elasticsearch.SearchAsync<Measurement>(
              s => s.Query(q => q.DateRange(
                               r => r.Field(f => f.MeasurementTimestamp)
                                        .GreaterThanOrEquals(
                                            period?.From ??
                                            DateTime.MinValue.ToUniversalTime())
                                        .LessThanOrEquals(
                                            period?.To ?? DateTime.UtcNow)))
                       .Index(MeasurementIndexName)));

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Period? period = null) =>
      this.Elasticsearch.Search<Measurement>(
          s => s.Query(q => q.DateRange(
                           r => r.Field(f => f.MeasurementTimestamp)
                                    .GreaterThanOrEquals(
                                        period?.From ??
                                        DateTime.MinValue.ToUniversalTime())
                                    .LessThanOrEquals(
                                        period?.To ?? DateTime.UtcNow)))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp))
                   .Index(MeasurementIndexName));

  public async Task<ISearchResponse<Measurement>>
      SearchMeasurementsSortedAsync(Period? period = null) => (
          await this.Elasticsearch.SearchAsync<Measurement>(
              s => s.Query(q => q.DateRange(
                               r => r.Field(f => f.MeasurementTimestamp)
                                        .GreaterThanOrEquals(
                                            period?.From ??
                                            DateTime.MinValue.ToUniversalTime())
                                        .LessThanOrEquals(
                                            period?.To ?? DateTime.UtcNow)))
                       .Sort(s => s.Descending(h => h.MeasurementTimestamp))
                       .Index(MeasurementIndexName)));

  public ISearchResponse<Measurement> SearchMeasurements(
      Device device, Period? period = null) =>
      this.Elasticsearch.Search<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                             r => r.Field(f => f.MeasurementTimestamp)
                                      .GreaterThanOrEquals(
                                          period?.From ??
                                          DateTime.MinValue.ToUniversalTime())
                                      .LessThanOrEquals(
                                          period?.To ?? DateTime.UtcNow)) &&
                         q.Term(t => t.DeviceId, device.SourceDeviceId))
                   .Index(MeasurementIndexName));

  public async Task<ISearchResponse<Measurement>>
  SearchMeasurementsAsync(Device device, Period? period = null) => (
      await this.Elasticsearch.SearchAsync<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                             r => r.Field(f => f.MeasurementTimestamp)
                                      .GreaterThanOrEquals(
                                          period?.From ??
                                          DateTime.MinValue.ToUniversalTime())
                                      .LessThanOrEquals(
                                          period?.To ?? DateTime.UtcNow)) &&
                         q.Term(t => t.DeviceId, device.SourceDeviceId))
                   .Index(MeasurementIndexName)));

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Device device, Period? period = null) =>
      this.Elasticsearch.Search<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                             r => r.Field(f => f.MeasurementTimestamp)
                                      .GreaterThanOrEquals(
                                          period?.From ??
                                          DateTime.MinValue.ToUniversalTime())
                                      .LessThanOrEquals(
                                          period?.To ?? DateTime.UtcNow)) &&
                         q.Term(t => t.DeviceId, device.SourceDeviceId))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp))
                   .Index(MeasurementIndexName));

  public async Task<ISearchResponse<Measurement>>
  SearchMeasurementsSortedAsync(Device device, Period? period = null) => (
      await this.Elasticsearch.SearchAsync<Measurement>(
          s => s.Query(
                    q => q.DateRange(
                             r => r.Field(f => f.MeasurementTimestamp)
                                      .GreaterThanOrEquals(
                                          period?.From ??
                                          DateTime.MinValue.ToUniversalTime())
                                      .LessThanOrEquals(
                                          period?.To ?? DateTime.UtcNow)) &&
                         q.Term(t => t.DeviceId, device.SourceDeviceId))
                   .Sort(s => s.Descending(h => h.MeasurementTimestamp))
                   .Index(MeasurementIndexName)));
}
}
