using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsAsync(
      Period? period = null,
      int? size = null);

  public ISearchResponse<Measurement>
  SearchMeasurements(
      Period? period = null,
      int? size = null);

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsSortedAsync(
      Period? period = null,
      int? size = null);

  public ISearchResponse<Measurement>
  SearchMeasurementsSorted(
      Period? period = null,
      int? size = null);

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByDeviceAsync(
      string deviceId,
      Period? period = null,
      int? size = null);

  public ISearchResponse<Measurement>
  SearchMeasurementsByDevice(
      string deviceId,
      Period? period = null,
      int? size = null);

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByDeviceSortedAsync(
      string deviceId,
      Period? period = null,
      int? size = null);

  public ISearchResponse<Measurement>
  SearchMeasurementsByDeviceSorted(
      string deviceId,
      Period? period = null,
      int? size = null);
};

public sealed partial class Client : IClient
{
  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsAsync(
      Period? period = null,
      int? size = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
      .Size(size ?? IClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(period?.To ?? DateTime.MaxValue.ToUniversalTime())))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurements(
      Period? period = null,
      int? size = null) =>
    Elasticsearch.Search<Measurement>(s => s
      .Size(size ?? IClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(period?.To ?? DateTime.MaxValue.ToUniversalTime())))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsSortedAsync(
      Period? period = null,
      int? size = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
      .Size(size ?? IClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(period?.To ?? DateTime.MaxValue.ToUniversalTime())))
      .Sort(s => s
        .Descending(h => h.Timestamp))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsSorted(
      Period? period = null,
      int? size = null) =>
    Elasticsearch.Search<Measurement>(s => s
      .Size(size ?? IClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(period?.To ?? DateTime.MaxValue.ToUniversalTime())))
      .Sort(s => s
        .Descending(h => h.Timestamp))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByDeviceAsync(
      string deviceId,
      Period? period = null,
      int? size = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
      .Size(size ?? IClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsByDevice(
      string deviceId,
      Period? period = null,
      int? size = null) =>
    Elasticsearch.Search<Measurement>(s => s
      .Size(size ?? IClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByDeviceSortedAsync(
      string deviceId,
      Period? period = null,
      int? size = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
      .Size(size ?? IClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))
      .Sort(s => s.Descending(h => h.Timestamp))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsByDeviceSorted(
      string deviceId,
      Period? period = null,
      int? size = null) =>
    Elasticsearch.Search<Measurement>(s => s
      .Size(size ?? IClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))
      .Sort(s => s.Descending(h => h.Timestamp))
      .Index(MeasurementIndexName));
}
