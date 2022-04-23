using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Flows.Models;
using OrchardCore.Title.Models;
using OrchardCore.Autoroute.Models;
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
        .WithPart("Contract",
          part => part
            .WithSettings(
              new ContractSettings
              {
              }))
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedDisabled,
                Pattern =
                @"""
                  {%- assign contract = ContentItem.Content.Contract -%}
                  {%- assign centers = contract.Center.ContainedItemIds | content_item_id -%}
                  {%- assign center = centers[0] -%}
                  {{- center -}}
                """,
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithDisplayName("Ruta")
            .WithDescription("Automatski generirana ruta ugovora")
            .WithSettings(
              new AutoroutePartSettings
              {
                ManageContainedItemRoutes = true,
                Pattern = @"{{ ContentItem.Content.TitlePart.Title | slugify }}"
              }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithDescription("Cjenovnik ugovora")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "CatalogueItem"
                },
              }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stranke")
            .WithDescription("Stranke ugovora")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "Person"
                },
              })));

  public static void AlterContractPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Contract",
      part => part
        .WithField("Center",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Zatvoreni distribucijski sustav")
            .WithDescription(
              "Zatvoreni distribucijski sustav " +
              "s kojim je sklopljen ugovor")
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
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              })));
}