using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Lists.Models;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Flows.Models;
using Ozds.Util;

namespace Ozds.Modules.Members;

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
    item.When(
        item => item.Latest || item.Published,
        item => ContentItemExtensions.As<T>(item));

  public static IEnumerable<T>? FromBag<T>(
      this ContentItem? item,
      string? name = null) where T : ContentPart =>
    item.Get<BagPart>(name ?? typeof(BagPart).Name)
      .WhenNonNullable(bag => bag.ContentItems
          .SelectFilter(item => ContentItemExtensions.As<T>(item)));

  public static IEnumerable<T>? FromFlow<T>(
      this ContentItem? item,
      string? name = null) where T : ContentPart =>
    item.Get<FlowPart>(name ?? typeof(FlowPart).Name)
      .WhenNonNullable(flow => flow.Widgets
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

  public static IEnumerable<T> AsParts<T>(
      this IEnumerable<ContentItem> items) where T : ContentPart =>
    items.Select(x => ContentItemExtensions.As<T>(x));

  public static T InitContentFields<T>(
      this T part) where T : ContentPart =>
    part
      .GetType()
      .GetProperties()
      .ForEach(property => property
        .When(
          property => property.GetValue(part) is null,
          property => ContentFieldTypes
            .FirstOrDefault(type => type == property.PropertyType)
            .Construct<ContentField>()
            .With(field =>
            {
              field.ContentItem = part.ContentItem;
              property.SetValue(part, field);
            })))
      .Return(part);

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
