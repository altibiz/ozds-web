using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this T? @this) where T : struct =>
    @this is null;

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
    @this.EmptyEnumerable();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this IAsyncEnumerable<T> @this) =>
    @this.EmptyAsyncEnumerable().Result;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this Task<T> @this) =>
    @this.Block().Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Empty<T>(
      [NotNullWhen(true)] this ValueTask<T> @this) =>
    @this.Block().Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> EmptyTask<T>(
      [NotNullWhen(true)] this Task<T> @this) =>
    (await @this).Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> EmptyValueTask<T>(
      [NotNullWhen(true)] this ValueTask<T> @this) =>
    (await @this).Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool EmptyEnumerable<T>(
      [NotNullWhen(true)] this IEnumerable<T> @this)
  {
    foreach (var item in @this)
    {
      return false;
    }

    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> EmptyAsyncEnumerable<T>(
      [NotNullWhen(true)] this IAsyncEnumerable<T> @this)
  {
    await foreach (var item in @this)
    {
      return false;
    }

    return true;
  }
}
