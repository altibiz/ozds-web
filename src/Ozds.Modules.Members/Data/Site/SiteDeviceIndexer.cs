using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Handlers;
using Ozds.Elasticsearch;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class SiteDeviceIndexer : ContentHandlerBase
{
  public override Task UpdatedAsync(
      UpdateContentContext context) =>
    context.ContentItem
      .AsContent<SecondarySiteType>()
      .When(
        secondarySite =>
          (Env.IsProduction() &&
           !SiteMeasurementSource.IsFake(
            secondarySite.Site.Value.Source.TermContentItemIds[0])) ||
          Env.IsDevelopment(),
        secondarySite =>
          Indexer
            .IndexDeviceAsync(
              SiteMeasurementSource.GetElasticsearchSource(
                secondarySite.Site.Value.Source.TermContentItemIds[0]) ??
              throw new InvalidOperationException("Invalid site source"),
              secondarySite.Site.Value.DeviceId.Text,
              new SourceDeviceData(
                ownerId: secondarySite.Site.Value.SourceData.Data
                  .FirstOrDefault(data => data.Name == "OwnerId")?.Value),
              secondarySite.Site.Value.MeasurementFrequency.Value,
              SiteStatus.GetElasticsearchStatus(
                secondarySite.Site.Value.Status.TermContentItemIds[0]))
            .Then(() => Logger.LogDebug(
              string.Format(
                "Updated device {0}",
                secondarySite.Site.Value.DeviceId.Text))),
        Task.CompletedTask);

  public override Task DraftSavedAsync(
      SaveDraftContentContext context) =>
    context.ContentItem
      .AsContent<SecondarySiteType>()
      .When(
        secondarySite =>
          (Env.IsProduction() &&
           !SiteMeasurementSource.IsFake(
            secondarySite.Site.Value.Source.TermContentItemIds[0])) ||
          Env.IsDevelopment(),
        secondarySite =>
          Indexer
            .IndexDeviceAsync(
              SiteMeasurementSource.GetElasticsearchSource(
                secondarySite.Site.Value.Source.TermContentItemIds[0]) ??
              throw new InvalidOperationException("Invalid site source"),
              secondarySite.Site.Value.DeviceId.Text,
              new SourceDeviceData(
                ownerId: secondarySite.Site.Value.SourceData.Data
                  .FirstOrDefault(data => data.Name == "OwnerId")?.Value),
              secondarySite.Site.Value.MeasurementFrequency.Value,
              Elasticsearch.DeviceState.Added)
            .Then(() => Logger.LogDebug(
              string.Format(
                "Added device {0}",
                secondarySite.Site.Value.DeviceId.Text))),
        Task.CompletedTask);

  public override Task RemovedAsync(
      RemoveContentContext context) =>
    context.ContentItem
      .AsContent<SecondarySiteType>()
      .When(
        secondarySite =>
          (Env.IsProduction() &&
           !SiteMeasurementSource.IsFake(
            secondarySite.Site.Value.Source.TermContentItemIds[0])) ||
          Env.IsDevelopment(),
        secondarySite =>
          Indexer
            .IndexDeviceAsync(
              SiteMeasurementSource.GetElasticsearchSource(
                secondarySite.Site.Value.Source.TermContentItemIds[0]) ??
              throw new InvalidOperationException("Invalid site source"),
              secondarySite.Site.Value.DeviceId.Text,
              new SourceDeviceData(
                ownerId: secondarySite.Site.Value.SourceData.Data
                  .FirstOrDefault(data => data.Name == "OwnerId")?.Value),
              secondarySite.Site.Value.MeasurementFrequency.Value,
              Elasticsearch.DeviceState.Removed)
            .Then(() => Logger.LogDebug(
              string.Format(
                "Removed device {0}",
                secondarySite.Site.Value.DeviceId.Text))),
        Task.CompletedTask);

  public SiteDeviceIndexer(
      IHostEnvironment env,
      ILogger<SiteDeviceIndexer> logger,
      IDeviceIndexer indexer)
  {
    Indexer = indexer;
    Logger = logger;
    Env = env;
  }

  IDeviceIndexer Indexer { get; }
  ILogger Logger { get; }
  IHostEnvironment Env { get; }
}
