using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using Ozds.Members.Payments;

namespace Ozds.Members.M0;

public static partial class AlterBankStatement
{
  public static void AlterBankStatementPart(this IContentDefinitionManager
          content) => content.AlterTypeDefinition("BankStatement",
      type =>
          type.DisplayedAs("Izvod")
              .Listable()
              .Securable()
              .Creatable()
              .WithPart(nameof(BankStatPart), part => part.WithPosition("0"))
              .WithPart("TitlePart",
                  part => part.WithPosition("1").WithSettings(new TitlePartSettings
                  {
                    Options = TitlePartOptions.GeneratedDisabled,
                    Pattern =
                        "{{ ContentItem.Content.BankStatPart.Date.Value | date: \"%D\" }}",
                  })));

  public static void AlterBankStatementType(
      this IContentDefinitionManager content) =>
      content.AlterPartDefinition(nameof(BankStatPart),
          part => part.WithField("Date", field => field.OfType("DateField")
                                                      .WithDisplayName("Datum")
                                                      .WithPosition("0")));
}
