using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterSite {
  public static void AlterSiteType(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterTypeDefinition(
          "Site", type => type.DisplayedAs("Obračunsko mjerno mjesto")
                              .Creatable()
                              .Listable()
                              .Draftable()
                              .Securable()
                              .WithPart("OMM", part => part.WithPosition("0")));

  public static void AlterSitePart(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterPartDefinition("Site",
          part => part.Attachable()
                      .WithField("Code",
                          field => field.OfType("TextField")
                                       .WithDisplayName("Šifra")
                                       .WithPosition("0")
                                       .WithSettings(new TextFieldSettings {
                                         Required = true,
                                       }))
                      .WithField("Type",
                          field => field.OfType("TextField")
                                       .WithDisplayName("Tip")
                                       .WithPosition("1")
                                       .WithSettings(new TextFieldSettings {
                                         Required = true,
                                       }))
                      .WithField("ZDSCode",
                          field => field.OfType("TextField")
                                       .WithDisplayName("Šifra ZDS-a")
                                       .WithPosition("7")
                                       .WithSettings(new TextFieldSettings {
                                         Required = true,
                                       }))
                      .WithField("DeviceCode",
                          field => field.OfType("TextField")
                                       .WithDisplayName("Šifra uređaja")
                                       .WithPosition("8"))
                      .WithField("UserCode",
                          field => field.OfType("TextField")
                                       .WithDisplayName("Šifra korisnika")
                                       .WithPosition("10"))
                      .WithField("Coefficient",
                          field => field.OfType("NumericField")
                                       .WithDisplayName("Koeficijent")
                                       .WithPosition("3"))
                      .WithField("PhaseNumber",
                          field => field.OfType("NumericField")
                                       .WithDisplayName("Broj faze")
                                       .WithPosition("4"))
                      .WithField("Tipuredaja",
                          field => field.OfType("TextField")
                                       .WithDisplayName("Tip uređaja")
                                       .WithPosition("9"))
                      .WithField("Geolocation",
                          field => field.OfType("BooleanField")
                                       .WithDisplayName("Geolokacija")
                                       .WithPosition("2"))
                      .WithField(
                          "Active", field => field.OfType("BooleanField")
                                                 .WithDisplayName("Aktivno")
                                                 .WithPosition("5"))
                      .WithField("UserType",
                          field => field.OfType("TextField")
                                       .WithDisplayName("Tip korisnika")
                                       .WithPosition("11"))
                      .WithField(
                          "Primarno", field => field.OfType("BooleanField")
                                                   .WithDisplayName("Primarno")
                                                   .WithPosition("6")));
}
