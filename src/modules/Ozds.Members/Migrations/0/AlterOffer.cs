using OrchardCore.Autoroute.Models;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Media.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Title.Models;

namespace Ozds.Members.M0;

public static partial class AlterOffer
{
  public static void AlterOfferType(this IContentDefinitionManager
          content) => content.AlterTypeDefinition("Offer",
      type =>
          type.DisplayedAs("Ponuda")
              .Creatable()
              .Listable()
              .Securable()
              .WithPart("Offer", part => part.WithPosition("1"))
              .WithPart(
                  "TitlePart", part => part.WithPosition("0").WithSettings(
                                   new TitlePartSettings
                                   {
                                     Options = TitlePartOptions.EditableRequired
                                   }))
              .WithPart("AutoroutePart",
                  part => part.WithPosition("2").WithSettings(
                      new AutoroutePartSettings
                      {
                        Pattern =
                            "offers-{{ ContentItem.DisplayText | slugify }}",
                      })));

  public static void AlterOfferPart(this IContentDefinitionManager
          content) => content.AlterPartDefinition("Offer",
      part =>
          part.WithField("Category",
                  field => field.OfType("TaxonomyField")
                               .WithDisplayName("Kategorija")
                               .WithEditor("Tags")
                               .WithDisplayMode("Tags")
                               .WithPosition("0")
                               .WithSettings(new TaxonomyFieldSettings
                               {
                                 Required = true,
                                 TaxonomyContentItemId =
                                     "4a6d7mtpab04yt9yedrsardz4r",
                                 Unique = true,
                               })
                               .WithSettings(
                                   new TaxonomyFieldTagsEditorSettings
                                   {
                                     Open = false,
                                   }))
              .WithField("ShortDescription",
                  field => field.OfType("TextField")
                               .WithDisplayName("Kratki opis")
                               .WithPosition("1")
                               .WithSettings(new TextFieldSettings
                               {
                                 Required = true,
                               }))
              .RemoveField("LongDescription") // some additions
              .WithField("LongDescription",
                  field => field.OfType("HtmlField")
                               .WithDisplayName(
                                   "Širi opis i dodatne specifikacije")
                               .WithEditor("Wysiwyg")
                               .WithPosition("2"))
              .WithField("FeaturedImage",
                  field => field.OfType("MediaField")
                               .WithDisplayName("Fotografija")
                               .WithEditor("Attached")
                               .WithPosition("3")
                               .WithSettings(new MediaFieldSettings
                               {
                                 Multiple = false,
                                 AllowMediaText = false,
                               }))
              .WithField("Company",
                  field => field.OfType("ContentPickerField")
                               .WithPosition("4")
                               .WithDisplayName("Company")
                               .WithSettings(new ContentPickerFieldSettings
                               {
                                 DisplayedContentTypes = new[] { "Company" },
                               }))
              .WithField("PersonName",
                  field => field.OfType("TextField")
                               .WithDisplayName(
                                   "Naziv pravne ili fizičke osobe")
                               .WithPosition("5")
                               .WithSettings(new TextFieldSettings
                               {
                                 Required = true,
                               }))
              .WithField("ContactPerson",
                  field => field.OfType("TextField")
                               .WithDisplayName("Kontakt osoba")
                               .WithPosition("6")
                               .WithSettings(new TextFieldSettings
                               {
                                 Required = true,
                               }))
              .WithField(
                  "Email", field => field.OfType("TextField")
                                        .WithDisplayName("Email")
                                        .WithPosition("7")
                                        .WithSettings(new TextFieldSettings
                                        {
                                          Required = true,
                                        }))
              .WithField("Phone", field => field.OfType("TextField")
                                               .WithDisplayName("Telefon")
                                               .WithPosition("8"))
              .WithField("Address", field => field.OfType("TextField")
                                                 .WithDisplayName("Adresa")
                                                 .WithPosition("9"))
              .WithField(
                  "Web", field => field.OfType("LinkField")
                                      .WithDisplayName("Web")
                                      .WithPosition("10")
                                      .WithSettings(new LinkFieldSettings
                                      {
                                        LinkTextMode = LinkTextMode.Url,
                                      }))
              .WithField("Instagram",
                  field => field.OfType("LinkField")
                               .WithDisplayName("Instagram")
                               .WithPosition("11")
                               .WithSettings(new LinkFieldSettings
                               {
                                 LinkTextMode = LinkTextMode.Url,
                               }))
              .WithField(
                  "Facebook", field => field.OfType("LinkField")
                                           .WithDisplayName("Facebook")
                                           .WithPosition("12")
                                           .WithSettings(new LinkFieldSettings
                                           {
                                             LinkTextMode = LinkTextMode.Url,
                                           }))
              .WithField(
                  "LinkedIn", field => field.OfType("LinkField")
                                           .WithDisplayName("LinkedIn")
                                           .WithPosition("13")
                                           .WithSettings(new LinkFieldSettings
                                           {
                                             LinkTextMode = LinkTextMode.Url,
                                           }))
              .WithField("YoutubeVideoId",
                  field => field.OfType("TextField")
                               .WithDisplayName("Youtube video ID")
                               .WithPosition("14")));
}
