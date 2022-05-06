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
        .Where(property =>
          property.PropertyType.IsGenericType.WriteJson() &&
          Type.Equals(
            property.PropertyType.GetGenericTypeDefinition(),
            typeof(Lazy<>)) &&
          property.PropertyType
            .GetGenericArguments()
            .FirstOrDefault()
            .WhenNonNullable(genericArgument => genericArgument
              .IsAssignableTo(typeof(ContentElement))))
        .ForEach(property => property.PropertyType
          .Construct(
            () =>
              @this.Get(
                property.PropertyType
                  .GetGenericArguments()
                  .FirstOrDefault(),
                property.Name) ??
              @this.Get(
                property.PropertyType
                  .GetGenericArguments()
                  .FirstOrDefault(),
                property.Name + "Part"))
          .WhenNonNullable(value => @this
            .SetProperty(property, value)))
        .Return(content));
}
