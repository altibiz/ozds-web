using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenOnlyWhenNullable<T>(
      this Task<T?> @this,
      T @default) =>
    (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenOnlyWhenNullable<T>(
      this Task<T?> @this,
      Task<T> @default) =>
    await (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenOnlyWhenNullable<T>(
      this Task<T?> @this,
      ValueTask<T> @default) =>
    await (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenOnlyWhenNullable<T>(
      this Task<T?> @this,
      Func<T> @default) =>
    (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenOnlyWhenNullableTask<T>(
      this Task<T?> @this,
      Func<Task<T>> @default) =>
    await (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenOnlyWhenNullableValueTask<T>(
      this Task<T?> @this,
      Func<ValueTask<T>> @default) =>
    await (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenOnlyWhenNullable<T>(
      this ValueTask<T?> @this,
      T @default) =>
    (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenOnlyWhenNullable<T>(
      this ValueTask<T?> @this,
      Task<T> @default) =>
    await (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenOnlyWhenNullable<T>(
      this ValueTask<T?> @this,
      ValueTask<T> @default) =>
    await (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenOnlyWhenNullable<T>(
      this ValueTask<T?> @this,
      Func<T> @default) =>
    (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenOnlyWhenNullableTask<T>(
      this ValueTask<T?> @this,
      Func<Task<T>> @default) =>
    await (await @this).OnlyWhenNullable(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenOnlyWhenNullableValueTask<T>(
      this ValueTask<T?> @this,
      Func<ValueTask<T>> @default) =>
    await (await @this).OnlyWhenNullable(@default);
}
