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
    Elastic
      .IndexAsync(device, s => s
        .RefreshInDevelopment(Env)
        .Index(DeviceIndexName))
      .ThenWith(_ => CreateLoadLogAsync(
        new LoadLog(
          device.Id,
          new()
          {
            From = device.MeasurementData.ExtractionStart,
            To = device.MeasurementData.ExtractionStart
          })));

  public IndexResponse IndexDevice(Device device) =>
    Elastic
      .Index(device, s => s
        .RefreshInDevelopment(Env)
        .Index(DeviceIndexName))
      .With(_ => CreateLoadLog(
        new LoadLog(
          device.Id,
          new()
          {
            From = device.MeasurementData.ExtractionStart,
            To = device.MeasurementData.ExtractionStart
          })));
}
