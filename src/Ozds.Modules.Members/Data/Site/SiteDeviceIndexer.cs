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
          !SiteMeasurementSource.IsFake(
            secondarySite.Site.Value.Source.TermContentItemIds[0]),
        secondarySite =>
          Indexer.IndexDeviceAsync(
            SiteMeasurementSource.GetElasticsearchSource(
              secondarySite.Site.Value.Source.TermContentItemIds[0]) ??
            throw new InvalidOperationException("Invalid site source"),
            secondarySite.Site.Value.DeviceId.Text,
            new SourceDeviceData(
              ownerId: secondarySite.Site.Value.SourceData.Data
                .FirstOrDefault(data => data.Name == "OwnerId")?.Value),
            SiteStatus.GetElasticsearchStatus(
              secondarySite.Site.Value.Status.TermContentItemIds[0])),
        Task.CompletedTask);

  public SiteDeviceIndexer(
      IDeviceIndexer indexer)
  {
    Indexer = indexer;
  }

  IDeviceIndexer Indexer { get; }
}
