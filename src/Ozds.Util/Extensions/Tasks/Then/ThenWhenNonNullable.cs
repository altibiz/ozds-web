using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullable<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, TOut?> selector) =>
    (await @this)
      .WhenNonNullable(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullableTask<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, Task<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullableTask(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullableValueTask<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullableValueTask(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> ThenWhenNonNullable<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, TOut?> selector) =>
    (await @this)
      .WhenNonNullable(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullableTask<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullableTask(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> ThenWhenNonNullableValueTask<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullableValueTask(selector);
}
