using System.Collections.Concurrent;
using OrchardCore.ContentManagement;
using YesSql;
using Ozds.Util;

namespace Ozds.Modules.Ozds;

public class MeasurementImporterCache
{
  public readonly record struct DeviceData
  (string Operator,
   string CenterId,
   string? CenterUserId,
   string OwnerId,
   string? OwnerUserId);

  public Task<DeviceData> GetDeviceData(string deviceId) =>
    DeviceCache.GetOrAdd(
      deviceId,
      deviceId =>
        new Lazy<Task<DeviceData>>(
          () => FetchData(deviceId))).Value;

  private async Task<DeviceData> FetchData(string deviceId)
  {
    var siteItem = await Session
      .Query<ContentItem>()
      .With<SiteIndex>(site => site.DeviceId == deviceId)
      .FirstOrDefaultAsync();
    if (siteItem is null) return default;

    var site = siteItem.AsContent<SecondarySiteType>();
    if (site is null) return default;

    var siteOwnerContentItemId =
      site.ContainedPart.Value?.ListContentItemId;
    if (siteOwnerContentItemId is null) return default;

    var owner = await Content
      .GetContentAsync<ConsumerType>(siteOwnerContentItemId);
    if (owner is null) return default;

    var centerContentItemId =
      owner.ContainedPart.Value?.ListContentItemId;
    if (centerContentItemId is null) return default;

    var center = await Content
      .GetContentAsync<CenterType>(centerContentItemId);
    if (center is null) return default;

    return
      new DeviceData
      {
        Operator = center.Operator.Value.Name.Text,
        CenterId = center.ContentItem.ContentItemId,
        CenterUserId =
          center.Center.Value.User.UserIds.FirstOrDefault(),
        OwnerId = owner.ContentItem.ContentItemId,
        OwnerUserId =
          owner.Consumer.Value.User.UserIds.FirstOrDefault()
      };
  }

  public MeasurementImporterCache(
      IContentManager content,
      ISession session)
  {
    Content = content;
    Session = session;
  }

  private IContentManager Content { get; }
  private ISession Session { get; }

  // NOTE: https://stackoverflow.com/a/54118193/4348107
  // TODO: test if this is good enough
  private ConcurrentDictionary<string, Lazy<Task<DeviceData>>>
    DeviceCache
  { get; } =
    new ConcurrentDictionary<string, Lazy<Task<DeviceData>>>();
}
