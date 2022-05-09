using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using OrchardCore.Lists.Models;
using OrchardCore.Flows.Models;
using YesSql;
using Newtonsoft.Json.Linq;
using Ozds.Util;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Members;

public class ReceiptHandler : ContentHandlerBase
{
  public override Task UpdatedAsync(UpdateContentContext context) =>
    Services
      .GetRequiredService<IContentManager>()
      .WhenNonNullable(content =>
        context.ContentItem
          .As<Receipt>()
          .WithTask(
            async receipt =>
              {
                receipt.Data =
                  (receipt.Site.ContentItemIds.FirstOrDefault(),
                   receipt.Date.Value) switch
                  {
                    (string siteContentItemId,
                     DateTime date) =>
                      (await content
                        .GetAsync(siteContentItemId),
                      await Session
                        .Query<ContentItem, PersonIndex>(person =>
                          person.SiteContentItemId == siteContentItemId)
                        .FirstOrDefaultAsync()
                        .Nullable()) switch
                      {
                        (ContentItem siteItem,
                         ContentItem consumerItem) =>
                          (siteItem.AsReal<Site>(),
                           siteItem
                            .Get<BagPart>("Catalogue")
                            .Nullable()
                            ?.ContentItems
                            .FirstOrDefault()
                            ?.As<Catalogue>(),
                           consumerItem.As<Person>(),
                           await consumerItem
                            .AsReal<ContainedPart>()
                            .WhenNonNullableTask(consumer => content
                              .GetAsync(consumer.ListContentItemId)
                              .Then(centerItem =>
                                (centerItem.Get<Person>("Operator"),
                                 centerItem.Get<Person>("CenterOwner"))
                                .Nullable()))) switch
                          {
                            (Site site,
                             Catalogue catalogue,
                             Person consumer,
                             (Person centerOwner,
                              Person @operator)) =>
                              (await Measurements
                                .GetEnergyMeasurementsAsync(
                                  site.DeviceId.Text,
                                  new Elasticsearch.Period
                                  {
                                    From = new DateTime(
                                        date.Year,
                                        date.AddMonths(-1).Month,
                                        1),
                                    To = new DateTime(
                                        date.Year,
                                        date.Month,
                                        1)
                                  }),
                              await Measurements
                                .GetPowerMeasurementAsync(
                                  site.DeviceId.Text,
                                  new Elasticsearch.Period
                                  {
                                    From = new DateTime(
                                        date.Year,
                                        date.AddMonths(-1).Month,
                                        1),
                                    To = new DateTime(
                                        date.Year,
                                        date.Month,
                                        1)
                                  })) switch
                              {
                                ((EnergyMeasurement beginEnergy,
                                  EnergyMeasurement endEnergy),
                                  PowerMeasurement power) =>
                                  ReceiptData.FromCalculation(
                                    consumer.Data.Value,
                                    centerOwner.Data.Value,
                                    @operator.Data.Value,
                                    CalculationData.Create(
                                      date,
                                      site.ContentItem.ContentItemId,
                                      catalogue.Data.Value,
                                      default(CatalogueData),
                                      beginEnergy.Energy,
                                      endEnergy.Energy,
                                      beginEnergy.HighCostEnergy,
                                      endEnergy.HighCostEnergy,
                                      beginEnergy.LowCostEnergy,
                                      endEnergy.LowCostEnergy,
                                      power.Power),
                                    Pricing.TaxRate)
                              },
                            _ => default
                          },
                        _ => default
                      },
                    _ => default
                  };
              }),
        Task.CompletedTask);

  public override Task InitializedAsync(InitializingContentContext context) =>
    context.ContentItem
      .AsContent<ReceiptType>()
      .With(receipt =>
        HttpContext
          .WhenNonNullable(httpContext =>
            httpContext.Request.Query["siteContentItemId"]
              .FirstOrDefault()
              .With(consumerContentItemId =>
                {
                  // TODO: cleaner way to do this?
                  var now = DateTime.UtcNow;

                  // NOTE: needed for shape generation
                  receipt.ContentItem.Content.Receipt.Site.ContentItemIds =
                    new JArray
                    {
                      consumerContentItemId
                    };
                  receipt.ContentItem.Content.Receipt.Date.Value = now;

                  // NOTE: needed for model generation
                  receipt.Receipt.Value.Site.ContentItemIds =
                    new[]
                    {
                      consumerContentItemId
                    };
                  receipt.Receipt.Value.Date.Value = now;
                })))
      .Return(Task.CompletedTask);

  public ReceiptHandler(
    YesSql.ISession session,
    IServiceProvider services,
    IHttpContextAccessor httpContextAccessor,
    IReceiptMeasurementProvider measurements)
  {
    Services = services;
    Session = session;
    Measurements = measurements;
    HttpContextAccessor = httpContextAccessor;
  }

  private IServiceProvider Services { get; }
  private YesSql.ISession Session { get; }
  private IReceiptMeasurementProvider Measurements { get; }
  private IHttpContextAccessor HttpContextAccessor { get; }
  private HttpContext? HttpContext { get => HttpContextAccessor.HttpContext; }
}
