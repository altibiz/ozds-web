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
        center.Operator.Value.Name.Text,
        center.ContentItem.ContentItemId,
        center.Center.Value.User.UserIds.First(),
        consumer.ContentItem.ContentItemId,
        consumer.Consumer.Value.User.UserIds.First(),
        SiteMeasurementSource
          .GetElasticsearchSource(site.Site.Value.Source)
          .ThrowWhenNull(),
        site.Site.Value.DeviceId.Text,
        ((int)site.Site.Value.MeasurementIntervalInSeconds.Value!),
        site.Site.Value.ExtractionStart.Value!.Value,
        ((int)site.Site.Value.ExtractionOffsetInSeconds.Value!),
        ((int)site.Site.Value.ExtractionTimeoutInSeconds.Value!),
        ((int)site.Site.Value.ExtractionRetries.Value!),
        ((int)site.Site.Value.ValidationIntervalInSeconds.Value!),
        new SourceDeviceData
        {
          ownerId = site.Site.Value.SourceData.Data
            .FirstOrDefault(data => data.Name == "OwnerId")?.Value,
        },
        status ?? SiteStatus
          .GetElasticsearchStatus(site.Site.Value.Status)
          .ThrowWhenNull());
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
