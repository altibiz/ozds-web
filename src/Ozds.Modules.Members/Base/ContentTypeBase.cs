using System.Reflection;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ContentTypeBase
{
  public ContentTypeBase(ContentItem contentItem)
  {
    ContentItem = contentItem;
  }

  public ContentItem ContentItem { get; init; }
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
              .When(
                partType =>
                  partType.IsAssignableTo(typeof(ContentElement)) &&
                  Type.Equals(
                    property.PropertyType.GetGenericTypeDefinition(),
                    typeof(Lazy<>)),
                partType =>
                  content.ContentItem
                    .CreateLazy(
                      partType,
                      property.Name)
                    .WhenNonNullable(lazy =>
                      content.SetProperty(property, lazy))))
          // NOTE: this forces the ForEach to run eagerly
          .Run());

  private static object? CreateLazy(
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
