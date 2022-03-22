using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch.HelbOzds {
  public partial interface IClient : IMeasurementProvider {};

  public sealed partial class Client : IClient {
    public IEnumerable<Elasticsearch.Measurement> GetMeasurements(
        string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      var task = GetMeasurementsListAsync(ownerId, deviceId, from, to);
      task.Wait();
      return task.Result;
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    GetMeasurementsAsync(string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      return await GetMeasurementsListAsync(ownerId, deviceId, from, to);
    }

    public IEnumerable<Elasticsearch.Measurement> GetMeasurementsSorted(
        string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      var task = GetMeasurementsListAsync(ownerId, deviceId, from, to);
      task.Wait();
      return task.Result.OrderBy(m => m.deviceDateTime);
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    GetMeasurementsSortedAsync(string ownerId, string deviceId,
        DateTime? from = null, DateTime? to = null) {
      return (await GetMeasurementsListAsync(ownerId, deviceId, from, to))
          .OrderBy(m => m.deviceDateTime);
    }

    // TODO: implement
    private async Task<IReadOnlyList<Elasticsearch.Measurement>>
    GetMeasurementsListAsync(string ownerId, string deviceId,
        DateTime? from = null, DateTime? to = null) {
      return await Task.FromResult<IReadOnlyList<Elasticsearch.Measurement>>(
          new List<Elasticsearch.Measurement>());
    }
  }
}
