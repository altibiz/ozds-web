using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Flows.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterReceipt {
  public static void AlterReceiptType(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterTypeDefinition("Receipt",
          type => type.DisplayedAs("Račun")
                      .Creatable()
                      .Listable()
                      .Draftable()
                      .Versionable()
                      .Securable()
                      .WithPart("Receipt", part => part.WithPosition("1"))
                      .WithPart("TitlePart", part => part.WithPosition("0"))
                      .WithPart("Calculation",
                          part => part.WithDisplayName("Obračun")
                                      .WithDescription("Obračun računa.")
                                      .WithSettings(new BagPartSettings {
                                        ContainedContentTypes =
                                            new[] { "Calculation" },
                                      })));

  public static void AlterReceiptPart(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterPartDefinition("Receipt",
          part =>
              part.WithField("DocumentNumber",
                      field => field.OfType("NumericField")
                                   .WithDisplayName("Broj dokumenta")
                                   .WithPosition("0"))
                  .WithField("Partner", field => field.OfType("TextField")
                                                     .WithDisplayName("Partner")
                                                     .WithPosition("1"))
                  .WithField("PartnerAdress",
                      field => field.OfType("TextField")
                                   .WithDisplayName("Adresa partnera")
                                   .WithPosition("2"))
                  .WithField("PartnerPostalCode",
                      field => field.OfType("NumericField")
                                   .WithDisplayName("Poštanski broj partnera")
                                   .WithPosition("3"))
                  .WithField(
                      "PartnerOIB", field => field.OfType("NumericField")
                                                 .WithDisplayName("OIB patnera")
                                                 .WithPosition("4"))
                  .WithField("DeliveryDate",
                      field => field.OfType("DateField")
                                   .WithDisplayName("Datum isporuke")
                                   .WithPosition("5"))
                  .WithField("PaymentCurrency",
                      field => field.OfType("TextField")
                                   .WithDisplayName("Valuta plaćanja")
                                   .WithPosition("6"))
                  .WithField("ContractDate",
                      field => field.OfType("DateField")
                                   .WithDisplayName("Datum ugovora")
                                   .WithPosition("7"))
                  .WithField("ContractNumber",
                      field => field.OfType("NumericField")
                                   .WithDisplayName("Broj ugovora")
                                   .WithPosition("8"))
                  .WithField("ProjectCode",
                      field => field.OfType("TextField")
                                   .WithDisplayName("Šifra projekta")
                                   .WithPosition("9"))
                  .WithField(
                      "FromDate", field => field.OfType("DateField")
                                               .WithDisplayName("Datum od")
                                               .WithPosition("10"))
                  .WithField("ToDate", field => field.OfType("DateField")
                                                    .WithDisplayName("Datum do")
                                                    .WithPosition("11"))
                  .WithField("InTotal", field => field.OfType("NumericField")
                                                     .WithDisplayName("Ukupno")
                                                     .WithPosition("12"))
                  .WithField("PDV", field => field.OfType("NumericField")
                                                 .WithDisplayName("PDV")
                                                 .WithPosition("13"))
                  .WithField("InTotalWithPDV",
                      field => field.OfType("NumericField")
                                   .WithDisplayName("Ukupni iznos")
                                   .WithPosition("14"))
                  .WithField("Remark", field => field.OfType("TextField")
                                                    .WithDisplayName("Napomena")
                                                    .WithPosition("15"))
                  .WithField("OperatorName",
                      field => field.OfType("TextField")
                                   .WithDisplayName("Ime operatera")
                                   .WithPosition("16"))
                  .WithField("OperatorSurname",
                      field => field.OfType("TextField")
                                   .WithDisplayName("Prezime operatera")
                                   .WithPosition("17"))
                  .WithField("ReceiptDate",
                      field => field.OfType("DateField")
                                   .WithDisplayName("Datum računa")
                                   .WithPosition("18")));
}
