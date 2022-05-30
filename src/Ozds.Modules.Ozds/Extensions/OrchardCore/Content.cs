using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Lists.Models;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Flows.Models;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public static partial class ContentExtensions
{
  public static TSettings? GetSettings<TSettings>(
      this IContentDefinitionManager content,
      ContentPart part) where TSettings : class?, new() =>
    content
      .GetTypeDefinition(part.ContentItem.ContentType).Parts
      .FirstOrDefault(part =>
        string.Equals(part.PartDefinition.Name, part.GetType().Name))
      ?.GetSettings<TSettings>();

  public static void AddToList(this ContentItem parent, ContentItem child)
  {
    child.Weld<ContainedPart>();
    child.Alter<ContainedPart>(part =>
      part.ListContentItemId = parent.ContentItemId);
  }

  public static T? AsReal<T>(
      this ContentItem? item) where T : ContentPart =>
    item is not null && (item.Latest || item.Published) ?
      item.As<T>()
    : null;

  public static IEnumerable<T>? FromBag<T>(
      this ContentItem? item,
      string? name = null) where T : ContentPart =>
    item.Get<BagPart>(name ?? typeof(BagPart).Name)
      .WhenNonNull(bag => bag.ContentItems
          .SelectFilter(item => ContentItemExtensions.As<T>(item)));

  public static IEnumerable<T>? FromFlow<T>(
      this ContentItem? item,
      string? name = null) where T : ContentPart =>
    item.Get<FlowPart>(name ?? typeof(FlowPart).Name)
      .WhenNonNull(flow => flow.Widgets
          .SelectFilter(item => ContentItemExtensions.As<T>(item)));

  public static string? GetId(this ContentPickerField contentPickerField) =>
      contentPickerField?.ContentItemIds?.FirstOrDefault();

  public static void SetId(this ContentPickerField contentPickerField,
      string value) => contentPickerField.ContentItemIds = new[] { value };

  public static string? GetId(this TaxonomyField taxonomyField) =>
      taxonomyField.TermContentItemIds?.FirstOrDefault();

  public static void SetId(
      this TaxonomyField field,
      string value) =>
    field.TermContentItemIds =
      new[]
      {
        value
      };

  public static void Apply(this ContentElement element, string name) =>
    element.ContentItem.Apply(name, element);

  public static IEnumerable<T> AsParts<T>(
      this IEnumerable<ContentItem> items) where T : ContentPart =>
    items.Select(x => ContentItemExtensions.As<T>(x));

  public static T InitContentFields<T>(
      this T part) where T : ContentPart
  {
    foreach (var property in typeof(T).GetProperties())
    {
      if (property.GetValue(part) is null)
      {
        ContentFieldTypes
          .FirstOrDefault(type => type == property.PropertyType)
          .WhenNonNull(type => type
            .Construct<ContentField>()
            .WithNonNull(field =>
            {
              field.ContentItem = part.ContentItem;
              property.SetValue(part, field);
            }));
      }
    }

    return part;
  }

  private static List<Type> ContentFieldTypes { get; } =
    new List<Type>
    {
      typeof(BooleanField),
      typeof(TaxonomyField),
      typeof(NumericField),
      typeof(TextField),
      typeof(DateField),
      typeof(ContentPickerField),
      typeof(UserPickerField),
    };
}
