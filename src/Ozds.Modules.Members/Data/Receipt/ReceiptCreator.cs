using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using YesSql;
using Ozds.Util;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Members;

// TODO: fix sites without consumer
public class ReceiptCreator : ContentHandlerBase
{
  public override async Task UpdatedAsync(UpdateContentContext context)
  {
    var content =
      Services
        .GetRequiredService<IContentManager>()
        .ThrowWhenNull();

    var receipt =
      context.ContentItem
        .As<Receipt>();
    if (receipt is null) return;

    var siteContentItemId =
      receipt.Site.ContentItemIds
        .FirstOrDefault()
        .ThrowWhenNull();
    var date = receipt.Date.Value.ThrowWhenNull();
    var dateFrom = receipt.DateFrom.Value.ThrowWhenNull();
    var dateTo = receipt.DateTo.Value.ThrowWhenNull();

    var secondarySite =
      await content
        .GetContentAsync<SecondarySiteType>(siteContentItemId)
        .ThrowWhenNull();
    var consumer =
      await Session
        .Query<ContentItem, PersonIndex>(person =>
            person.SiteContentItemId == siteContentItemId)
        .FirstOrDefaultAsync()
        .Nullable()
        .ThrowWhenNull()
        .Then(item => item.AsContent<ConsumerType>())
        .ThrowWhenNull();
    var center =
      await content
        .GetAsync(
            consumer.ContainedPart.Value.ThrowWhenNull().ListContentItemId)
        .ThrowWhenNull()
        .Then(item => item.AsContent<CenterType>())
        .ThrowWhenNull();

    var catalogue =
      secondarySite.Catalogue.Value.ContentItems
        .FirstOrDefault()
        .ThrowWhenNull()
        .As<Catalogue>()
        .ThrowWhenNull();
    var operatorCatalogue =
      await content
        .GetAsync(
          OperatorCatalogue.ContentItemIdFor(
            catalogue.TariffModel.TermContentItemIds.First()))
        .ThrowWhenNull()
        .Then(item => item.As<Catalogue>())
        .Nullable()
        .ThrowWhenNull();

    var source =
      SiteMeasurementSource.GetElasticsearchSource(
          secondarySite.Site.Value.Source.TermContentItemIds[0])
        .ThrowWhenNull();
    var deviceId = secondarySite.Site.Value.DeviceId.Text;
    var (beginEnergyMeasurement, endEnergyMeasurement) =
      await Measurements
        .GetEnergyMeasurementsAsync(
          source,
          deviceId,
          new()
          {
            From = dateFrom,
            To = dateTo
          });
    var powerMeasurement =
      await Measurements
        .GetPowerMeasurementAsync(
          source,
          deviceId,
          new()
          {
            From = dateFrom,
            To = dateTo
          });

    receipt.Data =
      await ReceiptData.Create(
        Taxonomy,
        center.Operator.Value.Data.Value,
        center.CenterOwner.Value.Data.Value,
        center.Title.Value.Title,
        consumer.Person.Value.Data.Value,
        await CalculationData.Create(
          Taxonomy,
          date,
          beginEnergyMeasurement.Date,
          endEnergyMeasurement.Date,
          secondarySite.ContentItem.ContentItemId,
          secondarySite.Title.Value.Title,
          catalogue.Data.Value,
          operatorCatalogue.Data.Value,
          beginEnergyMeasurement.Energy,
          endEnergyMeasurement.Energy,
          beginEnergyMeasurement.HighCostEnergy,
          endEnergyMeasurement.HighCostEnergy,
          beginEnergyMeasurement.LowCostEnergy,
          endEnergyMeasurement.LowCostEnergy,
          powerMeasurement.Power,
          Pricing.RenewableEnergyFeePrice,
          Pricing.BusinessUsageFeePrice),
        Pricing.TaxRate);

    context.ContentItem.Apply(receipt);

    Logger.LogDebug(
      string.Format(
        "Enriched receipt for {0} from {1} to {2}",
        secondarySite.Site.Value.DeviceId.Text,
        dateFrom.ToShortDateString(),
        dateTo.ToShortDateString()));
  }

  public override Task InitializedAsync(InitializingContentContext context) =>
    context.ContentItem
      .As<Receipt>()
      .With(receipt =>
        HttpContext
          .WhenNonNullable(httpContext =>
            httpContext.Request.Query["siteContentItemId"]
              .FirstOrDefault()
              .With(siteContentItemId =>
                {
                  var now = DateTime.UtcNow;

                  receipt.Site.ContentItemIds =
                    new[]
                    {
                      siteContentItemId
                    };
                  receipt.Date.Value = now;
                  if (Env.IsDevelopment())
                  {
                    receipt.DateFrom.Value = new DateTime(
                        now.Year,
                        now.Month,
                        1);
                    receipt.DateTo.Value = new DateTime(
                        now.Year,
                        now.AddMonths(1).Month,
                        1);
                  }
                  else
                  {
                    receipt.DateFrom.Value = new DateTime(
                        now.Year,
                        now.AddMonths(-1).Month,
                        1);
                    receipt.DateTo.Value = new DateTime(
                        now.Year,
                        now.Month,
                        1);
                  }

                  context.ContentItem.Apply(receipt);

                  Logger.LogDebug(
                      $"Prepopulated receipt for {siteContentItemId}");
                })))
      .Return(Task.CompletedTask);

  public ReceiptCreator(
    IHostEnvironment env,
    ILogger<ReceiptCreator> logger,
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
    Env = env;
    Logger = logger;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }
  private TaxonomyCacheService Taxonomy { get; }
  private IServiceProvider Services { get; }
  private YesSql.ISession Session { get; }
  private IReceiptMeasurementProvider Measurements { get; }
  private IHttpContextAccessor HttpContextAccessor { get; }
  private HttpContext? HttpContext { get => HttpContextAccessor.HttpContext; }
}
