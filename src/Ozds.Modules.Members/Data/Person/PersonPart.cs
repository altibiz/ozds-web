using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members;

public class PersonPart : ContentPart
{
  public TextField Oib { get; set; } = new();
  public TextField Name { get; set; } = new();
  public TextField MiddleName { get; set; } = new();
  public TextField Surname { get; set; } = new();
  public TextField Address { get; set; } = new();
  public TextField City { get; set; } = new();
  public TaxonomyField County { get; set; } = new();
  public TextField Phone { get; set; } = new();
  public TextField Email { get; set; } = new();
  public BooleanField Legal { get; set; } = new();

  public string LegalName
  {
    get =>
      Legal.Value ? Name.Text
      : String.IsNullOrWhiteSpace(MiddleName.Text) ?
        $"{Name.Text} {Surname.Text}"
      : $"{Name.Text} {MiddleName.Text} {Surname.Text}";
  }
}

public class PersonPartSettings : IFieldEditorSettings
{
  public DisplayModeResult GetFieldDisplayMode(
      string propertyName,
      string displayMode,
      BuildFieldEditorContext context,
      bool isAdminTheme) =>
    isAdminTheme ? displayMode
    : (propertyName == nameof(PersonPart.MiddleName) ||
      propertyName == nameof(PersonPart.Surname)) &&
      context.ContentPart.Content.Legal ? false
    : context.IsNew ? displayMode
    : "Disabled";

  public string GetFieldLabel(
      string propertyName,
      string displayName,
      bool isAdminTheme) =>
    propertyName switch
    {
      nameof(PersonPart.Name) =>
          // TODO: by Legal?
          true ? "Ime"
          : displayName,
      _ => displayName
    };
}
