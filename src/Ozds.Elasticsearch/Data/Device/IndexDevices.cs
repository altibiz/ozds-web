using Ozds.Util;

using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<BulkResponse> IndexDevicesAsync(IEnumerable<Device> devices);

  public BulkResponse IndexDevices(IEnumerable<Device> devices);
};

public sealed partial class Client : IClient
{
  public Task<BulkResponse> IndexDevicesAsync(IEnumerable<Device> devices) =>
    Elasticsearch
      .BulkAsync(s => s
        .IndexMany(devices)
        .Index(DeviceIndexName))
      .ThenWithTask(_ => IndexLoadLogsAsync(
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
      .WithNullable(_ => IndexLoadLogs(
        devices.Select(device =>
          new LoadLog(
            device.Id,
            new()
            {
              From = device.MeasurementData.ExtractionStart,
              To = device.MeasurementData.ExtractionStart
            }))));
}
