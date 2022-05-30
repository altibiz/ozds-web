using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<BulkResponse> DeleteDevicesAsync(IEnumerable<Id> deviceIds);

  public BulkResponse DeleteDevices(IEnumerable<Id> deviceIds);
};

public sealed partial class Client : IClient
{
  public Task<BulkResponse> DeleteDevicesAsync(IEnumerable<Id> deviceIds) =>
    Elasticsearch.BulkAsync(s => s
      .DeleteMany<Device>(deviceIds.ToStrings())
      .Index(DeviceIndexName))
      .ThenWith(_ => DeleteLogsAsync(deviceIds
        .Select(id => new Nest.Id(LoadLog.MakeId(id.ToString())))));

  public BulkResponse DeleteDevices(IEnumerable<Id> deviceIds) =>
    Elasticsearch.Bulk(s => s
      .DeleteMany<Device>(deviceIds.ToStrings())
      .Index(DeviceIndexName))
      .With(_ => DeleteLogs(deviceIds
        .Select(id => new Nest.Id(LoadLog.MakeId(id.ToString())))));
}
