using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ozds.Util;

public static partial class Objects
{
  public static string ToJson<T>(
      this T @this) =>
    JsonSerializer.Serialize(
      @this,
      new JsonSerializerOptions
      {
        AllowTrailingCommas = true,
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
      });

  public static string ToTitledJson<T>(
      this T @this,
      string title) =>
    $"{title}:\n{@this.ToJson()}";

  public static T LogJson<T>(
      this T @this,
      Action<string> logger) =>
    @this
      .ToJson()
      .WithNullable(logger)
      .Return(@this);

  public static T LogTitledJson<T>(
      this T @this,
      string title,
      Action<string> logger) =>
    @this
      .ToTitledJson(title)
      .WithNullable(logger)
      .Return(@this);

  public static Task<T> LogJsonTask<T>(
      this Task<T> @this,
      Action<string> logger) =>
    @this.Then(@this => @this.LogJson(logger));

  public static Task<T> LogTitledJsonTask<T>(
      this Task<T> @this,
      string title,
      Action<string> logger) =>
    @this.Then(@this => @this.LogTitledJson(title, logger));

  public static ValueTask<T> LogJsonValueTask<T>(
      this ValueTask<T> @this,
      Action<string> logger) =>
    @this.Then(@this => @this.LogJson(logger));

  public static ValueTask<T> LogTitledJsonValueTask<T>(
      this ValueTask<T> @this,
      string title,
      Action<string> logger) =>
    @this.Then(@this => @this.LogTitledJson(title, logger));

  public static T WriteJson<T>(
      this T @this) =>
    @this.LogJson(Console.WriteLine);

  public static T WriteTitledJson<T>(
      this T @this,
      string title) =>
    @this.LogTitledJson(title, Console.WriteLine);

  public static Task<T> WriteJsonTask<T>(
      this Task<T> @this) =>
    @this.LogJsonTask(Console.WriteLine);

  public static Task<T> WriteTitledJsonTask<T>(
      this Task<T> @this,
      string title) =>
    @this.LogTitledJsonTask(title, Console.WriteLine);

  public static ValueTask<T> WriteJsonValueTask<T>(
      this ValueTask<T> @this) =>
    @this.LogJsonValueTask(Console.WriteLine);

  public static ValueTask<T> WriteTitledJsonValueTask<T>(
      this ValueTask<T> @this,
      string title) =>
    @this.LogTitledJsonValueTask(title, Console.WriteLine);
}
