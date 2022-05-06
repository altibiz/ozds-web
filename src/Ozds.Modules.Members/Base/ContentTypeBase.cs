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

public static partial class ContentExtensions
{
  public static Lazy<R> InitLazy<T, R>(
      Func<T> f) where R : class =>
    new Lazy<R>(() => (f() as R)!);

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
      .WhenNonNullable(content => typeof(T)
        .GetProperties()
        .WithNullable(
          properties =>
            {
              foreach (var property in properties)
              {
                if (property.PropertyType.IsGenericType &&
                  Type.Equals(
                    property.PropertyType.GetGenericTypeDefinition(),
                    typeof(Lazy<>)) &&
                  property.PropertyType
                    .GetGenericArguments()
                    .FirstOrDefault()
                    .WhenNonNullable(genericArgument => genericArgument
                      .IsAssignableTo(typeof(ContentElement))))
                {
                  typeof(ContentExtensions)
                  .GetMethod("InitLazy")
                  ?.MakeGenericMethod(
                    new[]
                    {
                      typeof(ContentElement),
                      property.PropertyType.GetGenericArguments().First()
                    })
                  ?.Invoke(
                    null,
                    new[]
                    {
                      () =>
                        content.ContentItem.Get(
                          property.PropertyType
                            .GetGenericArguments()
                            .FirstOrDefault(),
                          property.Name) ??
                        content.ContentItem.Get(
                          property.PropertyType
                            .GetGenericArguments()
                            .FirstOrDefault(),
                          property.Name + "Part")
                    })
                  .WhenNonNullable(lazy => content
                    .SetProperty(property, lazy));
                }
              }
            })
        .Return(content));
  // TODO: declarative like this
  // .Where(property =>
  //   property.PropertyType.IsGenericType &&
  //   Type.Equals(
  //     property.PropertyType.GetGenericTypeDefinition(),
  //     typeof(Lazy<>)) &&
  //   property.PropertyType
  //     .GetGenericArguments()
  //     .FirstOrDefault()
  //     .WhenNonNullable(genericArgument => genericArgument
  //       .IsAssignableTo(typeof(ContentElement))))
  // .Select(property =>
  //   typeof(ContentExtensions)
  //   .GetMethod("InitLazy")
  //   ?.MakeGenericMethod(
  //     new[]
  //     {
  //       typeof(ContentElement),
  //       property.PropertyType.GetGenericArguments().First()
  //     })
  //   ?.Invoke(
  //     null,
  //     new[]
  //     {
  //       () =>
  //         content.ContentItem.Get(
  //           property.PropertyType
  //             .GetGenericArguments()
  //             .FirstOrDefault(),
  //           property.Name)
  //     })
  //   .WriteTitledJson("Lazy")
  //   .WhenNonNullable(value => content
  //     .SetProperty(property, value)))
  // .Return(content));
}
