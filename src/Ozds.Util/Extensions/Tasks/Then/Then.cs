using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> Then<TIn, TOut>(
      this Task<TIn> @this,
      Func<TIn, TOut> selector) =>
    selector(await @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> Then<TIn, TOut>(
      this ValueTask<TIn> @this,
      Func<TIn, TOut> selector) =>
    selector(await @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ThenTask<TIn, TOut>(
      this Task<TIn> @this,
      Func<TIn, Task<TOut>> selector) =>
    await selector(await @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ThenTask<TIn, TOut>(
      this ValueTask<TIn> @this,
      Func<TIn, Task<TOut>> selector) =>
    await selector(await @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ThenValueTask<TIn, TOut>(
      this Task<TIn> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await selector(await @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> ThenValueTask<TIn, TOut>(
      this ValueTask<TIn> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await selector(await @this);
}
