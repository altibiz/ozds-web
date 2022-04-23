using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhen<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, TOut?> selector) =>
    (await @this)
      .WhenNonNullable(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> ThenWhen<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, TOut?> selector) =>
    (await @this)
      .WhenNonNullable(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenTask<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, Task<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullableTask(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenTask<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullableTask(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> ThenWhenValueTask<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullableValueTask(@this => selector(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> ThenWhenValueTask<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask<TOut?>> selector) =>
    await (await @this)
      .WhenNonNullableValueTask(@this => selector(@this));
}