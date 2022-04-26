using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Flows.Models;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterReceipt
{
  public static void AlterReceiptType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Receipt",
      type => type
        .DisplayedAs("Račun")
        .Creatable()
        .Listable()
         .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naslov")
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedDisabled,
                // TODO: check
                Pattern =
                @"
                  {%- assign receipt = ContentItem.Content.Receipt -%}
                  {%- assign partner = ContentItem.Content.Partner -%}
                  {%- assign dateFrom = receipt.DateFrom.Value | date: '%Y-%m-%d' -%}
                  {%- assign dateTo = receipt.DateTo.Value | date: '%Y-%m-%d' -%}
                  {{- partner }} {{ dateFrom }} - {{ dateTo -}}
                ",
              }))
        .Securable()
        .WithPart("Receipt",
          part => part
            .WithPosition("0")
            .WithSettings(
              new ReceiptSettings
              {
              }))
       .WithPart("Person",
          part => part
            .WithDisplayName("Partner")
            .WithDescription("Partner projekta")
            .WithPosition("3")
            .WithSettings(
              new PersonSettings
              {
              }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithDescription("Stavke mjesecnog računa")
            .WithPosition("2")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "Calculation",
                  "ReceiptItem"
                },
              }))
        .WithPart("Contact",
          part => part
            .WithDisplayName("Operater")
            .WithPosition("4")
            .WithSettings(
              new ContactSettings
              {
              })));

  public static void AlterReceiptPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Receipt",
      part => part
        .WithDisplayName("Račun")
        .WithDescription("Poslovni podaci u racunu")
        .WithField("ProjectId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Identifikator projekta")
            .WithPosition("0")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("Official",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Dužnosnik")
            .WithPosition("1")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Multiple = false,
                Required = true,
                DisplayedContentTypes = new[]
                {
                  "Member",
                  "Center"
                }
              }))
        .WithField("Contract",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Ugovor")
            .WithPosition("6")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Multiple = false,
                Required = true,
                DisplayedContentTypes = new[]
                {
                  "Contract",
                }
              }))
        .WithField("DateFrom",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum od")
            .WithPosition("8")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("DateTo",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum do")
            .WithPosition("9")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("Date",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum računa")
            .WithPosition("10")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("DeliveryDate",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum isporuke")
            .WithPosition("11")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("PaymentCurrency",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Valuta plaćanja")
            .WithPosition("12")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4098639c3zswm084zyay3je1m9",
                Required = true,
                Unique = true,
              }))
        .WithField("InTotal",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Ukupno bez PDV-a")
            .WithPosition("13")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0
              }))
        .WithField("InTotalWithTax",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Ukupno")
            .WithPosition("14")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0
              }))
        .WithField("Remark",
          field => field
            .OfType("TextField")
            .WithDisplayName("Napomena")
            .WithPosition("15")
            .WithEditor("TextArea")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false
              })));
}
