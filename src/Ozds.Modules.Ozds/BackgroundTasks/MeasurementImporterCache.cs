using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using OrchardCore.ContentManagement;
using YesSql;

namespace Ozds.Modules.Ozds;

public class MeasurementImporterCache
{
  public readonly record struct DeviceData
  (string Operator,
   string CenterId,
   string? CenterUserId,
   string OwnerId,
   string? OwnerUserId);

  public Task<DeviceData?> GetDeviceData(string deviceId) =>
    DeviceCache.GetOrAdd(
      deviceId,
      deviceId =>
        new Lazy<Task<DeviceData?>>(
          () => FetchData(deviceId))).Value;

  private async Task<DeviceData?> FetchData(string deviceId)
  {
    using (var scope = Services.CreateAsyncScope())
    {
      var session = scope.ServiceProvider.GetRequiredService<ISession>();

      var siteItem = await session
        .Query<ContentItem, SiteIndex>(site => site.DeviceId == deviceId)
        .FirstOrDefaultAsync();
      if (siteItem is null) return default;

      var site = siteItem.AsContent<SecondarySiteType>();
      if (site is null) return default;

      return
        new DeviceData
        {
          Operator = site.Site.Value.Data.OperatorName,
          CenterId = site.Site.Value.Data.CenterContentItemId,
          CenterUserId = site.Site.Value.Data.CenterUserId,
          OwnerId = site.Site.Value.Data.OwnerContentItemId,
          OwnerUserId = site.Site.Value.Data.OwnerUserId
        };
    }
  }

  public MeasurementImporterCache(
      IServiceProvider services)
  {
    Services = services;
  }

  private IServiceProvider Services { get; }

  // NOTE: https://stackoverflow.com/a/54118193/4348107
  // TODO: test if this is good enough
  private ConcurrentDictionary<string, Lazy<Task<DeviceData?>>>
    DeviceCache
  { get; } =
    new ConcurrentDictionary<string, Lazy<Task<DeviceData?>>>();
}
