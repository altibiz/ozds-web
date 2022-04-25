using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Taxonomies.Fields;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class Contact : ContentPart
{
  public TextField Name { get; set; } = new();
  public TextField MiddleName { get; set; } = new();
  public TextField Surname { get; set; } = new();
  public TextField Phone { get; set; } = new();
  public TextField Email { get; set; } = new();
  public TaxonomyField Type { get; set; } = new();

  public string LegalName
  {
    get =>
      Legal ? Name.Text
      : MiddleName.Text.Empty() ?
        $"{Name.Text} {Surname.Text}"
      : $"{Name.Text} {MiddleName.Text} {Surname.Text}";
  }

  public bool Legal
  {
    get => PersonType.LegalId.In(Type.TermContentItemIds);
  }
}

public class ContactSettings : IFieldEditorSettings
{
  public DisplayModeResult GetFieldDisplayMode(
      string propertyName,
      string displayMode,
      BuildFieldEditorContext context,
      bool isAdminTheme) =>
    isAdminTheme ? displayMode
    : (propertyName == nameof(Contact.MiddleName) ||
      propertyName == nameof(Contact.Surname)) &&
      context.ContentPart.Content.Legal ? false
    : context.IsNew ? displayMode
    : "Disabled";

  public string GetFieldLabel(
      string propertyName,
      string displayName,
      bool isAdminTheme) =>
    propertyName switch
    {
      nameof(Contact.Name) =>
          // TODO: by Legal?
          true ? "Ime"
          : displayName,
      _ => displayName
    };
}
