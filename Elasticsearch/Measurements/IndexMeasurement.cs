using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IndexResponse IndexMeasurement(Measurement measurement);
  public Task<IndexResponse> IndexMeasurementAsync(Measurement measurement);
};

public sealed partial class Client : IClient {
  public IndexResponse IndexMeasurement(
      Measurement measurement) => this.Elasticsearch.Index(measurement,
      s => s.Index(MeasurementIndexName));

  public Task<IndexResponse> IndexMeasurementAsync(
      Measurement measurement) => this.Elasticsearch.IndexAsync(measurement,
      s => s.Index(MeasurementIndexName));
}
}
