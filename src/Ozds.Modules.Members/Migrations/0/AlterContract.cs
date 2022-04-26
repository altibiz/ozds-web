using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Flows.Models;
using OrchardCore.Title.Models;
using OrchardCore.Autoroute.Models;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.ContentFields.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterContract
{
  public static void AlterContractType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Contract",
      type => type
        .DisplayedAs("Ugovor")
        .Creatable()
        .Draftable()
        .Versionable()
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
                Options = TitlePartOptions.GeneratedDisabled,
                Pattern =
                @"
                  {%- assign contract = ContentItem.Content.Contract -%}
                  {%- assign contractId = contract.ContractId -%}
                  {%- assign centers = contract.Center.ContainedItemIds -%}
                  {%- assign center = centers[0] | content_item_id -%}
                  {%- assign members = contract.Center.ContainedItemIds -%}
                  {%- assign member = members[0] | content_item_id -%}
                  {%- if member -%}
                    {{- contractId }} {{ center }} {{ member -}}
                  {%- else -%}
                    {{- contractId }} {{ center -}}
                  {%- endif -%}
                ",
              }))
        .WithPart("Contract",
          part => part
            .WithDisplayName("Ugovor")
            .WithPosition("1")
            .WithSettings(
              new ContractSettings
              {
              }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stranke")
            .WithDescription("Stranke ugovora")
            .WithPosition("3")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "Person"
                },
              }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithDescription("Cjenovnik ugovora")
            .WithPosition("2")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "CatalogueItem"
                },
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithDisplayName("Ruta")
            .WithDescription("Automatski generirana ruta ugovora")
            .WithPosition("4")
            .WithSettings(
              new AutoroutePartSettings
              {
                ManageContainedItemRoutes = true,
                Pattern = @"{{ ContentItem.Content.TitlePart.Title | slugify }}"
              })));

  public static void AlterContractPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Contract",
      part => part
        .WithField("ContractId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Identifikator ugovora")
            .WithPosition("0")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("Type",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tip ugovora")
            .WithPosition("1")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                Required = true,
                Unique = true,
                TaxonomyContentItemId = "44r3168eak25bvqpbmy2fs3pns",
              }))
        .WithField("Center",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Zatvoreni distribucijski sustav")
            .WithDescription(
              "Zatvoreni distribucijski sustav " +
              "s kojim je sklopljen ugovor")
            .WithPosition("2")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Required = true,
                Multiple = false,
                DisplayedContentTypes = new[]
                {
                  "Center",
                }
              }))
        .WithField("Member",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Član")
            .WithDescription("Član s kojim je sklopljen ugovor")
            .WithPosition("3")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Required = false,
                Multiple = false,
                DisplayedContentTypes = new[]
                {
                  "Member",
                }
              }))
        .WithField("Date",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum")
            .WithDescription("Datum kada je sklopljen ugovor")
            .WithPosition("4")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("DateFrom",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum od")
            .WithDescription("Početni datum valjanosti ugovora")
            .WithPosition("5")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("DateTo",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum do")
            .WithDescription("Krajnji datum valjanosti ugovora")
            .WithPosition("6")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              })));
}
