using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToTitledJson<T>(
      this T @this,
      string title) =>
    $"{title}:\n{@this.ToJson()}";

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T LogJson<T>(
      this T @this,
      Action<string> logger) =>
    @this
      .ToJson()
      .With(logger)
      .Return(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T LogTitledJson<T>(
      this T @this,
      string title,
      Action<string> logger) =>
    @this
      .ToTitledJson(title)
      .With(logger)
      .Return(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<T> LogJsonTask<T>(
      this Task<T> @this,
      Action<string> logger) =>
    @this.Then(@this => @this.LogJson(logger));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<T> LogTitledJsonTask<T>(
      this Task<T> @this,
      string title,
      Action<string> logger) =>
    @this.Then(@this => @this.LogTitledJson(title, logger));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> LogJsonValueTask<T>(
      this ValueTask<T> @this,
      Action<string> logger) =>
    await @this.Then(@this => @this.LogJson(logger));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> LogTitledJsonValueTask<T>(
      this ValueTask<T> @this,
      string title,
      Action<string> logger) =>
    await @this.Then(@this => @this.LogTitledJson(title, logger));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WriteJson<T>(
      this T @this) =>
    @this.LogJson(Console.WriteLine);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WriteTitledJson<T>(
      this T @this,
      string title) =>
    @this.LogTitledJson(title, Console.WriteLine);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<T> WriteJsonTask<T>(
      this Task<T> @this) =>
    @this.LogJsonTask(Console.WriteLine);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<T> WriteTitledJsonTask<T>(
      this Task<T> @this,
      string title) =>
    @this.LogTitledJsonTask(title, Console.WriteLine);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WriteJsonValueTask<T>(
      this ValueTask<T> @this) =>
    await @this.LogJsonValueTask(Console.WriteLine);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WriteTitledJsonValueTask<T>(
      this ValueTask<T> @this,
      string title) =>
    await @this.LogTitledJsonValueTask(title, Console.WriteLine);
}
