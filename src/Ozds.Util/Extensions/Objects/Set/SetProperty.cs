using System.Runtime.CompilerServices;
using System.Reflection;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? SetProperty<T>(
      this T? @this,
      PropertyInfo field,
      object? value) where T : class
  {
    field.SetValue(@this, value);

    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? SetProperty<T>(
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? SetProperty<T>(
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
