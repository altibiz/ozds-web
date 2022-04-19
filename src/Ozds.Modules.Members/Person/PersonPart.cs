using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members.Persons
{
  public enum PersonType { Natural, Legal }

  public class PersonPart : ContentPart
  {
    public TextField Oib { get; set; } = default!;

    public TextField Name { get; set; } = default!;

    public TextField Surname { get; set; } = default!;

    public TextField Address { get; set; } = default!;

    public TextField City { get; set; } = default!;

    public TaxonomyField County { get; set; } = default!;

    public TextField Phone { get; set; } = default!;

    public TextField Email { get; set; } = default!;

    public TaxonomyField ContribType { get; set; } = default!;

    public TextField Skills { get; set; } = default!;

    public string LegalName
    {
      get => Name?.Text +
             (string.IsNullOrEmpty(Surname?.Text) ? "" : " " + Surname?.Text);
    }

    public string OldSalt { get; set; } = default!;
    public string OldHash { get; set; } = default!;
  }

  public class PersonPartSettings : IFieldEditorSettings
  {
    public PersonType Type { get; set; }

    public DisplayModeResult GetFieldDisplayMode(string propertyName,
        string displayMode, BuildFieldEditorContext context,
        bool isAdminTheme)
    {
      if (isAdminTheme)
        return displayMode;
      if (propertyName == nameof(PersonPart.Surname) &&
          Type == PersonType.Legal)
        return false;

      return context.IsNew ? displayMode : "Disabled";
    }

    public string GetFieldLabel(
        string propertyName, string displayName, bool isAdminTheme)
    {
      return propertyName switch
      {
        nameof(PersonPart.Name) =>
            Type == PersonType.Legal ? "Naziv"
                                     : displayName,
        _ => displayName
      };
    }
  }
}
