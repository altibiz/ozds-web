using System.Reflection;

namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static T? Set<T>(
      this T? @this,
      PropertyInfo field,
      object? value) where T : class
  {
    field.SetValue(@this, value);

    return @this;
  }

  public static T? Set<T>(
      this T? @this,
      string field,
      object? value) where T : class
  {
    @this
      ?.GetType()
      .GetField(field)
      ?.SetValue(@this, value);

    return @this;
  }

  public static T? Set<T>(
      this T? @this,
      string field,
      BindingFlags binding,
      object? value) where T : class
  {
    @this
      ?.GetType()
      .GetField(field)
      ?.SetValue(@this, value);

    return @this;
  }
}
