using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<ISearchResponse<Measurement>> SearchMeasurementsAsync(
      Period? period = null);

  public ISearchResponse<Measurement> SearchMeasurements(
      Period? period = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsSortedAsync(
      Period? period = null);

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      Period? period = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsAsync(
      string deviceId, Period? period = null);

  public ISearchResponse<Measurement> SearchMeasurements(
      string deviceId, Period? period = null);

  public Task<ISearchResponse<Measurement>> SearchMeasurementsSortedAsync(
      string deviceId, Period? period = null);

  public ISearchResponse<Measurement> SearchMeasurementsSorted(
      string deviceId, Period? period = null);
};

public sealed partial class Client : IClient
{
  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsAsync(Period? period = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.MeasurementTimestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(period?.To ?? DateTime.UtcNow)))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurements(Period? period = null) =>
    Elasticsearch.Search<Measurement>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.MeasurementTimestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(period?.To ?? DateTime.UtcNow)))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsSortedAsync(Period? period = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.MeasurementTimestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(period?.To ?? DateTime.UtcNow)))
      .Sort(s => s
        .Descending(h => h.MeasurementTimestamp))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsSorted(Period? period = null) =>
    Elasticsearch.Search<Measurement>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.MeasurementTimestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(period?.To ?? DateTime.UtcNow)))
      .Sort(s => s
        .Descending(h => h.MeasurementTimestamp))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsAsync(string deviceId, Period? period = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.MeasurementTimestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.UtcNow)) && q
        .Term(t => t.DeviceId, deviceId))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurements(string deviceId, Period? period = null) =>
    Elasticsearch.Search<Measurement>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.MeasurementTimestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.UtcNow)) && q
        .Term(t => t.DeviceId, deviceId))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsSortedAsync(string deviceId, Period? period = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
        .Query(q => q
          .DateRange(r => r
            .Field(f => f.MeasurementTimestamp)
            .GreaterThanOrEquals(
              period?.From ?? DateTime.MinValue.ToUniversalTime())
            .LessThanOrEquals(
              period?.To ?? DateTime.UtcNow)) && q
          .Term(t => t.DeviceId, deviceId))
        .Sort(s => s.Descending(h => h.MeasurementTimestamp))
        .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsSorted(string deviceId, Period? period = null) =>
    Elasticsearch.Search<Measurement>(s => s
        .Query(q => q
          .DateRange(r => r
            .Field(f => f.MeasurementTimestamp)
            .GreaterThanOrEquals(
              period?.From ?? DateTime.MinValue.ToUniversalTime())
            .LessThanOrEquals(
              period?.To ?? DateTime.UtcNow)) && q
          .Term(t => t.DeviceId, deviceId))
        .Sort(s => s.Descending(h => h.MeasurementTimestamp))
        .Index(MeasurementIndexName));
}
