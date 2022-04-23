using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Taxonomies.Fields;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class Person : ContentPart
{
  public TextField Oib { get; set; } = new();
  public TextField Name { get; set; } = new();
  public TextField MiddleName { get; set; } = new();
  public TextField Surname { get; set; } = new();
  public TextField Address { get; set; } = new();
  public TextField City { get; set; } = new();
  public TextField PostalCode { get; set; } = new();
  public TaxonomyField County { get; set; } = new();
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

public class PersonSettings : IFieldEditorSettings
{
  public DisplayModeResult GetFieldDisplayMode(
      string propertyName,
      string displayMode,
      BuildFieldEditorContext context,
      bool isAdminTheme) =>
    isAdminTheme ? displayMode
    : (propertyName == nameof(Person.MiddleName) ||
      propertyName == nameof(Person.Surname)) &&
      context.ContentPart.Content.Legal ? false
    : context.IsNew ? displayMode
    : "Disabled";

  public string GetFieldLabel(
      string propertyName,
      string displayName,
      bool isAdminTheme) =>
    propertyName switch
    {
      nameof(Person.Name) =>
          // TODO: by Legal?
          true ? "Ime"
          : displayName,
      _ => displayName
    };
}
