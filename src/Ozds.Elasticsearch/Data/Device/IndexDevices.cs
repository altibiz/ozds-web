using Ozds.Extensions;

using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<BulkResponse> IndexDevicesAsync(IEnumerable<Device> devices);

  public BulkResponse IndexDevices(IEnumerable<Device> devices);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<BulkResponse> IndexDevicesAsync(IEnumerable<Device> devices) =>
    Elasticsearch
      .BulkAsync(s => s
        .IndexMany(devices)
        .Index(DeviceIndexName))
      .ThenWith(_ => CreateLoadLogsAsync(
        devices.Select(device =>
          new LoadLog(
            device.Id,
            new()
            {
              From = device.MeasurementData.ExtractionStart,
              To = device.MeasurementData.ExtractionStart
            }))));

  public BulkResponse IndexDevices(IEnumerable<Device> devices) =>
    Elasticsearch
      .Bulk(s => s
        .IndexMany(devices)
        .Index(DeviceIndexName))
      .With(_ => CreateLoadLogs(
        devices.Select(device =>
          new LoadLog(
            device.Id,
            new()
            {
              From = device.MeasurementData.ExtractionStart,
              To = device.MeasurementData.ExtractionStart
            }))));
}
