using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using Ozds.Util;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class ReceiptCreator : ContentHandlerBase
{
  public override async Task UpdatedAsync(UpdateContentContext context)
  {
    if (!Tenant.WasTenantActivated()) return;

    var receipt = context.ContentItem.As<Receipt>();
    if (receipt is null) return;

    using (var scope = Services.CreateAsyncScope())
    {
      var content = scope.ServiceProvider
        .GetRequiredService<IContentManager>()
        .ThrowWhenNull();

      var siteContentItemId =
        receipt.Site.ContentItemIds
          .FirstOrDefault()
          .ThrowWhenNull();
      var date = receipt.Date.Value.ThrowWhenNull();
      var dateFrom = receipt.DateFrom.Value.ThrowWhenNull();
      var dateTo = receipt.DateTo.Value.ThrowWhenNull();

      var secondarySite = await content
        .GetContentAsync<SecondarySiteType>(
          siteContentItemId)
        .ThrowWhenNull();
      var consumer = await content
        .GetContentAsync<ConsumerType>(
          secondarySite.ContainedPart.Value
          .ThrowWhenNull().ListContentItemId)
        .ThrowWhenNull();
      var center = await content
        .GetContentAsync<CenterType>(
          consumer.ContainedPart.Value
          .ThrowWhenNull().ListContentItemId)
        .ThrowWhenNull();

      var catalogue = secondarySite.Catalogue.Value.ContentItems
        .First()
        .As<Catalogue>()
        .ThrowWhenNull();
      var operatorCatalogue = await content
        .GetContentAsync<CatalogueType>(
          OperatorCatalogue.ContentItemIdFor(
            catalogue.TariffModel.TermContentItemIds.First())
            .ThrowWhenNull())
        .ThrowWhenNull()
        .Then(catalogue => catalogue.Catalogue.Value);

      var source =
        SiteMeasurementSource.GetElasticsearchSource(
            secondarySite.Site.Value.Source.TermContentItemIds
            .First())
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
          center.Center.Value.User.UserIds.FirstOrDefault(),
          center.Operator.Value.Data.Value,
          center.CenterOwner.Value.Data.Value,
          center.Title.Value.Title,
          consumer.Consumer.Value.User.UserIds.FirstOrDefault(),
          consumer.Person.Value.Data.Value,
          await CalculationData.Create(
            Taxonomy,
            date,
            beginEnergyMeasurement.Timestamp,
            endEnergyMeasurement.Timestamp,
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
    IServiceProvider services,
    IHttpContextAccessor httpContextAccessor,
    TaxonomyCacheService taxonomy,
    IReceiptMeasurementProvider measurements,
    ITenantActivationChecker tenant)
  {
    Taxonomy = taxonomy;
    Services = services;
    Measurements = measurements;
    HttpContextAccessor = httpContextAccessor;
    Env = env;
    Logger = logger;
    Tenant = tenant;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }
  private TaxonomyCacheService Taxonomy { get; }
  private IServiceProvider Services { get; }
  private IReceiptMeasurementProvider Measurements { get; }
  private IHttpContextAccessor HttpContextAccessor { get; }
  private HttpContext? HttpContext { get => HttpContextAccessor.HttpContext; }
  private ITenantActivationChecker Tenant { get; }
}
