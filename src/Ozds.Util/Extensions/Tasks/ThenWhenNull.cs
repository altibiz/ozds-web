using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenWhenNull<T>(
      this Task<T?> @this,
      T @default) =>
    (await @this).WhenNull(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenWhenNull<T>(
      this Task<T?> @this,
      ValueTask<T> @default) =>
    await (await @this).WhenNullAsync(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenWhenNull<T>(
      this Task<T?> @this,
      Func<T> @default) =>
    (await @this).WhenNull(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenWhenNullAwait<T>(
      this Task<T?> @this,
      Func<ValueTask<T>> @default) =>
    await (await @this).WhenNullAsync(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenWhenNull<T>(
      this ValueTask<T?> @this,
      T @default) =>
    (await @this).WhenNull(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenWhenNull<T>(
      this ValueTask<T?> @this,
      ValueTask<T> @default) =>
    await (await @this).WhenNullAsync(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenWhenNull<T>(
      this ValueTask<T?> @this,
      Func<T> @default) =>
    (await @this).WhenNull(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenWhenNullAwait<T>(
      this ValueTask<T?> @this,
      Func<ValueTask<T>> @default) =>
    await (await @this).WhenNullAsync(@default);
}
