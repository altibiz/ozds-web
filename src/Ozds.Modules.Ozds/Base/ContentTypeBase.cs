using System.Reflection;
using OrchardCore.ContentManagement;
using OrchardCore.Lists.Models;
using OrchardCore.DisplayManagement.Razor;
using OrchardCore.Taxonomies.Fields;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public abstract class ContentTypeBase
{
  protected ContentTypeBase(ContentItem contentItem)
  {
    ContentItem = contentItem;
  }

  public ContentItem ContentItem { get; init; }

  public Lazy<ContainedPart?> ContainedPart { get; init; } = default!;
}

// NOTE: don't use for now - needs more testing
public abstract class ContentTypeBase<TDerived> :
  ContentTypeBase,
  IContentTypeBaseDerivedIndicator
  where TDerived : ContentTypeBase<TDerived>
{
  protected ContentTypeBase(ContentItem item) : base(item)
  {
    (this as TDerived)!.PopulateContent();
  }
}

// TODO: YesSql integration
public static class ContentTypeBaseExtensions
{
  public static string ContentTypeName(this Type @this) =>
    @this.Name.RegexRemove("Type$");

  public static string ContentTypeName<T>() =>
    typeof(T).ContentTypeName();

  public static T? AsContent<T>(
      this ContentItem @this) where T : ContentTypeBase =>
    @this.ContentType != ContentTypeName<T>() ? null
    : Activator
        .CreateInstance(
          typeof(T),
          BindingFlags.NonPublic |
          BindingFlags.Public |
          BindingFlags.Instance,
          null,
          new[] { @this },
          null)
        .As<T>()
        .WhenNonNull(
          content =>
            !content.GetType()
              .IsAssignableTo<IContentTypeBaseDerivedIndicator>() ?
              content.PopulateContent()
            : null);

  public static Task<T?> NewContentAsync<T>(
      this IContentManager content) where T : ContentTypeBase =>
    content
      .NewAsync(typeof(T).Name.RegexRemove(@"Type$"))
      .Then(item => item.AsContent<T>());

  public static Task<T?> CloneContentAsync<T>(
      this IContentManager content,
      ContentItem item) where T : ContentTypeBase =>
    content
      .CloneAsync(item)
      .Then(item => item.AsContent<T>());

  public static Task<T?> GetContentAsync<T>(
      this IContentManager content,
      string id) where T : ContentTypeBase =>
    content
      .GetAsync(id)
      .Then(item => item.AsContent<T>());

  public static Task<T?> GetContentAsync<T>(
      this IContentManager content,
      string id,
      VersionOptions options) where T : ContentTypeBase =>
    content
      .GetAsync(id, options)
      .Then(item => item.AsContent<T>());

  public static Task<T?> GetVersionedContentAsync<T>(
      this IContentManager content,
      string id) where T : ContentTypeBase =>
    content
      .GetVersionAsync(id)
      .Then(item => item.AsContent<T>());

  public static Task<IEnumerable<T>> GetContentAsync<T>(
      this IContentManager content,
      IEnumerable<string> ids) where T : ContentTypeBase =>
    content
      .GetAsync(ids)
      .Then(items =>
        items.SelectFilter(item =>
          item.AsContent<T>()));

  public static Task<T?> GetTaxonomyTermAsync<T>(
      this IOrchardDisplayHelper orchardDisplayHelper,
      TaxonomyField taxonomy) where T : ContentTypeBase =>
    orchardDisplayHelper
      .GetTaxonomyTermAsync(
        taxonomy.TaxonomyContentItemId,
        taxonomy.TermContentItemIds.First())
      .Then(item => item.AsContent<T>());

  internal static T PopulateContent<T>(
      this T content) where T : ContentTypeBase
  {
    foreach (var property in typeof(T).GetProperties())
    {
      if (!property.PropertyType.IsGenericType ||
          !Equals(
            property.PropertyType.GetGenericTypeDefinition(),
            typeof(Lazy<>)))
      {
        continue;
      }

      var partType = property.PropertyType
        .GetGenericArguments()
        .FirstOrDefault();
      if (partType is null ||
          !partType.IsAssignableTo(typeof(ContentElement)))
      {
        continue;
      }

      content.ContentItem
        .CreateLazy(partType, property.Name)
        .With(lazy =>
          content.Set(property, lazy));
    }

    return content;
  }

  private static object CreateLazy(
      this ContentItem contentItem,
      Type partType,
      string partName) =>
    typeof(Lazy<>)
      .MakeGenericType(
          new[]
          {
            partType
          })
      .GetConstructor(
          new[]
          {
            typeof(Func<>)
              .MakeGenericType(
                new[]
                {
                  partType
                })
          })
      .ThrowWhenNull()
      .Invoke(
        new[]
        {
          LazyFactoryCaster
            .MakeGenericMethod(
              new []
              {
                typeof(ContentElement),
                partType
              })
            .Invoke(
              null,
              new []
              {
                () =>
                  contentItem.Get(partType, partName) ??
                  contentItem.Get(partType, partName + "Part")
              })
        });

  private static readonly MethodInfo LazyFactoryCaster =
    typeof(Functions)
      .GetMethods()
      .Where(method =>
        method.Name == nameof(Functions.Cast) &&
        method.GetGenericArguments().Length == 2)
      .First();
}

internal interface IContentTypeBaseDerivedIndicator
{
}
