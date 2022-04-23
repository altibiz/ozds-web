using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this T? @this) =>
    @this is null;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty(
      [NotNullWhen(true)] this string? @this) =>
    string.IsNullOrWhiteSpace(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this IEnumerable<T> @this) =>
    @this.FirstOrDefault() is null;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this IAsyncEnumerable<T> @this) =>
    @this.FirstOrDefault() is null;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this Task<T> @this) =>
    @this.BlockTask().Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this ValueTask<T> @this) =>
    @this.BlockValueTask().Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> EmptyTask<T>(
      [NotNullWhen(true)] this Task<T> @this) =>
    (await @this).Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<bool> EmptyValueTask<T>(
      [NotNullWhen(true)] this ValueTask<T> @this) =>
    (await @this).Empty();
}
