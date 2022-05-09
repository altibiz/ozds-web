using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using OrchardCore.Lists.Models;
using OrchardCore.Flows.Models;
using YesSql;
using Ozds.Util;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Members;

public class ReceiptHandler : ContentHandlerBase
{
  public override Task UpdatedAsync(UpdateContentContext context) =>
    context.ContentItem
      .As<Receipt>()
      .WithTask(
        async receipt =>
          {
            receipt.Data =
              (receipt.Site.ContentItemIds.FirstOrDefault(),
               receipt.Date.Value)
              switch
              {
                (string siteContentItemId,
                 DateTime date) =>
                  (await Content
                    .GetAsync(siteContentItemId),
                   await Session
                    .Query<ContentItem, PersonIndex>(person =>
                      person.SiteContentItemId == siteContentItemId)
                    .FirstOrDefaultAsync()
                    .Nullable())
                  switch
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
                        .WhenNonNullableTask(consumer => Content
                          .GetAsync(consumer.ListContentItemId)
                          .Then(centerItem => centerItem
                            .Get<Person>("Operator")
                            .Nullable())))
                      switch
                      {
                        (Site site,
                         Catalogue catalogue,
                         Person consumer,
                         Person @operator) =>
                          (await Measurements
                            .GetEnergyMeasurementsAsync(
                              site.DeviceId.Text,
                              new Elasticsearch.Period
                              {
                                From = date.AddMonths(-1),
                                To = date
                              }),
                           await Measurements
                            .GetPowerMeasurementAsync(
                              site.DeviceId.Text,
                              new Elasticsearch.Period
                              {
                                From = date.AddMonths(-1),
                                To = date
                              }))
                          switch
                          {
                            ((EnergyMeasurement beginEnergy,
                              EnergyMeasurement endEnergy),
                              PowerMeasurement power) =>
                              ReceiptData.FromCalculation(
                                consumer.Data.Value,
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
          });

  public ReceiptHandler(
    IContentManager content,
    ISession session,
    IReceiptMeasurementProvider measurements)
  {
    Content = content;
    Session = session;
    Measurements = measurements;
  }

  private IContentManager Content { get; }
  private ISession Session { get; }
  private IReceiptMeasurementProvider Measurements { get; }
}
