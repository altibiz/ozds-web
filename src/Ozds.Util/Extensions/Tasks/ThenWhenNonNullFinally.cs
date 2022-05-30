using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullFinally<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, TOut> selector) =>
    (await @this)
      .WhenNonNull(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullFinallyAwait<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await (await @this)
      .WhenNonNullFinallyAsync(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullFinally<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, TOut> selector) =>
    (await @this)
      .WhenNonNullFinally(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullFinallyAwait<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await (await @this)
      .WhenNonNullFinallyAsync(selector);
}
