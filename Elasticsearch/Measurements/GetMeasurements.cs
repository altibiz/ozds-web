using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient : IMeasurementProvider {
  public IEnumerable<Measurement> getMeasurements(DateTime from, DateTime to);

  public Task<IEnumerable<Measurement>> getMeasurementsAsync(
      DateTime from, DateTime to);

  public IEnumerable<Measurement> getMeasurementsSorted(
      DateTime from, DateTime to);

  public Task<IEnumerable<Measurement>> getMeasurementsSortedAsync(
      DateTime from, DateTime to);
};

public sealed partial class Client : IClient {
  public IEnumerable<Measurement> getMeasurements(DateTime from, DateTime to) =>
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

  public async Task<IEnumerable<Measurement>> getMeasurementsAsync(
      DateTime from, DateTime to) =>
      (await this._client.SearchAsync<Measurement>(
           search => search.Index(Client.MeasurementsIndexName)
                         .Query(q => q.Bool(
                                    b => b.Filter(
                                        f => f.DateRange(
                                            r => r.Field(f => f.deviceDateTime)
                                                     .GreaterThanOrEquals(from)
                                                     .LessThanOrEquals(to)))))))
          .Hits.Select(hit => hit.Source);

  public IEnumerable<Measurement> getMeasurementsSorted(
      DateTime from, DateTime to) =>
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

  public async Task<IEnumerable<Measurement>> getMeasurementsSortedAsync(
      DateTime from, DateTime to) =>
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

  public IEnumerable<Measurement> getMeasurements(
      string ownerId, string deviceId, DateTime from, DateTime to) =>
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

  public async Task<IEnumerable<Measurement>> getMeasurementsAsync(
      string ownerId, string deviceId, DateTime from, DateTime to) =>
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

  public IEnumerable<Measurement> getMeasurementsSorted(
      string ownerId, string deviceId, DateTime from, DateTime to) =>
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

  public async Task<IEnumerable<Measurement>> getMeasurementsSortedAsync(
      string ownerId, string deviceId, DateTime from, DateTime to) =>
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
