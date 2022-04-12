using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using Ozds.Modules.Members.Payments;

namespace Ozds.Modules.Members.M1;

public static partial class AlterPledgeVariant
{
  public static void AlterPledgeVariantType(
      this IContentDefinitionManager content) =>
      content.AlterTypeDefinition("PledgeVariant",
          type => type.DisplayedAs("Vrijednost uplate")
                      .Stereotype("Widget")
                      .Creatable()
                      .WithPart(
                          nameof(PledgeVariant), part => part.WithPosition("0"))
                      .WithPart("TitlePart",
                          part => part.WithPosition("1").WithSettings(
                              new TitlePartSettings
                              {
                                Options = TitlePartOptions.EditableRequired,
                              })));

  public static void AlterPledgeVariantPart(this IContentDefinitionManager
          content) => content.AlterPartDefinition(nameof(PledgeVariant),
      part => part.WithField("Price",
          field => field.OfType("NumericField")
                       .WithDisplayName("Cijena")
                       .WithPosition("0")
                       .WithSettings(new NumericFieldSettings
                       {
                         Required = true,
                         Scale = 2
                       })));
}
