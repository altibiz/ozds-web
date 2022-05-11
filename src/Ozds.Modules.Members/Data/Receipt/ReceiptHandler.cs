using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using OrchardCore.Lists.Models;
using YesSql;
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
                        .GetContentAsync<SecondarySiteType>(siteContentItemId),
                       await Session
                        .Query<ContentItem, PersonIndex>(person =>
                          person.SiteContentItemId == siteContentItemId)
                        .FirstOrDefaultAsync()
                        .Nullable()) switch
                      {
                        (SecondarySiteType site,
                         ContentItem consumerItem) =>
                          (site.Catalogue.Value.ContentItems
                            .FirstOrDefault()
                            ?.As<Catalogue>(),
                           consumerItem.As<Person>(),
                           await consumerItem
                            .AsReal<ContainedPart>()
                            .WhenNonNullableTask(consumer => content
                              .GetAsync(consumer.ListContentItemId)
                              .Then(centerItem =>
                                centerItem
                                  .AsContent<CenterType>()
                                  .WhenNonNullable(center =>
                                    (center.Operator.Value,
                                     center.CenterOwner.Value,
                                     center.Title.Value.Title))))) switch
                          {
                            (Catalogue catalogue,
                             Person consumer,
                             (Person @operator,
                              Person centerOwner,
                              string centerTitle)) =>
                              ((await content.GetAsync(
                                OperatorCatalogue.ContentItemIdFor(
                                  catalogue.TariffModel.TermContentItemIds
                                    .First())))
                                 .AsReal<Catalogue>(),
                               await Measurements
                                .GetEnergyMeasurementsAsync(
                                  site.Site.Value.DeviceId.Text,
                                  new()
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
                                  site.Site.Value.DeviceId.Text,
                                  new()
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
                                (Catalogue operatorCatalogue,
                                 (EnergyMeasurement energyBeginMeasurement,
                                  EnergyMeasurement endEnergyMeasurement),
                                  PowerMeasurement powerMeasurement) =>
                                  await ReceiptData.Create(
                                    Taxonomy,
                                    @operator.Data.Value,
                                    centerOwner.Data.Value,
                                    centerTitle,
                                    consumer.Data.Value,
                                    await CalculationData.Create(
                                      Taxonomy,
                                      date,
                                      site.ContentItem.ContentItemId,
                                      site.Title.Value.Title,
                                      catalogue.Data.Value,
                                      operatorCatalogue.Data.Value,
                                      energyBeginMeasurement.Energy,
                                      endEnergyMeasurement.Energy,
                                      energyBeginMeasurement.HighCostEnergy,
                                      endEnergyMeasurement.HighCostEnergy,
                                      energyBeginMeasurement.LowCostEnergy,
                                      endEnergyMeasurement.LowCostEnergy,
                                      powerMeasurement.Power,
                                      Pricing.RenewableEnergyFeePrice,
                                      Pricing.BusinessUsageFeePrice),
                                    Pricing.TaxRate),
                                _ => default
                              },
                            _ => default
                          },
                        _ => default
                      },
                    _ => default
                  };

                context.ContentItem.Apply(receipt);
              }),
        Task.CompletedTask);

  public override Task InitializedAsync(InitializingContentContext context) =>
    context.ContentItem
      .As<Receipt>()
      .With(receipt =>
        HttpContext
          .WhenNonNullable(httpContext =>
            httpContext.Request.Query["siteContentItemId"]
              .FirstOrDefault()
              .With(consumerContentItemId =>
                {
                  receipt.Site.ContentItemIds =
                    new[]
                    {
                      consumerContentItemId
                    };
                  receipt.Date.Value = DateTime.UtcNow;

                  context.ContentItem.Apply(receipt);
                })))
      .Return(Task.CompletedTask);

  public ReceiptHandler(
    TaxonomyCacheService taxonomy,
    YesSql.ISession session,
    IServiceProvider services,
    IHttpContextAccessor httpContextAccessor,
    IReceiptMeasurementProvider measurements)
  {
    Taxonomy = taxonomy;
    Services = services;
    Session = session;
    Measurements = measurements;
    HttpContextAccessor = httpContextAccessor;
  }

  private TaxonomyCacheService Taxonomy { get; }
  private IServiceProvider Services { get; }
  private YesSql.ISession Session { get; }
  private IReceiptMeasurementProvider Measurements { get; }
  private IHttpContextAccessor HttpContextAccessor { get; }
  private HttpContext? HttpContext { get => HttpContextAccessor.HttpContext; }
}
