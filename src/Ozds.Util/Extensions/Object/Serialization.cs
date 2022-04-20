using System.Text.Json;

namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static string ToJson<T>(this T? @this) =>
    JsonSerializer.Serialize(@this);

  public static string Properties<T>(this T? @this) =>
    @this
      ?.GetType()
      .GetProperties()
      .Aggregate(
        "",
        (current, next) =>
        string.IsNullOrWhiteSpace(current) ? next.Name
        : $"{current}, {next.Name}")
      ?? "";
}
