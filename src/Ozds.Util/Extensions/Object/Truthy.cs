using System.Diagnostics.CodeAnalysis;

namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static bool Truthy<T>([NotNullWhen(true)] this T? @this) =>
    @this is not null;

  public static bool Truthy([NotNullWhen(true)] this string? @this) =>
    !String.IsNullOrWhiteSpace(@this);

  public static bool Truthy([NotNullWhen(true)] this bool? @this) =>
    @this switch
    {
      bool @thisBool => @thisBool,
      null => false,
    };
}
