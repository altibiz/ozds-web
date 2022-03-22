using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient : IMeasurementProvider {
  public IEnumerable<Measurement> GetMeasurements(
      DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      DateTime? from = null, DateTime? to = null);

  public IEnumerable<Measurement> GetMeasurementsSorted(
      DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      DateTime? from = null, DateTime? to = null);
};

public sealed partial class Client : IClient {
  public IEnumerable<Measurement> GetMeasurements(
      DateTime? from = null, DateTime? to = null) =>
      this._client
          .Search<Measurement>(
              search => search.Index(Client.MeasurementsIndexName)
                            .Query(
                                q => q.Bool(
                                    b => b.Filter(
                                        f => f.DateRange(
                                            r => r.Field(f => f.deviceDateTime)
                                                     .GreaterThanOrEquals(from)
                                                     .LessThanOrEquals(to))))))
          .Hits.Select(hit => hit.Source);

  public async Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      DateTime? from = null, DateTime? to = null) =>
      (await this._client.SearchAsync<Measurement>(
           search => search.Index(Client.MeasurementsIndexName)
                         .Query(q => q.Bool(
                                    b => b.Filter(
                                        f => f.DateRange(
                                            r => r.Field(f => f.deviceDateTime)
                                                     .GreaterThanOrEquals(from)
                                                     .LessThanOrEquals(to)))))))
          .Hits.Select(hit => hit.Source);

  public IEnumerable<Measurement> GetMeasurementsSorted(
      DateTime? from = null, DateTime? to = null) =>
      this._client
          .Search<Measurement>(
              search => search.Index(Client.MeasurementsIndexName)
                            .Query(
                                q => q.Bool(
                                    b => b.Filter(
                                        f => f.DateRange(
                                            r => r.Field(f => f.deviceDateTime)
                                                     .GreaterThanOrEquals(from)
                                                     .LessThanOrEquals(to)))))
                            .Sort(s => s.Descending(h => h.deviceDateTime)))
          .Hits.Select(hit => hit.Source);

  public async Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      DateTime? from = null, DateTime? to = null) =>
      (await this._client.SearchAsync<Measurement>(
           search => search.Index(Client.MeasurementsIndexName)
                         .Query(q => q.Bool(
                                    b => b.Filter(
                                        f => f.DateRange(
                                            r => r.Field(f => f.deviceDateTime)
                                                     .GreaterThanOrEquals(from)
                                                     .LessThanOrEquals(to)))))
                         .Sort(s => s.Descending(h => h.deviceDateTime))))
          .Hits.Select(hit => hit.Source);

  public IEnumerable<Measurement> GetMeasurements(string ownerId,
      string deviceId, DateTime? from = null, DateTime? to = null) =>
      this._client
          .Search<Measurement>(
              search =>
                  search.Index(Client.MeasurementsIndexName)
                      .Query(
                          q => q.Bool(
                              b => b.Filter(
                                        f => f.DateRange(
                                            r => r.Field(f => f.deviceDateTime)
                                                     .GreaterThanOrEquals(from)
                                                     .LessThanOrEquals(to)))
                                       .Must(m => m.Term(
                                                 t => t.Field(f => f.deviceId)
                                                          .Value(deviceId))))))
          .Hits.Select(hit => hit.Source);

  public async Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      string ownerId, string deviceId, DateTime? from = null,
      DateTime? to = null) =>
      (await this._client.SearchAsync<Measurement>(
           search =>
               search.Index(Client.MeasurementsIndexName)
                   .Query(
                       q => q.Bool(
                           b => b.Filter(f => f.DateRange(
                                             r => r.Field(f => f.deviceDateTime)
                                                      .GreaterThanOrEquals(from)
                                                      .LessThanOrEquals(to)))
                                    .Must(m => m.Term(
                                              t => t.Field(f => f.deviceId)
                                                       .Value(deviceId)))))))
          .Hits.Select(hit => hit.Source);

  public IEnumerable<Measurement> GetMeasurementsSorted(string ownerId,
      string deviceId, DateTime? from = null, DateTime? to = null) =>
      this._client
          .Search<Measurement>(
              search =>
                  search.Index(Client.MeasurementsIndexName)
                      .Query(
                          q => q.Bool(
                              b => b.Filter(
                                        f => f.DateRange(
                                            r => r.Field(f => f.deviceDateTime)
                                                     .GreaterThanOrEquals(from)
                                                     .LessThanOrEquals(to)))
                                       .Must(m => m.Term(
                                                 t => t.Field(f => f.deviceId)
                                                          .Value(deviceId)))))
                      .Sort(s => s.Descending(h => h.deviceDateTime)))
          .Hits.Select(hit => hit.Source);

  public async Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      string ownerId, string deviceId, DateTime? from = null,
      DateTime? to = null) =>
      (await this._client.SearchAsync<Measurement>(
           search =>
               search.Index(Client.MeasurementsIndexName)
                   .Query(
                       q => q.Bool(
                           b => b.Filter(f => f.DateRange(
                                             r => r.Field(f => f.deviceDateTime)
                                                      .GreaterThanOrEquals(from)
                                                      .LessThanOrEquals(to)))
                                    .Must(m => m.Term(
                                              t => t.Field(f => f.deviceId)
                                                       .Value(deviceId)))))
                   .Sort(s => s.Descending(h => h.deviceDateTime))))
          .Hits.Select(hit => hit.Source);
}
}
