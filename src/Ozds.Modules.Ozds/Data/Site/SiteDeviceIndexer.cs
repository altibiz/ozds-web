using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using Ozds.Elasticsearch;
using Ozds.Util;

namespace Ozds.Modules.Ozds;

public class SiteDeviceIndexer : ContentHandlerBase
{
  public override Task UpdatedAsync(
      UpdateContentContext context) =>
    IndexDevice(context.ContentItem);

  public override Task DraftSavedAsync(
      SaveDraftContentContext context) =>
    IndexDevice(context.ContentItem, DeviceState.Added);

  public override Task RemovedAsync(
      RemoveContentContext context) =>
    IndexDevice(context.ContentItem, DeviceState.Removed);

  private async Task IndexDevice(
      ContentItem item,
      string? status = null)
  {
    var site = item.AsContent<SecondarySiteType>();
    if (site is null ||
        (Env.IsProduction() &&
         SiteMeasurementSource.IsFake(
           site.Site.Value.Source.TermContentItemIds.First()))) return;

    await Loader
      .LoadDeviceAsync(
        source:
          SiteMeasurementSource
            .GetElasticsearchSource(site.Site.Value.Source)
            .ThrowWhenNull(),
        sourceDeviceId:
          site.Site.Value.DeviceId.Text,
        sourceDeviceData:
          new DeviceSourceDeviceData
          {
            ownerId = site.Site.Value.SourceData.Data
              .FirstOrDefault(data => data.Name == "OwnerId")?.Value,
          },
        owner:
          new DeviceOwnerData(
            @operator: site.Site.Value.Data.OperatorName,
            centerId: site.Site.Value.Data.CenterContentItemId,
            centerUserId: site.Site.Value.Data.CenterUserId,
            ownerId: site.Site.Value.Data.OwnerContentItemId,
            ownerUserId: site.Site.Value.Data.OwnerUserId),
        measurement:
          new DeviceMeasurementData(
            measurementIntervalInSeconds:
              ((int)site.Site.Value.MeasurementIntervalInSeconds.Value!),
            extractionStart:
              site.Site.Value.ExtractionStart.Value!.Value,
            extractionOffsetInSeconds:
              ((int)site.Site.Value.ExtractionOffsetInSeconds.Value!),
            extractionTimeoutInSeconds:
              ((int)site.Site.Value.ExtractionTimeoutInSeconds.Value!),
            extractionRetries:
              ((int)site.Site.Value.ExtractionRetries.Value!),
            validationIntervalInSeconds:
              ((int)site.Site.Value.ValidationIntervalInSeconds.Value!)),
        state:
          new DeviceStateData(
            state: status ?? SiteStatus
              .GetElasticsearchStatus(site.Site.Value.Status)
              .ThrowWhenNull()));
  }

  public SiteDeviceIndexer(
      IHostEnvironment env,
      ILogger<SiteDeviceIndexer> logger,
      IDeviceLoader loader)
  {
    Env = env;
    Logger = logger;
    Loader = loader;
  }

  IHostEnvironment Env { get; }
  ILogger Logger { get; }
  IDeviceLoader Loader { get; }
}
