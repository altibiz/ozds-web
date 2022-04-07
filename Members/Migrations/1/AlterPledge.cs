using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Title.Models;
using Members.Payments;

namespace Members.M1;

public static partial class AlterPledge
{
  public static void AlterPledgeType(this IContentDefinitionManager
          content) => content.AlterTypeDefinition("Pledge",
      type =>
          type.DisplayedAs("Uplatnica")
              .Listable()
              .Securable()
              .Creatable()
              .WithPart(
                  nameof(Pledge), part => part.WithPosition("0").WithSettings(
                                      new PledgeSettings()))
              .WithPart("TitlePart",
                  part => part.WithPosition("1").WithSettings(
                      new TitlePartSettings
                      {
                        Options = TitlePartOptions.GeneratedDisabled,
                        Pattern =
                            "{{ ContentItem.Content.Pledge.PayerName.Text }} - {{ ContentItem.Content.Pledge.Amount.Value | format_number: \"C\"  }}",
                      })));

  public static void AlterPledgePart(this IContentDefinitionManager
          content) => content.AlterPartDefinition(nameof(Pledge),
      part =>
          part.WithField("Amount",
                  field => field.OfType("NumericField")
                               .WithDisplayName("Iznos")
                               .WithPosition("0")
                               .WithSettings(
                                   new NumericFieldSettings { Scale = 2 }))
              .WithField("PayerName",
                  field => field.OfType("TextField")
                               .WithDisplayName("Platitelj")
                               .WithPosition("1")
                               .WithSettings(new TextFieldSettings
                               {
                                 Required = true,
                               }))
              .WithField("Note", field => field.OfType("TextField")
                                              .WithDisplayName("Opis plaćanja")
                                              .WithPosition("9"))
              .WithField(
                  "Email", field => field.OfType("TextField")
                                        .WithDisplayName("Email")
                                        .WithEditor("Email")
                                        .WithSettings(new TextFieldSettings
                                        {
                                          Required = true,
                                        }))
              .WithField(
                  "Oib", field => field.OfType("TextField")
                                      .WithDisplayName("OIB")
                                      .WithPosition("1")
                                      .WithSettings(new TextFieldSettings
                                      {
                                        Required = true,
                                      }))
              .WithField(
                  "ReferenceNr", field => field.OfType("TextField")
                                              .WithDisplayName("Poziv na broj")
                                              .WithPosition("3"))
              .WithField("Variant",
                  field =>
                      field.OfType("TaxonomyField")
                          .WithEditor("Tags")
                          .WithDisplayMode("Tags")
                          .WithDisplayName("Količina")
                          .WithPosition("6")
                          .WithSettings(new TaxonomyFieldSettings
                          {
                            TaxonomyContentItemId = "5599209fa3d04b0da7482e655",
                            Unique = true,
                            Required = true
                          })
                          .WithSettings(new TaxonomyFieldTagsEditorSettings
                          {
                            Open = false,
                          }))
              .WithField("Person",
                  field => field.OfType("ContentPickerField")
                               .WithDisplayName("Član")
                               .WithSettings(new ContentPickerFieldSettings
                               {
                                 DisplayedContentTypes = new[] { "Member",
                                   "Company" },
                               })));
}
