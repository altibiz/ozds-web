using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public BulkResponse IndexMeasurements(
      IEnumerable<Measurement> measurements);

  public Task<BulkResponse> IndexMeasurementsAsync(
      IEnumerable<Measurement> measurements);
};

public sealed partial class Client : IClient
{
  public BulkResponse IndexMeasurements(
      IEnumerable<Measurement> measurements) =>
    this.Elasticsearch.Bulk(
        s => s.IndexMany(measurements).Index(MeasurementIndexName));

  public async Task<BulkResponse> IndexMeasurementsAsync(
      IEnumerable<Measurement> measurements) =>
    await this.Elasticsearch.BulkAsync(
        s => s.IndexMany(measurements).Index(MeasurementIndexName));
}
