using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Nullable<T>(
      this T @this) =>
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> Nullable<T>(
      this Task<T> @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> Nullable<T>(
      this ValueTask<T> @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T?> Nullable<T>(
      this IEnumerable<T> @this) =>
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IAsyncEnumerable<T?> Nullable<T>(
      this IAsyncEnumerable<T> @this) =>
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T NonNull<T>(
      this T? @this) =>
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<T> NonNull<T>(
      this Task<T?> @this) =>
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<T> NonNull<T>(
      this ValueTask<T?> @this) =>
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> NonNull<T>(
      this IEnumerable<T?> @this) =>
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IAsyncEnumerable<T> NonNull<T>(
      this IAsyncEnumerable<T?> @this) =>
    @this!;
}
