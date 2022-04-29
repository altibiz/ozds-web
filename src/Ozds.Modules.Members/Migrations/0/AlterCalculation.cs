using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
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
            .WithPosition("0")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedHidden,
                Pattern =
                @"
{%- assign calc = ConntentItem.Content.Calculation -%}
{%- assign site = calc.Site.ContainedItemIds[0] | content_item_id -%}
{%- assign tariffModel = calc.TariffModel.TermContentItemIds[0] | content_item_id -%}
{%- assign dateFrom = calc.DateFrom.Value | date: '%Y-%m-%d' -%}
{%- assign dateTo = calc.DateTo.Value | date: '%Y-%m-%d' -%}
{{- site }} {{ tariffModel }} {{ dateFrom }} - {{ dateTo -}}
                ",
              }))
        .WithPart("Calculation",
          part => part
            .WithDisplayName("Obračun")
            .WithPosition("1")
            .WithSettings(
              new CalculationSettings
              {
              }))
        .WithPart("UsageExpenditure", "Expenditure",
          part => part
            .WithDisplayName("Troškovi korištenja mreže ZDS-a")
            .WithPosition("2"))
        .WithPart("SupplyExpenditure", "Expenditure",
          part => part
            .WithDisplayName("Troškovi opskrbe električnom energijom")
            .WithPosition("3")));


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
        .WithField("TariffModel",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tarifni model")
            .WithPosition("1")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                Unique = true,
                Required = true,
                TaxonomyContentItemId = ""
              }))
        .WithField("DateFrom",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum od")
            .WithDescription("Početni datum mjerenja")
            .WithPosition("2")
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
            .WithPosition("3")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("MeasurementServiceFee",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Naknada za mjernu uslugu")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0
              })));
}
