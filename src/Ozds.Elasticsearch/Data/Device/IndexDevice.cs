using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<IndexResponse> IndexDeviceAsync(Device device);

  public IndexResponse IndexDevice(Device device);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<IndexResponse> IndexDeviceAsync(Device device) =>
    Elasticsearch.IndexAsync(device, s => s.Index(DeviceIndexName))
      .ThenWith(_ => CreateLoadLogAsync(
        new LoadLog(
          device.Id,
          new()
          {
            From = device.MeasurementData.ExtractionStart,
            To = device.MeasurementData.ExtractionStart
          })));

  public IndexResponse IndexDevice(Device device) =>
    Elasticsearch.Index(device, s => s.Index(DeviceIndexName))
      .With(_ => CreateLoadLog(
        new LoadLog(
          device.Id,
          new()
          {
            From = device.MeasurementData.ExtractionStart,
            To = device.MeasurementData.ExtractionStart
          })));
}
