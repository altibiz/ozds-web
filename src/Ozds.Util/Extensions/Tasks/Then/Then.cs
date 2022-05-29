using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task Then(
      this Task @this,
      Action selector)
  {
    await @this;
    selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask Then(
      this ValueTask @this,
      Action selector)
  {
    await @this;
    selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task ThenTask(
      this Task @this,
      Func<Task> selector)
  {
    await @this;
    await selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task ThenTask(
      this ValueTask @this,
      Func<Task> selector)
  {
    await @this;
    await selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task ThenValueTask(
      this Task @this,
      Func<ValueTask> selector)
  {
    await @this;
    await selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask ThenValueTask(
      this ValueTask @this,
      Func<ValueTask> selector)
  {
    await @this;
    await selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> Then<TOut>(
      this Task @this,
      Func<TOut> selector)
  {
    await @this;
    return selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> Then<TOut>(
      this ValueTask @this,
      Func<TOut> selector)
  {
    await @this;
    return selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ThenTask<TOut>(
      this Task @this,
      Func<Task<TOut>> selector)
  {
    await @this;
    return await selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ThenTask<TOut>(
      this ValueTask @this,
      Func<Task<TOut>> selector)
  {
    await @this;
    return await selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ThenValueTask<TOut>(
      this Task @this,
      Func<ValueTask<TOut>> selector)
  {
    await @this;
    return await selector();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> ThenValueTask<TOut>(
      this ValueTask @this,
      Func<ValueTask<TOut>> selector)
  {
    await @this;
    return await selector();
  }

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
