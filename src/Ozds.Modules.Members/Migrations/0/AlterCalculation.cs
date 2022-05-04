using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

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
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedHidden,
                Pattern =
                @"
{%- assign calc = ContentItem.Content.Calculation -%}
{%- assign siteId = calc.Site.ContainedItemIds[0] -%}
{%- assign site = siteId | content_item_id -%}
{%- assign tariffModelId = calc.TariffModel.TermContentItemIds[0] -%}
{%- assign tariffModel = tariffModelId | content_item_id -%}
{%- assign dateFrom = calc.DateFrom.Value | date: '%Y-%m-%d' -%}
{%- assign dateTo = calc.DateTo.Value | date: '%Y-%m-%d' -%}
{{- site }} {{ tariffModel }} {{ dateFrom }} - {{ dateTo -}}
                ",
              }))
        .WithPart("Calculation",
          part => part
            .WithDisplayName("Obračun")
            .WithPosition("2")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("UsageExpenditure", "BagPart",
          part => part
            .WithDisplayName("Troškovi korištenja mreže ZDS-a")
            .WithPosition("3")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes =
                new[]
                {
                  "Expenditure"
                }
              }))
        .WithPart("SupplyExpenditure", "BagPart",
          part => part
            .WithDisplayName("Troškovi opskrbe električnom energijom")
            .WithPosition("4")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes =
                new[]
                {
                  "Expenditure"
                }
              })));


  public static void AlterCalculationPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Calculation",
      part => part
        .WithField("Site",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Obračunsko mjerno mjesto")
            .WithPosition("1")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Multiple = false,
                Required = true,
                DisplayedContentTypes = new[]
                {
                  "SecondarySite",
                }
              }))
        .WithField("TariffModel",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tarifni model")
            .WithPosition("2")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                Unique = true,
                Required = true,
                TaxonomyContentItemId = "42d7a5kashgdgztx5ehjb4deca"
              }))
        .WithField("DateFrom",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum od")
            .WithDescription("Početni datum mjerenja")
            .WithPosition("3")
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
            .WithPosition("4")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("MeasurementServiceFee",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Naknada za mjernu uslugu")
            .WithPosition("5")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2
              })));
}
