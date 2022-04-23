using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Flows.Models;
using OrchardCore.Title.Models;
using OrchardCore.ContentFields.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCalculation
{
  public static void AlterCalculationType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Calculation",
      type => type
        .DisplayedAs("Obračun")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("Calculation",
          part => part
            .WithPosition("0")
            .WithSettings(
              new CalculationSettings
              {
              }))
        .WithPart("TitlePart",
          part => part
            .WithPosition("1")
            .WithDisplayName("Naziv")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedDisabled,
                // TODO: check
                Pattern =
                @"""
                  {%- assign receipt = ContentItem.Content.Receipt -%}
                  {%- assign sites = calc.Site.ContainedItemIds | content_item_id -%}
                  {%- assign site = site[0] -%}
                  {%- assign source = site.Source -%}
                  {%- assign deviceId = site.DeviceId -%}
                  {%- assign dateFrom = calc.DateFrom.Value | date: '%Y-%m-%d' -%}
                  {%- assign dateTo = calc.DateTo.Value | date: '%Y-%m-%d' -%}
                  {{- source }} {{ deviceId }} {{ dateFrom }} - {{ dateTo -}}
                """,
              }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithDescription("Stavke mjesečnog obračuna")
            .WithPosition("2")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "CalculationItem"
                },
              })));

  public static void AlterCalculationPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Calculation",
      part => part
        .WithField("Site",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Obračunsko mjerno mjesto")
            .WithPosition("0")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Multiple = false,
                Required = true,
                DisplayedContentTypes = new[]
                {
                  "Site",
                }
              }))
        .WithField("DateFrom",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum od")
            .WithDescription("Početni datum mjerenja")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("DateTo",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum do")
            .WithDescription("Krajnji datum mjerenja")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("MeasurementServiceFee",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Naknada za mjernu uslugu")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0
              }))
        .WithField("InTotal",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Ukupno")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0
              })));
}
