using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using Ozds.Elasticsearch;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class SiteDeviceLoader : ContentHandlerBase
{
  public override Task DraftSavedAsync(
      SaveDraftContentContext context) =>
    IndexDevice(context.ContentItem, DeviceState.Added);

  public override Task PublishedAsync(
      PublishContentContext context) =>
    IndexDevice(context.ContentItem);

  public override Task UnpublishedAsync(
      PublishContentContext context) =>
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

    var source =
      SiteMeasurementSource
        .GetDeviceMeasurementSource(site.Site.Value.Source)
        .ThrowWhenNull();
    var sourceDeviceId = site.Site.Value.SourceDeviceId.Text;
    var phase =
      SitePhase
        .GetDevicePhase(site.Site.Value.Phase)
        .ThrowWhenNull();
    var deviceId = Device.MakeId(source, sourceDeviceId);

    await Loader
      .LoadDeviceAsync(
        new LoadDevice(
          source: source,
          sourceDeviceId: sourceDeviceId,
          sourceDeviceData:
            new LoadDeviceSourceDeviceData
            {
              ownerId = site.Site.Value.SourceData.Data
                .FirstOrDefault(data => data.Name == "OwnerId")?.Value,
            },
          phase: phase,
          owner:
            new LoadDeviceOwnerData(
              @operator: site.Site.Value.Data.OperatorName,
              centerId: site.Site.Value.Data.CenterContentItemId,
              centerUserId: site.Site.Value.Data.CenterUserId,
              ownerId: site.Site.Value.Data.OwnerContentItemId,
              ownerUserId: site.Site.Value.Data.OwnerUserId),
          measurement:
            new LoadDeviceMeasurementData(
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
            new LoadDeviceStateData(
              state: status ?? SiteStatus
                .GetElasticsearchStatus(site.Site.Value.Status)
                .ThrowWhenNull())));

    Logger.LogDebug(
      $"Indexed device {deviceId} for site {item.ContentItemId}");
  }

  public SiteDeviceLoader(
      IHostEnvironment env,
      ILogger<SiteDeviceLoader> logger,
      IDeviceLoader loader)
  {
    Env = env;
    Logger = logger;
    Loader = loader;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }
  private IDeviceLoader Loader { get; }
}
