namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static T? Nullable<T>(
      this T @this) =>
    @this;

  public static async Task<T?> NullableTask<T>(
      this Task<T> @this) =>
    await @this;

  public static async ValueTask<T?> NullableTask<T>(
      this ValueTask<T> @this) =>
    await @this;

  public static T NonNullable<T>(
      this T? @this) =>
    @this!;

  public static async Task<T> NonNullableTask<T>(
      this Task<T?> @this) =>
    (await @this)!;

  public static async ValueTask<T> NonNullableTask<T>(
      this ValueTask<T?> @this) =>
    (await @this)!;
}
