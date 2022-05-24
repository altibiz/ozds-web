using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullableFinally<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, TOut> selector) =>
    (await @this)
      .WhenNonNullable(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullableFinallyTask<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, Task<TOut>> selector) =>
    await (await @this)
      .WhenNonNullableFinallyTask(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullableFinallyValueTask<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await (await @this)
      .WhenNonNullableFinallyValueTask(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> ThenWhenNonNullableFinally<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, TOut> selector) =>
    (await @this)
      .WhenNonNullableFinally(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullableFinallyTask<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task<TOut>> selector) =>
    await (await @this)
      .WhenNonNullableFinallyTask(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> ThenWhenNonNullableFinallyValueTask<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await (await @this)
      .WhenNonNullableFinallyValueTask(selector);
}
