using Ozds.Modules.Members.Base;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Lists.Models;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members.Utils;

public static class ContentExtensions
{
  public static TSetting? GetSettings<TSetting>(
      this IContentDefinitionManager content, ContentPart part)
      where TSetting : class?, new()
  {
    var contentTypeDefinition =
        content.GetTypeDefinition(part.ContentItem.ContentType);
    var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(
        x => string.Equals(x.PartDefinition.Name, part.GetType().Name));
    return contentTypePartDefinition?.GetSettings<TSetting>();
  }

  public static void AddToList(this ContentItem parent, ContentItem child)
  {
    child.Weld<ContainedPart>();
    child.Alter<ContainedPart>(x => x.ListContentItemId = parent.ContentItemId);
  }

  public static T? AsReal<T>(this ContentItem contentItem)
      where T : ContentPart?
  {
    if (!contentItem.Latest && !contentItem.Published)
    {
      return null;
    }

    return contentItem.As<T>();
  }

  private static List<Type> s_contentFieldTypes = new List<Type> {
    typeof(BooleanField),
    typeof(TaxonomyField),
    typeof(NumericField),
    typeof(TextField),
    typeof(DateField),
    typeof(ContentPickerField),
    typeof(UserPickerField),
  };

  public static T InitContentFields<T>(this T part)
      where T : ContentPart
  {
    foreach (var prop in part.GetType().GetProperties())
    {
      if (prop.GetValue(part) is not null)
      {
        continue;
      }

      var fieldType =
          s_contentFieldTypes.FirstOrDefault(type => type == prop.PropertyType);
      if (fieldType is null) { continue; }

      var field = Activator.CreateInstance(fieldType) as ContentField;
      if (field is null) { continue; }

      field.ContentItem = part.ContentItem;
      prop.SetValue(part, field);
    }

    return part;
  }

  public static string? GetId(this ContentPickerField contentPickerField) =>
      contentPickerField?.ContentItemIds?.FirstOrDefault();

  public static void SetId(this ContentPickerField contentPickerField,
      string value) => contentPickerField.ContentItemIds = new[] { value };

  public static string? GetId(this TaxonomyField taxonomyField) =>
      taxonomyField.TermContentItemIds?.FirstOrDefault();

  public static void SetId(this TaxonomyField field,
      string value) => field.TermContentItemIds = new[] { value };

  public static async Task<ContentItem> GetTerm(this TaxonomyField field,
      TaxonomyCachedService service) => await service.GetFirstTerm(field);

  public static async Task<TPart> GetTerm<TPart>(
      this TaxonomyField field, TaxonomyCachedService service)
      where TPart : ContentPart => (await service.GetFirstTerm(field)).As<TPart>();

  public static IEnumerable<T> AsParts<T>(this IEnumerable<ContentItem> items)
      where T : ContentPart => items.Select(x => x.As<T>());
}
