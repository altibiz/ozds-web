using Ozds.Users.Persons;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Media.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Title.Models;

namespace Ozds.Users.M0;

public static partial class AlterCompany
{
  public static void AlterCompanyType(this IContentDefinitionManager
          content) => content.AlterTypeDefinition("Company",
      type =>
          type.DisplayedAs("Tvrtka")
              .Creatable()
              .Listable()
              .Securable()
              .WithPart(
                  "PersonPart", part => part.WithPosition("0").WithSettings(
                                    new PersonPartSettings
                                    {
                                      Type = PersonType.Legal,
                                    }))
              .WithPart("Company", part => part.WithPosition("2"))
              .WithPart("AliasPart", part => part.WithPosition("3"))
              .WithPart("TitlePart",
                  part => part.WithPosition("1").WithSettings(
                      new TitlePartSettings
                      {
                        Options = TitlePartOptions.GeneratedDisabled,
                        Pattern =
                            "{{ ContentItem.Content.PersonPart.Name.Text }}",
                      })));

  public static void AlterCompanyPart(this IContentDefinitionManager content) =>
      content.AlterPartDefinition("Company",
          part => part.WithField("AuthorizedRep",
                          field => field.OfType("TextField")
                                       .WithDisplayName(
                                           "Ovlaštena osoba za zastupanje")
                                       .WithPosition("1")
                                       .WithSettings(new TextFieldSettings
                                       {
                                         Required = true,
                                       }))
                      .WithField("Revenue2019",
                          field => field.OfType("NumericField")
                                       .WithDisplayName("Promet u 2019")
                                       .WithPosition("2"))
                      .WithField("EmployeeNumber",
                          field => field.OfType("NumericField")
                                       .WithDisplayName("Broj zaposlenih")
                                       .WithPosition("3"))
                      .WithField("OrganisationType",
                          field => field.OfType("TaxonomyField")
                                       .WithDisplayName(
                                           "Vrsta organizacije/Pravni oblik")
                                       .WithEditor("Tags")
                                       .WithDisplayMode("Tags")
                                       .WithPosition("4")
                                       .WithSettings(new TaxonomyFieldSettings
                                       {
                                         Required = true,
                                         TaxonomyContentItemId =
                                             "4gy5x0s0wck1p2k2mv19pmwsxw",
                                         Unique = true,
                                       })
                                       .WithSettings(
                                           new TaxonomyFieldTagsEditorSettings
                                           {
                                             Open = false,
                                           }))
                      .WithField("RepRole",
                          field => field.OfType("TaxonomyField")
                                       .WithDisplayName("Funkcija")
                                       .WithEditor("Tags")
                                       .WithDisplayMode("Tags")
                                       .WithPosition("5")
                                       .WithSettings(new TaxonomyFieldSettings
                                       {
                                         Required = true,
                                         TaxonomyContentItemId =
                                             "4cet7kh16f89cxpk2zp9gz4353",
                                         Unique = true,
                                       })
                                       .WithSettings(
                                           new TaxonomyFieldTagsEditorSettings
                                           {
                                             Open = false,
                                           }))
                      .WithField("Activity",
                          field => field.OfType("TaxonomyField")
                                       .WithDisplayName("Djelatnost")
                                       .WithEditor("Tags")
                                       .WithDisplayMode("Tags")
                                       .WithPosition("6")
                                       .WithSettings(new TaxonomyFieldSettings
                                       {
                                         Required = true,
                                         TaxonomyContentItemId =
                                             "4m514eexhnwqnx4asz7xqadfcz",
                                       })
                                       .WithSettings(
                                           new TaxonomyFieldTagsEditorSettings
                                           {
                                             Open = false,
                                           }))
                      .WithField("PermanentAssociates",
                          field => field.OfType("NumericField")
                                       .WithDisplayName(
                                           "Broj stalnih suradnika")
                                       .WithPosition("7"))
                      .WithField("Logo",
                          field => field.OfType("MediaField")
                                       .WithDisplayName("Logo")
                                       .WithPosition("0")
                                       .WithEditor("Attached")
                                       .WithSettings(new MediaFieldSettings
                                       {
                                         Multiple = false,
                                         AllowMediaText = false,
                                       })));
}
