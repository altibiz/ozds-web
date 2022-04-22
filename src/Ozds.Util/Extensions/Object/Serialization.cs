using System.Text.Json;

namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static string ToJson<T>(this T? @this) =>
    JsonSerializer.Serialize(
      @this,
      new JsonSerializerOptions
      {
        AllowTrailingCommas = true,
        WriteIndented = true,
      });

  public static string WriteJson<T>(this T? @this) =>
    @this
      .ToJson()
      .Named(json => Console.WriteLine(json));
}
