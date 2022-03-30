using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch.HelbOzds {
  public partial interface IClient : IMeasurementProvider {};

  public sealed partial class Client : IClient {
    public string Source { get => Client.s_source; }

    public IEnumerable<Elasticsearch.Measurement> GetMeasurements(
        Device device, Period? period = null) {
      var task = GetElasticsearchMeasurementsAsync(device, period);
      task.Wait();
      return task.Result;
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    GetMeasurementsAsync(Device device, Period? period = null) {
      return await GetElasticsearchMeasurementsAsync(device, period);
    }

    private async Task<IEnumerable<Elasticsearch.Measurement>>
    GetElasticsearchMeasurementsAsync(Device device, Period? period = null) {
      return (await GetNativeMeasurementsAsync(device, period))
          .Select(ConvertMeasurement);
    }

    // TODO: implement
    private async Task<List<Measurement>> GetNativeMeasurementsAsync(
        Device device, Period? period = null) {
      return await Task.FromResult<List<Measurement>>(new List<Measurement>());
    }

    // TODO: implement
    private Elasticsearch.Measurement ConvertMeasurement(
        Measurement measurement) {
      return new Elasticsearch.Measurement(measurement.Timestamp, null, Source,
          Elasticsearch.Device.MakeId(Source, measurement.DeviceId));
    }

    private const string s_source = "HelbOzds";
  }
}
