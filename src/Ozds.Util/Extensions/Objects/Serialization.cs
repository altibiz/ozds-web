using System.Text.Json;

namespace Ozds.Util;

public static partial class Objects
{
  public static string ToJson<T>(
      this T? @this) =>
    JsonSerializer.Serialize(
      @this,
      new JsonSerializerOptions
      {
        AllowTrailingCommas = true,
        WriteIndented = true,
      });

  public static string ToTitledJson<T>(
      this T? @this,
      string title) =>
    $"{title}\n{@this.ToJson()}";

  public static string LogJson<T>(
      this T? @this,
      Action<string> logger) =>
    @this.ToJson().WithNullable(logger);

  public static string LogTitledJson<T>(
      this T? @this,
      string title,
      Action<string> logger) =>
    @this.ToTitledJson(title).WithNullable(logger);

  public static string WriteJson<T>(
      this T? @this) =>
    @this.LogJson(Console.WriteLine);

  public static string WriteTitledJson<T>(
      this T? @this,
      string title) =>
    @this.LogTitledJson(title, Console.WriteLine);
}
