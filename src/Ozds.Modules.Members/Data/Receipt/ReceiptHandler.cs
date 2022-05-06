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
                          (await Elasticsearch
                            .GetMonthlyEnergyMeasurementsAsync(
                              date.Year,
                              date.AddMonths(-1).Month),
                           await Elasticsearch
                            .GetMonthlyPowerMeasurementAsync(
                              date.Year,
                              date.AddMonths(-1).Month))
                          switch
                          {
                            ((Measurement beginEnergy,
                              Measurement endEnergy),
                             Measurement power) =>
                              ReceiptData.FromCalculation(
                                consumer.Data.Value,
                                @operator.Data.Value,
                                CalculationData.Create(
                                  date,
                                  site.ContentItem.ContentItemId,
                                  catalogue.Data.Value,
                                  default(CatalogueData),
                                  beginEnergy.Data.energyIn,
                                  endEnergy.Data.energyIn,
                                  beginEnergy.Data.energyIn_T1,
                                  endEnergy.Data.energyIn_T1,
                                  beginEnergy.Data.energyIn_T2,
                                  endEnergy.Data.energyIn_T2,
                                  power.Data.powerIn),
                                TariffItem.TaxRate,
                                TariffItem.RenewableEnergyFeePrice,
                                TariffItem.BusinessUsageFeePrice),
                            _ => null as ReceiptData?
                          },
                        _ => null
                      },
                    _ => null
                  },
                _ => null
              };
          });

  public ReceiptHandler(
    IContentManager content,
    ISession session,
    Elasticsearch.IClient elasticsearch)
  {
    Content = content;
    Session = session;
    Elasticsearch = elasticsearch;
  }

  private IContentManager Content { get; }
  private ISession Session { get; }
  private Elasticsearch.IClient Elasticsearch { get; }
}
