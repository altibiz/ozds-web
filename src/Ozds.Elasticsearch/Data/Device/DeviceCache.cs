using System.Collections.Concurrent;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial class ElasticsearchClient
{
  private Task<Device?> GetCachedDeviceAsync(
      string deviceId) =>
    DevicesByDeviceIdAsync.GetOrAdd(
      deviceId,
      new Lazy<Task<Device?>>(() =>
        GetDeviceAsync(deviceId)
          .Then(response => response.Source.Nullable()))).Value;

  private Task<IEnumerable<Device>> GetCachedDevicesByOwnerAsync(
      string ownerId) =>
    DevicesByContentIdAsync.GetOrAdd(
        ownerId,
        new Lazy<Task<List<Device>>>(() =>
          SearchDevicesByOwnerAsync(ownerId)
            .Then(response => response
              .Sources()
              .ToList()))).Value
      .Then(devices => devices
        .AsEnumerable());

  private Task<IEnumerable<Device>> GetCachedDevicesByOwnerUserAsync(
      string ownerUserId) =>
    DevicesByUserIdAsync.GetOrAdd(
        ownerUserId,
        new Lazy<Task<List<Device>>>(() =>
          SearchDevicesByOwnerUserAsync(ownerUserId)
            .Then(response => response
              .Sources()
              .ToList()))).Value
      .Then(devices => devices
        .AsEnumerable());

  private ConcurrentDictionary<string, Lazy<Task<Device?>>>
    DevicesByDeviceIdAsync
  { get; } =
      new ConcurrentDictionary<string, Lazy<Task<Device?>>>();

  private ConcurrentDictionary<string, Lazy<Task<List<Device>>>>
    DevicesByContentIdAsync
  { get; } =
      new ConcurrentDictionary<string, Lazy<Task<List<Device>>>>();

  private ConcurrentDictionary<string, Lazy<Task<List<Device>>>>
    DevicesByUserIdAsync
  { get; } =
      new ConcurrentDictionary<string, Lazy<Task<List<Device>>>>();
}
