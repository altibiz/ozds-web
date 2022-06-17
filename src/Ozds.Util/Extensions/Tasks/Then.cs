using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task After(
      this Task @this,
      Action @do)
  {
    await @this;
    @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask After(
      this ValueTask @this,
      Action @do)
  {
    await @this;
    @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task AfterAwait(
      this Task @this,
      Func<ValueTask> @do)
  {
    await @this;
    await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask AfterAwait(
      this ValueTask @this,
      Func<ValueTask> @do)
  {
    await @this;
    await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> Then<TOut>(
      this Task @this,
      Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> Then<TOut>(
      this ValueTask @this,
      Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ThenAwait<TOut>(
      this Task @this,
      Func<ValueTask<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> ThenAwait<TOut>(
      this ValueTask @this,
      Func<ValueTask<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> Then<TIn, TOut>(
      this Task<TIn> @this,
      Func<TIn, TOut> @do) =>
    @do(await @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> Then<TIn, TOut>(
      this ValueTask<TIn> @this,
      Func<TIn, TOut> @do) =>
    @do(await @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ThenAwait<TIn, TOut>(
      this Task<TIn> @this,
      Func<TIn, ValueTask<TOut>> @do) =>
    await @do(await @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> ThenAwait<TIn, TOut>(
      this ValueTask<TIn> @this,
      Func<TIn, ValueTask<TOut>> @do) =>
    await @do(await @this);
}
