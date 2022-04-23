using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenFinally<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, TOut> selector) =>
    (await @this)
      .WhenNonNullableFinally(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> ThenFinally<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, TOut> selector) =>
    (await @this)
      .WhenNonNullableFinally(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenFinallyTask<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, Task<TOut>> selector) =>
    await (await @this)
      .WhenNonNullableFinallyTask(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenFinallyTask<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task<TOut>> selector) =>
    await (await @this)
      .WhenNonNullableFinallyTask(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenFinallyValueTask<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await (await @this)
      .WhenNonNullableFinallyValueTask(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> ThenFinallyValueTask<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await (await @this)
      .WhenNonNullableFinallyValueTask(@this => selector(@this));
}
