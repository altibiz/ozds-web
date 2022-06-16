using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
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

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null,
      int? size = null);

  public ISearchResponse<Measurement>
  SearchMeasurementsByOwner(
      string ownerId,
      Period? period = null,
      int? size = null);

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByOwnerSortedAsync(
      string ownerId,
      Period? period = null,
      int? size = null);

  public ISearchResponse<Measurement>
  SearchMeasurementsByOwnerSorted(
      string ownerId,
      Period? period = null,
      int? size = null);

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null,
      int? size = null);

  public ISearchResponse<Measurement>
  SearchMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null,
      int? size = null);

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByOwnerUserSortedAsync(
      string ownerUserId,
      Period? period = null,
      int? size = null);

  public ISearchResponse<Measurement>
  SearchMeasurementsByOwnerUserSorted(
      string ownerUserId,
      Period? period = null,
      int? size = null);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsAsync(
      Period? period = null,
      int? size = null) =>
    Elastic.SearchAsync<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
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
    Elastic.Search<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
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
    Elastic.SearchAsync<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
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
    Elastic.Search<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
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
    Elastic.SearchAsync<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
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
    Elastic.Search<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
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
    Elastic.SearchAsync<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
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
    Elastic.Search<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
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

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null,
      int? size = null) =>
    Elastic.SearchAsync<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.OwnerId.Suffix("keyword"), ownerId))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsByOwner(
      string ownerId,
      Period? period = null,
      int? size = null) =>
    Elastic.Search<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.OwnerId.Suffix("keyword"), ownerId))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByOwnerSortedAsync(
      string ownerId,
      Period? period = null,
      int? size = null) =>
    Elastic.SearchAsync<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.OwnerId.Suffix("keyword"), ownerId))
      .Sort(s => s.Descending(h => h.Timestamp))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsByOwnerSorted(
      string ownerId,
      Period? period = null,
      int? size = null) =>
    Elastic.Search<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.OwnerId.Suffix("keyword"), ownerId))
      .Sort(s => s.Descending(h => h.Timestamp))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null,
      int? size = null) =>
    Elastic.SearchAsync<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.OwnerUserId.Suffix("keyword"), ownerUserId))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null,
      int? size = null) =>
    Elastic.Search<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.OwnerUserId.Suffix("keyword"), ownerUserId))
      .Index(MeasurementIndexName));

  public Task<ISearchResponse<Measurement>>
  SearchMeasurementsByOwnerUserSortedAsync(
      string ownerUserId,
      Period? period = null,
      int? size = null) =>
    Elastic.SearchAsync<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.OwnerUserId.Suffix("keyword"), ownerUserId))
      .Sort(s => s.Descending(h => h.Timestamp))
      .Index(MeasurementIndexName));

  public ISearchResponse<Measurement>
  SearchMeasurementsByOwnerUserSorted(
      string ownerUserId,
      Period? period = null,
      int? size = null) =>
    Elastic.Search<Measurement>(s => s
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThanOrEquals(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.DeviceData.OwnerUserId.Suffix("keyword"), ownerUserId))
      .Sort(s => s.Descending(h => h.Timestamp))
      .Index(MeasurementIndexName));
}
