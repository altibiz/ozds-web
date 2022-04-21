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
        .Securable()
        .WithPart("Receipt",
          part => part
            .WithPosition("0")
            .WithSettings(
              new ReceiptSettings
              {
              }))
        .WithPart("TitlePart",
          part => part
            .WithPosition("1")
            .WithDisplayName("Naslov")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedDisabled,
                // TODO: check
                Pattern =
                @"
                  {%- assign receipt = ContentItem.Content.Receipt -%}
                  {%- assign partner = receipt.Partner.Text -%}
                  {%- assign dateFrom = receipt.DateFrom.Value | date: '%Y-%m-%d' -%}
                  {%- assign dateTo = receipt.DateTo.Value | date: '%Y-%m-%d' -%}
                  {{- partner }} {{ dateFrom }} - {{ dateTo -}}
                ",
              }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithDescription("Stavke mjesecnog računa.")
            .WithPosition("2")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[] {
                  "Calculation",
                  "ReceiptItem"
                },
              })));

  public static void AlterReceiptPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Receipt",
      part => part
        .WithDisplayName("Račun")
        .WithDescription("Poslovni podaci u racunu")
        .WithField("Official",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Dužnosnik")
            .WithPosition("0")
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
        .WithField("DocumentId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Broj dokumenta")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("Partner",
          field => field
            .OfType("TextField")
            .WithDisplayName("Partner")
            .WithPosition("2")
              .WithSettings(
                new TextFieldSettings
                {
                  Required = true
                }))
        .WithField("PartnerAdress",
          field => field
            .OfType("TextField")
            .WithDisplayName("Adresa partnera")
            .WithPosition("3")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("PartnerPostalCode",
          field => field
            .OfType("TextField")
            .WithDisplayName("Poštanski broj partnera")
            .WithPosition("4")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("PartnerOIB",
          field => field
            .OfType("TextField")
            .WithDisplayName("OIB patnera")
            .WithPosition("5")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("DeliveryDate",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum isporuke")
            .WithPosition("6")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("PaymentCurrency",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Valuta plaćanja")
            .WithPosition("7")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4098639c3zswm084zyay3je1m9",
                Required = true,
                Unique = true,
              }))
        .WithField("ContractDate",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum ugovora")
            .WithPosition("8")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("ContractId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Broj ugovora")
            .WithPosition("9")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("ProjectId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Šifra projekta")
            .WithPosition("10")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("DateFrom",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum od")
            .WithPosition("11")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("DateTo",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum do")
            .WithPosition("12")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("InTotal",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Ukupno")
            .WithPosition("13")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0
              }))
        .WithField("Tax",
          field => field
            .OfType("NumericField")
            .WithDisplayName("PDV")
            .WithPosition("14")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0
              }))
        .WithField("InTotalWithTax",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Ukupni iznos")
            .WithPosition("15")
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
            .WithPosition("16")
            .WithEditor("TextArea")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false
              }))
        .WithField("OperatorName",
          field => field
            .OfType("TextField")
            .WithDisplayName("Ime operatera")
            .WithPosition("17")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("OperatorSurname",
          field => field
            .OfType("TextField")
            .WithDisplayName("Prezime operatera")
            .WithPosition("18")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("ReceiptDate",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum računa")
            .WithPosition("19")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              })));
}
