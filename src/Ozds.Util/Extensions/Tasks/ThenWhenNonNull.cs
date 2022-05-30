using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNull<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, TOut?> selector) =>
    (await @this)
      .WhenNonNull(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullAwait<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullAsync(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNull<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, TOut?> selector) =>
    (await @this)
      .WhenNonNull(selector);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenNonNullAwait<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullAsync(selector);
}
