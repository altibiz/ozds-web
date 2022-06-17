using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Nullable<T>(
      this T @this) =>
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> NullableTask<T>(
      this Task<T> @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T?> NullableTask<T>(
      this ValueTask<T> @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T?> NullableEnumerable<T>(
      this IEnumerable<T> @this) =>
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IAsyncEnumerable<T?> NullableEnumerable<T>(
      this IAsyncEnumerable<T> @this) =>
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T NonNull<T>(
      this T? @this) =>
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<T> NonNullTask<T>(
      this Task<T?> @this) =>
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<T> NonNullTask<T>(
      this ValueTask<T?> @this) =>
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> NonNullEnumerable<T>(
      this IEnumerable<T?> @this) =>
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IAsyncEnumerable<T> NonNullEnumerable<T>(
      this IAsyncEnumerable<T?> @this) =>
    @this!;
}
