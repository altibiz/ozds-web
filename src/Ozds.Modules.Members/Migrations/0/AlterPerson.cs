using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterPerson
{
  public static void AlterPersonPart(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterPartDefinition("PersonPart",
          part =>
              part.Attachable()
                  .WithDisplayName("Osoba")
                  .WithDescription("Poslovni i kontakt podaci osobe.")
                  .WithField(
                      "Name", field => field.OfType("TextField")
                                           .WithDisplayName("Ime")
                                           .WithPosition("1")
                                           .WithSettings(new TextFieldSettings
                                           {
                                             Required = true,
                                           }))
                  .WithField("Surname", field => field.OfType("TextField")
                                                     .WithDisplayName("Prezime")
                                                     .WithPosition("1"))
                  .WithField(
                      "Oib", field => field.OfType("TextField")
                                          .WithDisplayName("OIB")
                                          .WithPosition("0")
                                          .WithSettings(new TextFieldSettings
                                          {
                                            Required = true,
                                          }))
                  .WithField("Address", field => field.OfType("TextField")
                                                     .WithDisplayName("Adresa")
                                                     .WithPosition("4"))
                  .WithField(
                      "City", field => field.OfType("TextField")
                                           .WithDisplayName("Grad/Općina")
                                           .WithPosition("5"))
                  .WithField("Phone", field => field.OfType("TextField")
                                                   .WithDisplayName("Telefon")
                                                   .WithEditor("Tel")
                                                   .WithPosition("7"))
                  .WithField("County",
                      field => field.OfType("TaxonomyField")
                                   .WithDisplayName("Županija")
                                   .WithEditor("Tags")
                                   .WithDisplayMode("Tags")
                                   .WithPosition("6")
                                   .WithSettings(new TaxonomyFieldSettings
                                   {
                                     TaxonomyContentItemId =
                                         "4d0dew9ar7h9nsbpcs7jg2egwe",
                                     Unique = true,
                                   })
                                   .WithSettings(
                                       new TaxonomyFieldTagsEditorSettings
                                       {
                                         Open = false,
                                       }))
                  .WithField("Email",
                      field => field.OfType("TextField")
                                   .WithDisplayName("Email")
                                   .WithEditor("Email")
                                   .WithPosition("11")
                                   .WithSettings(new TextFieldSettings
                                   {
                                     Required = true,
                                   }))
                  .WithField("ContribType",
                      field =>
                          field.OfType("TaxonomyField")
                              .WithDisplayName(
                                  "Aktivan/neaktivan doprinos radu udruge?")
                              .WithEditor("Tags")
                              .WithDisplayMode("Tags")
                              .WithPosition("10")
                              .WithSettings(new TaxonomyFieldSettings
                              {
                                TaxonomyContentItemId =
                                    "4k7n3gw5wm7660vqpm0805hedy",
                                Unique = true,
                              })
                              .WithSettings(
                                  new TaxonomyFieldTagsEditorSettings
                                  {
                                    Open = false,
                                  }))
                  .WithField("Skills",
                      field => field.OfType("TextField")
                                   .WithDisplayName("Vještine i znanja")
                                   .WithEditor("TextArea")
                                   .WithPosition("9"))
                  .WithField(
                      "MiddleName", field => field.OfType("TextField")
                                                 .WithDisplayName("Srednje ime")
                                                 .WithPosition("3"))
                  .WithField("Naziv", field => field.OfType("TextField")
                                                   .WithDisplayName("Naziv")
                                                   .WithPosition("1"))
                  .WithField("Lgal", field => field.OfType("BooleanField")
                                                  .WithDisplayName("Pravna")
                                                  .WithPosition("5")));
}
