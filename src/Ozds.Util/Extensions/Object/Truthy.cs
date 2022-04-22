using System.Diagnostics.CodeAnalysis;

namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static bool Truthy<T>([NotNullWhen(true)] this T? @this) =>
    @this is not null;

  public static bool Truthy([NotNullWhen(true)] this string? @this) =>
    !string.IsNullOrWhiteSpace(@this);

  public static bool Truthy([NotNullWhen(true)] this bool? @this) =>
    @this switch
    {
      bool @thisBool => @thisBool,
      null => false,
    };

  public static bool Truthy<T>(
      [NotNullWhen(true)] this IEnumerable<T>? @this) =>
    !@this.IsEmpty();

  public static bool Truthy<T>(
      [NotNullWhen(true)] this IAsyncEnumerable<T>? @this) =>
    !@this.IsEmpty();
}
