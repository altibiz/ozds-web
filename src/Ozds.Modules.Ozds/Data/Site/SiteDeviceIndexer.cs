using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
           site.Site.Value.Source.TermContentItemIds.First())))
    {
      return;
    }

    var content = Services
      .GetRequiredService<IContentManager>()
      .ThrowWhenNull();

    var consumer = await content
      .GetContentAsync<ConsumerType>(site.ContainedPart.Value
        .ThrowWhenNull().ListContentItemId)
      .ThrowWhenNull();
    var center = await content
      .GetContentAsync<CenterType>(consumer.ContainedPart.Value
        .ThrowWhenNull().ListContentItemId)
      .ThrowWhenNull();

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
            @operator: center.Operator.Value.Name.Text,
            centerId: center.ContentItem.ContentItemId,
            centerUserId: center.Center.Value.User.UserIds.First(),
            ownerId: consumer.ContentItem.ContentItemId,
            ownerUserId: consumer.Consumer.Value.User.UserIds.First()),
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
      IDeviceLoader loader,
      IServiceProvider services)
  {
    Env = env;
    Logger = logger;
    Loader = loader;
    Services = services;
  }

  IHostEnvironment Env { get; }
  ILogger Logger { get; }
  IDeviceLoader Loader { get; }
  IServiceProvider Services { get; }
}
