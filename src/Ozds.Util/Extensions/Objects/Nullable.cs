namespace Ozds.Util;

public static partial class Objects
{
  public static T? Nullable<T>(
      this T @this) =>
    @this;

  public static async Task<T?> Nullable<T>(
      this Task<T> @this) =>
    await @this;

  public static async ValueTask<T?> Nullable<T>(
      this ValueTask<T> @this) =>
    await @this;

  public static IEnumerable<T?> Nullable<T>(
      this IEnumerable<T> @this) =>
    @this;

  public static IAsyncEnumerable<T?> Nullable<T>(
      this IAsyncEnumerable<T> @this) =>
    @this;

  public static T NonNullable<T>(
      this T? @this) =>
    @this!;

  public static async Task<T> NonNullable<T>(
      this Task<T?> @this) =>
    (await @this)!;

  public static async ValueTask<T> NonNullable<T>(
      this ValueTask<T?> @this) =>
    (await @this)!;

  public static IEnumerable<T> NonNullable<T>(
      this IEnumerable<T?> @this) =>
    @this!;

  public static IAsyncEnumerable<T> NonNullable<T>(
      this IAsyncEnumerable<T?> @this) =>
    @this!;
}