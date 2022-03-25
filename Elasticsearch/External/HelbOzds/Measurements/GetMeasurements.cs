using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch.HelbOzds {
  public partial interface IClient : IMeasurementProvider {};

  public sealed partial class Client : IClient {
    public string Source { get => Client.s_source; }

    public IEnumerable<Elasticsearch.Measurement> GetMeasurements(
        Device device, DateTime? from = null, DateTime? to = null) {
      var task = GetElasticsearchMeasurementsAsync(device, from, to);
      task.Wait();
      return task.Result;
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    GetMeasurementsAsync(
        Device device, DateTime? from = null, DateTime? to = null) {
      return await GetElasticsearchMeasurementsAsync(device, from, to);
    }

    private async Task<IEnumerable<Elasticsearch.Measurement>>
    GetElasticsearchMeasurementsAsync(
        Device device, DateTime? from = null, DateTime? to = null) {
      return (await GetNativeMeasurementsAsync(device, from, to))
          .Select(ConvertMeasurement);
    }

    // TODO: implement
    private async Task<List<Measurement>> GetNativeMeasurementsAsync(
        Device device, DateTime? from = null, DateTime? to = null) {
      return await Task.FromResult<List<Measurement>>(new List<Measurement>());
    }

    // TODO: implement
    private Elasticsearch.Measurement ConvertMeasurement(
        Measurement measurement) {
      return new Elasticsearch.Measurement(
          measurement.Timestamp, null, Source, measurement.DeviceId);
    }
  }
}
