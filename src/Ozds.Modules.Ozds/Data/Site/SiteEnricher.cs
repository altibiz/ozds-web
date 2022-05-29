using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using Ozds.Extensions;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class SiteEnricher : ContentHandlerBase
{
  public override Task UpdatedAsync(
      UpdateContentContext context) =>
    Enrich(context.ContentItem);

  private async Task Enrich(ContentItem item)
  {
    if (!Tenant.WasTenantActivated())
    {
      return;
    }

    var site = item.As<Site>();
    if (site is null)
    {
      return;
    }

    var content = Services
      .GetRequiredService<IContentManager>()
      .ThrowWhenNull();

    var secondarySite = item
      .AsContent<SecondarySiteType>()
      .ThrowWhenNull();

    var ownerContentItemId = secondarySite.ContainedPart.Value
      .ThrowWhenNull().ListContentItemId;
    var owner = await content
      .GetContentAsync<ConsumerType>(ownerContentItemId)
      .ThrowWhenNull();

    var centerContentItemId = owner.ContainedPart.Value
      .ThrowWhenNull().ListContentItemId;
    var center = await content
      .GetContentAsync<CenterType>(centerContentItemId)
      .ThrowWhenNull();

    site.Data =
      new SiteData
      {
        OperatorName = center.Operator.Value.Name.Text,
        OperatorOib = center.Operator.Value.Oib.Text,

        CenterOwnerName = center.CenterOwner.Value.Name.Text,
        CenterOwnerOib = center.CenterOwner.Value.Oib.Text,
        CenterContentItemId = center.ContentItem.ContentItemId,
        CenterUserId = center.Center.Value.User.UserIds.FirstOrDefault(),

        OwnerName = owner.Person.Value.Name.Text,
        OwnerOib = owner.Person.Value.Name.Text,
        OwnerContentItemId = owner.ContentItem.ContentItemId,
        OwnerUserId = owner.Consumer.Value.User.UserIds.FirstOrDefault(),
      };
    site.Apply();

    Logger.LogDebug(
      string.Format(
        "Enricheded site {0} with device {1}",
        site.ContentItem.ContentItemId,
        secondarySite.Site.Value.SourceDeviceId.Text));
  }


  public SiteEnricher(
    IHostEnvironment env,
    ILogger<SiteEnricher> logger,
    IServiceProvider services,
    ITenantActivationChecker tenant)
  {
    Services = services;
    Env = env;
    Logger = logger;
    Tenant = tenant;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }
  private IServiceProvider Services { get; }
  private ITenantActivationChecker Tenant { get; }
}
