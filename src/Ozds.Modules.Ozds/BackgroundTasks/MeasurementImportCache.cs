using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using OrchardCore.ContentManagement;
using YesSql;

namespace Ozds.Modules.Ozds;

public class MeasurementImportCache
{
  public readonly record struct Device
  (string Operator,
   string CenterId,
   string? CenterUserId,
   string OwnerId,
   string? OwnerUserId);

  public Task<Device?> GetDeviceAsync(string deviceId) =>
    DeviceCache.GetOrAdd(
      deviceId,
      deviceId =>
        new Lazy<Task<Device?>>(
          () => FetchDevice(deviceId))).Value;

  private async Task<Device?> FetchDevice(string deviceId)
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
        new Device
        {
          Operator = site.Site.Value.Data.OperatorName,
          CenterId = site.Site.Value.Data.CenterContentItemId,
          CenterUserId = site.Site.Value.Data.CenterUserId,
          OwnerId = site.Site.Value.Data.OwnerContentItemId,
          OwnerUserId = site.Site.Value.Data.OwnerUserId
        };
    }
  }

  public MeasurementImportCache(
      IServiceProvider services)
  {
    Services = services;
  }

  private IServiceProvider Services { get; }

  private ConcurrentDictionary<string, Lazy<Task<Device?>>> DeviceCache
  { get; } = new ConcurrentDictionary<string, Lazy<Task<Device?>>>();
}
