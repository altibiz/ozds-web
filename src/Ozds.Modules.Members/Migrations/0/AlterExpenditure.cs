using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Flows.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterExpenditure
{
  public static void AlterExpenditureType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Expenditure",
      type => type
        .DisplayedAs("Trošak")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("Expenditure",
          part => part
          .WithPosition("1")
          .WithSettings(
            new FieldEditorSettings
            {
            }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithPosition("2")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "ExpenditureItem"
                },
              })));

  public static void AlterExpenditurePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Expenditure",
      part => part
        .WithDisplayName("Trošak")
        .WithField("InTotal",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Ukupno")
            .WithPosition("1")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2,
              })));
}
