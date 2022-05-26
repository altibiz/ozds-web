using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<IndexResponse> IndexDeviceAsync(Device device);

  public IndexResponse IndexDevice(Device device);
};

public sealed partial class Client : IClient
{
  public Task<IndexResponse> IndexDeviceAsync(Device device) =>
    Elasticsearch.IndexAsync(device, s => s.Index(DeviceIndexName))
      .ThenWithTask(_ => IndexLoadLogAsync(
        new LoadLog(
          device.Id,
          new()
          {
            From = device.MeasurementData.ExtractionStart,
            To = device.MeasurementData.ExtractionStart
          })));

  public IndexResponse IndexDevice(Device device) =>
    Elasticsearch.Index(device, s => s.Index(DeviceIndexName))
      .WithNullable(_ => IndexLoadLog(
        new LoadLog(
          device.Id,
          new()
          {
            From = device.MeasurementData.ExtractionStart,
            To = device.MeasurementData.ExtractionStart
          })));
}
