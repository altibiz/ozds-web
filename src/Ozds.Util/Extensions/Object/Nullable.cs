namespace Ozds.Util;

public static partial class ObjectExtensions
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

  public static T NonNullable<T>(
      this T? @this) =>
    @this!;

  public static async Task<T> NonNullable<T>(
      this Task<T?> @this) =>
    (await @this)!;

  public static async ValueTask<T> NonNullable<T>(
      this ValueTask<T?> @this) =>
    (await @this)!;
}
