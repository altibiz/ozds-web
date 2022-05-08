using System.Reflection;
using OrchardCore.ContentManagement;
using OrchardCore.Lists.Models;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ContentTypeBase
{
  public ContentTypeBase(ContentItem contentItem)
  {
    ContentItem = contentItem;
  }

  public ContentItem ContentItem { get; init; }

  public Lazy<ContainedPart?> ContainedPart { get; init; } = default!;
}

public static class ContentTypeBaseExtensions
{
  public static T? AsContent<T>(
      this ContentItem @this) where T : ContentTypeBase =>
    Activator
      .CreateInstance(
        typeof(T),
        BindingFlags.NonPublic | BindingFlags.Instance,
        null,
        new[] { @this },
        null)
      .As<T>()
      .With(content =>
        typeof(T)
          .GetProperties()
          .ForEach(property =>
            property.PropertyType
              .GetGenericArguments()
              .FirstOrDefault()
              .WhenWith(
                partType =>
                  partType.IsAssignableTo(typeof(ContentElement)) &&
                  Type.Equals(
                    property.PropertyType.GetGenericTypeDefinition(),
                    typeof(Lazy<>)),
                partType =>
                  content.ContentItem
                    .CreateLazy(partType, property.Name)
                    .WithNullable(lazy =>
                      content.SetProperty(property, lazy))))
          .Run());

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
      .GetAsync(id)
      .Then(item => item.AsContent<T>());

  public static Task<IEnumerable<T>> GetContentAsync<T>(
      this IContentManager content,
      IEnumerable<string> ids) where T : ContentTypeBase =>
    content
      .GetAsync(ids)
      .Then(items =>
        items.SelectFilter(item =>
          item.AsContent<T>()));

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
        method.GetGenericArguments().Count() == 2)
      .First();
}
